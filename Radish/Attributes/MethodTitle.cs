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
using System.Web;

namespace Radish
{
    /// <summary>
    /// Defines REST API method title in the HTML documentation output.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public sealed class MethodTitleAttribute : RadishAttribute
    {
        /// <summary>
        /// Title of the REST API method.
        /// </summary>
        public string Title { private set; get; }

        /// <summary>
        /// Defines REST API method title in the HTML documentation output.
        /// </summary>
        /// <param name="title">Title of the REST API method.</param>
        public MethodTitleAttribute(string title)
        {
            this.Title = HttpUtility.HtmlEncode(title);
        }
    }
}
