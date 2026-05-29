$(document).ready(function () {
    $(".delBtnTT").click(function (e) {
        e.preventDefault();
        var result = confirm("Bạn có chắc chắn muốn xóa tin tức này không?");
        if (result) {
            $.ajax({
                url: "/Admin/TinTucs/Delete",
                type: "POST",
                data: {
                    id: $(this).data("id")
                },
                dataType: "json",
                success: function (data) {
                    if (data != null) {
                        window.location.reload();
                    } else {
                        alert("loi");
                    }
                },
                error: function () {
                    alert("Lỗi trong khi xóa!");
                }
            });
        }
    });

    $(".delBtnDM").click(function (e) {
        e.preventDefault();
        var result = confirm("Bạn có chắc chắn muốn xóa danh mục này không?");
        if (result) {
            $.ajax({
                url: "/Admin/DanhMucs/Delete",
                type: "POST",
                data: {
                    id: $(this).data("id")
                },
                dataType: "json",
                success: function (data) {
                    if (data != null) {
                        window.location.reload();
                    } else {
                        alert("loi");
                    }
                },
                error: function () {
                    alert("Lỗi trong khi xóa!");
                }
            });
        }
    });

    $(".delBtnHD").click(function (e) {
        e.preventDefault();
        var result = confirm("Bạn có chắc chắn muốn xóa hóa đơn này không?");
        if (result) {
            $.ajax({
                url: "/Admin/HoaDons/Delete",
                type: "POST",
                data: {
                    id: $(this).data("id")
                },
                dataType: "json",
                success: function (data) {
                    if (data != null) {
                        window.location.reload();
                    } else {
                        alert("loi");
                    }
                },
                error: function () {
                    alert("Lỗi trong khi xóa!");
                }
            });
        }
    });

    $(".delBtnLSP").click(function (e) {
        e.preventDefault();
        var result = confirm("Bạn có chắc chắn muốn xóa loại sản phẩm này không?");
        if (result) {
            $.ajax({
                url: "/Admin/LoaiSPs/Delete",
                type: "POST",
                data: {
                    id: $(this).data("id")
                },
                dataType: "json",
                success: function (data) {
                    if (data != null) {
                        window.location.reload();
                    } else {
                        alert("loi");
                    }
                },
                error: function () {
                    alert("Lỗi trong khi xóa!");
                }
            });
        }
    });

    $(".delBtnSP").click(function (e) {
        e.preventDefault();
        var result = confirm("Bạn có chắc chắn muốn xóa sản phẩm này không?");
        if (result) {
            $.ajax({
                url: "/Admin/SanPhams/Delete",
                type: "POST",
                data: {
                    id: $(this).data("id")
                },
                dataType: "json",
                success: function (data) {
                    if (data != null) {
                        window.location.reload();
                    } else {
                        alert("loi");
                    }
                },
                error: function () {
                    alert("Lỗi trong khi xóa!");
                }
            });
        }
    });

    $(".delBtnLTK").click(function (e) {
        e.preventDefault();
        var result = confirm("Bạn có chắc chắn muốn xóa loại tài khoản này không?");
        if (result) {
            $.ajax({
                url: "/Admin/LoaiTaiKhoans/Delete",
                type: "POST",
                data: {
                    id: $(this).data("id")
                },
                dataType: "json",
                success: function (data) {
                    if (data != null) {
                        window.location.reload();
                    } else {
                        alert("loi");
                    }
                },
                error: function () {
                    alert("Lỗi trong khi xóa!");
                }
            });
        }
    });

    //$(".delBtnVT").click(function (e) {
    //    e.preventDefault();
    //    var result = confirm("Bạn có chắc chắn muốn xóa vai trò này không?");
    //    if (result) {
    //        $.ajax({
    //            url: "/Admin/VaiTros/Delete",
    //            type: "POST",
    //            data: {
    //                id: $(this).data("id")
    //            },
    //            dataType: "json",
    //            success: function (data) {
    //                if (data != null) {
    //                    window.location.reload();
    //                } else {
    //                    alert("loi");
    //                }
    //            },
    //            error: function () {
    //                alert("Lỗi trong khi xóa!");
    //            }
    //        });
    //    }
    //});

    //$(".delBtnUQ").click(function (e) {
    //    e.preventDefault();
    //    var result = confirm("Bạn có chắc chắn muốn xóa ủy quyền này không?");
    //    if (result) {
    //        $.ajax({
    //            url: "/Admin/UyQuyens/Delete",
    //            type: "POST",
    //            data: {
    //                id: $(this).data("id")
    //            },
    //            dataType: "json",
    //            success: function (data) {
    //                if (data != null) {
    //                    window.location.reload();
    //                } else {
    //                    alert("loi");
    //                }
    //            },
    //            error: function () {
    //                alert("Lỗi trong khi xóa!");
    //            }
    //        });
    //    }
    //});
});