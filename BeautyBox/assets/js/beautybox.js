document.addEventListener("DOMContentLoaded", function () {
  var slider = document.querySelector(".bb-hero__slider");
  if (!slider) return;

  var track = slider.querySelector(".bb-hero__slides");
  var slides = slider.querySelectorAll(".bb-hero__slide");
  var dots = slider.querySelectorAll(".bb-hero__dot");
  if (!track || !slides.length) return;

  var index = 0;
  var timer;

  function goTo(i) {
    index = (i + slides.length) % slides.length;
    track.style.transform = "translateX(-" + index * 100 + "%)";
    dots.forEach(function (dot, d) {
      dot.classList.toggle("is-active", d === index);
    });
  }

  function next() {
    goTo(index + 1);
  }

  function prev() {
    goTo(index - 1);
  }

  function startAutoplay() {
    stopAutoplay();
    timer = setInterval(next, 5000);
  }

  function stopAutoplay() {
    if (timer) clearInterval(timer);
  }

  dots.forEach(function (dot, i) {
    dot.addEventListener("click", function () {
      goTo(i);
      startAutoplay();
    });
  });

  var prevBtn = slider.querySelector(".bb-hero__nav--prev");
  var nextBtn = slider.querySelector(".bb-hero__nav--next");

  if (prevBtn) {
    prevBtn.addEventListener("click", function () {
      prev();
      startAutoplay();
    });
  }

  if (nextBtn) {
    nextBtn.addEventListener("click", function () {
      next();
      startAutoplay();
    });
  }

  slider.addEventListener("mouseenter", stopAutoplay);
  slider.addEventListener("mouseleave", startAutoplay);

  goTo(0);
  startAutoplay();
});
