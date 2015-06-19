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

namespace Radish
{
    /// <summary>
    /// Defines parameter to be passed to REST API method.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public sealed class RequestParameterAttribute : RadishAttribute
    {
        /// <summary>
        /// Name of the parameter.
        /// </summary>
        public string Name { private set; get; }

        /// <summary>
        /// Description of the parameter.
        /// </summary>
        public string Description { private set; get; }

        /// <summary>
        /// Type of the parameter (for example, "String" or "Integer", or any other type). 
        /// </summary>
        public string Type { private set; get; }

        /// <summary>
        /// Indicates whether the parameter is required or it is optional.
        /// </summary>
        public bool Required { private set; get; }

        /// <summary>
        /// Defines order of the parameter in the list of parameters for the REST API method.
        /// Parameters with smaller order value will be placed first in the documentation output.
        /// </summary>
        public uint Order { private set; get; }

        /// <summary>
        /// Defines parameter to be passed to REST API method.
        /// </summary>
        /// <param name="name">Name of the parameter.</param>
        /// <param name="type">Type of the parameter (for example, "String" or "Integer", or any other type).</param>
        /// <param name="description">Description of the parameter.</param>
        public RequestParameterAttribute(string name, string type, string description) : this(name, type, description, true, 0) { }

        /// <summary>
        /// Defines parameter to be passed to REST API method.
        /// </summary>
        /// <param name="name">Name of the parameter.</param>
        /// <param name="type">Type of the parameter (for example, "String" or "Integer", or any other type).</param>
        /// <param name="description">Description of the parameter.</param>
        /// <param name="required">Indicates whether the parameter is required or it is optional.</param>
        public RequestParameterAttribute(string name, string type, string description, bool required) : this(name, type, description, required, 0) { }

        /// <summary>
        /// Defines parameter to be passed to REST API method.
        /// </summary>
        /// <param name="name">Name of the parameter.</param>
        /// <param name="type">Type of the parameter (for example, "String" or "Integer", or any other type).</param>
        /// <param name="description">Description of the parameter.</param>
        /// <param name="order">
        /// Defines order of the parameter in the list of parameters for the REST API method.
        /// Parameters with smaller order value will be placed first in the documentation output.
        /// </param>
        public RequestParameterAttribute(string name, string type, string description, uint order) : this(name, type, description, true, order) { }

        /// <summary>
        /// Defines parameter to be passed to REST API method.
        /// </summary>
        /// <param name="name">Name of the parameter.</param>
        /// <param name="type">Type of the parameter (for example, "String" or "Integer", or any other type).</param>
        /// <param name="description">Description of the parameter.</param>
        /// <param name="required">Indicates whether the parameter is required or it is optional.</param>
        /// <param name="order">
        /// Defines order of the parameter in the list of parameters for the REST API method.
        /// Parameters with smaller order value will be placed first in the documentation output.
        /// </param>
        public RequestParameterAttribute(string name, string type, string description, bool required, uint order)
        {
            this.Name = name;
            this.Type = type;
            this.Description = description;
            this.Required = required;
            this.Order = order;
        }    
    }
}
