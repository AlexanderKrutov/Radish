#region License
//   Copyright (C) 2015 Alexander Krutov
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//      http://www.apache.org/licenses/LICENSE-2.0
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.
#endregion License

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace Radish
{
    /// <summary>
    /// Contains information about currently building documentation.
    /// </summary>
    public class DocumentorContext
    {
        /// <summary>
        /// Current documentor
        /// </summary>
        private Documentor documentor;

        /// <summary>
        /// Creates new instance of DocumentorContext.
        /// </summary>
        /// <param name="documentor">Current documentor</param>
        internal DocumentorContext(Documentor documentor)
        {
            this.documentor = documentor;
        }

        /// <summary>
        /// Gets collection of REST methods found by documentor.
        /// </summary>
        public IEnumerable<MethodInfo> RestMethods 
        { 
            get { return documentor.RestMethods; }
        }

        /// <summary>
        /// Gets dictionary of predefined method groups.
        /// </summary>
        public Dictionary<string, MethodGroup> MethodGroups
        {
            get { return documentor.MethodGroups; } 
        }

        /// <summary>
        /// Renders template set of specified type with passing array of optional parameters to the template. 
        /// Parameters should be handled by replacement rules defined for the template.
        /// </summary>
        /// <typeparam name="T">Type of template set. Should be inherited from AbstractTemplateSet.</typeparam>
        /// <param name="template">Expression to define required template (string property) inside the template set.</param>
        /// <param name="parameters">Array of optional parameters to be passed to template and processed by rules.</param>
        /// <returns>Rendered template content as string.</returns>
        /// <example>
        /// This code renders "Title" template inside the template set of type "MyTemplateSet" with passing one string parameter:
        /// <code>
        /// context.RenderTemplate&lt;MyTemplateSet&gt;(ts => ts.Title, "test");
        /// </code>
        /// </example> 
        public string RenderTemplate<T>(Expression<Func<T, string>> template, params object[] parameters) where T : AbstractTemplateSet
        {
            return documentor.TemplateSet.RenderTemplate(this, template, parameters);
        }
    }
}
