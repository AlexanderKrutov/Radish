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

namespace Radish
{
    /// <summary>
    /// Base class for all Radish attributes.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public abstract class RadishAttribute : Attribute { }

    /// <summary>
    /// Contains extension methods to access Radish attributes.
    /// </summary>
    public static class AttributeExtensions
    {
        /// <summary>
        /// Gets all Radish attributes of specified type.
        /// </summary>
        /// <typeparam name="T">Type of the Radish attribute.</typeparam>
        /// <param name="methodInfo">MethodInfo object to get Radish attributes.</param>
        /// <returns>IEnumerable collection of Radish attributes of specified type, or empty collection if attributes of specified type were not found.</returns>
        public static IEnumerable<T> GetRadishAttributes<T>(this MethodInfo methodInfo) where T : RadishAttribute
        {
            return methodInfo.GetCustomAttributes(typeof(T), false).OfType<T>();
        }

        /// <summary>
        /// Gets Radish attribute of specified type.
        /// </summary>
        /// <typeparam name="T">Type of the Radish attribute.</typeparam>
        /// <param name="methodInfo">MethodInfo object to get Radish attribute.</param>
        /// <returns>First found Radish attribute of specified type, or null if attribute of specified type was not found.</returns>
        public static T GetRadishAttribute<T>(this MethodInfo methodInfo) where T : RadishAttribute 
        {
            return methodInfo.GetRadishAttributes<T>().FirstOrDefault() as T;
        }
    }
}
