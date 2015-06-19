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
    /// Defines order of the REST API method in the HTML documentation output.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public sealed class OrderAttribute : RadishAttribute
    {
        /// <summary>
        /// Unique key of the group
        /// </summary>
        public string GroupKey { private set; get; }

        /// <summary>
        /// Method's order in the group.
        /// Methods in the same group will be sorted by this value, from lowest to greatest.
        /// </summary>
        public uint MethodOrder { private set; get; }

        /// <summary>
        /// Assignes the REST API method to the group of methods, without specifying order in this group.
        /// </summary>
        /// <param name="groupKey">Group of methods. Other methods with the same groupKey will be grouped together in the documentation file.</param>
        public OrderAttribute(string groupKey) : this(groupKey, 0) { }

        /// <summary>
        /// Sets order of the the REST API method without assigning the method to any group.
        /// </summary>
        /// <param name="methodOrder">Order of the method. All other methods without group will be sorted by this value, from lowest to greatest.</param>
        public OrderAttribute(uint methodOrder) : this(null, methodOrder) { }

        /// <summary>
        /// Assignes the REST API method to the group of methods and specifies method's order inside the group.
        /// </summary>
        /// <param name="groupKey">Unique key of the group</param>
        /// <param name="methodOrder">Method's order inside the group. Methods in the same group will be sorted by this value, from lowest to greatest.</param>
        public OrderAttribute(string groupKey, uint methodOrder)
        {
            this.GroupKey = groupKey;
            this.MethodOrder = methodOrder;
        }
    }
}
