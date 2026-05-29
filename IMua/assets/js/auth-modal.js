(function ($) {
  "use strict";

  var modal = document.getElementById("authModal");
  if (!modal) return;

  var returnUrlInput = document.getElementById("authReturnUrl");
  var loginAlert = document.getElementById("loginAlert");
  var registerAlert = document.getElementById("registerAlert");

  function showAlert(el, message, type) {
    if (!el) return;
    el.textContent = message;
    el.hidden = false;
    el.className = "auth-modal__alert is-" + (type || "error");
  }

  function hideAlerts() {
    [loginAlert, registerAlert].forEach(function (el) {
      if (el) {
        el.hidden = true;
        el.textContent = "";
      }
    });
  }

  function setTab(tab) {
    modal.querySelectorAll("[data-auth-tab]").forEach(function (btn) {
      btn.classList.toggle("is-active", btn.getAttribute("data-auth-tab") === tab);
    });
    modal.querySelectorAll("[data-auth-panel]").forEach(function (panel) {
      panel.classList.toggle("is-active", panel.getAttribute("data-auth-panel") === tab);
    });
    modal.classList.toggle("auth-modal--wide", tab === "register");
    hideAlerts();
  }

  window.openAuthModal = function (tab, returnUrl) {
    tab = tab || "login";
    if (returnUrl && returnUrlInput) {
      returnUrlInput.value = returnUrl;
    }
    setTab(tab);
    modal.classList.add("is-open");
    modal.setAttribute("aria-hidden", "false");
    document.body.classList.add("auth-modal-open");
    var firstInput = modal.querySelector('[data-auth-panel="' + tab + '"] input:not([type="hidden"])');
    if (firstInput) setTimeout(function () { firstInput.focus(); }, 300);
  };

  window.closeAuthModal = function () {
    modal.classList.remove("is-open");
    modal.setAttribute("aria-hidden", "true");
    document.body.classList.remove("auth-modal-open");
    hideAlerts();
  };

  /* Open triggers */
  $(document).on("click", "[data-auth-open]", function (e) {
    e.preventDefault();
    var tab = $(this).data("auth-open") || "login";
    var returnUrl = $(this).data("return-url") || "";
    openAuthModal(tab, returnUrl);
  });

  /* Tab switch */
  $(document).on("click", "[data-auth-tab]", function (e) {
    e.preventDefault();
    setTab($(this).data("auth-tab"));
  });

  /* Close */
  $(document).on("click", "[data-auth-close]", function () {
    closeAuthModal();
  });

  $(document).on("keydown", function (e) {
    if (e.key === "Escape" && modal.classList.contains("is-open")) {
      closeAuthModal();
    }
  });

  /* Show password */
  $("#modalShowPassword").on("change", function () {
    var pw = document.getElementById("modal_MatKhau");
    if (pw) pw.type = this.checked ? "text" : "password";
  });

  /* Avatar preview */
  $("#modalImageFile").on("change", function (e) {
    var file = e.target.files[0];
    if (file) {
      document.getElementById("modalAvatarPreview").src = URL.createObjectURL(file);
    }
  });

  /* Login AJAX */
  $("#modal-login-form").on("submit", function (e) {
    e.preventDefault();
    var $form = $(this);
    var $btn = $("#modalLoginSubmit");
    $btn.prop("disabled", true);
    hideAlerts();

    $.ajax({
      url: $form.attr("action"),
      type: "POST",
      data: $form.serialize(),
      dataType: "json",
      headers: { "X-Requested-With": "XMLHttpRequest" },
      success: function (res) {
        if (res.success) {
          if (typeof showToast === "function") {
            showToast(res.message || "Đăng nhập thành công!", "success");
          }
          closeAuthModal();
          window.location.href = res.redirectUrl || "/";
        } else {
          showAlert(loginAlert, res.message || "Đăng nhập thất bại", "error");
          $btn.prop("disabled", false);
        }
      },
      error: function () {
        showAlert(loginAlert, "Có lỗi xảy ra, vui lòng thử lại.", "error");
        $btn.prop("disabled", false);
      }
    });
  });

  /* Register AJAX */
  $("#modal-register-form").on("submit", function (e) {
    e.preventDefault();
    var $form = $(this);
    var $btn = $("#modalRegisterSubmit");
    $btn.prop("disabled", true);
    hideAlerts();

    var formData = new FormData(this);

    $.ajax({
      url: $form.attr("action"),
      type: "POST",
      data: formData,
      processData: false,
      contentType: false,
      dataType: "json",
      headers: { "X-Requested-With": "XMLHttpRequest" },
      success: function (res) {
        if (res.success) {
          showAlert(registerAlert, res.message || "Đăng ký thành công!", "success");
          if (typeof showToast === "function") {
            showToast(res.message || "Đăng ký thành công!", "success");
          }
          $form[0].reset();
          var avatar = document.getElementById("modalAvatarPreview");
          if (avatar) avatar.src = avatar.getAttribute("data-default");
          setTimeout(function () {
            setTab("login");
          }, 1500);
        } else {
          showAlert(registerAlert, res.message || "Đăng ký thất bại", "error");
        }
        $btn.prop("disabled", false);
      },
      error: function () {
        showAlert(registerAlert, "Có lỗi xảy ra, vui lòng thử lại.", "error");
        $btn.prop("disabled", false);
      }
    });
  });

  /* Auto-open from URL ?auth=login|register */
  $(function () {
    var params = new URLSearchParams(window.location.search);
    var auth = params.get("auth");
    var returnUrl = params.get("returnUrl") || "";

    if (auth === "login" || auth === "register") {
      openAuthModal(auth, returnUrl);
      if (window.history.replaceState) {
        var clean = window.location.pathname;
        window.history.replaceState({}, document.title, clean);
      }
    }
  });
})(jQuery);
