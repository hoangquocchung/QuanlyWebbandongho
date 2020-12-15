/*var product = {
    init: function () {
        product.loadImages();
    },
    loadImages: function () {
        var html = '';
        $.ajax({
            url: '/Product/LoadImage',
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                if (response.status == true) {
                    var data = response.data;

                    $.each(data, function (i, item) {
                        html += '<div class="small-img-col"><img src="' + item + '" width="100%" class="small-img" /></div>'
                    });
                    $('.small-img-row').html(html);
                }

            }
        });
    }
}
product.init();*/

$(document).ready(function () {
    
    $.ajax({
        url: '/Product/LoadImage',
        type: 'GET',
        data: {
            id: $('#hidProductID').val(),
        },
        dataType: 'json',
        success: function (response) {
                var data = response.data;
                var html = '';
                $.each(data, function (i, item) {
                    html += '<div class="small-img-col"><img src="' + item + '" width="100%" class="small-img" /></div>'
                });
                $('.small-img-row').append(html);
        }
    });

})