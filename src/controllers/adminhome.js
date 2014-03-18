app.controller("AdminHomeController", [
	"$scope", "ApiFactory", "LogInFactory", "$location", "$http",
	function($scope, ApiFactory, LogInFactory, $location, $http) {
		var token = LogInFactory.getToken();
		var role = LogInFactory.getRole();
		var firstname = LogInFactory.getFirstName();
 		if(token === ""){
 			$location.path("/");
 		}
 		if (role === "student") {
 			$location.path("/Student");

 		}
 		
 		$http({	method: 'GET', 
					url: 'http://dispatch.ru.is/h26/api/v1/my/courses/'
				
				}).
    			success(function(data, status, headers, config) {
      				$scope.courses = data;
      				console.log(data);
    			}).
		    		error(function(data, status, headers, config) {
		      		// called asynchronously if an error occurs
		      		// or server returns response with an error status.
		    	});

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