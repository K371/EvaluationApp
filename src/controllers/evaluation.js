app.controller("EvaluationController", [
	"$scope", "ApiFactory", "$routeParams", "LogInFactory", "$http",
	function($scope, ApiFactory, $routeParams, LogInFactory, $http) {
		var evaluationID = $routeParams.evaluationID;
		var token = LogInFactory.getToken();
		console.log(token);
<<<<<<< HEAD
=======
		
>>>>>>> 724ec963089a57dad641b9026c8459278c6a590e



		/* Only works for students atm, must use LogInFactory to determine role */
		$scope.redirectBack = function(){
			if(LogInFactory.getRole() === "student")
			$location.path('/Student');
			if(LogInFactory.getRole() === "admin")
			$location.path('/admin');

		};
		
		if(evaluationID !== undefined) {
			ApiFactory.getEvaluationById(evaluationID).then(function(response) {
				console.log("Success, data: ", response.data);
				$scope.evaluation = response.data;
			}, function(errorMessage) {
				console.log("Error fetching evaluation: " + errorMessage);
			});
		}
		else {
			$scope.evaluation = {
				TitleIS: "",
				TitleEN: "",
				IntroTextIS: "",
				IntroTextEN: "",
				CourseQuestions: [],
				TeacherQuestions: []
			};
		}

		$scope.addAnswer = function(question) {
			question.Answers.push("New answer");
		}

		$scope.addCourseQuestion = function() {
			$scope.evaluation.CourseQuestions.push({
				ID: $scope.evaluation.CourseQuestions.length,
				TextIS: "",
				TextEN: "",
				ImageURL: "",
				Type: "single",
				Answers: []
			});
		}
	}
])