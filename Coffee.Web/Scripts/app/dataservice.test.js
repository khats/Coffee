define('dataservice-test', ['dataservice'], function (dataservice) {
    dataservice.getNews({
        success: function(result) {
            console.log(result.Data);
        },
        error: function(result) {
            console.log(result);
        }
    },{page:0, count:5});
})
