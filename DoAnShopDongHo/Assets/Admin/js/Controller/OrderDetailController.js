var order = {
    init: function () {
        order.registerEvents()
    },
    registerEvents: function () {
        $('.btn-images').off('click').on('click', function (e) {
            e.preventDefault();//bỏ qua dấu thăng
            $('#imagesManange').modal('show');
            $('#hidProductID').val($(this).data('id'));
            product.loadImages();
        });
        $('.btn-success').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = btn.data('id');
            $.ajax({
                url: "/Admin/Order/ChangeStatus",
                data: { id: id },
                dataType: "json",
                type: "POST",
                success: function (response) {
                    if (response.status == true) {
                        btn.text('Đã xử lý');
                    } else {
                        btn.text('Xử lý');
                    }
                }
            });
        });
    }
}
order.init();