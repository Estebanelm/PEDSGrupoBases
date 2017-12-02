var app = angular.module("paginaPrincipalApp", []); 

app.controller('paginaPrincipalCtrl', function($scope, $http,$filter,$location,$window,$sce) {

$scope.estudianteId=localStorage.getItem('userId'); 
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
$scope.fechaServidor=new Date();
$scope.formuContenido={};
$scope.tecnologiasDisponibles=[];
$scope.myTec=[];




$http.get($scope.serverURL+$scope.estudianteId+"/estudiantes/"+$scope.estudianteId).then(function (response) {$scope.usuario = response.data;}
    , function(response) {
        //Second function handles error
        $scope.usuario = "Error";
    });

$http.get($scope.serverURL+"tecnologias/").then(function (response) {$scope.tecnologiasDisponibles = response.data; 
   	$scope.tecnologiasDisponibles = $filter('orderBy')($scope.tecnologiasDisponibles, 'Categoria');}
    , function(response) {
        //Second function handles error
        $scope.tecnologiasDisponibles = "Error";
    });

$http.get($scope.serverURL+$scope.estudianteId+"/publicaciones?pag="+$scope.paginaPublicacionesVisibles).then(function (response) {
	$scope.publicacionesVisibles = response.data;
	$scope.tutoriasVisibles=$filter('filter')($scope.publicacionesVisibles, {"Costo" :""});
	$scope.contenidoVisible=$filter('filter')($scope.publicacionesVisibles, {"Costo" :"!"});
}
    , function(response) {
        //Second function handles error
        $scope.publicacionesVisibles = "Error";
    });

$scope.BuildTecs=function(p_myTec){
		aTecs=[];
		for (var i = 0; i<p_myTec.length ; i++) {
			  
			  aTecs.push({"Nombre": p_myTec[i].Nombre , "Apoyos": -1, "MiApoyo":"null"});
		}
		return aTecs;
	};

$scope.Publicar=function(){

	newPub={}
	newPub.Id=0;
	newPub.Titulo=$scope.formuContenido.Titulo;
	newPub.Id_autor=$scope.estudianteId;
	newPub.Nombre_autor=$scope.usuario.Nombre;
	newPub.Tecnologias=$scope.BuildTecs($scope.myTec);
	newPub.Descripcion=$scope.formuContenido.Descripcion;
	newPub.CantidadComentarios=0;
	newPub.CantidadEvaluaciones=0;
	newPub.FechaCreacion=new Date();
	newPub.Link=$scope.formuContenido.Link;
	newPub.Documento=$scope.formuContenido.Doc;
	newPub.Video=$scope.formuContenido.Video;
	newPub.MiEvaluacion="null";
	jsonPub=JSON.stringify(newPub);
	$http.post($scope.serverURL+"publicaciones",jsonPub).then(function (response) { }, 
				function(response) { });

};


$scope.AddMyTec=function(p_nombre){
		if($scope.myTec.length<3){

			$scope.myTec.push($filter('filter')($scope.tecnologiasDisponibles, {"Nombre" :p_nombre})[0]);
			p_nombre="!"+p_nombre;
			$scope.tecnologiasDisponibles = $filter('filter')($scope.tecnologiasDisponibles, {"Nombre" :p_nombre});

		}
		else{
			swal({
			  position: 'center',
			  type: 'error',
			  title: 'Sólo se pueden elegir 10 tecnologías',
			  showConfirmButton: false,
			  timer: 1500
				})
		}
	 };
$scope.ErraseMyTec=function(p_index){
		
			
			$scope.tecnologiasDisponibles.push($scope.myTec[p_index]);
			$scope.tecnologiasDisponibles = $filter('orderBy')($scope.tecnologiasDisponibles, 'Categoria');
			$scope.myTec.splice(p_index,1);
			
	};
$scope.HacerComentario=function(p_Id,p_contenido){
	newObj={};
	newObj.Id_Comentario=0;
	newObj.Id_autor=$scope.estudianteId;
	newObj.Nombre_autor=$scope.usuario.Nombre;
	newObj.Contenido=p_contenido;
	newObj.Fecha_comentario=new Date();
	newObj.Id_publicacion=p_Id;
	jsonComment=JSON.stringify(newObj);

	$http.post($scope.serverURL+"comentarios/"+p_Id.toString(),jsonComment).then(function (response) {
		$scope.ObtenerComentarios(p_Id);
		for (var i = 0; i<$scope.publicacionesVisibles.length; i++) {
			if($scope.publicacionesVisibles[i].Id==p_Id){
				 $scope.publicacionesVisibles[i].CantidadComentarios+=1;
			}

		}
		$scope.tutoriasVisibles=$filter('filter')($scope.publicacionesVisibles, {"Costo" :""});
		$scope.contenidoVisible=$filter('filter')($scope.publicacionesVisibles, {"Costo" :"!"});
	}, 
				function(response) { });



};

$scope.ObtenerComentarios=function(p_Id){
	$http.get($scope.serverURL+"comentarios/"+p_Id.toString()+"?pag="+$scope.paginaComentarios).then(function (response) {$scope.listaComentarios = response.data;}
    , function(response) {
        //Second function handles error
        $scope.listaComentarios = "Error";
    });

};

$scope.ObtenerPublicaciones=function(){
	$http.get($scope.serverURL+$scope.estudianteId+"/publicaciones?pag="+$scope.paginaPublicacionesVisibles).then(function (response) {
	$scope.publicacionesVisibles = response.data;
	$scope.tutoriasVisibles=$filter('filter')($scope.publicacionesVisibles, {"Costo" :""});
	$scope.contenidoVisible=$filter('filter')($scope.publicacionesVisibles, {"Costo" :"!"});
}
    , function(response) {
        //Second function handles error
        $scope.publicacionesVisibles = "Error";
    });

};

// obtienen los comentarios del servidor para una publicacion dada
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
//cambia de página de comentarios hacia arriba 
$scope.addCommentPage=function(p_Id){
	$scope.paginaComentarios+=1;
	$scope.ObtenerComentarios(p_Id);

};
//cambia de página de comentarios hacia abajo
$scope.subsCommentPage=function(p_Id){
	if($scope.paginaComentarios>=2){$scope.paginaComentarios-=1;
	$scope.ObtenerComentarios(p_Id);}
	
};
//cambia de página de publiacciones hacia arriba
$scope.addPublicacionesPage=function(){
	$scope.paginaPublicacionesVisibles+=1;
	$scope.ObtenerPublicaciones();

};
//cambia de página de publicaciones hacia abajo
$scope.subsPublicacionesPage=function(){
	if($scope.paginaPublicacionesVisibles>=2){$scope.paginaPublicacionesVisibles-=1;
	$scope.ObtenerPublicaciones();}
	
};
//hace un put al servidor de una evaluacion hecha 
$scope.Evaluar=function(p_eval,p_Id){
	jsonEval=JSON.stringify({"Id_estudiante":$scope.estudianteId,"Tipo_evaluacion":p_eval,"Id_publicacion":p_Id});
	$http.put($scope.serverURL+"evaluaciones/"+p_Id.toString(),jsonEval).then(function (response) { }, 
				function(response) { });

};





$scope.darLike=function(p_Id,p_esTuto){
	x="null";
	fechaACtual= new Date();
	fechatuto=0;
	registrado=false;
	aprovacion=true;
	for (var i = 0; i<$scope.publicacionesVisibles.length; i++) {
		if($scope.publicacionesVisibles[i].Id==p_Id){
			if(p_esTuto==true){

					fechatuto=$scope.publicacionesVisibles[i].FechaTutoria;
					registrado=$scope.publicacionesVisibles[i].EstoyRegistrado;
					if(fechatuto>fechaACtual&&registrado){aprovacion=true;}
					else{aprovacion=false;}	
					
			}
			if(aprovacion){
				x=$scope.publicacionesVisibles[i].MiEvaluacion;
				if(x=="pos"){
					$scope.publicacionesVisibles[i].MiEvaluacion="null"; 
					$scope.Evaluar("null",p_Id);
					$scope.publicacionesVisibles[i].CantidadEvaluaciones-=1;}

				else{
					
					$scope.publicacionesVisibles[i].MiEvaluacion="pos";
					$scope.Evaluar("pos",p_Id);
					if(x=="null"){ $scope.publicacionesVisibles[i].CantidadEvaluaciones+=1;}
					
					
				}

			}
		}
		
		
	}
	$scope.tutoriasVisibles=$filter('filter')($scope.publicacionesVisibles, {"Costo" :""});
	$scope.contenidoVisible=$filter('filter')($scope.publicacionesVisibles, {"Costo" :"!"});
};

$scope.darDislike=function(p_Id,p_esTuto){
	x="null";
	fechaACtual= new Date();
	fechatuto=0;
	registrado=false;
	aprovacion=true;
	for (var i = 0; i<$scope.publicacionesVisibles.length; i++) {
		if($scope.publicacionesVisibles[i].Id==p_Id){
			if(p_esTuto==true){

					fechatuto=$scope.publicacionesVisibles[i].FechaTutoria;
					registrado=$scope.publicacionesVisibles[i].EstoyRegistrado;
					if(fechatuto>fechaACtual&&registrado){aprovacion=true;}
					else{aprovacion=false;}	
					
			}
			if(aprovacion){
				x=$scope.publicacionesVisibles[i].MiEvaluacion;
				if(x=="neg"){
					$scope.publicacionesVisibles[i].MiEvaluacion="null"; 
					$scope.Evaluar("null",p_Id); 
					$scope.publicacionesVisibles[i].CantidadEvaluaciones-=1;}
				else{
					$scope.publicacionesVisibles[i].MiEvaluacion="neg";
					$scope.Evaluar("neg",p_Id);
					if(x=="null"){$scope.publicacionesVisibles[i].CantidadEvaluaciones+=1;}
					
				}
			}
		}
		
	}	
	$scope.tutoriasVisibles=$filter('filter')($scope.publicacionesVisibles, {"Costo" :""});
	$scope.contenidoVisible=$filter('filter')($scope.publicacionesVisibles, {"Costo" :"!"});

};
// decide si poner el icono de evaluado o sin evaluar positivo
$scope.myPosEval=function(p_Id){
	$scope.helper= $scope.publicacionesVisibles.filter(function(item){return item.Id == p_Id;});
	if($scope.helper[0].MiEvaluacion=="pos"){
		return 1;
	}
	else{return 0;}

};	
//decide si poner el icono de evaluado o sin evaluar negativo
$scope.myNegEval=function(p_Id){
	$scope.helper= $scope.publicacionesVisibles.filter(function(item){return item.Id == p_Id;});
	if($scope.helper[0].MiEvaluacion=="neg"){
		return 1;
	}
	else{return 0;}

};	



});