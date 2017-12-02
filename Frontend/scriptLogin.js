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
           
           if(response.data=="user"){window.location = "/paginaPrincipal.html";}
           else{}

                
                

            


        },
            function (response) {

            });
    
    };




});