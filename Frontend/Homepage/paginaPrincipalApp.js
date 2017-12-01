var app = angular.module("paginaPrincipalApp", []); 

app.controller('paginaPrincipalCtrl', function($scope, $http,$filter,$location,$window,$sce) {

$scope.estudianteId="777"; //este debe venir del login
$scope.idprueba="201270170"; 
$scope.serverURL="http://186.176.172.50/DigiTutor/api/";
$scope.usuario={};
$scope.publicacionesVisibles=[];
$scope.contenidoVisible;
$scope.tutoriasVisibles;
$scope.paginaPublicacionesVisibles=1;
$scope.comment=-1;
$scope.listaComentarios=[];
$scope.paginaComentarios=1;



$http.get($scope.serverURL+$scope.estudianteId+"/estudiantes/"+$scope.estudianteId).then(function (response) {$scope.usuario = response.data;}
    , function(response) {
        //Second function handles error
        $scope.usuario = "Error";
    });

$http.get($scope.serverURL+$scope.idprueba+"/publicaciones?pag="+$scope.paginaPublicacionesVisibles).then(function (response) {
	$scope.publicacionesVisibles = response.data;
	$scope.tutoriasVisibles=$filter('filter')($scope.publicacionesVisibles, {"Costo" :""});
	$scope.contenidoVisible=$filter('filter')($scope.publicacionesVisibles, {"Costo" :"!"});
}
    , function(response) {
        //Second function handles error
        $scope.publicacionesVisibles = "Error";
    });

$scope.ObtenerComentarios=function(p_Id){
	$http.get($scope.serverURL+"comentarios/"+p_Id.toString()+"?pag="+$scope.paginaComentarios).then(function (response) {$scope.listaComentarios = response.data;}
    , function(response) {
        //Second function handles error
        $scope.listaComentarios = "Error";
    });

};

$scope.CommentButton=function(p_Id){
	if(p_Id!=$scope.comment){
		$scope.comment=p_Id;
		$scope.ObtenerComentarios(p_Id);
	}
	else{
		$scope.comment=-1;
		$scope.paginaComentarios=1;
	}
};

$scope.addCommentPage=function(p_Id){
	$scope.paginaComentarios+=1;
	$scope.ObtenerComentarios(p_Id);

};

$scope.subsCommentPage=function(p_Id){
	if($scope.paginaComentarios>=2){$scope.paginaComentarios-=1;
	$scope.ObtenerComentarios(p_Id);}
	
	

};



});