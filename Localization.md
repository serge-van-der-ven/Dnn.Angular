## DNN Localization

When using ng-include form AngularJS, you basically load html into a placeholder in the webpage which is already shown in the browser on the client.
As you may or may not know, DNN relies on server controls (or user controls, which are in turn also server controls) to stream html to the browser and even the
new DNN8 SPA and MVC technology are effectively wrapped in server controls, so called host controls ().

The advantage of this technique is obviously that SPA and MVC modules can rely on the DNN framework support to access ModuleId, TabId, ModuleActions and so forth to seamlessly integrate
SPA and MVC modules into DNN. In the end, they're just server controls.

Plain html templates that are loaded on demand by ng-include don't have this advantage and don't even have any associated server side code.

So, how can we localize content in plain html templates or even get access to ModuleId, TabId and other DNN stuff?

The answer lies in the same bootstrapping as we did for the WebAPI! The DNN AngularJS bootstrap automatically attaches AngularJS to an Html element (usually a DIV) and extends the HttpProvider. 
The effect is that all HTTP requests are automatically stuffed with DNN specific headers, like ModuleId and TabId. This includes requests made by ng-include to get the html templates!

Now this is good news! Using a IHttpHandler to serve html templates we can intercept the requests made by ng-include, get the DNN specific HTTP headers from the request, which contain TabId, ModuleId etc., 
and use these Id's to provide DNN framework functionality to the html template.

This is done in a similar way that DNN8 uses for the SPA module; with the DNN TokenReplace engine! However, since we're not using the WebAPI we can not use the DNN8 helpers (which are build for HttpRequestMessage) and need to provide similar helpers for plain old HttpRequests.

## web.config

To make this thing work, you need to register the IHttpHandler in the web.config of your DNN site. Just register the handler for a specific path (like *.html, but you could use basically any extension) and
you're set to go!

## Examples and screenshots

Will provide examples and screenshots shortly!
