$("#submit").click(function () {
    $(".list-link").empty();
    var link =$("input[name=link]").val();
    var selector = $("input[name=selector]").val();
    $.ajax({
        type: "POST",
        url: "linksDetail",
        data: {
            link: link,
            selector: selector,
        },
        success: function (data) {
            data.forEach(element => $(".list-link").append("<li>"+element+"</li>"));
        }
    });
});

$("#insert").click(function () {
    var link = $("input[name=link-step]").val();
    var selector = $("input[name=superselector]").val();
    var titleSelector = $("input[name=titleSelector]").val();
    var descriptionSelector = $("input[name=descriptionSelector]").val();
    var imgSelector = $("input[name=imgSelector]").val();
    var contentSelector = $("input[name=contentSelector]").val();
    var token = $("input[name=__RequestVerificationToken]").val();
    $.ajax({
        type: "POST",
        url: "Create",
        data: {
            link: link,
            superSelector: selector,
            titleSelector: titleSelector,
            descriptionSelector: descriptionSelector,
            imgSelector: imgSelector,
            contentSelector: contentSelector,
            __RequestVerificationToken: token,
        },
        success: function (data) {
            if (data == "True") {
                swal("Good job!", "You clicked the button!", "success")
            } else {
                swal("Cancelled", "Your imaginary file is safe :)", "error");
            }
        }, error: function (data) {
            swal("Cancelled", "Your imaginary file is safe :)", "error");
        }
    });
});
$(".next-step").click(function () {
    $("input[name=link-step]").val($("input[name=link]").val());
    $("input[name=superselector]").val($("input[name=selector]").val());
});