app.factory("LogInFactory", [
	"$http",
	function($http) {
		return{
			logMeIn: function(userObj){
				var promise = $http({
				method: 'POST',
				url: 'http://project3api.haukurhaf.net/api/v1/login',
				data: {	'user' : userObj.user, 
						'pass' : userObj.pass
				}
			})
			.then(	function(response){
						var user = response.data.User;
						var token = response.data.Token;
						return response;
					}
			);
			
			return promise;
			}
		};

	},

]);