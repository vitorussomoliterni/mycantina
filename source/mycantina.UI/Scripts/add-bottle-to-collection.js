(function () {
    $(document).ready(function () {
        var source = $("#consumer-select-template").html();
        var template = Handlebars.compile(source);
        var bottleId = $("#bottleIdForJquery").html();

        $("#add-collection-btn").click(function (e) {
            $("#add-collection-btn").hide();

            $.get('/Consumer/ConsumersList/',
                function (data, textStatus, jqXHR) {
                    var context = { consumers: data };
                    var html = template(context);
                    $("#select-consumer-form").html(html);

                    $("#consumer-selection").change(function (e) {
                        var consumerSelected = this.value;
                        
                        $("#confirm-consumer-btn").prop('disabled', false);
                        
                        $("#confirm-consumer-btn").click(function (e) {
                            console.log('Button clicked');
                            window.location.href = "/ConsumerBottle/Create?consumerId=" + consumerSelected + "&bottleId=" + bottleId;
                        });
                    });
                });
        });
    });
})();