
  // Load more
  $(function () {
    $(".more").slice(0, 10).show();
    $("#loadMore").on('click', function (e) {
        e.preventDefault();
        $(".more:hidden").slice(0, 4).slideDown();
        if ($(".more:hidden").length == 0) {
            $("#load").fadeOut('slow');
        }
        
    });
});
