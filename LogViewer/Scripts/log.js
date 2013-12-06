$(function () {
    var log4net = $.connection.signalrAppenderHub;
 
    $('#test-signalr').on('click', function() {
        log4net.server.test();
    });

    $('#test-signalr-group').on('click', function () {
        log4net.server.testGroup();
    });

    log4net.client.testAll = function(t) {
        alert(t);
    };

    log4net.client.testGroup = function (t) {
        alert(t);
    };

    $.connection.hub.start(function () {
        log4net.server.listen();
    });
    
    // Делаем ajax запросики, что бы проверить логгирование
    $(".test-ajax-link > a").on('click', function (event) {
        event.preventDefault();
        $.get(this.href, {}, function (response) {
        });
    });
    

});