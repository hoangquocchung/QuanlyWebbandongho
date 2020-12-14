var product = {
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
product.init();