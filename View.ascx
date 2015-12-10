<%@ Control language="C#" Inherits="Dnn.Angular.Demo.View" AutoEventWireup="true" Codebehind="View.ascx.cs" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.Client.ClientResourceManagement" Assembly="DotNetNuke.Web.Client" %>

<!-- Should not use hardcoded paths here... -->
<dnn:DnnJsInclude runat="server" FilePath="~/desktopmodules/dnn.angular.demo/scripts/app/dnn.angular.demo.js"/> 

<div dnn-app="DnnAngularDemo" dnn-moduleId="<% =this.ModuleId %>" ng-cloak ng-controller="DemoController as vm">
    <div ng-include="vm.templateUrl"></div>
</div>