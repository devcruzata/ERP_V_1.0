var app = angular.module("app", ["xeditable"]);

app.run(function(editableOptions) {
  editableOptions.theme = 'bs3'; // bootstrap3 theme. Can be also 'bs2', 'default'
});

app.controller('Ctrl', function($scope) {
  $scope.user = {
    name: 'Kashan',
	cname: 'Onlyswim',
	email: 'kashan@gmail.com',
	mobileno: 8798798799,
	skypeno: '',
	source: 'Referral',
	desc: 'Needs Redesigning of his website and probably Ecommerce website'
	
	
  };  
});