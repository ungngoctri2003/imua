(function () {
  "use strict";

  var prefersReducedMotion = window.matchMedia("(prefers-reduced-motion: reduce)").matches;

  /* Scroll progress bar */
  function initScrollProgress() {
    var bar = document.getElementById("scrollProgress");
    if (!bar) return;

    function updateProgress() {
      var scrollTop = window.scrollY || document.documentElement.scrollTop;
      var docHeight = document.documentElement.scrollHeight - window.innerHeight;
      var progress = docHeight > 0 ? (scrollTop / docHeight) * 100 : 0;
      bar.style.width = progress + "%";
    }

    window.addEventListener("scroll", updateProgress, { passive: true });
    updateProgress();
  }

  /* Back to top */
  function initBackToTop() {
    var btn = document.getElementById("backToTop");
    if (!btn) return;

    window.addEventListener("scroll", function () {
      if (window.scrollY > 400) {
        btn.classList.add("visible");
      } else {
        btn.classList.remove("visible");
      }
    }, { passive: true });

    btn.addEventListener("click", function () {
      window.scrollTo({ top: 0, behavior: prefersReducedMotion ? "auto" : "smooth" });
    });
  }

  /* Header scroll shrink */
  function initHeaderScroll() {
    var header = document.querySelector(".header");
    if (!header) return;

    var lastScroll = 0;
    window.addEventListener("scroll", function () {
      var current = window.scrollY;
      if (current > 80) {
        header.classList.add("is-scrolled");
      } else {
        header.classList.remove("is-scrolled");
      }
      lastScroll = current;
    }, { passive: true });
  }

  /* Intersection Observer for reveal */
  function initScrollReveal() {
    if (prefersReducedMotion) {
      document.querySelectorAll(".reveal, .stagger-children, .footer__item").forEach(function (el) {
        el.classList.add("is-visible");
      });
      return;
    }

    document.querySelectorAll(".stagger-children").forEach(function (container) {
      var children = container.children;
      for (var i = 0; i < children.length; i++) {
        children[i].style.transitionDelay = i * 0.08 + "s";
      }
    });

    var observer = new IntersectionObserver(function (entries) {
      entries.forEach(function (entry) {
        if (entry.isIntersecting) {
          entry.target.classList.add("is-visible");
          observer.unobserve(entry.target);
        }
      });
    }, {
      threshold: 0.12,
      rootMargin: "0px 0px -40px 0px"
    });

    document.querySelectorAll(".reveal, .stagger-children").forEach(function (el) {
      observer.observe(el);
    });

    document.querySelectorAll(".footer__item").forEach(function (el, i) {
      el.style.transitionDelay = i * 0.1 + "s";
      observer.observe(el);
    });
  }

  /* Button ripple effect */
  function initRipple() {
    if (prefersReducedMotion) return;

    document.addEventListener("click", function (e) {
      var btn = e.target.closest(".btn-primary, .button, .product__add--cart a, .header__search--icon");
      if (!btn) return;

      var rect = btn.getBoundingClientRect();
      var size = Math.max(rect.width, rect.height);
      var ripple = document.createElement("span");
      ripple.className = "btn-ripple";
      ripple.style.width = ripple.style.height = size + "px";
      ripple.style.left = e.clientX - rect.left - size / 2 + "px";
      ripple.style.top = e.clientY - rect.top - size / 2 + "px";
      btn.appendChild(ripple);

      setTimeout(function () {
        if (ripple.parentNode) ripple.parentNode.removeChild(ripple);
      }, 600);
    });
  }

  /* Cart shake on add */
  window.animateCart = function () {
    var icon = document.querySelector(".cart__icon");
    var number = document.querySelector(".cart__number");
    if (icon) {
      icon.classList.remove("is-shaking");
      void icon.offsetWidth;
      icon.classList.add("is-shaking");
    }
    if (number) {
      number.classList.remove("is-updated");
      void number.offsetWidth;
      number.classList.add("is-updated");
    }
  };

  /* Stats counter */
  function initStatsCounter() {
    var items = document.querySelectorAll(".stats-bar__number[data-count]");
    if (!items.length) return;

    function animateCount(el) {
      var target = parseInt(el.getAttribute("data-count"), 10);
      if (isNaN(target)) return;

      if (prefersReducedMotion) {
        el.textContent = target.toLocaleString("vi-VN");
        el.classList.add("is-counted");
        return;
      }

      var duration = 1800;
      var start = 0;
      var startTime = null;

      function step(timestamp) {
        if (!startTime) startTime = timestamp;
        var progress = Math.min((timestamp - startTime) / duration, 1);
        var eased = 1 - Math.pow(1 - progress, 3);
        var current = Math.floor(start + (target - start) * eased);
        el.textContent = current.toLocaleString("vi-VN");
        if (progress < 1) {
          requestAnimationFrame(step);
        } else {
          el.classList.add("is-counted");
        }
      }

      requestAnimationFrame(step);
    }

    var observer = new IntersectionObserver(function (entries) {
      entries.forEach(function (entry) {
        if (entry.isIntersecting) {
          animateCount(entry.target);
          observer.unobserve(entry.target);
        }
      });
    }, { threshold: 0.5 });

    items.forEach(function (el) {
      observer.observe(el);
    });
  }

  /* Page load */
  function initPageLoad() {
    document.body.classList.add("is-loaded");
  }

  document.addEventListener("DOMContentLoaded", function () {
    initScrollProgress();
    initBackToTop();
    initHeaderScroll();
    initScrollReveal();
    initRipple();
    initStatsCounter();
    initPageLoad();
  });
})();
