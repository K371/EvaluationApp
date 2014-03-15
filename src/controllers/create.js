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
				TitleEN: "English Version is not supported",
				IntroTextIS: "",
				IntroTextEN: "English Version is not supported",
				CourseQuestions: [],
				TeacherQuestions: []
			};
		
		$scope.submitTemplate = function() {
			console.log($scope.evaluation);
			ApiFactory.submitEvaluationTemplate($scope.evaluation);
			$location.path('#/admin/');

		}

		$scope.addAnswer = function(question) {
			question.Answers.push({
				ID: question.Answers.length,
				TextIS: "",
				TextEN: "English Version not supported",
				ImageURL: "",
				Weight: 5
			});
		}

		$scope.addPicture = function(question){
			question.Answers.push({
				ID: question.Answers.length,
				TextIS: "",
				TextEN: "English Version not supported",
				ImageURL: "#",
				Weight: 5
			});
		}

		$scope.addCourseQuestionSingle = function() {
			$scope.evaluation.CourseQuestions.push({
				ID: $scope.evaluation.CourseQuestions.length,
				TextIS: "",
				TextEN: "English Version is not supported",
				ImageURL: "",
				Type: "single",
				Answers: []
			});
		}

		$scope.addCourseQuestionMultiple = function() {
			$scope.evaluation.CourseQuestions.push({
				ID: $scope.evaluation.CourseQuestions.length,
				TextIS: "",
				TextEN: "English Version is not supported",
				ImageURL: "",
				Type: "multiple",
				Answers: []
			});
		}

		$scope.addCourseQuestionText = function() {
			$scope.evaluation.CourseQuestions.push({
				ID: $scope.evaluation.CourseQuestions.length,
				TextIS: "",
				TextEN: "English Version is not supported",
				ImageURL: "",
				Type: "text",
			});
		}

		$scope.addTeacherQuestionSingle = function() {
			$scope.evaluation.TeacherQuestions.push({
				ID: $scope.evaluation.TeacherQuestions.length,
				TextIS: "",
				TextEN: "English Version is not supported",
				ImageURL: "",
				Type: "single",
				Answers: []
			});
		}

		$scope.addTeacherQuestionText = function() {
			$scope.evaluation.TeacherQuestions.push({
				ID: $scope.evaluation.TeacherQuestions.length,
				TextIS: "",
				TextEN: "English Version is not supported",
				ImageURL: "",
				Type: "text",
			});
		}

		$scope.addTeacherQuestionMultiple = function() {
			$scope.evaluation.TeacherQuestions.push({
				ID: $scope.evaluation.TeacherQuestions.length,
				TextIS: "",
				TextEN: "English Version is not supported",
				ImageURL: "",
				Type: "multiple",
				Answers: []
			});
		}
	}
]);