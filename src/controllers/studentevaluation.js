app.controller("StudentEvaluationController", [
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
			ApiFactory.getEvaluationById(evaluationID).then(function(response) 
			{
				// success get templateid from evaluationid
				ApiFactory.getEvaluationTemplateById(response.data.TemplateID).then(function(template){
					$scope.evaluation = template.data;
				}, function(errorMessage){
					console.log("Error fetching evaluation: " + errorMessage);
				});
				// ----------------------------------------
			}, 

			function(errorMessage) {
				console.log("Error fetching evaluation: " + errorMessage);
			});
		}
		else {
			$location.path('/');
		}

		$scope.submitEvaluation = function(){

			//ApiFactory.addEvaluation();
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