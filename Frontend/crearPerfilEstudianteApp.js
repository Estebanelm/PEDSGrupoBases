

var app = angular.module("crearPerfilEstudianteApp", []); 

app.controller('tecnologiasDisponiblesCtrl', function($scope, $http,$filter,$location,$window) {
	$scope.tecnologiasDisponibles=[];
	$scope.tecnologiasDisponibles.push({"Nombre":"c++","Categoria":"progra"},{"Nombre":"c--","Categoria":"progra"},{"Nombre":"calculo","Categoria":"mate"},
	{"Nombre":"animales","Categoria":"biolo"});
	$scope.tecnologiasDisponibles = $filter('orderBy')($scope.tecnologiasDisponibles, 'Nombre');

	$scope.aCategorias=[];
	$scope.myTec=[];
	$scope.aUniversidades=[{"Nombre":"TEC"},{"Nombre":"UCR"},{"Nombre":"UNA"}];
	$scope.aPaises=[{"Nombre":"Costa Rica"},{"Nombre":"USA"},{"Nombre":"España"}];
	$scope.formulario={};
	$scope.sSelCategoria="";
	
	$scope.sPassword;
	$scope.spassword2;
	$scope.bCondiciones;
	
	
	//función que determina si se aceptaron los terminos y condiciones y si las contraseñas ingresadas coiciden. 
	//en caso de no cumplir las condicones retorna un string indicando el error.
	$scope.VerificaFormulario=function(){
		if($scope.bCondiciones){
			if($scope.sPassword==$scope.sPassword2){
				if($scope.sPassword.length>=4){return "ok";}
				else{return "La Contraseña debe ser de mínimo 4 caracteres";}	
			

			}
			else {return "Las contraseñas ingresadas no coinciden";}
		}
		else{return "Primero debe aceptar los términos y condiciones";}

	};
	
	$scope.BuildTecs=function(){
		aTecs=[];
		for (var i = 0; i<$scope.myTec.length ; i++) {
			  
			  aTecs.push({"Nombre": $scope.myTec[i].Nombre , "Apoyos": -1, "MiApoyo":"null"});
		}
		return aTecs;
	};

	$scope.BuildStudent=function(){
		$scope.formulario.Tecnologias=$scope.BuildTecs();
		$scope.formulario.CantSeguidores=-1;
		$scope.formulario.Participacion=-1;
		$scope.formulario.Reputacion=-1;
		//$scope.formulario.FechaInscripcion=new Date();


	};

	$scope.SubmitForm=function(){
		sVerificacion=$scope.VerificaFormulario();
		if(sVerificacion!="ok"){
			swal({
			  position: 'center',
			  type: 'error',
			  title: sVerificacion,
			  showConfirmButton: false,
			  timer: 2000
				})

		}
		else{
			$scope.BuildStudent();
			url="api/estudiantes?pwd="+$scope.sPassword;
			jsonStudent=angular.toJson($scope.formulario);
			 
			//post
			$http.post(url,jsonStudent).then(function (response) {
				if(response.data=="200 ok"){
					swal({
			 			  position: 'center',
						  type: 'success',
						  title: "Perfil creado con éxito",
						  showConfirmButton: false,
						  timer: 2000
					})
					window.location="http://www.grupobases.hol.es/";
				}
				if(response.data=="401 datos invalidos"){
					swal({
					  position: 'center',
					  type: 'error',
					  title: "Datos Inválidos",
					  showConfirmButton: false,
					  timer: 2000
					})
				}	
				else{
					swal({
					  position: 'center',
					  type: 'error',
					  title: "ERROR DE SERVIDOR",
					  showConfirmButton: false,
					  timer: 2000
					})

				}


			}, 
				function(response) {
					swal({
					  position: 'center',
					  type: 'error',
					  title: jsonStudent,
					  showConfirmButton: false,
					  timer: 2000
					})


				});
		}	
	}

	//función para seleccionar una tecnologia de la lista de disponibles, se quita de la lista y se agrega en la sección de mis tecnologias 
	$scope.AddMyTec=function(p_nombre){
		if($scope.myTec.length<10){

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
			$scope.tecnologiasDisponibles = $filter('orderBy')($scope.tecnologiasDisponibles, 'Nombre');
			$scope.myTec.splice(p_index,1);
			
	};

	$scope.ClkCategory=function(p_category){
			
			if(p_category=="todas"){$scope.sSelCategoria="";
			}
			else{
			$scope.sSelCategoria=p_category;
			}
	};

	$scope.GetUnique=function(collection, keyname) {
              var output = [], 
                  keys = [];

              angular.forEach(collection, function(item) {
                  var key = item[keyname];
                  if(keys.indexOf(key) === -1) {
                      keys.push(key);
                      output.push(item);
                  }
              });
        return output;
   };

	$scope.GetCategorias=function(){
		aHelper=["todas"];
		aHelper2= $scope.GetUnique($scope.tecnologiasDisponibles,"Categoria");
		for (var i = aHelper2.length - 1; i >= 0; i--) {
			aHelper.push(aHelper2[i].Categoria);
		}  
		return aHelper;

	};   
	

   // get para obtener toda la lista de tecnologias server/tecnologias
   /*$http.get("https://www.w3schools.com/angular/customers_sql.aspx").then(function (response) {$scope.tecnologiasDisponibles = response.data.records;}
    , function(response) {
        //Second function handles error
        $scope.tecnologiasDisponibles = "Error";
    });*/

    // get para obtener toda la lista de paises server/paises
   /*$http.get("https://www.w3schools.com/angular/customers_sql.aspx").then(function (response) {$scope.aPaises = response.data.records;}
    , function(response) {
        //Second function handles error
        $scope.tecnologiasDisponibles = "Error";
    });*/

    // get para obtener toda la lista de universidades server/universidades
   /*$http.get("https://www.w3schools.com/angular/customers_sql.aspx").then(function (response) {$scope.aUniversidades = response.data.records;}
    , function(response) {
        //Second function handles error
        $scope.tecnologiasDisponibles = "Error";
    });*/

    $scope.aCategorias=$scope.GetCategorias();

    
});
