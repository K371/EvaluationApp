var app = angular.module("EvaluationApp", ["ngRoute"]);

app.directive('ngEnter', function () {
    return function (scope, element, attrs) {
        element.bind("keydown keypress", function (event) {
            if(event.which === 13) {
                scope.$apply(function (){
                    scope.$eval(attrs.ngEnter);
                });
            }
        });
    };
});

app.config(function($routeProvider) {
	$routeProvider.when("/Student", {
		templateUrl: "templates/studenthome.html", 			/* student home */
		controller: "HomeController",
	}).when("/admin", {
		templateUrl: "templates/adminhome.html",			/* admin home */
		controller: "AdminHomeController"
	}).when("/Admin/Evaluation/:evaluationID", {
		templateUrl: "templates/adminevaluation.html", 		/* admin evaluation page */
		controller: "StudentEvaluationController"
	}).when("/Admin/Create", {
		templateUrl: "templates/create.html", 		/* admin evaluation page */
		controller: "CreateController"
	}).when("/Student/Evaluation/:evaluationID", {			/* student evaluation page */
		templateUrl: "templates/studentevaluation.html",
		controller: "StudentEvaluationController" 				/* temp using same controller */
	}).when("/Admin/EvaluationTemplate/:evaluationID", {			/* student evaluation page */
		templateUrl: "templates/adminevaluation.html",
		controller: "EvaluationController" 				/* temp using same controller */
	}).when("/", {											/* if nothing fits */
		templateUrl: "templates/login.html",
		controller: "LogInController",
	}).when("/Courses", {											/* if nothing fits */
		templateUrl: "templates/courses.html",
		controller: "CourseController",
	}).when("/Courses/:courseID", {											/* if nothing fits */
		templateUrl: "templates/courseevaluations.html",
		controller: "CourseEvaluationController",
	}).otherwise({ redirectTo: "/"});
});