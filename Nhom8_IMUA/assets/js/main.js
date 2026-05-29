
//Navigation
const navElement = document.querySelectorAll(".nav");

document.onscroll = function () {
  const scrollTop = window.scrollY || document.documentElement.scrollTop;

  if (scrollTop <= 189) {
    navElement[0].style.display = "block";
    navElement[1].style.display = "none";
  } else {
    navElement[0].style.display = "none";
    navElement[1].style.display = "block";
  }
};

//Slide
let slideIndex = 1;

showSlides(slideIndex);

// Next/previous controls
function plusSlides(n) {
    showSlides((slideIndex += n));
}

// Thumbnail image controls
function currentSlide(n) {
    showSlides((slideIndex = n));
}

function showSlides(n) {
    let i;
    let dots = document.querySelectorAll(".demo");

    if (n > dots.length) {
        slideIndex = 1;
    }
    
    for (i = 0; i < dots.length; i++) {
        dots[i].className = dots[i].className.replace(" active", "");
    }

    dots[slideIndex - 1].className += " active";
}

//Image zoom
$("#zoom_01").elevateZoom({
    zoomType: "inner",
    cursor: "crosshair",
    zoomWindowFadeIn: 500,
    zoomWindowFadeOut: 750,
});