cordova.define('cordova/plugin_list', function(require, exports, module) {
module.exports = [
    {
        "file": "plugins/com.ionic.keyboard/www/keyboard.js",
        "id": "com.ionic.keyboard.keyboard",
        "clobbers": [
            "cordova.plugins.Keyboard"
        ]
    },
    {
        "file": "plugins/com.msopentech.azure-mobile-services/www/MobileServices.Web.Ext.js",
        "id": "com.msopentech.azure-mobile-services.AzureMobileServices.Ext",
        "runs": true
    },
    {
        "file": "plugins/com.msopentech.azure-mobile-services/www/MobileServices.Web-1.2.5.js",
        "id": "com.msopentech.azure-mobile-services.AzureMobileServices",
        "runs": true
    },
    {
        "file": "plugins/com.phonegap.plugins.facebookconnect/www/phonegap/plugin/facebookConnectPlugin/facebookConnectPlugin.js",
        "id": "com.phonegap.plugins.facebookconnect.FacebookConnectPlugin",
        "clobbers": [
            "facebookConnectPlugin"
        ]
    },
    {
        "file": "plugins/org.apache.cordova.statusbar/www/statusbar.js",
        "id": "org.apache.cordova.statusbar.statusbar",
        "clobbers": [
            "window.StatusBar"
        ]
    },
    {
        "file": "plugins/org.apache.cordova.inappbrowser/www/inappbrowser.js",
        "id": "org.apache.cordova.inappbrowser.inappbrowser",
        "clobbers": [
            "window.open"
        ]
    }
];
module.exports.metadata = 
// TOP OF METADATA
{
    "com.ionic.keyboard": "1.0.3",
    "com.msopentech.azure-mobile-services": "0.1.6",
    "com.phonegap.plugins.facebookconnect": "0.9.0",
    "org.apache.cordova.statusbar": "0.1.6",
    "org.apache.cordova.inappbrowser": "0.5.3"
}
// BOTTOM OF METADATA
});