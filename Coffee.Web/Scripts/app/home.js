define('home', ['jquery', 'infuser', 'ko'], function ($, infuser, ko) {
    return {
        call: function () {
            infuser.get('home', function (template) {
                $('#root').html(template);
                function myModel() {
                    this.currencies = [{
                        name: 'USD',
                        buy: 8300,
                        sell: 8500,
                    }, {
                        name: 'EUR',
                        buy: 12000,
                        sell: 13000,
                    }];
                }
                ko.applyBindings(new myModel());
                $('.nav li a[href=#home]').parent().addClass('active');
            });
        }
    };
})