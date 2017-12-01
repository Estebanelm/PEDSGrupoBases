var app = angular.module("paginaPrincipalApp", []); 

app.controller('paginaPrincipalCtrl', function($scope, $http,$filter,$location,$window) {

$scope.estudianteId="777"; //este debe venir del login 
$scope.serverURL="http://186.176.172.50/DigiTutor/api/";
$scope.usuario={};



$http.get($scope.serverURL+$scope.estudianteId+"/estudiantes/"+$scope.estudianteId).then(function (response) {$scope.usuario = response.data;}
    , function(response) {
        //Second function handles error
        $scope.usuario = "Error";
    });



});