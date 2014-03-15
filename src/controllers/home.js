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
		
		}, function(errorMessage) {
			console.log("Error: " + errorMessage);
		}, function(updateMessage) {
			console.log("Update: " + updateMessage);
		});

		var count = 0;
		$scope.str = "";
		$scope.whatEval = function(evaluation){
			
			if(count < 8){
				
			//console.log(evaluation.ID);
			console.log(++count);
		ApiFactory.getEvaluationById(evaluation.ID).then(function(data){
				$scope.str = data.data.TemplateTitleIS;
		}, function(errorMessage) {
			console.log("Error: " + errorMessage);
		}, function(updateMessage) {
			console.log("Update: " + updateMessage);
		});
			
			
		}
	}
	


	}
]);