app.factory("ApiFactory", [
	"$q", "$timeout", "$http",
	function($q, $timeout, $http) {
		function createEvaluation(id, titleIS, titleEN, introIS, introEN) {
			return {
				ID: id,
				TitleIS: titleIS,
				TitleEN: titleEN,
				IntroTextIS: introIS,
				IntroTextEN: introEN,
				CourseQuestions: [],
				TeacherQuestions: []
			};
		}

		function createQuestion(id, textIS, textEN, imageUrl, type) {
			return {
				ID: id,
				TextIS: textIS,
				TextEN: textEN,
				ImageURL: imageUrl,
				Type: type,
				Answers: []
			}
		}

		function generateEvaluations() {
			var result = [];
			for(var i = 0; i < 8; ++i) {
				var number = i+1;
				var evaluation = createEvaluation(i, "Kennslumat " + number, "Evaluation " + number, "Derp", "Derp");
				for(var j = 0; j < 3; ++j) {
					var qNumber = j+1;
					var question = createQuestion(j, "Hvað er derp" + qNumber + "?", "What is derp " + qNumber + "?", "", "single");
					evaluation.CourseQuestions.push(question);
				}
				result.push(evaluation);
			}
			return result;
		}

		var evaluations = generateEvaluations();

		return {
			getEvaluationTemplates: function(){
				var templates = $http({	method: 'GET', 
				url: 'http://project3api.haukurhaf.net/api/v1/evaluationtemplates'
				
				}).
    			success(function(data, status, headers, config) {
      				return data;
    			}).
		    	error(function(data, status, headers, config) {
		      // called asynchronously if an error occurs
		      // or server returns response with an error status.
		    	});
    	return templates;
			},

			getAllEvaluations: function() {
				var deferred = $q.defer();

				deferred.resolve(evaluations);
			
				return deferred.promise;
			},
			getEvaluationById: function(id) {
				var deferred = $q.defer();

				if(evaluations[id]) {
					deferred.resolve(evaluations[id]);
				}
				else {
					deferred.reject("No evaluation with this id");
				}

				return deferred.promise;
			},
			addEvaluation: function(evaluation) {
				var deferred = $q.defer();

				

				return deferred.promise;
			}
		};

	},

]);