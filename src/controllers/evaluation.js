app.controller("EvaluationController", [
	"$scope", "ApiFactory", "$routeParams", "LogInFactory", "$http", "$location",
	function($scope, ApiFactory, $routeParams, LogInFactory, $http, $location) {
		var evaluationID = $routeParams.evaluationID;
		var token = LogInFactory.getToken();
		if(token === ""){
 			$location.path("/");
 		}
 		$scope.startDate = "";
 		$scope.startTime = "";
 		$scope.endDate = "";
 		$scope.endTime = "";


		/* Only works for students atm, must use LogInFactory to determine role */
		$scope.redirectBack = function(){
			if(LogInFactory.getRole() === "student")
			$location.path('/Student');
			if(LogInFactory.getRole() === "admin")
			$location.path('/admin');

		};
		
		if(evaluationID !== undefined) {
			ApiFactory.getEvaluationTemplateById(evaluationID).then(function(response) {
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

		$scope.submitEvaluation = function(){
			var startDateTime = $scope.startDate + "T" + $scope.startTime + ":00.0000000+00:00";
			var endDateTime = $scope.endDate + "T" + $scope.endTime + ":00.0000000+00:00";
			var submission = {
				TemplateID: evaluationID,
				StartDate: startDateTime,
				EndDate: endDateTime
			}
			ApiFactory.addEvaluation(submission);
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