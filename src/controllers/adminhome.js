app.controller("AdminHomeController", [
	"$scope", "ApiFactory", "LogInFactory", "$location",
	function($scope, ApiFactory, LogInFactory, $location) {
		var token = LogInFactory.getToken();
 		if(token === ""){
 			$location.path("/");
 		}

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