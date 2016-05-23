(function(){
    $(document).ready(function () {
        var source = $("#region-option-template").html();
        var template = Handlebars.compile(source);

        $("#country-select").change(function (event) {
            var countrySelected = this.value;
            console.log('The user has selected ' + countrySelected);

            $.get('/Bottle/Regions', { countryId: countrySelected }),
            function (data, textStatus, jqXHR) {
                console.log('The success handler for the get was called');
                var context = { regions: data };
                var html = template(context);
                $("#region").html(html);
                $("#region").prop('disabled', false);
            }
        })
    })
})();