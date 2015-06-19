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
    /// Defines a group of REST API methods inside HTML documentation.
    /// </summary>
    public class MethodGroup
    {
        /// <summary>
        /// Order of the group.
        /// Groups with lesser values will be placed first in the documentation.
        /// </summary>
        public uint GroupOrder { get; set; }

        /// <summary>
        /// Title of the group as it will be displayed in the documentation.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Description of the group as it will be displayed in the documentation.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Defines a group of REST API methods inside HTML documentation.
        /// </summary>
        /// <param name="groupOrder">
        /// Order of the group.
        /// Groups with lesser values will be placed first in the documentation.
        /// </param>
        /// <param name="title">Title of the group as it will be displayed in the documentation.</param>
        /// <param name="description">Description of the group as it will be displayed in the documentation.</param>
        internal MethodGroup (uint groupOrder, string title, string description)
        {
            this.GroupOrder = groupOrder;
            this.Title = title;
            this.Description = description;
        }
    }
}
