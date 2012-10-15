define('cards', ['jquery', 'infuser', 'ko'], function ($, infuser, ko) {
    return {
        call: function () {
            infuser.get('cards', function (template) {
                $('#root').html(template);
                function myModel() {
                    this.cards = [{
                        id: 1,
                        currency: 'USD',
                        paymentSystem: 'VISA',
                    }, {
                        id: 2,
                        currency: 'EUR',
                        paymentSystem: 'MAESTRO',
                    }];
                }
                ko.applyBindings(new myModel());
                $('.nav li a[href=#cards]').parent().addClass('active');
            });
        }
    };
})