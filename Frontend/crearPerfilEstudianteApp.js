
var app = angular.module("crearPerfilEstudianteApp", []); 

app.controller('tecnologiasDisponiblesCtrl', function($scope, $http,$filter) {
	$scope.tecnologiasDisponibles=[];
	$scope.tecnologiasDisponibles.push({"Nombre":"c++","Categoria":"progra"},{"Nombre":"c--","Categoria":"progra"},{"Nombre":"calculo","Categoria":"mate"},
	{"Nombre":"animales","Categoria":"biolo"});
	$scope.tecnologiasDisponibles = $filter('orderBy')($scope.tecnologiasDisponibles, 'Nombre');

	$scope.aCategorias=[];

	$scope.myTec=[];

	$scope.sSelCategoria="";
	
	
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
	$scope.aCategorias=$scope.GetCategorias();
   // get para obtener toda la lista de tecnologias server/tecnologias
   /*$http.get("https://www.w3schools.com/angular/customers_sql.aspx").then(function (response) {$scope.tecnologiasDisponibles = response.data.records;}
    , function(response) {
        //Second function handles error
        $scope.tecnologiasDisponibles = "Error";
    });*/

    
});