(function(){
    $(document).ready(function () {
        var source = $("#region-option-template").html();
        var template = Handlebars.compile(source);
        
        $("#country-select").change(function (event) {
            var countrySelected = this.value;

            $.get('/Bottle/Regions', { countryId: countrySelected },
            function (data, textStatus, jqXHR) {
                var context = { regions: data };
                var html = template(context);
                $("#region").html(html);
                $("#region").prop('disabled', false);
            });
        });
    });
})();