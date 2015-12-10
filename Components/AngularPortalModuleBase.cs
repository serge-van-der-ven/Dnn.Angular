// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AngularPortalModuleBase.cs" company="XCESS expertise center bv">
//   Copyright (c) 2015 XCESS expertise center bv
//   
//   The software is owned by XCESS and is protected by 
//   the Dutch copyright laws and international treaty provisions.
//   You are allowed to make copies of the software solely for backup or archival purposes.
//   You may not lease, rent, export or sublicense the software.
//   You may not reverse engineer, decompile, disassemble or create derivative works from the software.
//   
//   Owned by XCESS expertise center b.v., Storkstraat 19, 3833 LB Leusden, The Netherlands
//   T. +31-33-4335151, E. info@xcess.nl, I. http://www.xcess.nl
// </copyright>
// <summary>
//   
// </summary>
//  --------------------------------------------------------------------------------------------------------------------

using System;
using System.IO;
using DotNetNuke.Common;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Framework.JavaScriptLibraries;
using DotNetNuke.Web.Client;
using DotNetNuke.Web.Client.ClientResourceManagement;

namespace Dnn.Angular.Demo.Components
{
    public abstract class AngularPortalModuleBase : PortalModuleBase
    {
        #region Overrides of UserControl

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Init"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> object that contains the event data. </param>
        protected override void OnInit(EventArgs e)
        {
            JavaScript.RequestRegistration(CommonJs.DnnPlugins);

            var desktopModuleFolder = this.ModuleConfiguration.DesktopModule.FolderName;
            var scriptFolder = Path.Combine(Globals.DesktopModulePath, desktopModuleFolder, "Scripts");

            ClientResourceManager.RegisterScript(this.Page, scriptFolder + "/angular.min.js", FileOrder.Js.jQuery);
            ClientResourceManager.RegisterScript(this.Page, scriptFolder + "/angular-route.min.js", FileOrder.Js.jQuery);
            ClientResourceManager.RegisterScript(this.Page, scriptFolder + "/angular-resource.min.js", FileOrder.Js.jQuery);
            ClientResourceManager.RegisterScript(this.Page, scriptFolder + "/dnn.angular.js", FileOrder.Js.DefaultPriority);

            base.OnInit(e);
        }

        #endregion
    }
}