
// Navigate information 

const chooseInfo = document.getElementById('more-infos');
const choose = document.getElementsByClassName('choose');
const paragraph = document.getElementsByClassName('paragraph');


function styleItem(a, b, c) {
    a.style.cssText = 'color:black ; border-bottom: 2px solid black ; padding-bottom: 6px';
    b.style.cssText = 'color:gray ; border-bottom: none';
    c.style.cssText = 'color:gray ; border-bottom: none';
}

function displayPrph(e, f, g) {
    e.style.display = 'block';
    f.style.display = 'none';
    g.style.display = 'none';
}

chooseInfo.addEventListener('click', event => {

    if (event.target === choose[0]) {

        styleItem(choose[0], choose[1], choose[2])
        displayPrph(paragraph[0], paragraph[2], paragraph[1])
    }

    else if (event.target === choose[1]) {

        styleItem(choose[1], choose[0], choose[2])
        displayPrph(paragraph[1], paragraph[0], paragraph[2])
    }

    else {
        styleItem(choose[2], choose[0], choose[1])
        displayPrph(paragraph[2], paragraph[0], paragraph[1])
    }
});


// Product gallery

var productImg = document.getElementById("productImg");
var smallImg = document.getElementsByClassName("small-img");

smallImg[0].onclick = function () {
    productImg.src = smallImg[0].src;
}
smallImg[1].onclick = function () {
    productImg.src = smallImg[1].src;
}
smallImg[2].onclick = function () {
    productImg.src = smallImg[2].src;
}
smallImg[3].onclick = function () {
    productImg.src = smallImg[3].src;
}


// Information tab
var buttons = document.getElementsByClassName('tab-link');
var contents = document.getElementsByClassName('tab-content');
function showContent(id) {
    for (var i = 0; i < contents.length; i++) {
        contents[i].style.display = 'none';
    }
    var content = document.getElementById(id);
    content.style.display = 'block';
}
for (var i = 0; i < buttons.length; i++) {
    buttons[i].addEventListener("click", function () {
        var id = this.textContent;
        for (var i = 0; i < buttons.length; i++) {
            buttons[i].classList.remove("active");
        }
        this.className += " active";
        showContent(id);
    });
}
showContent('tab-info1');


function openCity(evt, cityName) {
    var i, tabcontent, tablinks;
    tabcontent = document.getElementsByClassName("tabcontent");
    for (i = 0; i < tabcontent.length; i++) {
        tabcontent[i].style.display = "none";
    }
    tablinks = document.getElementsByClassName("tablinks");
    for (i = 0; i < tablinks.length; i++) {
        tablinks[i].className = tablinks[i].className.replace(" active", "");
    }
    document.getElementById(cityName).style.display = "block";
    evt.currentTarget.className += " active";
}

//load more

$(function () {
    $("div").slice(0, 4).show();
    $("#loadMore").on('click', function (e) {
        e.preventDefault();
        $("div:hidden").slice(0, 4).slideDown();
        if ($("div:hidden").length == 0) {
            $("#load").fadeOut('slow');
        }
        $('html,body').animate({
            scrollTop: $(this).offset().top
        }, 1500);
    });
});
