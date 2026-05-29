(function () {
  function normalizePath(path) {
    path = (path || "").toLowerCase().split("?")[0];
    if (path.endsWith("/index")) {
      path = path.slice(0, -6);
    }
    if (path.length > 1 && path.endsWith("/")) {
      path = path.slice(0, -1);
    }
    return path;
  }

  function getAdminController(path) {
    var parts = path.split("/").filter(Boolean);
    var adminIndex = parts.indexOf("admin");
    if (adminIndex >= 0 && parts.length > adminIndex + 1) {
      return parts[adminIndex + 1];
    }
    return "";
  }

  function setActiveNav() {
    var currentPath = normalizePath(window.location.pathname);
    var currentController = getAdminController(currentPath);

    document.querySelectorAll(".sidebar .nav-item.active").forEach(function (el) {
      el.classList.remove("active");
    });
    document.querySelectorAll(".sidebar .nav-link.active").forEach(function (el) {
      el.classList.remove("active");
    });

    if (!currentController) {
      return;
    }

    var bestLink = null;
    var bestScore = -1;
    var links = document.querySelectorAll(".sidebar .nav-link[href]");

    links.forEach(function (link) {
      var href = link.getAttribute("href");
      if (!href || href === "#" || href.charAt(0) === "#") {
        return;
      }

      var linkPath = normalizePath(href);
      var linkController = getAdminController(linkPath);
      if (!linkController || linkController !== currentController) {
        return;
      }

      var score = linkPath.length;
      if (score > bestScore) {
        bestScore = score;
        bestLink = link;
      }
    });

    if (!bestLink) {
      return;
    }

    var item = bestLink.closest(".nav-item");
    if (item) {
      item.classList.add("active");
    }
    bestLink.classList.add("active");

    var collapse = bestLink.closest(".collapse");
    if (collapse) {
      collapse.classList.add("show");
      var toggle = document.querySelector('[href="#' + collapse.id + '"]');
      if (toggle) {
        toggle.setAttribute("aria-expanded", "true");
      }
    }
  }

  if (document.readyState === "loading") {
    document.addEventListener("DOMContentLoaded", setActiveNav);
  } else {
    setActiveNav();
  }
})();
