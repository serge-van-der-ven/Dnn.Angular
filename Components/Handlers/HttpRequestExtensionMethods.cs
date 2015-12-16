// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HttpRequestExtensionMethods.cs" company="XCESS expertise center bv">
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

using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Modules;

namespace Dnn.Angular.Demo.Components.Handlers
{
    /// <summary>
    /// HttpRequest extensions inline with the DNN8 StandardTabAndModuleInfoProvider. Should be integrated with DnnPlatform, just as all the classes in the DNN8 folder.
    /// </summary>
    public static class HttpRequestExtensionMethods
    {
        private const string ModuleIdKey = "ModuleId";
        private const string TabIdKey = "TabId";

        public static bool TryFindTabId(this HttpRequest request, out int tabId)
        {
            tabId = FindInt(request, TabIdKey);
            return tabId > Null.NullInteger;
        }

        public static bool TryFindModuleId(this HttpRequest request, out int moduleId)
        {
            moduleId = FindInt(request, ModuleIdKey);
            return moduleId > Null.NullInteger;
        }

        public static ModuleInfo FindModuleInfo(this HttpRequest request)
        {
            var moduleInfo = default(ModuleInfo);

            int moduleId;
            int tabId;
                
            // ReSharper disable once InvertIf
            if (TryFindTabId(request, out tabId) && TryFindModuleId(request, out moduleId))
            {
                var controller = new ModuleController();
                moduleInfo = controller.GetModule(moduleId, tabId, false);
            }

            return moduleInfo;
        }

        private static int FindInt(HttpRequest request, string key)
        {
            IEnumerable<string> values;
            string value = null;
            if (request.Headers.TryGetValues(key, out values))
            {
                value = values.FirstOrDefault();
            }

            if (string.IsNullOrWhiteSpace(value) && request.Url != null)
            {
                var queryString = HttpUtility.ParseQueryString(request.Url.Query);
                value = queryString[key];
            }

            int id;
            return int.TryParse(value, out id) ? id : Null.NullInteger;
        }

        private static bool TryGetValues(this NameValueCollection source, string key, out IEnumerable<string> values)
        {
            values = source.GetValues(key);
            return values != null;
        }
    }
}