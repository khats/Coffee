define('main', ['jquery','infuser', 'ko', 'dataservice'], function ($, infuser, ko, dataservice) {
    return {
        call: function () {
            infuser.get('main', function (template) {
                $('#root').html(template);
                dataservice.getNews({
                    success: function (result) {
                        function myModel() {
                            this.news = result.data;
                        }
                        ko.applyBindings(new myModel());
                    }
                }, { page: 0, count: 5 });
            });
        }
    };
});