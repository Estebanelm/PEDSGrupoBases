var app = angular.module("paginaPrincipalApp", []); 

app.controller('paginaPrincipalCtrl', function($scope, $http,$filter,$location,$window,$sce) {

$scope.estudianteId="777"; //este debe venir del login
$scope.idusuario="201270170"; 
$scope.serverURL="http://186.176.172.50/DigiTutor/api/";
$scope.usuario={};
$scope.publicacionesVisibles=[];
$scope.contenidoVisible;
$scope.tutoriasVisibles;
$scope.paginaPublicacionesVisibles=1;
$scope.comment=-1;
$scope.like=-1;
$scope.listaComentarios=[];
$scope.paginaComentarios=1;
$scope.rutaLikeArray=["Recursos/like-off.png","Recursos/like.png"];
$scope.rutaDislikeArray=["Recursos/dislike-off.png","Recursos/dislike.png"];
$scope.rutaLikeIndex=0;




$http.get($scope.serverURL+$scope.estudianteId+"/estudiantes/"+$scope.estudianteId).then(function (response) {$scope.usuario = response.data;}
    , function(response) {
        //Second function handles error
        $scope.usuario = "Error";
    });

$http.get($scope.serverURL+$scope.idusuario+"/publicaciones?pag="+$scope.paginaPublicacionesVisibles).then(function (response) {
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

$scope.ObtenerPublicaciones=function(){
	$http.get($scope.serverURL+$scope.idusuario+"/publicaciones?pag="+$scope.paginaPublicacionesVisibles).then(function (response) {
	$scope.publicacionesVisibles = response.data;
	$scope.tutoriasVisibles=$filter('filter')($scope.publicacionesVisibles, {"Costo" :""});
	$scope.contenidoVisible=$filter('filter')($scope.publicacionesVisibles, {"Costo" :"!"});
}
    , function(response) {
        //Second function handles error
        $scope.publicacionesVisibles = "Error";
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

$scope.addPublicacionesPage=function(){
	$scope.paginaPublicacionesVisibles+=1;
	$scope.ObtenerPublicaciones();

};

$scope.subsPublicacionesPage=function(){
	if($scope.paginaPublicacionesVisibles>=2){$scope.paginaPublicacionesVisibles-=1;
	$scope.ObtenerPublicaciones();}
	
};

$scope.Evaluar=function(p_eval,p_Id){
	jsonEval=JSON.stringify({"Id_estudiante":$scope.idusuario,"Tipo_evaluacion":p_eval,"Id_publicacion":p_Id});
	$http.put($scope.serverURL+"evaluaciones/"+p_Id.toString(),jsonEval).then(function (response) { }, 
				function(response) { });

};





$scope.darLike=function(p_Id){
	x="null";
	for (var i = 0; i<$scope.publicacionesVisibles.length; i++) {
		if($scope.publicacionesVisibles[i].Id==p_Id){
			x=$scope.publicacionesVisibles[i].MiEvaluacion;
			if(x=="pos"){$scope.publicacionesVisibles[i].MiEvaluacion="null"; $scope.Evaluar("null",p_Id);}
			else{
				$scope.publicacionesVisibles[i].MiEvaluacion="pos";
				$scope.Evaluar("pos",p_Id);
			}
		}
		
	}
};

$scope.darDislike=function(p_Id){
	x="null";
	for (var i = 0; i<$scope.publicacionesVisibles.length; i++) {
		if($scope.publicacionesVisibles[i].Id==p_Id){
			x=$scope.publicacionesVisibles[i].MiEvaluacion;
			if(x=="neg"){$scope.publicacionesVisibles[i].MiEvaluacion="null"; $scope.Evaluar("null",p_Id);}
			else{
				$scope.publicacionesVisibles[i].MiEvaluacion="neg";
				$scope.Evaluar("neg",p_Id);
			}
		}
		
	}	
	

};
$scope.myPosEval=function(p_Id){
	$scope.helper= $scope.publicacionesVisibles.filter(function(item){return item.Id == p_Id;});
	if($scope.helper[0].MiEvaluacion=="pos"){
		return 1;
	}
	else{return 0;}

};	

$scope.myNegEval=function(p_Id){
	$scope.helper= $scope.publicacionesVisibles.filter(function(item){return item.Id == p_Id;});
	if($scope.helper[0].MiEvaluacion=="neg"){
		return 1;
	}
	else{return 0;}

};	



});