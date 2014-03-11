app.controller("LogInController", [
	"$scope", "ApiFactory",
	function($scope, $http) {
		$scope.username = "";
		$scope.password = "";
 
		$scope.getCredentials = function(){
			var userObj = {
				user: $scope.username,
				pass: $scope.password
			};
			console.log(userObj);
			$http({
				method: 'POST',
				url: 'http://project3api.haukurhaf.net/api/v1/login',
				data: userObj,
				headers: {'Content-Type' : 'application/json'}
			});

				


		};
	}
]);