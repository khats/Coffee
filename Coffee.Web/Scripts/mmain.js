(function () {
    var root = this;

        define('jquery', [], function () { return root.jQuery; });
        define('ko', [], function () { return root.ko; });
        define('amplify', [], function () { return root.amplify; });
        define('infuser', [], function () { return root.infuser; });
        define('moment', [], function () { return root.moment; });
        define('sammy', [], function () { return root.Sammy; });
        define('toastr', [], function () { return root.toastr; });
        define('underscore', [], function () { return root._; });
    
        requirejs([
                'ko.bindingHandlers', 
                'ko.debug.helpers'
        ], boot);
    
        function boot() {
            require(['config','infuser'],function() {
                require(['helpers'], function (hlp) {
                     hlp.getHash() != '' ? $(window).trigger('hashchange') : require(['main'], function(m) { m.call(); });
                });
            });
    }
})();