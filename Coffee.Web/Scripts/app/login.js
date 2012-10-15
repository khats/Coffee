define('login', ['jquery', 'infuser','ko'], function ($, infuser, ko) {
    return {
        call: function() {
            infuser.get('login', function(template) {
                $('#root').html(template);
                ko.applyBindings();
                $('.nav li a[href=#login]').parent().hide();
            });
        }
    };
})