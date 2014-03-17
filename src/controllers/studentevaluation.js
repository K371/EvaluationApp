app.controller("StudentEvaluationController", [
	"$scope", "ApiFactory", "$routeParams", "LogInFactory", "$http", "$location", "$timeout",
	function($scope, ApiFactory, $routeParams, LogInFactory, $http, $location, $timeout) {
		var evaluationID = $routeParams.evaluationID;
		var courseID = $routeParams.courseID;
		var token = LogInFactory.getToken();
		$scope.showIt = false;
		if(token === ""){
 			$location.path("/");
 		}

 		$scope.startDate = "";
 		$scope.startTime = "";
 		$scope.endDate = "";
 		$scope.endTime = "";

 		$scope.ans = [];

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
				//console.log(response);
				// success get templateid from evaluationid
				ApiFactory.getEvaluationTemplateById(response.data.TemplateID).then(function(template){
					$scope.evaluation = template.data;
					console.log(template.data.CourseQuestions.length);
				}, function(errorMessage){
					console.log("Error fetching evaluation: " + errorMessage);
				});
				// ----------------------------------------
			}, 

			function(errorMessage) {
				console.log("Error fetching evaluation: " + errorMessage);
			});

			ApiFactory.getTeachers(courseID).then(function(response) 
			{
				$scope.teachers = response.data;
				console.log(response.data);	
			}, 

			function(errorMessage) {
				console.log("Error fetching evaluation: " + errorMessage);
			});
		}
		else {
			$location.path('/');
		}

		$scope.submitEvaluation = function(){
			console.log($scope.ans);
			$scope.showIt = true;
			//ApiFactory.addEvaluation();
			//var answers = new Array();

			
			
			//ApiFactory.addEvaluation(submission);

			$timeout(function(){$location.path("/Student");},1500);
		}

		
	}
])