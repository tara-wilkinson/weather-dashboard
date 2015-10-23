var WeatherReports = angular.module('WeatherReports', ['slick']);



WeatherReports.controller('WeatherController', function ($scope, WeatherService) {

    getWeather();
    function getWeather() {
        WeatherService.getWeatherReport()
            .success(function (weather) {
                $scope.weather = weather;
                console.log($scope.weather);


            })
            .error(function (error) {
                $scope.status = 'Unable to load weather data: ' + error.message;
                console.log($scope.status);
            });
    }
});

WeatherReports.factory('WeatherService', ['$http', function ($http) {

    var WeatherService = {};
    WeatherService.getWeatherReport = function () {
        return $http.get('/Dashboard/Weather');
    };
    return WeatherService;

}]);



