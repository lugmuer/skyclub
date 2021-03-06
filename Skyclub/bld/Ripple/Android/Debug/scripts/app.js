﻿// For an introduction to the Blank template, see the following documentation:
// http://go.microsoft.com/fwlink/?LinkID=397704
// To debug code on page load in Ripple or on Android devices/emulators: launch your app, set breakpoints, 
// and then run "window.location.reload()" in the JavaScript Console.


angular.module('application', ['ionic'])
.run(function ($ionicPlatform) {
    $ionicPlatform.ready(function () {
        // Hide the accessory bar by default (remove this to show the accessory bar above the keyboard
        // for form inputs
        if (window.cordova && window.cordova.plugins.Keyboard) {
            cordova.plugins.Keyboard.hideKeyboardAccessoryBar(true);
        }
        if (window.StatusBar) {
            // org.apache.cordova.statusbar required
            StatusBar.styleDefault();
        }
    });
})

.factory('FBService', function () {
    // Might use a resource here that returns a JSON array

    function newLogin() {
        facebookConnectPlugin.login(function (response) {
            console.log(response);
            if (response.status === 'connected') {
                // Logged into your app and Facebook.
                return response.authResponse.accessToken;
            } else {
                // The person is logged into Facebook, but not your app.
                return '';
            }
        });
    }



    function LoginIfNecessary() {
        facebookConnectPlugin.getLoginStatus(
            function (response)
            {
                if (respons.status != 'connected')
                { newLogin(); }
            },
            function (error)
            { alert("" + error) });
    }


    return {
        getAccessToken: function () {
            LoginIfNecessary();
            return facebookConnectPlugin.getAccessToken(function (token) {

                return token;
            }, function (err) {
                return err;
            });
        }
    }
})


.factory('Mobile', ['FBService', function (FBService) {
    // Might use a resource here that returns a JSON array




    return {

        login: function () {
            var token = FBService.getAccessToken();
            var client = new WindowsAzure.MobileServiceClient("https://skyclub.azure-mobile.net/",  "pUKivIEHKGwYdMhDByZvyIgyGnXEPF23");

            var promise = client.login("facebook", FBService.getAccessToken(), true).done(function (results) {
                var userId = results.userId;
            });
        }
    }
}])



.controller('RootCtrl', ['$scope', 'Mobile', function ($scope, Mobile) {
    $scope.onControllerChanged = function (oldController, oldIndex, newController, newIndex) {
        console.log('Controller changed', oldController, oldIndex, newController, newIndex);
        console.log(arguments);
    };
    Mobile.login();
}])


.controller('HomeCtrl', function ($scope, $timeout, $ionicModal, $ionicActionSheet) {
    $scope.items = [];

    $ionicModal.fromTemplateUrl('newTask.html', function (modal) {
        $scope.settingsModal = modal;
    });

    var removeItem = function (item, button) {
        $ionicActionSheet.show({
            buttons: [],
            destructiveText: 'Delete Task',
            cancelText: 'Cancel',
            cancel: function () {
                return true;
            },
            destructiveButtonClicked: function () {
                $scope.items.splice($scope.items.indexOf(item), 1);
                return true;
            }
        });
    };

    var completeItem = function (item, button) {
        item.isCompleted = true;
    };

    $scope.onReorder = function (el, start, end) {
        ionic.Utils.arrayMove($scope.items, start, end);
    };

    $scope.onRefresh = function () {
        console.log('ON REFRESH');

        $timeout(function () {
            $scope.$broadcast('scroll.refreshComplete');
        }, 1000);
    }


    $scope.removeItem = function (item) {
        removeItem(item);
    };

    $scope.newTask = function () {
        $scope.settingsModal.show();
    };

    // Create the items
    for (var i = 0; i < 25; i++) {
        $scope.items.push({
            title: 'Task ' + (i + 1),
            buttons: [{
                text: 'Done',
                type: 'button-success',
                onButtonClicked: completeItem,
            }, {
                text: 'Delete',
                type: 'button-danger',
                onButtonClicked: removeItem,
            }]
        });
    }

})

    .controller('ChatCtrl', function ($scope) {
        $scope.items = [];
        

    })

.controller('TaskCtrl', function ($scope) {
    $scope.close = function () {
        $scope.modal.hide();
    }
});
