app.controller("EvaluationController", [
	"$scope", "ApiFactory", "LogInFactory", "$routeParams", "$location",
	function($scope, ApiFactory, LogInFactory, $routeParams, $location) {
		var evaluationID = $routeParams.evaluationID;

		/* Only works for students atm, must use LogInFactory to determine role */
		$scope.redirectBack = function(){
			if(LogInFactory.getRole() === "student")
			$location.path('/Student');
			if(LogInFactory.getRole() === "admin")
			$location.path('/admin');

		};
		
		if(evaluationID !== undefined) {
			ApiFactory.getEvaluationById(evaluationID).then(function(data) {
				$scope.evaluation = data;
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