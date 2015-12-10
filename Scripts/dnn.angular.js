var dnnAngular = {
    /* The attribute that should be assigned to an DNN Angular App */
    appAttribute: 'dnn-app',
    appModuleIdAttrNames: ['dnn-moduleid', 'data-moduleid', 'id'],

    /* If the page contains angular, setup the auto-bootstrap of all DNN Angular Apps */
    autoRunBootstrap: function autoRunBootstrap() {
        if (angular) {
            angular.element(document).ready(function() {
                dnnAngular.bootstrapAll();
            });
        }
    },

    /* 
        This function actually bootstraps an element (note: should be the element with the appAttribute, see above) with AngularJS.
        Normally you won't call this manually as it will be auto-bootstrapped.
        All params optional except for 'element'.
    */
    bootstrap: function(element, dnnAngularModuleName, moduleId, dependencies, config) {
        /* First, try to get moduleId */
        /* use fn-param, or get from DOM, or get url-param */
        moduleId = moduleId || dnnAngular.findModuleIdInDom(element) || dnnAngular.findModuleIdInUrl('mid');

        if (moduleId == null) {
            throw "The DNN moduleId not supplied and automatic lookup failed!";
        };

        /* Retrieve the DNN ServicesFrameWork for the moduleId */
        var sf = $.ServicesFramework(moduleId);

        // Create a micro AngularJS Module to configure the DNN specific parameters, add it to the dependencies. Note that the order is important!
        angular.module('configDnnApp' + moduleId, [])
            .constant('appInstanceId', moduleId)
            .constant('appServiceFramework', sf)
            .constant('dnnHttpHeaders', { "ModuleId": moduleId, "TabId": sf.getTabId(), "RequestVerificationToken": sf.getAntiForgeryValue() });
        var allDependencies = ['configDnnApp' + moduleId, 'angular4dnn'].concat(dependencies || [dnnAngularModuleName]);

        angular.element(document).ready(function() {
            try {
                angular.bootstrap(element, allDependencies, config); // start the app
            } catch (e) {
                /* Make sure that if one app breaks, others continue to work */
                if (console && console.error) {
                    console.error(e);
                }
            }
        });
    },

    /* Auto-bootstrap all sub-tags having a special attribute (for Multiple-Apps-per-Page) */
    /* The special attribute is configured as dnnAngular.appAttribute */
    bootstrapAll: function bootstrapAll(element) {
        element = element || document;
        var allAppTags = element.querySelectorAll('[' + dnnAngular.appAttribute + ']');

        angular.forEach(allAppTags, function(appTag) {
            var dnnAngularModuleName = appTag.getAttribute(dnnAngular.appAttribute);
            dnnAngular.bootstrap(appTag, dnnAngularModuleName, null, null, null);
        });
    },

    /* Find the ModuleId in the DOM */
    findModuleIdInDom: function findModuleIdInDom(element) {
        var attribute;
        var ngElement = angular.element(element);

        /* Loop through the list of available ModuleId attribute names */
        for (var i = 0; i < dnnAngular.appModuleIdAttrNames.length; i++) {
            /* Get the attribute from the element */
            attribute = ngElement.attr(dnnAngular.appModuleIdAttrNames[i]);
            if (attribute) {
                var moduleId = parseInt(attribute.toString().replace(/\D/g, ''));
                return moduleId;
            }
        }
        return null;
    },

    /* Find the ModuleId in the URL */
    findModuleIdInUrl: function findModuleIdInUrl(name) {
        name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
        var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
            results = regex.exec(location.search);

        /* if nothing found, try normal URL because DNN places parameters in /key/value notation */
        if (results === null) {
            /* Otherwise try parts of the URL */
            var matches = window.location.pathname.match("/" + name + "/([^/]+)", 'i');

            /* Check if we found anything, if we do find it, we must reverse the results so we get the "last" one in case there are multiple hits */
            if (matches !== null && matches.length > 1) {
                results = matches.reverse()[0];
            }
        } else {
            results = results[1];
        }

        return results === null ? null : decodeURIComponent(results.replace(/\+/g, " "));
    }
};

/* 
    MAIN ENTRY POINT: Trigger the AngularJS Bootstrapping
*/
dnnAngular.autoRunBootstrap();

/*
    The Angular Module 'angular4dnn' (re)configures AngularJS so it can be used with DNN. The module is automatically injected as dependency into all other Angular modules
    during the (auto-run) bootstrap process.
*/
var angular4dnn = angular.module('angular4dnn', []);

angular4dnn.config(function($httpProvider, dnnHttpHeaders) {
    angular.extend($httpProvider.defaults.headers.common, dnnHttpHeaders);
});