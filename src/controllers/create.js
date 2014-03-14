app.controller("CreateController", [
	"$scope", "ApiFactory", "LogInFactory", "$location", "$http",
	function($scope, ApiFactory, LogInFactory, $location, $http) {
		var token = LogInFactory.getToken();
		var firstname = LogInFactory.getFirstName();
 		if(token === ""){
 			$location.path("/");
 		}
 		


		
			$scope.evaluation = {
				TitleIS: "",
				TitleEN: "",
				IntroTextIS: "",
				IntroTextEN: "",
				CourseQuestions: [],
				TeacherQuestions: []
			};
		
		$scope.submitTemplate = function() {
			console.log($scope.evaluation.IntroTextIS);

		}

		$scope.addAnswer = function(question) {
			question.Answers.push("");
		}

		$scope.addCourseQuestionSingle = function() {
			$scope.evaluation.CourseQuestions.push({
				ID: $scope.evaluation.CourseQuestions.length,
				TextIS: "",
				TextEN: "",
				ImageURL: "",
				Type: "single",
				Answers: ["", ""]
			});
		}

		$scope.addCourseQuestionMultiple = function() {
			$scope.evaluation.CourseQuestions.push({
				ID: $scope.evaluation.CourseQuestions.length,
				TextIS: "",
				TextEN: "",
				ImageURL: "",
				Type: "multiple",
				Answers: ["", ""]
			});
		}

		$scope.addCourseQuestionText = function() {
			$scope.evaluation.CourseQuestions.push({
				ID: $scope.evaluation.CourseQuestions.length,
				TextIS: "",
				TextEN: "",
				ImageURL: "",
				Type: "text",
			});
		}

		$scope.addTeacherQuestionSingle = function() {
			$scope.evaluation.TeacherQuestions.push({
				ID: $scope.evaluation.TeacherQuestions.length,
				TextIS: "",
				TextEN: "",
				ImageURL: "",
				Type: "single",
				Answers: ["", ""]
			});
		}

		$scope.addTeacherQuestionText = function() {
			$scope.evaluation.TeacherQuestions.push({
				ID: $scope.evaluation.TeacherQuestions.length,
				TextIS: "",
				TextEN: "",
				ImageURL: "",
				Type: "text",
			});
		}

		$scope.addTeacherQuestionMultiple = function() {
			$scope.evaluation.TeacherQuestions.push({
				ID: $scope.evaluation.TeacherQuestions.length,
				TextIS: "",
				TextEN: "",
				ImageURL: "",
				Type: "multiple",
				Answers: ["", ""]
			});
		}
	}
]);