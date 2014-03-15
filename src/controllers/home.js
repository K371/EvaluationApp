app.controller("HomeController", [
	"$scope", "ApiFactory", "LogInFactory", "$location", "$http",
	function($scope, ApiFactory, LogInFactory, $location, $http) {
		var token = LogInFactory.getToken();
		var firstname = LogInFactory.getFirstName();
 		if(token === ""){
 			$location.path("/");
 		}
 		
 		$scope.firstname = firstname;

 		ApiFactory.getCourses().then(function(data) {
			$scope.courses = data.data;
			console.log(data);
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