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

 		$scope.courseAns = [];
 		$scope.teacherAns = [];
 		
 		
 		

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
				for (var i in response.data){
						$scope.teacherAns.push(new Array());
					}
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
			//console.log($scope.courseAns);
			
			//console.log($scope.teacherText);
			var allAnswers = [];
		
			for (value in $scope.courseAns){
				 	

					allAnswers.push({
						QuestionID: value,
						TeacherSSN: "",
						Value: $scope.courseAns[value]
					});
					
			}
			
			for (teacher in $scope.teacherAns){
				var ssn = "";
				var qID = 0;
				var tempSSN = "";
				var tempQID = 0;
				 	for(value in $scope.teacherAns[teacher]){
				 
					 	var str = [];
					 	str = $scope.teacherAns[teacher][value].split('/');
					 	if(tempSSN == ""){
					 		tempSSN = str[2];
					 		ssn = tempSSN;
					 	}
					 	else{
					 		ssn = tempSSN;
					 	}
					 	//ÞESSI QID VERÐA SÖMU OG GÖMLU, VITLAUST
					 	
						 	if(tempQID === 0){
						 		tempQID = parseInt(str[1]);
						 		qID = tempQID;
						 	}
						 	else{
						 		qID = ++tempQID;
						 	}
					 	
					 	
					 	
					 	
						allAnswers.push({
							QuestionID: qID,
							TeacherSSN: ssn,
							Value: str[0]
						});
					}
			}

			console.log(allAnswers);
			$scope.showIt = true;
			//ApiFactory.addEvaluation();
			//var answers = new Array();

			
			
			//ApiFactory.addEvaluation(submission);

			$timeout(function(){$location.path("/Student");},1500);
		}

		
	}
])