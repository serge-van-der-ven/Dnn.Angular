# Dnn.Angular.Demo
This is a simple example which demonstrates the combination of AngularJS and DNN (DotNetNuke).
The example focuses on AngularJS bootstrapping to easilly use AngularJS in your DNN modules and even allow multiple AngularJS modules on the samen page.

## Preparations
1. Create a regular DNN module using whichever templates you'd like to use.
1. Ensure that the following javascript files are loaded (in the given order):
* angular.min.js
* angular-resource.min.js
* angular-route.min.js
* dnn.angular.js

The file "dnn.angular.js" contains the actual AngularJS bootstrapping (based upon a concept of the folks of 2Sic Internet Solutions). The only thing you need to do is specify some attributes in your view of the DNN module and AngularJS is automatically attached.

## DNN module (view/edit/settings/whatever)
1. Create the wrapper for your Angular HTML templates. Notice that you don't need ng-include. You could also include the actual HTML within the wrapper itself.
1. The magic is in the "dnn-app" attribute. Here you link your AngularJS app/module (which obviously should have same name) to the wrapper (div).
1. You specify the DNN module-Id in the attribute dnn-moduleId and your done. No need to include your own jQuery ready() functions or other scripts to link JS and HTML. Its readly just as easy as in the example!
1. Finally you should include the javascript file(s) that contains your AngularJS app. Please refer to the example how this is done. In this case the AngularJS app contains the logic for the DemoController.

```'html
<div dnn-app="DnnAngularDemo" dnn-moduleId="<% =this.ModuleId %>" ng-cloak ng-controller="DemoController as vm">
    <div ng-include="vm.templateUrl"></div>
</div>
```

## Multiple modules
As you can see in the example, the DNN moduleId is never used within the AngularJS app (why should you), yet server side it is available in the ActiveModule property of the DnnApiController, just as you would expect.
Please note that the example itself is of no real use. It just demonstrates how AngularJS / HTML / DNN / WebAPI work realy nice together without to much coding.

## (DNN) Localization? Yes it does!
[new page](./Localization.md)

## Next Steps
I will most likely create a DNN JavaScript Library with the Angular 4 DNN bootstrapping so it can be easilly installed and referenced by developers. Maybe even a nuget package, who knows...

## Finally
I will add some more documentation later. Feel free to contact me for questions or remarks. We've already build many modules like this and it actually works awesome!

