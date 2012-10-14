$(document).ready(function () {
    function buildMainPage() {
        if (!$("#main-page").get(0)) {
            $("#event").off('page-loaded-main').on('page-loaded-main', function () {
                buildMainPage();
            });
            return;
        }
        $.get("/html/news-item.html")
            .success(function(newsItem) {
                var template = newsItem;
                for (var i = 0; i < 5; i++) {
                    $("ul.news-block").append(template);
                }
            });
        /*$.get(reqApi.templates.newsList([0, 5])).success(function (data) {
            if (data.IsSuccess) {
                $.get("/html/news-item.html")
                        );
                
            }*/
        $('#register').on('click', function () {
            setHash('register');
        });
        $('#login').on('click', function () {
            setHash('login');
        });
    }

   /* function buildRegisterPage() {
        if (!$("#register-page").get(0)) {
            $("#event").on('page-loaded-register', function () {
                buildRegisterPage();
            });
            return;
        }
        $("#confirm").off('click').on('click', function (e) {
            var regData = $("form").serialize();
            ($.post(reqApi.templates.register(), regData).success(function (data) {
                if (data.IsSuccess) {
                    setHash("cards");
                } else {
                    $("#errors").html(data.ErrorMessage);
                }
            }));
        });
    }*/

    function buildLoginPage() {
        if (!$("#login-page").get(0)) {
            $("#event").on('page-loaded-login', function () {
                buildLoginPage();
            });
            return;
        }

        $("#confirm").on('click', function () {
            var loginData = $("form").serialize();            
            $.post(reqApi.templates.login(), loginData).success(function (data) {
                if (data.IsSuccess) {
                    setHash("login-pin");
                } else {
                    $("#errors").html(data.ErrorMessage);
                }
            });
        });
    }


    /*function buildPinPage() {
        if (!$("#login-pin-page").get(0)) {
            $("#event").on('page-loaded-login-pin', function () {
                buildPinPage();
            });
            return;
        }

        $("#confirm").on('click', function (e) {
            var pinData = $("form").serialize();
            $.post(reqApi.templates.loginPin(), pinData).success(function (data) {
                if (data.IsSuccess) {
                    setHash("cards");
                } else {
                    $("#errors").html(data.ErrorMessage);
                }
            });
        });
    }

    function buildCardsPage() {
        if (!$("#cards-page").get(0)) {
            $("#event").on('page-loaded-cards', function () {
                buildCardsPage();
            });
            return;
        }

       /* $.get(reqApi.templates.cardsList()).success(function (data) {
            if (data.IsSuccess) {
                for (var i = 0; i < data.Data.length; i++) {
                    var item = data.Data[i];
                    $('#cards-table-body').append(sFormat("<tr><td>{0}</td><td>{1}</td><td>{2}</td></tr>", [item.CardAccountId, item.CurrencyCode, item.LogoPaymentSystem]));
                }
            }
        });#1#
        $('#cards-table-body').on('click', 'tr', function () {
            $(this).css('background-color', 'green').siblings().each(function () {
                $(this).css('background-color', '');
            });
            $('#editCard, #deleteCard').attr('disabled', false);
        });
        $('#addCard').off('click').on('click', function () {
            setHash('card');
        });
        $('#editCard').on('click', function () {
            setHash('card');
        });
        
        $('#deleteCard').off('click').on('click', function () {

        });

    }
    
    function buildCardPage() {
        if (!$("#card-page").get(0)) {
            $("#event").on('page-loaded-card', function () {
                buildCardsPage();
            });
            return;
        }

        /*$.get(reqApi.templates.cardsList()).success(function (data) {
            if (data.IsSuccess) {
                for (var i = 0; i < data.Data.length; i++) {
                    var item = data.Data[i];
                    $('#cards-table-body').append(sFormat("<tr><td>{0}</td><td>{1}</td><td>{2}</td></tr>", [item.CardAccountId, item.CurrencyCode, item.LogoPaymentSystem]));
                }
            }
        });#1#

    }*/

    $(window).on('hashchange', function () {
        var hash = getHash();
        hash != "" && loadPage(hash);
        switch (hash) {
            case "main":
                buildMainPage();
                break;
            /*case "register":
                buildRegisterPage();
                break;*/
            case "login":
                buildLoginPage();
                break;

       /*     case "login-pin":
                buildPinPage();
                break;
            case "cards":
                buildCardsPage();
                break;
            case "card":
                buildCardPage();
                break;*/
        }
    });
    setHash('main');
});