'use strict';

/* Controllers */

function LogListCtrl($scope) {
    $scope.slider = 1000;
    $scope.events = [];
    $scope.locations = {};
    $scope.level = { 'info': true, 'debug': true, 'warn': true, 'error': true };
    //Функция обрезания массива событий, если количество превысило установленное максимальное
    var cutEvents = function () {
        if ($scope.slider < $scope.events.length) {
            $scope.events = $scope.events.slice(0, $scope.slider);
        }
    };
    $scope.checkAll = function (isCheck) {
        angular.forEach($scope.locations, function (val, key) { val.show = isCheck });
    };
    //Инициализация слайдера максимального количества событий
    $('.slider').slider({ value: 1000 }).on('slide',
        function (ev) {
            $scope.slider = ev.value;
            cutEvents();
            $scope.$apply();
        });

    var log4net = $.connection.signalrAppenderHub;
    log4net.client.onLoggedEvent = function (formattedEvent, loggedEvent) {
        // Название класса события
        var clsName = loggedEvent.LoggerName;
        // Если класс еще не добавлен в массив классов
        if (!$scope.locations[clsName]) {
            $scope.locations[clsName] = { show: $scope.addEventFromNewClasses, count: 1 };
        }
        //Если класс уже добавлен - увеличиваем счетчик событий по классу
        else
            $scope.locations[clsName].count++;
        //Если добавление событий не остановлено и тип события не отфильтрован и класс события не отфильтрован
        if (!$scope.isStopped && $scope.level[loggedEvent.Level.Name.toLowerCase()] &&
            $scope.locations[clsName].show) {
            // Добавляем событие в начало массива, если оно не отфильтровано
            $scope.events.unshift(loggedEvent);
            cutEvents();
        }
        $scope.$apply();
    };


}
