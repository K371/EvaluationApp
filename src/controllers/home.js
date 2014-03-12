app.controller("HomeController", [
	"$scope", "ApiFactory", "LogInFactory", "$location", "$http",
	function($scope, ApiFactory, LogInFactory, $location, $http) {
		var token = LogInFactory.getToken();
		var firstname = LogInFactory.getFirstName();
 		if(token === ""){
 			$location.path("/");
 		}
 		
 		$scope.firstname = firstname;
 		
 		
 		//GET EVALUATIONS!!!
 		$http({	method: 'GET', 
				url: 'http://project3api.haukurhaf.net/api/v1/my/evaluations'
				
				}).
    			success(function(data, status, headers, config) {
      				console.log("Get Evaluations!");
      				console.log(data);
      				console.log(status);
      				console.log(headers);
      				console.log(config);
    			}).
    	error(function(data, status, headers, config) {
      // called asynchronously if an error occurs
      // or server returns response with an error status.
    	});


    	//GET COURSES
    	$http({	method: 'GET', 
				url: 'http://project3api.haukurhaf.net/api/v1/my/courses'
				
				}).
    			success(function(data, status, headers, config) {
      				console.log("Get courses!");
      				console.log(data);
      				console.log(status);
      				console.log(headers);
      				console.log(config);
    			}).
    	error(function(data, status, headers, config) {
      // called asynchronously if an error occurs
      // or server returns response with an error status.
    	});



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