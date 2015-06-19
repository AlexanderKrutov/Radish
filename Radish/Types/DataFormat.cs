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

namespace Radish
{
    /// <summary>
    /// Data format to interact with REST API
    /// </summary>
    public enum DataFormat
    {
        /// <summary>
        /// Data format is JSON
        /// </summary>
        Json,

        /// <summary>
        /// Data format is XML
        /// </summary>
        Xml,

        /// <summary>
        /// Data format is raw. 
        /// Will not be highlighted and formatted in the documentation output.
        /// </summary>
        Raw
    }

    internal static class DataFormatUtils
    {
        internal static DataFormat DetectDataFormat(string data)
        {
            data = data.Trim();
            if ((data.StartsWith("{") && data.EndsWith("}")) || (data.StartsWith("[") && data.EndsWith("]")))
                return DataFormat.Json;
            else if (data.StartsWith("<") && data.EndsWith(">"))
                return DataFormat.Xml;
            else
                return DataFormat.Raw;
        }
    }
}
