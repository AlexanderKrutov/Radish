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
using System.Linq;
using System.Reflection;
using System.Text;

namespace Radish
{
    /// <summary>
    /// This is the main class from Radish library.
    /// It's responsible for output configuration and creating documentation for your REST API.
    /// </summary>
    public class Documentor
    {
        #region Private and Internal Fields and Properties

        /// <summary>
        /// Instance of <see cref="DocumentorContext"/>.
        /// </summary>
        private DocumentorContext mContext = null;

        /// <summary>
        /// Cached documentation content.
        /// </summary>
        private string mCachedContent = null;

        /// <summary>
        /// Collection of methods to be included in the documentation.
        /// </summary>
        internal IEnumerable<MethodInfo> RestMethods { get; private set; }

        /// <summary>
        /// Dictionary of defined method groups.
        /// </summary>
        internal Dictionary<string, MethodGroup> MethodGroups = new Dictionary<string, MethodGroup>();

        /// <summary>
        /// Array of assemblies to look for REST API methods.
        /// </summary>
        internal Assembly[] RestApiAssemblies { get; private set; }

        #endregion Private and Internal Fields and Properties

        #region Public Properties

        /// <summary>
        /// Gets generated HTML documentation content. That's the main Radish method.
        /// </summary>
        public string Content
        {
            get
            {
                if (mCachedContent == null)
                {
                    mCachedContent = CreateDocumentation();
                }
                return mCachedContent;
            }
        }
 
        /// <summary>
        /// Template set used for rendering documentation content.
        /// </summary>
        public AbstractTemplateSet TemplateSet { get; set; }

        #endregion Public Properties

        /// <summary>
        /// Adds new group of REST API methods.
        /// </summary>
        /// <param name="key">Unique group key. All methods with this group key will be grouped together in one documentation section.</param>
        /// <param name="title">Group title. Will be displayed at the beginning of the documentation section.</param>
        public void AddMethodGroup(string key, string title) { }

        /// <summary>
        /// Adds new group of REST API methods.
        /// </summary>
        /// <param name="key">Unique group key. All methods with this group key will be grouped together in one documentation section.</param>
        /// <param name="title">Group title. Will be displayed at the beginning of the documentation section.</param>
        /// <param name="description">Group description. Will be placed after group title in the output.</param>        
        public void AddMethodGroup(string key, string title, string description) { }

        /// <summary>
        /// Adds new group of REST API methods.
        /// </summary>
        /// <param name="key">Unique group key. All methods with this group key will be grouped together in one documentation section.</param>
        /// <param name="title">Group title. Will be displayed at the beginning of the documentation section.</param>
        /// <param name="order">Order of the group. Group with smallest order value will be placed first in the documentation.</param>
        public void AddMethodGroup(string key, string title, uint order) { }

        /// <summary>
        /// Adds new group of REST API methods.
        /// </summary>
        /// <param name="key">Unique group key. All methods with this group key will be grouped together in one documentation section.</param>
        /// <param name="title">Group title. Will be displayed at the beginning of the documentation section.</param>
        /// <param name="description">Group description. Will be placed after group title in the output.</param>
        /// <param name="order">Order of the group. Group with smallest order value will be placed first in the documentation.</param>
        public void AddMethodGroup(string key, string title, string description, uint order)
        {
            if (MethodGroups.ContainsKey(key))
            {
                throw new ArgumentException(String.Format("Group with key \"{0}\" was already added.", key));
            }
            MethodGroups.Add(key, new MethodGroup(order, title, description));
        }

        /// <summary>
        /// Initializes new Documentor instance. 
        /// REST methods will be looked for in the process executable in the default application domain. 
        /// </summary>
        public Documentor() : this(Assembly.GetEntryAssembly()) { }

        /// <summary>
        /// Initializes new Documentor instance. 
        /// REST methods will be looked for in the specified assemblies.
        /// </summary>
        public Documentor(params Assembly[] assemblies)
        {
            // set assemblies to look for REST methods
            RestApiAssemblies = assemblies;

            // initiate context
            mContext = new DocumentorContext(this);

            // default template set
            TemplateSet = new SimpleTemplateSet();
        }

        /// <summary>
        /// Creates documentation content.
        /// </summary>
        /// <returns>
        /// HTML documentation content as string.
        /// </returns>
        private string CreateDocumentation()
        {
            if (TemplateSet == null)
            {
                TemplateSet = new SimpleTemplateSet();
            }

            // take all REST methods marked with [Method] attibute
            RestMethods =
                RestApiAssemblies
                .Distinct()
                .SelectMany(a => a.GetTypes())
                .SelectMany(c => c.GetMethods()
                .Where(m => m.IsDefined(typeof(MethodAttribute), false)));

            // methods count
            int methodsCount = RestMethods.Count();

            // order methods according to groups order and methods order in each group 
            RestMethods = RestMethods.OrderBy(m =>
            {
                // get method group info from attribute
                OrderAttribute attrGroup = m.GetRadishAttribute<OrderAttribute>();

                // if group is not defined for the method, assume the method has default order "0" 
                if (attrGroup == null)
                    return 0;

                // if group is not defined in groups list
                if (attrGroup.GroupKey != null && !MethodGroups.ContainsKey(attrGroup.GroupKey))
                {
                    string fullMethodName = string.Format("{0}.{1}()", m.ReflectedType.Name, m.Name);
                    throw new Exception(String.Format("Group with key \"{0}\" was specified for the method \"{1}\", but group with this key was not added to the Documentor. Use AddMethodGroup(\"{0}\", ...) to set it.", attrGroup.GroupKey, fullMethodName));
                }

                // if default group (group key is null or empty)
                if (String.IsNullOrEmpty(attrGroup.GroupKey))
                {
                    return attrGroup.MethodOrder;
                }
                // group was defined
                else
                {
                    return (int)(MethodGroups[attrGroup.GroupKey].GroupOrder * methodsCount) + attrGroup.MethodOrder;
                }
            });

            // initiate rendering documentation with Page template
            return TemplateSet.RenderTemplate<AbstractTemplateSet>(mContext, ts => ts.Page);
        }
    }
}
