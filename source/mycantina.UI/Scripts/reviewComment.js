(function () {
    $(document).ready(function () {
        var source = $("#review-comment-template").html();
        var template = Handlebars.compile(source);

        $("#review-button").click(function (e) {
            $("#review-button").hide()
            $("#review-comment").html(template);
        });
    });
})();