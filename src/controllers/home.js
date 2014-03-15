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
 		
 		$scope.firstname = firstname;

 		ApiFactory.getCourses().then(function(data) {
			$scope.courses = data.data;
		}, function(errorMessage) {
			console.log("Error: " + errorMessage);
		}, function(updateMessage) {
			console.log("Update: " + updateMessage);
		});

		ApiFactory.getEvaluationTemplates().then(function(data) {
			$scope.evaluationtemplates = data.data;
		}, function(errorMessage) {
			console.log("Error: " + errorMessage);
		}, function(updateMessage) {
			console.log("Update: " + updateMessage);
		});

		ApiFactory.getAllEvaluations().then(function(data) {
			$scope.evaluations = data.data;
		}, function(errorMessage) {
			console.log("Error: " + errorMessage);
		}, function(updateMessage) {
			console.log("Update: " + updateMessage);
		});


	}
]);