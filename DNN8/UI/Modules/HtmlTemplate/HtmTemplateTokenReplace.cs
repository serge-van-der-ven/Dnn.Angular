// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HtmTemplateTokenReplace.cs" company="XCESS expertise center bv">
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

using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Tokens;

// ReSharper disable once CheckNamespace
namespace DotNetNuke.UI.Modules.HtmlTemplate
{
    public class HtmTemplateTokenReplace : TokenReplace
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HtmTemplateTokenReplace" /> class.
        /// </summary>
        /// <param name="module">The module.</param>
        /// <param name="htmlTemplateFile">The HTML template file.</param>
        public HtmTemplateTokenReplace(ModuleInfo module, string htmlTemplateFile)
        {
            this.PropertySource["module"] = new ModulePropertyAccess(module);
            this.PropertySource["resx"] = new ModuleLocalizationPropertyAccess(htmlTemplateFile);
        }
    }
}