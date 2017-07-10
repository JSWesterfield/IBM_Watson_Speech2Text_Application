var App = {
    utilities: {}
    , layout: {}
    , page: {
        handlers: {
        }
        , startUp: null
    }
    , services: {}
    , ui: {}
};

App.moduleOptions = {
    APPNAME: "myApp"
    , extraModuleDependencies: []
    , runners: []
    , page: App.page//we need this object here for later use
};

App.layout.startUp = function () {

    console.debug("App.layout.startUp");

    //this does a null check on sabio.page.startUp
    if (App.page.startUp) {
        console.debug("App.page.startUp");
        App.page.startUp();
    }
};

App.APPNAME = "myApp";//legacy
$(document).ready(App.layout.startUp);