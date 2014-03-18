app.controller("ReviewController", [
	"$scope", "ApiFactory", "LogInFactory", "$location", "$http", "$routeParams",
	function($scope, ApiFactory, LogInFactory, $location, $http, $routeParams) {
		var token = LogInFactory.getToken();
		var role = LogInFactory.getRole();
		var firstname = LogInFactory.getFirstName();
		var courseID = $routeParams.courseID;
		$scope.courseID = $routeParams.courseID;
		var evaluationID = $routeParams.evaluationID;

 		if(token === ""){
 			$location.path("/");
 		}
 		if (role === "student") {
 			$location.path("/Student");
 		}
 		
 			$http({	method: 'GET', 
					url: 'http://dispatch.ru.is/h26/api/v1/evaluations/' + evaluationID
				
				}).
    			success(function(data, status, headers, config) {
      				$scope.evaluation = data;
      				console.log(data);
    			}).
		    		error(function(data, status, headers, config) {
		      		// called asynchronously if an error occurs
		      		// or server returns response with an error status.
		    	});
 		


	}
]);