$(document).ready(function () {
    $(".delBtnVT").click(function (e) {
        e.preventDefault();
        var result = confirm("Bạn có chắc chắn muốn xóa vai trò này không?");
        if (result) {
            $.ajax({
                url: "/Admin/VaiTros/Delete",
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

    $(".delBtnUQ").click(function (e) {
        e.preventDefault();
        var result = confirm("Bạn có chắc chắn muốn xóa ủy quyền này không?");
        if (result) {
            $.ajax({
                url: "/Admin/UyQuyens/Delete",
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
});