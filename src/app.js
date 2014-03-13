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
		templateUrl: "templates/home.html",
		controller: "HomeController",
	}).when("/admin", {
		templateUrl: "templates/adminhome.html",
		controller: "AdminHomeController"
	}).when("/Admin/Evaluation/:evaluationID", {
		templateUrl: "templates/evaluation.html",
		controller: "EvaluationController"
	}).when("/evaluation/", {
		templateUrl: "templates/evaluation.html",
		controller: "EvaluationController" 
	}).when("/Student/Evaluation/:evaluationID", {
		templateUrl: "templates/stevaluation.html",
		controller: "EvaluationController" /* temp using same controller */
	}).when("/", {
		templateUrl: "templates/login.html",
		controller: "LogInController",
	}).otherwise({ redirectTo: "/"});
});