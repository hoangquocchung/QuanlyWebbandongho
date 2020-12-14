var product = {
    init: function () {
        product.registerEvents()
    },
    registerEvents: function () {
        $('.btn-detail').off('click').on('click', function (e) {
            e.preventDefault();//bỏ qua dấu thăng
            $('#detailProduct').modal('show');
            $('#hidProductID').val($(this).data('id'));
            product.loadImages();
        });
    }
}
product.init();