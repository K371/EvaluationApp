app.controller("CourseEvaluationController", [
	"$scope", "ApiFactory", "LogInFactory", "$location", "$http", "$routeParams",
	function($scope, ApiFactory, LogInFactory, $location, $http, $routeParams) {
		var token = LogInFactory.getToken();
		var role = LogInFactory.getRole();
		var firstname = LogInFactory.getFirstName();
		var courseID = $routeParams;

 		if(token === ""){
 			$location.path("/");
 		}
 		if (role === "admin") {
 			$location.path("/admin");
 		}
 		
 		$scope.getMyEvals = function(){
 			$http({	method: 'GET', 
					url: 'http://dispatch.ru.is/h26/api/v1/courses/' + courseID + '/20141/evaluations/'
				
				}).
    			success(function(data, status, headers, config) {
      				console.log(data);
    			}).
		    		error(function(data, status, headers, config) {
		      		// called asynchronously if an error occurs
		      		// or server returns response with an error status.
		    	});
 		}


	}
]);