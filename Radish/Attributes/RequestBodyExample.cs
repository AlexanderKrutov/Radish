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
    /// Defines example of request data for REST API method.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public sealed class RequestBodyExampleAttribute : RadishAttribute
    {
        /// <summary>
        /// Example of request data. Can be JSON, XML or raw string data.
        /// </summary>
        public string Data { private set; get; }

        /// <summary>
        /// Request data format. Can be JSON, XML or raw string data.
        /// </summary>
        public DataFormat Format { private set; get; }

        /// <summary>
        /// Defines example of request data for REST API method.
        /// Request data format will be detected automatically. Default data type is Raw.
        /// </summary>
        /// <param name="data">Example of request data. Can be JSON, XML or raw string data.</param>
        public RequestBodyExampleAttribute(string data) : this(data, DataFormat.Raw) { }

        /// <summary>
        /// Defines example of request data for REST API method, 
        /// with explicitly specifying data type.
        /// </summary>
        /// <param name="data">Example of request data.</param>
        /// <param name="format">Request data format (JSON, XML or raw string).</param>
        public RequestBodyExampleAttribute(string data, DataFormat format)
        {
            this.Format = (format == DataFormat.Raw) ? DataFormatUtils.DetectDataFormat(data) : format;
            this.Data = HttpUtility.JavaScriptStringEncode(data);
        }
    }
}
