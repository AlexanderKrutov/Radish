﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Radish.Templates.Simple {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Templates {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Templates() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Radish.Templates.Simple.Templates", typeof(Templates).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;!-- Method --&gt;
        ///&lt;div class=&quot;method&quot;&gt;
        ///	&lt;div class=&quot;method-header&quot;&gt;
        ///		&lt;div class=&quot;method-header-route&quot;&gt; &lt;div class=&quot;method-header-route-{{method}}&quot;&gt;{{method}}&lt;/div&gt;{{route}}&lt;/div&gt;
        ///		&lt;div class=&quot;method-header-toggler&quot;&gt;&lt;a href=&quot;javascript:toggle(&apos;method-description-{{id}}&apos;, &apos;toggle-{{id}}&apos;);&quot;&gt;&lt;div id=&quot;toggle-{{id}}&quot; class=&quot;toggler&quot;&gt;{{title}}&lt;/div&gt;&lt;/a&gt;&lt;/div&gt;
        ///	&lt;/div&gt;
        ///
        ///    &lt;div class=&quot;method-description&quot; id=&quot;method-description-{{id}}&quot; style=&quot;display: none;&quot;&gt;
        ///
        ///        &lt;p&gt;{{description}}&lt;/p&gt;
        ///
        ///        {{re [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string Method {
            get {
                return ResourceManager.GetString("Method", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;!-- Methods group --&gt;
        ///&lt;h2&gt;{{title}}&lt;/h2&gt;
        ///&lt;p&gt;{{description}}&lt;p&gt;.
        /// </summary>
        internal static string MethodsGroup {
            get {
                return ResourceManager.GetString("MethodsGroup", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;!DOCTYPE html&gt;
        ///&lt;html&gt;
        ///&lt;head&gt;
        ///    &lt;title&gt;{{title}}&lt;/title&gt;
        ///    &lt;meta http-equiv=&quot;Content-Type&quot; content=&quot;text/html; charset=UTF-8&quot; /&gt;
        ///    &lt;meta name=&quot;generator&quot; content=&quot;Radish: https://github.com/alexanderkrutov/radish/&quot;&gt;
        ///    &lt;style type=&quot;text/css&quot;&gt;
        ///        body 
        ///		{
        ///            font-family: monospace;
        ///        }
        ///
        ///		h4 
        ///		{
        ///			font-style: italic;
        ///		}
        ///		
        ///		p
        ///		{
        ///			margin-left: 10px;
        ///		}
        ///		
        ///		table 
        ///		{
        ///			width: 100%;
        ///			border-collapse: collapse;
        ///		}
        ///
        ///		table, th, td 
        ///		{
        ///			bo [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string Page {
            get {
                return ResourceManager.GetString("Page", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;!-- Request body description --&gt;
        ///&lt;h4&gt;Request&lt;/h4&gt;
        ///&lt;p&gt;{{description}}&lt;p&gt;.
        /// </summary>
        internal static string RequestBodyDescription {
            get {
                return ResourceManager.GetString("RequestBodyDescription", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;!-- Request body example --&gt;
        ///&lt;h4&gt;Request body example&lt;/h4&gt;
        ///&lt;pre id=&quot;request-example-{{id}}&quot; class=&quot;databox&quot;&gt;
        ///&lt;/pre&gt;
        ///
        ///&lt;script type=&quot;text/javascript&quot;&gt;
        ///    highlightExampleCode(&apos;request-example-{{id}}&apos;, &apos;{{data}}&apos;, &apos;{{data-format}}&apos;);
        ///&lt;/script&gt;.
        /// </summary>
        internal static string RequestBodyExample {
            get {
                return ResourceManager.GetString("RequestBodyExample", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;!-- Request Parameter --&gt;
        ///&lt;tr&gt;
        ///	&lt;td class=&quot;parameter-name&quot;&gt;{{name}}&lt;/td&gt;
        ///	&lt;td&gt;{{type}}&lt;/td&gt;
        ///	&lt;td&gt;{{required}}&lt;/td&gt;
        ///	&lt;td&gt;{{description}}&lt;/td&gt;
        ///&lt;/tr&gt;.
        /// </summary>
        internal static string RequestParameter {
            get {
                return ResourceManager.GetString("RequestParameter", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;!-- Request parameters --&gt;
        ///&lt;h4&gt;Request Parameters&lt;/h4&gt;
        ///&lt;table&gt;
        ///	&lt;tr&gt;
        ///		&lt;th&gt;Parameter&lt;/th&gt;
        ///		&lt;th&gt;Type&lt;/th&gt;
        ///		&lt;th&gt;Required&lt;/th&gt;
        ///		&lt;th&gt;Description&lt;/th&gt;
        ///	&lt;/tr&gt;
        ///	{{request-parameters-list}}
        ///&lt;/table&gt;.
        /// </summary>
        internal static string RequestParametersList {
            get {
                return ResourceManager.GetString("RequestParametersList", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;!-- Response body description --&gt;
        ///&lt;h4&gt;Response&lt;/h4&gt;
        ///&lt;p&gt;{{description}}&lt;/p&gt;.
        /// </summary>
        internal static string ResponseBodyDescription {
            get {
                return ResourceManager.GetString("ResponseBodyDescription", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;!-- Response body example --&gt;
        ///&lt;h4&gt;Response example&lt;/h4&gt;
        ///&lt;pre id=&quot;response-example-{{id}}&quot; class=&quot;databox&quot;&gt;
        ///&lt;/pre&gt;
        ///
        ///&lt;script type=&quot;text/javascript&quot;&gt;
        ///    highlightExampleCode(&apos;response-example-{{id}}&apos;, &apos;{{data}}&apos;, &apos;{{data-format}}&apos;);
        ///&lt;/script&gt;.
        /// </summary>
        internal static string ResponseBodyExample {
            get {
                return ResourceManager.GetString("ResponseBodyExample", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;!-- response code --&gt;
        ///&lt;div class=&quot;response-code&quot;&gt; 
        ///	&lt;div class=&quot;response-code-box-{{code}}&quot;&gt;{{code}}&lt;/div&gt;
        ///	&lt;div class=&quot;response-code-description&quot;&gt;{{description}}&lt;/div&gt;
        ///&lt;/div&gt;.
        /// </summary>
        internal static string ResponseCode {
            get {
                return ResourceManager.GetString("ResponseCode", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;!-- Response codes --&gt;
        ///&lt;h4&gt;Response codes&lt;/h4&gt;
        ///&lt;div class=&quot;response-codes&quot;&gt;
        ///	{{response-codes-list}}
        ///&lt;/div&gt;.
        /// </summary>
        internal static string ResponseCodesList {
            get {
                return ResourceManager.GetString("ResponseCodesList", resourceCulture);
            }
        }
    }
}
