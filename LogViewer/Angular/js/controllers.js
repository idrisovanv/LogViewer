'use strict';

/* Controllers */

function LogListCtrl($scope) {
    $scope.slider = 1000;
    $scope.events = [];
    $scope.locations = {};
    $scope.level = { 'info': true, 'debug': true, 'warn': true, 'error': true };
    //������� ��������� ������� �������, ���� ���������� ��������� ������������� ������������
    var cutEvents = function () {
        if ($scope.slider < $scope.events.length) {
            $scope.events = $scope.events.slice(0, $scope.slider);
        }
    };
    $scope.checkAll = function (isCheck) {
        angular.forEach($scope.locations, function (val, key) { val.show = isCheck });
    };
    //������������� �������� ������������� ���������� �������
    $('.slider').slider({ value: 1000 }).on('slide',
        function (ev) {
            $scope.slider = ev.value;
            cutEvents();
            $scope.$apply();
        });

    var log4net = $.connection.signalrAppenderHub;
    log4net.client.onLoggedEvent = function (formattedEvent, loggedEvent) {
        // �������� ������ �������
        var clsName = loggedEvent.LoggerName;
        // ���� ����� ��� �� �������� � ������ �������
        if (!$scope.locations[clsName]) {
            $scope.locations[clsName] = { show: $scope.addEventFromNewClasses, count: 1 };
        }
        //���� ����� ��� �������� - ����������� ������� ������� �� ������
        else
            $scope.locations[clsName].count++;
        //���� ���������� ������� �� ����������� � ��� ������� �� ������������ � ����� ������� �� ������������
        if (!$scope.isStopped && $scope.level[loggedEvent.Level.Name.toLowerCase()] &&
            $scope.locations[clsName].show) {
            // ��������� ������� � ������ �������, ���� ��� �� �������������
            $scope.events.unshift(loggedEvent);
            cutEvents();
        }
        $scope.$apply();
    };


}
