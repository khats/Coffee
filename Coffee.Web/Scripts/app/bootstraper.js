define('bootstraper', ['config','infuser','ko'], function (config, infuser, ko) {
    infuser.get('main', function(template) {
        $('#root').html(template);

        function myModel() {
            this.news = [
                { subject: "aaa", description: "bbb" },
                { subject: "ccc", description: "ddd" }
            ];
        }

        ko.applyBindings(new myModel());
    });
});