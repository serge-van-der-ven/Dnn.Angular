// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DnnHtmlHandler.cs" company="XCESS expertise center bv">
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
using System.Globalization;
using System.IO;
using System.Text;
using System.Web;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Portals;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Services.Localization;

namespace Dnn.Angular.Demo.Components.Handlers
{
    /// <summary>
    /// The dnn.angular.js AngularJS bootstrapping ensures that the DNN WebAPI request headers are attached to each HTTP(s) request send to the server. This IHttpHandler also makes
    /// use of that to determine the (tab)module that requested the specific html.
    /// </summary>
    public class DnnHtmlHandler : IHttpHandler
    {
        private ModuleInfo GetActiveModule(HttpContext context)
        {
            // TODO: Should refactor to the standaard DNN StandardTabAndModuleInfoProvider, using the HttpConfiguration. However this requires a HttpRequestMessage and conversion of the HttpRequest(Base) to HttpRequestMessage
            var request = context.Request;
            return request.FindModuleInfo();
        }

        private void SetCulture(HttpContext context)
        {
            var portalSettings = PortalSettings.Current;
            var request = context.Request;
            var cultureCode = portalSettings.DefaultLanguage;

            if (request.QueryString["language"] != null)
            {
                cultureCode = request.QueryString["language"];
            }
            else if (request.Cookies["language"] != null)
            {
                cultureCode = request.Cookies["language"].Value;
            }

            // ReSharper disable once InvertIf
            if (LocaleController.Instance.IsEnabled(ref cultureCode, portalSettings.PortalId))
            {
                Localization.SetThreadCultures(new CultureInfo(cultureCode), portalSettings);

                // Set the language cookie
                Localization.SetLanguage(cultureCode);
            }
        }

        #region Implementation of IHttpHandler

        /// <summary>
        /// Enables processing of HTTP Web requests by a custom HttpHandler that implements the <see cref="T:System.Web.IHttpHandler"/> interface.
        /// </summary>
        /// <param name="context">An <see cref="T:System.Web.HttpContext"/> object that provides references to the intrinsic server objects (for example, Request, Response, Session, and Server) used to service HTTP requests. </param>
        public void ProcessRequest(HttpContext context)
        {
            var activeModule = default(ModuleInfo);
            var htmlTemplateFile = string.Empty;

            try
            {
                activeModule = this.GetActiveModule(context);
                htmlTemplateFile = context.Request.PhysicalPath;
            }
            catch
            {
                Exceptions.ProcessHttpException(context.Request);
            }

            this.SetCulture(context);

            var content = FileSystemUtils.ReadFile(htmlTemplateFile);
            var tokenReplace = new DotNetNuke.UI.Modules.HtmlTemplate.HtmTemplateTokenReplace(activeModule, htmlTemplateFile);

            var response = context.Response;
            response.Clear();
            response.ContentType = "text/html";
            response.ContentEncoding = Encoding.UTF8;
            response.Write(tokenReplace.ReplaceEnvironmentTokens(content));
        }

        /// <summary>
        /// Gets a value indicating whether another request can use the <see cref="T:System.Web.IHttpHandler"/> instance.
        /// </summary>
        /// <returns>
        /// true if the <see cref="T:System.Web.IHttpHandler"/> instance is reusable; otherwise, false.
        /// </returns>
        public bool IsReusable => true;

        #endregion
    }
}