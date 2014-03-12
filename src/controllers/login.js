app.controller("LogInController", [
	"$scope", "$http", "LogInFactory", "$location",
	function($scope, $http, LogInFactory, $location) {
		$scope.username = "";
		$scope.password = "";
 		var user = {};
 		var token = "";

		$scope.getCredentials = function(){
			var userObj = {
				user: $scope.username,
				pass: $scope.password
			}
			LogInFactory.logMeIn(userObj).then(function(data){
				if(data.data.User.Role === 'student'){
					$location.path('/home');
				}
				else if(data.data.User.Role === 'admin'){
					$location.path('/adminhome');
				}
			});

			$scope.wrongUser = "Wrong Username and/or Password";
			
		
		};
	

	}
]);