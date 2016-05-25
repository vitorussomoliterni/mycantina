(function () {
    $(document).ready(function () {
        var source = $("#user-select-template").html();
        var template = Handlebars.compile(source);

        $("#add-collection-btn").click(function (e) {
            $("#select-user-form").append(template);
        });
    });
})();