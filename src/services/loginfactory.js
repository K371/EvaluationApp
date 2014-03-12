app.factory("LogInFactory", [
	"$http",
	function($http) {
		var token = "";
		var firstname = "";
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
						token = response.data.Token;
						firstname = response.data.User.FullName;
						$http.defaults.headers.common['Authorization'] = "Basic " + token;
						return response;
					}
			);

			return promise;
			},

			getToken: function(){
				var tempToken = token;
				return tempToken;
			},
			
			getFirstName: function(){
				var str = firstname;
				str = str.split(" ");
	
				return str[0];
			}

		};

	},

]);