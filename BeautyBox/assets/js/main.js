// Toast notification
function showToast(message, type) {
  var container = document.getElementById("toastContainer");
  if (!container) return;

  var toast = document.createElement("div");
  toast.className = "toast " + (type || "success");
  toast.textContent = message;
  container.appendChild(toast);

  requestAnimationFrame(function () {
    toast.classList.add("show");
  });

  setTimeout(function () {
    toast.classList.remove("show");
    setTimeout(function () {
      if (toast.parentNode) toast.parentNode.removeChild(toast);
    }, 350);
  }, 3000);
}

// Add to cart via AJAX
function addToCart(productId, quantity, onSuccess) {
  $.ajax({
    type: "POST",
    url: "/Cart/AddToCart",
    data: { id: productId, quantity: quantity || 1 },
    dataType: "json",
    success: function (data) {
      if (data != null) {
        $(".cart__number").html("(" + data.SoLuong + ")");
        if (typeof animateCart === "function") animateCart();
        showToast("Đã thêm vào giỏ hàng thành công!", "success");
        if (onSuccess) onSuccess(data);
      } else {
        showToast("Lỗi khi thêm vào giỏ hàng!", "error");
      }
    },
    error: function () {
      showToast("Lỗi khi thêm vào giỏ hàng!", "error");
    }
  });
}

// Search validation
function valid_search() {
  var search = document.getElementById("SearchString");
  var errorEl = document.getElementById("searchError");
  if (!search) return true;

  if (search.value.trim() === "") {
    if (errorEl) errorEl.classList.add("show");
    search.focus();
    return false;
  }
  if (errorEl) errorEl.classList.remove("show");
  return true;
}

// Mobile navigation
document.addEventListener("DOMContentLoaded", function () {
  var navToggle = document.getElementById("navToggle");
  var navMenu = document.getElementById("navMenu");

  if (navToggle && navMenu) {
    navToggle.addEventListener("click", function () {
      navMenu.classList.toggle("open");
    });
  }

  // Mobile dropdown toggle
  var megaItems = document.querySelectorAll(".nav__item--mega, .nav__item--sale");
  megaItems.forEach(function (item) {
    var link = item.querySelector(":scope > .nav__link");
    if (link) {
      link.addEventListener("click", function (e) {
        if (window.innerWidth <= 768) {
          e.preventDefault();
          item.classList.toggle("open");
        }
      });
    }
  });

  // Mobile sub-category toggle inside mega menu
  var subItems = document.querySelectorAll(".nav__item--has-sub");
  subItems.forEach(function (item) {
    var link = item.querySelector(":scope > .mega-sidebar__link");
    if (link) {
      link.addEventListener("click", function (e) {
        if (window.innerWidth <= 768) {
          e.preventDefault();
          subItems.forEach(function (other) {
            if (other !== item) other.classList.remove("open");
          });
          item.classList.toggle("open");
        }
      });
    }
  });

  // AJAX add to cart
  $(document).on("click", ".ajax-to-cart", function (e) {
    e.preventDefault();
    var id = $(this).data("id");
    addToCart(id, 1);
  });

  // Clear search error on input
  var searchInput = document.getElementById("SearchString");
  if (searchInput) {
    searchInput.addEventListener("input", function () {
      var errorEl = document.getElementById("searchError");
      if (errorEl) errorEl.classList.remove("show");
    });
  }
});

// Product gallery slide
let slideIndex = 1;

function plusSlides(n) {
  showSlides((slideIndex += n));
}

function currentSlide(n) {
  showSlides((slideIndex = n));
}

function showSlides(n) {
  var dots = document.querySelectorAll(".demo");
  if (!dots.length) return;

  if (n > dots.length) slideIndex = 1;
  if (n < 1) slideIndex = dots.length;

  for (var i = 0; i < dots.length; i++) {
    dots[i].className = dots[i].className.replace(" active", "");
  }

  dots[slideIndex - 1].className += " active";
}

// Image zoom on product detail
$(document).ready(function () {
  if ($("#zoom_01").length) {
    $("#zoom_01").elevateZoom({
      zoomType: "inner",
      cursor: "crosshair",
      zoomWindowFadeIn: 500,
      zoomWindowFadeOut: 750
    });
  }
});
