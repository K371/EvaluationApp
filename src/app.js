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
	$routeProvider.when("/home", {
		templateUrl: "templates/home.html",
		controller: "HomeController",
	}).when("/admin", {
		templateUrl: "templates/adminhome.html",
		controller: "AdminHomeController"
	}).when("/evaluation/:evaluationID", {
		templateUrl: "templates/evaluation.html",
		controller: "EvaluationController"
	}).when("/evaluation/", {
		templateUrl: "templates/evaluation.html",
		controller: "EvaluationController"
	}).when("/", {
		templateUrl: "templates/login.html",
		controller: "LogInController",
	}).otherwise({ redirectTo: "/"});
});