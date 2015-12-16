// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ModuleLocalizationPropertyAccess.cs" company="XCESS expertise center bv">
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

using System.IO;
using DotNetNuke.Entities.Users;
using DotNetNuke.Services.Localization;
using DotNetNuke.Services.Tokens;
using Newtonsoft.Json;

// ReSharper disable once CheckNamespace
namespace DotNetNuke.UI.Modules.HtmlTemplate
{
    public class ModuleLocalizationDto
    {
        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("localresourcefile")]
        public string LocalResourceFile { get; set; }
    }

    public class ModuleLocalizationPropertyAccess : JsonPropertyAccess<ModuleLocalizationDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ModuleLocalizationPropertyAccess" /> class.
        /// </summary>
        /// <param name="htmlTemplateFile">The HTML template file.</param>
        public ModuleLocalizationPropertyAccess(string htmlTemplateFile)
        {
            this.HtmlTemplateFile = htmlTemplateFile;
        }

        private string HtmlTemplateFile { get; }

        protected override string ProcessToken(ModuleLocalizationDto model, UserInfo accessingUser, Scope accessLevel)
        {
            return string.IsNullOrEmpty(model.Key)
                       ? string.Empty
                       : Localization.GetString(model.Key, string.IsNullOrWhiteSpace(model.LocalResourceFile) ? this.GetResourceFile() : model.LocalResourceFile);
        }

        private string GetResourceFile()
        {
            var fileName = Path.GetFileName(this.HtmlTemplateFile);
            var path = this.HtmlTemplateFile.Replace(fileName, string.Empty);
            return Path.Combine(path, Localization.LocalResourceDirectory + "/", Path.ChangeExtension(fileName, "resx"));
        }
    }
}