app.controller("LogInController", [
	"$scope", "$http",
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
				method: "POST",
				url: 'http://project3api.haukurhaf.net/api/v1/login',
				data: {	'user' : $scope.username, 
						'pass' : $scope.password
				}
				//headers: {'Content-Type' : 'application/json'}
			})
			.then(	function(response){
						console.log(response);	
					},
					function(response){
						console.log("Failed");
					}	
			);

			/*ApiFactory.logMeIn(userObj).then(function(data) {
				console.log("Success, data:", data);
			}, function(errorMessage){
				console.log("Error: " + errorMessage);
			},	function(updateMessage){
				console.log("Update: " + updateMessage);
			});*/

		};
	}
]);