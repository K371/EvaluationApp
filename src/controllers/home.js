app.controller("HomeController", [
	"$scope", "ApiFactory", "LogInFactory", "$location",
	function($scope, ApiFactory, LogInFactory, $location) {
		var token = LogInFactory.getToken();
		var firstname = LogInFactory.getFirstName();
 		if(token === ""){
 			$location.path("/");
 		}
 		
 		$scope.firstname = firstname;
 		
 		
 		

		ApiFactory.getAllEvaluations().then(function(data) {
			//console.log("Success, data: ", data);
			$scope.evaluations = data;
		}, function(errorMessage) {
			console.log("Error: " + errorMessage);
		}, function(updateMessage) {
			console.log("Update: " + updateMessage);
		});


	}
]);