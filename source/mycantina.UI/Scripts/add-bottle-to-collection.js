(function () {
    $(document).ready(function () {
        var source = $("#consumer-select-template").html();
        var template = Handlebars.compile(source);

        $("#add-collection-btn").click(function (e) {
            //$("#select-user-form").append(template);
            $("#add-collection-btn").hide();

            $.get('/Consumer/ConsumersList/',
                function (data, textStatus, jqXHR) {
                    var context = { consumers: data };
                    var html = template(context);
                    $("#select-consumer-form").html(html);

                    $("#consumer-selection").change(function (e) {
                        $("#confirm-consumer-btn").prop('disabled', false);

                        // TODO: Get the selected option

                        $("#confirm-consumer-btn").click(function (e) {
                            console.log('Button clicked');
                            // TODO: Add a link to the create consumer bottle action
                        });
                    });
                });
        });
    });
})();