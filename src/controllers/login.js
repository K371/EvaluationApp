app.controller("LogInController", [
	"$scope", "$http",
	function($scope, $http) {
		$scope.username = "";
		$scope.password = "";
 		var user = {};
 		var token = "";
		$scope.getCredentials = function(){
			$http({
				method: 'POST',
				url: 'http://project3api.haukurhaf.net/api/v1/login',
				data: {	'user' : $scope.username, 
						'pass' : $scope.password
				}
			})
			.then(	function(response){
						user = response.data.User;
						token = response.data.Token;
						console.log(user);
						if (user.Role == "admin") {
							console.log("Woah look out, Admin here!");
						}
						else{
							console.log("Just a mere student peasant...");

						}
						
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