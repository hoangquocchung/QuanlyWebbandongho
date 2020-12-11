var ProductCategory = {
    intit: function () {
        ProductCategory.registerEvents();
    },
    registerEvents: function () {
        $('.btn-success').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = btn.data('id');
            $.ajax({
                url: "/Admin/ProductCategory/ChangeStatus",
                data: { id: id },
                dataType: "json",
                type: "POST",
                success: function (response) {
                    if (response.status == true) {
                        btn.text('Kích hoạt');
                    } else {
                        btn.text('Khóa');
                    }
                }
            });
        });

    }
}
ProductCategory.intit();