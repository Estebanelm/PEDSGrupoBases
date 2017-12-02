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
        localStorage.setItem('userId',$scope.formulario.ID);
        return newObj;


    };

    $scope.irCrearEstudiante=function(){
      window.location="crearPerfilEstudiante.html";
    };
    $scope.irCrearAdmin=function(){
      window.location="crearPerfilAdmin.html";
    };

    $scope.SubmitForm = function () {
        $scope.login = $scope.BuildLogin($scope.formulario);
        url = $scope.serverURL + "login";
        jsonLogin = JSON.stringify($scope.login);

        //post
        $http.post(url, jsonLogin).then(function (response) {
           
           if(response.data=="user"){window.location = "paginaPrincipal.html";}
           else{window.location = "paginaAdmin.html";}

                
                

            


        },
            function (response) {
              swal({
            position: 'center',
            type: 'error',
            title: "Datos incorrectos",
            showConfirmButton: false,
            timer: 12000
          })
            


            });
    
    };




});