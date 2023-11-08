$(document).ready(function () {
    LoadCkEditor4();
    $.ajax({
        url: "/index/PopularPost",
        type: "get"
    }).done(function (data) {
        $("#popular_posts").html(data);
    });
});  
//بعد از لود صفحه به پارشیال مورد نظر میره و اطلاعات اچ تی ام ال رو میگیره و این
//اطلاعات رو به هر جا که آی دی
//popular_posts
//داشته باشه میده.
//این آی دی رو به تگ
//aside
//در صفحه
//post
//و
//search 
//داریم


function LoadCkEditor4() {
    console.log("LoadCkEditor4");

    if (!document.getElementById("ckEditor4"))
        return;
    console.log("LoadCkEditor42");

    $("body").append("<script src='/ckeditor4/ckeditor/ckeditor.js'></script>");

    CKEDITOR.replace('ckEditor4',
        {
            customConfig: '/ckeditor4/ckeditor/config.js'
        });
}

function changePage(pageId) {
    var url = new URL(window.location.href);
    var search_params = url.searchParams;
    // Change PageId
    search_params.set('pageId', pageId);
    url.search = search_params.toString();

    // the new url string
    var new_url = url.toString();

    window.location.replace(new_url);
}

function changePage2(pageId) {
    var url = new URL(window.location.href);
    var search_params = url.searchParams;
}

function change(pageId) {
    var url = new URL(windows.location.href)
    var search_params = url.searchParams;
}