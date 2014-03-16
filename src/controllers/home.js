app.controller("HomeController", [
	"$scope", "ApiFactory", "LogInFactory", "$location", "$http",
	function($scope, ApiFactory, LogInFactory, $location, $http) {
		var token = LogInFactory.getToken();
		var role = LogInFactory.getRole();
		var firstname = LogInFactory.getFirstName();
		
 		if(token === ""){
 			$location.path("/");
 		}
 		if (role === "admin") {
 			$location.path("/admin");
 		}
 		var temp;

 		$scope.firstname = firstname;


		ApiFactory.getMyEvaluations().then(function(data) {
			
			temp = data.data;
			
			$scope.evaluations = temp;

			    angular.forEach($scope.evaluations,function(value,index){
                //console.log(value.ID);
                //console.log(value);
            })
		
		}, function(errorMessage) {
			console.log("Error: " + errorMessage);
		}, function(updateMessage) {
			console.log("Update: " + updateMessage);
		});

	

			$scope.names = [];
	


			ApiFactory.getAllEvaluations().then(function(data3) {
				//console.log(data3.data);

				 angular.forEach(data3.data,function(value,index){
				 	//console.log(value.ID);
				 	//console.log(value.TemplateTitleIS);
				 	$scope.names[value.ID] = value.TemplateTitleIS;

				})


		}, function(errorMessage) {
			console.log("Error: " + errorMessage);
		}, function(updateMessage) {
			console.log("Update: " + updateMessage);
		
		
		});



	

	}
]);