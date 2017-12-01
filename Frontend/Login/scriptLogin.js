var app = angular.module('loginApp', []);

app.controller('loginCtrl', function ($scope, $http, $filter, $location, $window) {


    $scope.formulario = {};
    $scope.login = {};
    $scope.serverURL = "http://186.176.172.50/DigiTutor/api/";
    $scope.ID;
    $scope.Password;

    $scope.BuildLogin = function (p_formulario) {
        newObj = {};
        newObj.id_estudiante = $scope.formulario.ID;
        newObj.contrasena = $scope.formulario.Password;
        return newObj;


    };

    $scope.SubmitForm = function () {
        $scope.login = $scope.BuildLogin($scope.formulario);
        url = $scope.serverURL + "login";
        jsonLogin = JSON.stringify($scope.login);

        //post
        $http.post(url, jsonLogin).then(function (response) {
            window.setTimeout(function () {

                // Move to a new location or you can do something else
                window.location = "http://186.176.172.50/DigiTutor/";

            }, 2000);


        },
            function (response) {

            });
    }




});