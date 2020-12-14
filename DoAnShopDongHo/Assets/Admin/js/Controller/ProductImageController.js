var product = {
    init: function () {
        product.registerEvents()
    },
    registerEvents: function () {
        $('.btn-images').off('click').on('click', function (e) {
            e.preventDefault();//bỏ qua dấu thăng
            $('#imagesManange').modal('show');
            $('#hidProductID').val($(this).data('id'));
            product.loadImages();
        });

        $('#btnChooImages').off('click').on('click', function (e) {
            e.preventDefault();//bỏ qua dấu thăng
            var finder = new CKFinder();
            finder.selectActionFunction = function (url) {
                $('.imageList').append('<div style="margin-right: 20px;margin-bootom:20px;"><img src="' + url + '" style="width:70px;height:70px;" /><a href="#" class="btn-delImages" style="margin-left: 10px;"><i class="fa fa-times"></i></a></div>');
                $('.btn-delImages').off('click').on('click', function (e) {
                    e.preventDefault();
                    $(this).parent().remove();
                })
            };
            finder.popup();
        });
        $('#btn-SaveImages').off('click').on('click', function () {
            var images = []; // khai báo 1 mảng
            var id = $('#hidProductID').val();

            $.each($('.imageList img'), function (i, item) {
                images.push($(item).prop('src'));
            })

            $.ajax({
                url: '/Admin/Product/SaveImages',
                type: 'POST',
                data: {
                    id: id,
                    images: JSON.stringify(images)
                },
                dataType: 'json',
                success: function (response) {
                    if (response.status) {
                        $('#imagesManange').modal('hide');
                        $('.imageList').html('');
                        alert('Save Thành công');
                    }

                }
            });
        })
    },
    loadImages: function () {
        $.ajax({
            url: '/Admin/Product/LoadImage',
            type: 'GET',
            data: {
                id: $('#hidProductID').val(),
            },
            dataType: 'json',
            success: function (response) {
                var data = response.data;
                var html = '';
                $.each(data, function (i, item) {
                    html += '<div style="margin-right: 20px;margin-bootom:20px;"><img src="' + item + '" style="width:70px;height:70px;" /><a href="#" class="btn-delImages" style="margin-left: 10px;"><i class="fa fa-times"></i></a></div>'
                });
                $('.imageList').html(html);
                $('.btn-delImages').off('click').on('click', function (e) {
                    e.preventDefault();
                    $(this).parent().remove();
                })

            }
        });
    }
}
product.init();