# Dnn.Angular.Demo
This is a simple example which demonstrates the combination of AngularJS and DNN (DotNetNuke).
The example focuses on AngularJS bootstrapping to easilly use AngularJS in your DNN modules and even allow multiple AngularJS modules on the samen page.

## Preparations
1. Create a regular DNN module using whichever templates you'd like to use.
1. Ensure that the following javascript files are loaded (in the given order):
..* angular.min.js
..* angular-resource.min.js
..* angular-route.min.js
..* dnn.angular.js

The file "dnn.angular.js" contains the actual AngularJS bootstrapping (based upon a concept of the folks of 2Sic Internet Solutions). The only thing you need to do is specify some attributes in your view of the DNN module and AngularJS is automatically attached.

## DNN Module (view)
1. Create the wrapper for your Angular HTML templates. Notice that you don't need ng-include. You could also include the actual HTML within the wrapper itself.
1. Also include the javascript file that contains your AngularJS app. Please see the example how this is done.

```'html
<div dnn-app="DnnAngularDemo" dnn-moduleId="<% =this.ModuleId %>" ng-cloak ng-controller="DemoController as vm">
    <div ng-include="vm.templateUrl"></div>
</div>
```

## Multiple modules
As you can see in the example, the DNN moduleId is never used within the AngularJS app (why should you), yet server side it is available in the ActiveModule property of the DnnApiController, just as you would expect.

## Finally
Will add some more documentation later. Feel free to contact me for questions.