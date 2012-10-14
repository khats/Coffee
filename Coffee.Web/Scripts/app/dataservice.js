define('dataservice', ['amplify'], function (amplify) {
    var init = function() {
        amplify.request.define('get-news', 'ajax', {
            url: '/api/News/EnumerateNews',
            dataType: 'json',
            type: 'GET',
        });
    };
    getNews = function(callbacks, data) {
        return amplify.request('get-news', data, callbacks.success, callbacks.error);
    };
    
    init();
    
    return {
        getNews: getNews
    };
});