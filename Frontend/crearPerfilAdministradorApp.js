var app = angular.module("crearPerfilAdministradorApp", []); 

app.controller('AdminCtrl', function($scope, $http,$filter,$location,$window) {
	

	$scope.formulario={};
	$scope.administrador={};
	$scope.serverURL="http://186.176.172.50/DigiTutor/api/";
	$scope.sPassword;
	$scope.spassword2;
	
	
	//función que determina si las contraseñas ingresadas coiciden. 
	//en caso de no cumplir las condicones retorna un string indicando el error.
	$scope.VerificaFormulario=function(p_sPassword,p_sPassword2){
			if(p_sPassword==p_sPassword2){
				if(p_sPassword.length>=4){return "ok";}
				else{return "La Contraseña debe ser de mínimo 4 caracteres";}	
			

			}
			else {return "Las contraseñas ingresadas no coinciden";}

	};
	

	$scope.BuildAdmin=function(p_formulario){
		newObj={};
		newObj.Nombre=p_formulario.Nombre;
		newObj.Apellido=p_formulario.Apellido;
		newObj.NombreUsuario=p_formulario.NombreUsuario;
		newObj.Correo=p_formulario.Correo;
		newObj.Id=-1;
		newObj.FechaInscripcion=new Date();

		return newObj;


	};

	$scope.SubmitForm=function(){
		sVerificacion=$scope.VerificaFormulario($scope.sPassword,$scope.sPassword2);
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
			$scope.administrador=$scope.BuildAdmin($scope.formulario);
			url=$scope.serverURL+"admins?pwd="+$scope.sPassword;
			jsonAdmin=JSON.stringify($scope.administrador);
			 
			//post
			$http.post(url,jsonAdmin).then(function (response) {

					swal({
			 			  position: 'center',
						  type: 'success',
						  title: "Perfil creado con éxito",
						  showConfirmButton: false,
						  timer: 2000
					})
					window.setTimeout(function(){

       						 // Move to a new location or you can do something else
        					 window.location="http://186.176.172.50/DigiTutor/";

    				}, 2000);


			}, 
				function(response) {
					swal({
					  position: 'center',
					  type: 'error',
					  title: "nombre de usuario o correo ya utilizados",
					  showConfirmButton: false,
					  timer: 12000
					})


				});
		}	
	}

	

    
});
