define('helpers',[], function () {
    /*
    Api requests
*/
    var reqApi = {
        templates: {
            newsList: function (params) {
                return sFormat('api/News/EnumerateNews?page={0}&count={1}', params);
            },
            register: function (params) {
                return 'api/Administrator/CreateUser';
            },

            login: function () {
                return 'api/Account/CheckUserLoginAndPassword';
            },

            loginPin: function () {
                return 'api/Account/Authorize';
            },
            cardsList: function () {
                return 'api/CardAccount/EnumerateCardAccount';
            }
        }
    };

    /*
        Helpers
    */
    function sFormat(str, params) {
        for (var i = 0; i < params.length; i++)
            str = str.replace(new RegExp("\\{" + i + "\\}", "g"), params[i]);
        return str;
    }

    function loadPage(pageName) {
        $.get(sFormat("/html/{0}.html", [pageName]), function (data) {
            $("#root").html(data);
            $("#event").trigger("page-loaded-" + pageName);
        });
    }

    function getHash() {
        return window.location.hash.indexOf('#') != -1 ? window.location.hash.substring(1) : window.location.hash;
    }

    function setHash(nextHash) {
        window.location.hash = nextHash == undefined ? '#' : (nextHash.indexOf('#') != -1 ? nextHash : '#' + nextHash);
        $(window).trigger('hashchange');
    }

    return { sFormat: sFormat, apiUrls:reqApi.templates};
});