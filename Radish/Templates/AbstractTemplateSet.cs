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
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Radish
{
    /// <summary>
    /// This is a base class for all template sets for Radish.
    /// It does not contain templating logic (rules), and has only one abstract template called "Page".
    /// Use this class as a base if you want to customize all templating logic for your documentation and define your own rules set.
    /// If you want to leave templating logic unhanged, and only want to customize look and feel of your documentation, 
    /// take a look on <see cref="BasicTemplateSet"/>.
    /// If you happy with standard template set, see <see cref="SimpleTemplateSet"/>.
    /// </summary>
    public abstract class AbstractTemplateSet
    {
        /// <summary>
        /// List of replacement rules for all templates inside the set.
        /// </summary>
        internal List<Rule> ReplacementRules { get; set; }

        /// <summary>
        /// Template for whole documentation page. Override this with custom content in child class.
        /// </summary>
        public abstract string Page { get; set; }

        /// <summary>
        /// Creates new instance of <see cref="AbstractTemplateSet"/>
        /// </summary>
        public AbstractTemplateSet()
        {
            ReplacementRules = new List<Rule>();
        }

        #region Internal Methods

        /// <summary>
        /// Gets template name by template expression.
        /// </summary>
        /// <typeparam name="T">Template set class. Should be inherited from <see cref="AbstractTemplateSet"/>.</typeparam>
        /// <param name="template">Expression defining template inside the template set. For example: ts => ts.Page.</param>
        /// <returns>String name of the template, i. e. the name of property defined by the expression.</returns>
        internal string GetTemplateName<T>(Expression<Func<T, string>> template) where T : AbstractTemplateSet
        {
            var body = template.Body;
            var convertExpression = body as UnaryExpression;
            if (convertExpression != null)
            {
                if (convertExpression.NodeType != ExpressionType.Convert)
                {
                    throw new ArgumentException("Invalid property expression.", "exp");
                }
                body = convertExpression.Operand;
            }
            return ((MemberExpression)body).Member.Name;
        }

        /// <summary>
        /// Gets template content by template name.
        /// </summary>
        /// <param name="templateName">String name of the template.</param>
        /// <returns>Template content as string</returns>
        internal string GetTemplateContent(string templateName) 
        {
            return this.GetType().GetProperty(templateName).GetValue(this, null) as string;
        }

        /// <summary>
        /// Renders specified template.
        /// Rendering means recursively applying all replacement rules defined for the template and nested templates.
        /// </summary>
        /// <typeparam name="T">Template set class. Should be inherited from <see cref="AbstractTemplateSet"/>.</typeparam>
        /// <param name="context">Documentor context</param>
        /// <param name="template">Expression defining template inside the template set. For example: ts => ts.Page.</param>
        /// <param name="parameters">Optional range of parameters to be passed into rules.</param>
        /// <returns>Template content as string, where tags replaced with output values according to defined rules.</returns>
        internal string RenderTemplate<T>(DocumentorContext context, Expression<Func<T, string>> template, params object[] parameters) where T : AbstractTemplateSet
        {
            string templateName = GetTemplateName(template);
            StringBuilder contentBuilder = new StringBuilder(GetTemplateContent(templateName));
            IEnumerable<Rule> rulesToExecute = ReplacementRules.Where(r => r.TemplateName == templateName);
            foreach (Rule rule in rulesToExecute)
            {
                contentBuilder.Replace(String.Format("{{{{{0}}}}}", rule.Tag), rule.Execute(context, parameters));
            }
            return contentBuilder.ToString();
        }

        #endregion Internal Methods
    }

    /// <summary>
    /// Syntax sugar methods to work with template sets.
    /// </summary>
    public static class TemplateSetExtensions
    {
        /// <summary>
        /// Adds new replacement rule to the template set.
        /// </summary>
        /// <param name="templateSet">Template set</param>
        /// <typeparam name="T">Template set class. Should be inherited from <see cref="AbstractTemplateSet"/>.</typeparam>
        /// <param name="template">Expression that defines template inside the template set. For example: ts => ts.Page.</param>
        /// <param name="tag">String tag for which the rule will be applied to.</param>
        /// <param name="rule">Rule that will be executed on specified template. All occurencies of tag will be replaced by returning value of the rule.</param>
        public static void AddRule<T>(this T templateSet, Expression<Func<T, string>> template, string tag, RuleDelegate rule) where T : AbstractTemplateSet
        {
            templateSet.ReplacementRules.Add(new Rule(templateSet.GetTemplateName(template), tag, rule));
        }

        /// <summary>
        /// Removes all replacement rules associated with a tag in particular template. 
        /// </summary>
        /// <param name="templateSet">Template set</param>
        /// <typeparam name="T">Template set class. Should be inherited from <see cref="AbstractTemplateSet"/>.</typeparam>
        /// <param name="template">Expression that defines template inside the template set. For example: ts => ts.Page.</param>
        /// <param name="tag">String tag inside the template.</param>
        public static void RemoveRulesForTag<T>(this T templateSet, Expression<Func<T, string>> template, string tag) where T : AbstractTemplateSet
        {
            string templateName = templateSet.GetTemplateName(template);
            templateSet.ReplacementRules.RemoveAll(r => r.TemplateName == templateName && r.Tag == tag);
        }

        /// <summary>
        /// Removes all rules for particular template.
        /// </summary>
        /// <param name="templateSet">Template set</param>
        /// <typeparam name="T">Template set class. Should be inherited from <see cref="AbstractTemplateSet"/>.</typeparam>
        /// <param name="template">Expression that defines template inside the template set. For example: ts => ts.Page.</param>
        public static void RemoveRulesForTemplate<T>(this T templateSet, Expression<Func<T, string>> template) where T : AbstractTemplateSet
        {
            string templateName = templateSet.GetTemplateName(template);
            templateSet.ReplacementRules.RemoveAll(r => r.TemplateName == templateName);
        }

        /// <summary>
        /// Removes all rules for the template set.
        /// </summary>
        /// <param name="templateSet">Template set</param>
        public static void RemoveAllRules<T>(this T templateSet) where T : AbstractTemplateSet
        {
            templateSet.ReplacementRules.Clear();
        }
    }
}
