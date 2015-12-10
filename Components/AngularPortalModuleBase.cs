// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AngularPortalModuleBase.cs" company="XCESS expertise center bv">
//   Copyright (c) 2015 XCESS expertise center bv, Serge van der Ven
// 
//   Permission is hereby granted, free of charge, to any person obtaining a copy
//   of this software and associated documentation files (the "Software"), to deal
//   in the Software without restriction, including without limitation the rights
//   to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//   copies of the Software, and to permit persons to whom the Software is
//   furnished to do so, subject to the following conditions:
// 
//   The above copyright notice and this permission notice shall be included in
//   all copies or substantial portions of the Software.
// 
//   THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//   IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//   FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.  IN NO EVENT SHALL THE
//   AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//   LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//   OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
//   THE SOFTWARE.
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