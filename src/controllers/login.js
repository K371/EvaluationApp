app.controller("LogInController", [
	"$scope", "$http", "LogInFactory",
	function($scope, $http, LogInFactory) {
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
				console.log(data.data.User.FullName);
				console.log(data.data.Token);
			});
			
			/*$http({
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

			
			*/
		};
	}
]);