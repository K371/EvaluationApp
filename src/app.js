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
		controller: "HomeController"
	}).when("/Admin/Evaluation/:evaluationID", {
		templateUrl: "templates/adminevaluation.html", 		/* admin evaluation page */
		controller: "EvaluationController"
	}).when("/Admin/Create", {
		templateUrl: "templates/create.html", 		/* admin evaluation page */
		controller: "CreateController"
	}).when("/evaluation/", {
		templateUrl: "templates/evaluation.html",	 		/* does nothing atm */
		controller: "EvaluationController" 
	}).when("/Student/Evaluation/:evaluationID", {			/* student evaluation page */
		templateUrl: "templates/studentevaluation.html",
		controller: "EvaluationController" 				/* temp using same controller */
	}).when("/", {											/* if nothing fits */
		templateUrl: "templates/login.html",
		controller: "LogInController",
	}).otherwise({ redirectTo: "/"});
});