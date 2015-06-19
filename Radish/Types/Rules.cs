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
    /// Defines signature of replacement rule handler.
    /// </summary>
    /// <param name="context">Documentor context used while rendering documentation.</param>
    /// <param name="parameters">Range of parameters to be passed to this rule.</param>
    /// <returns>String content as a result of rule execution.</returns>
    public delegate string RuleDelegate(DocumentorContext context, params object[] parameters);

    /// <summary>
    /// Defines rule with parameters.
    /// </summary>
    internal class Rule
    {
        /// <summary>
        /// String name of the template.
        /// </summary>
        internal string TemplateName { get; set; }

        /// <summary>
        /// String tag for which the rule will be applied to.
        /// </summary>
        internal string Tag { get; set; }

        /// <summary>
        /// Rule with parameters.
        /// </summary>
        internal RuleDelegate RuleDelegate { get; set; }

        /// <summary>
        /// Creates new rule with parameters.
        /// </summary>
        /// <param name="templateName">String name of the template.</param>
        /// <param name="tag">String tag for which the rule will be applied to.</param>
        /// <param name="ruleDelegate">Rule delegate to be invoked to process the rule.</param>
        internal Rule(string templateName, string tag, RuleDelegate ruleDelegate)
        {
            this.TemplateName = templateName;
            this.Tag = tag;
            this.RuleDelegate = ruleDelegate;
        }

        /// <summary>
        /// Executes rule in the context and passes range of parameters to it.
        /// </summary>
        /// <param name="context">DocumentorContext used while rendering documentation.</param>
        /// <param name="parameters">Range of parameters to be passed to this rule.</param>
        /// <returns>String content as a result of rule execution.</returns>
        internal string Execute(DocumentorContext context, params object[] parameters)
        {
            return this.RuleDelegate.Invoke(context, parameters);
        }
    }
}
