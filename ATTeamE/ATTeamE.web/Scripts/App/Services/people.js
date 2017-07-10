(function () {

    'use strict';

    angular.module('myApp')
        .factory('peopleService', peopleServiceFactory);

    function peopleServiceFactory() {

        var aPeopleServiceObject = App.services.people;
        console.log('People Service created successfully');
        return aPeopleServiceObject;

    };

})();