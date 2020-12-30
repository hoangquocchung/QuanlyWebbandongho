// khai báo theo đối tượng
var Cart = {
    init: function () { //init trang gọi đầu tiên
        Cart.regEvents();
    },
    regEvents: function () {
        $('.btnDelete').off('click').on('click', function (e) {
            e.preventDefault();
            $.ajax({
                data: { id: $(this).data('id') },
                url: 'Cart/Delete',
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        window.location.href = "/gio-hang";
                    }
                }
            });
        });

        $('.btnUpdate').off('click').on('click', function () {
            //lấy 1 giá trị từ lớp .input_quantity
            var ListQuantity = $('.input_quantity');
            var cartList = [];// tạo 1 mảng
            //lặp danh sách
            $.each(ListQuantity, function (i, item) {
                //push từng đối tượng vào mảng
                cartList.push({
                    Quantity: $(item).val(),
                    Product: {
                        ID: $(item).data('id')
                    }
                });
            });
            $.ajax({
                url: '/Cart/Update',
                data: { cartModel: JSON.stringify(cartList) },
                contentTyte: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        window.location.href = '/gio-hang'
                    }
                }
            });
        });


        
    }
}
Cart.init();