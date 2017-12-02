var app = angular.module('loginApp', ["ngRoute"]);

app.config(function($routeProvider) {
  $routeProvider
  .when("/", {
    templateUrl : $location.url()
  })
  .when("/hola", {
    templateUrl : "file:///C:/Users/PC/Documents/PEDSGrupoBases/Frontend/Homepage/paginaPrincipal.html"
  })
  .when("/green", {
    templateUrl : "green.htm"
  })
  .when("/blue", {
    templateUrl : "blue.htm"
  });
});

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
       /* $scope.login = $scope.BuildLogin($scope.formulario);
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

            });*/
    
    }




});