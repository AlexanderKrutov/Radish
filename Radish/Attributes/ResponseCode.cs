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
    /// Defines HTTP response code for REST API method and text description for this code.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public sealed class ResponseCodeAttribute : RadishAttribute
    {
        /// <summary>
        /// Integer value of HTTP status code (like 200, 201, 404, 500 and so on).
        /// </summary>
        public uint Code { private set; get; }

        /// <summary>
        /// Text description of the code.
        /// </summary>
        public string Description { private set; get; }

        /// <summary>
        /// Defines HTTP response code for REST API method and text description for this code.
        /// </summary>
        /// <param name="code">Integer value of HTTP status code (like 200, 201, 404, 500 and so on).</param>
        /// <param name="description">Text description of the code .</param>
        public ResponseCodeAttribute(uint code, string description)
        {
            this.Code = code;
            this.Description = description;
        }
    }
}
