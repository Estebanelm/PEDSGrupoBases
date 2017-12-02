var app = angular.module('administradorApp', []);

app.controller('administradorCtrl', function ($scope, $http, $filter, $location, $window) {


    /*$scope.formulario = {};
    $scope.reclutamiento = {};
    $scope.Pais;
    $scope.Universidad;
    $scope.Tec1;
    $scope.Tec2;
    $scope.Tec3;
    $scope.Tec4;
    $scope.w1;
    $scope.w2;
    $scope.w3;
    $scope.w4;*/
    $scope.serverURL = "http://186.176.172.50/DigiTutor/api/";
    

    $scope.SubmitForm = function () {
        url = $scope.serverURL + "estudiantes?id_pais=" + $scope.formulario.pais + "&id_un=" + $scope.formulario.universidad + "&tec1=" + $scope.formulario.tec1 + "&w1=" + $scope.formulario.w1 + "&tec2=" + $scope.formulario.tec2 + "&w2=" + $scope.formulario.w2 + "&tec3=" + $scope.formulario.tec3 + "&w3=" + $scope.formulario.w3 + "&tec4=" + $scope.formulario.tec4 + "&w4=" + $scope.formulario.w4 + "&pag=1";
        //url = "http://186.176.172.50/DigiTutor/api/estudiantes?id_pais=Costa Rica&id_un=3&tec1=2&w1=30&tec2=4&w2=70&tec3=0&w3=0&tec4=0&w4=0&pag=1";

        // get para obtener toda la lista de universidades server/universidades
        $http.get(url).then(function (response) { alert(response.data) }
            , function (response) {
                //Second function handles error
                  alert("Error");
            });
    }

    // get para obtener toda la lista de paises server/paises
    $http.get($scope.serverURL + "paises/").then(function (response) { $scope.Paises = response.data }
        , function (response) {
            //Second function handles error
            $scope.Paises = "Error";
        });

    // get para obtener toda la lista de universidades server/universidades
    $http.get($scope.serverURL + "universidades/").then(function (response) { $scope.Universidades = response.data; }
        , function (response) {
            //Second function handles error
            $scope.Universidades = "Error";
        });

    $http.get($scope.serverURL + "tecnologias/").then(function (response) { $scope.tecnologiasDisponibles = response.data; }
        , function (response) {
            //Second function handles error
            $scope.tecnologiasDisponibles = "Error";
        });

});