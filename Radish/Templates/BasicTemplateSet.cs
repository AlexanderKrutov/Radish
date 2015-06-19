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
    /// Base template set for two predefined template sets: <see cref="SimpleTemplateSet"/> and <see cref="BootstrapTemplateSet"/>.
    /// Contains predefined rules set for rendering tags in templates.
    /// Use this as a base class if you want to write own template set that will use the same rendering rules as in predefined templates.
    /// </summary>
    public abstract class BasicTemplateSet : AbstractTemplateSet
    {
        /// <summary>
        /// Documentation title.
        /// </summary>
        public abstract string Title { get; set; }
        
        /// <summary>
        /// Template for documentation section of particular method.
        /// </summary>
        public abstract string Method { get; set; }

        /// <summary>
        /// Template for documentation section of group of methods.
        /// </summary>
        public abstract string MethodsGroup { get; set; }

        /// <summary>
        /// Template for single row in a table of request parameters of the method.
        /// </summary>
        public abstract string RequestParameter { get; set; }

        /// <summary>
        /// Template for rendering table of request parameters of the method.
        /// </summary>
        public abstract string RequestParametersList { get; set; }

        /// <summary>
        /// Template for documentation section about request information for the method.
        /// </summary>
        public abstract string RequestBodyDescription { get; set; }
        
        /// <summary>
        /// Template for documentation section with example of request body for the method. 
        /// </summary>
        public abstract string RequestBodyExample { get; set; }

        /// <summary>
        /// Template for documentation section about response information for the method.
        /// </summary>
        public abstract string ResponseBodyDescription { get; set; }

        /// <summary>
        /// Template for documentation section with example of response body for the method. 
        /// </summary>
        public abstract string ResponseBodyExample { get; set; }

        /// <summary>
        /// Template for single row in a table of response codes for the method.
        /// </summary>
        public abstract string ResponseCode { get; set; }

        /// <summary>
        /// Template for table of response codes for the method.
        /// </summary>
        public abstract string ResponseCodesList { get; set; }

        /// <summary>
        /// Creates new instance of BasicTemplateSet. 
        /// </summary>
        public BasicTemplateSet() : base()
        {
            // Main Page template
            this.AddRule(ts => ts.Page, "title", Page_Title);
            this.AddRule(ts => ts.Page, "methods-list", Page_MethodsList);
            this.AddRule(ts => ts.Page, "json-methods-list", Page_JsonMethodsList);

            // Methods Group template
            this.AddRule(ts => ts.MethodsGroup, "title", MethodsGroup_Title);
            this.AddRule(ts => ts.MethodsGroup, "description", MethodsGroup_Description);

            // Method template
            this.AddRule(ts => ts.Method, "method", Method_Method);
            this.AddRule(ts => ts.Method, "route", Method_Route);
            this.AddRule(ts => ts.Method, "id", Method_Id);
            this.AddRule(ts => ts.Method, "title", Method_Title);
            this.AddRule(ts => ts.Method, "description", Method_Description);
            this.AddRule(ts => ts.Method, "request-parameters-list", Method_RequestParametersList);
            this.AddRule(ts => ts.Method, "request-body", Method_RequestBody);
            this.AddRule(ts => ts.Method, "request-body-example", Method_RequestBodyExample);
            this.AddRule(ts => ts.Method, "response-body", Method_ResponseBody);
            this.AddRule(ts => ts.Method, "response-body-example", Method_ResponseBodyExample);
            this.AddRule(ts => ts.Method, "response-codes-list", Method_ResponseCodesList);

            // Request Parameters List template
            this.AddRule(ts => ts.RequestParametersList, "request-parameters-list", RequestParametersList_RequestParametersList);

            // Request Parameter template
            this.AddRule(ts => RequestParameter, "name", RequestParameter_Name);
            this.AddRule(ts => RequestParameter, "type", RequestParameter_Type);
            this.AddRule(ts => RequestParameter, "required", RequestParameter_Required);
            this.AddRule(ts => RequestParameter, "description", RequestParameter_Description);

            // Request Body template
            this.AddRule(ts => ts.RequestBodyDescription, "description", RequestBody_Description);

            // Request Body Example template
            this.AddRule(ts => ts.RequestBodyExample, "data", RequestBody_Data);
            this.AddRule(ts => ts.RequestBodyExample, "data-format", RequestBody_DataFormat);
            this.AddRule(ts => ts.RequestBodyExample, "id", Method_Id);

            // Response Body template
            this.AddRule(ts => ts.ResponseBodyDescription, "description", ResponseBody_Description);

            // Response Body Example template
            this.AddRule(ts => ts.ResponseBodyExample, "data", ResponseBody_Data);
            this.AddRule(ts => ts.ResponseBodyExample, "data-format", ResponseBody_DataFormat);
            this.AddRule(ts => ts.ResponseBodyExample, "id", Method_Id);

            // Response Codes List template
            this.AddRule(ts => ts.ResponseCodesList, "response-codes-list", ResponseCodesList_ResponseCodesList);

            // Response Code template
            this.AddRule(ts => ts.ResponseCode, "code", ResponseCode_Code);
            this.AddRule(ts => ts.ResponseCode, "description", ResponseCode_Description);
        }

        #region Rules Handlers

        private string Page_Title(DocumentorContext context, params object[] parameters)
        {
            return context.RenderTemplate<BasicTemplateSet>(ts => ts.Title);
        }

        private string Page_MethodsList(DocumentorContext context, params object[] parameters)
        {
            StringBuilder sbMethods = new StringBuilder();
            string latestGroupKey = null;
            foreach (MethodInfo method in context.RestMethods)
            {
                OrderAttribute attrOrder = method.GetRadishAttribute<OrderAttribute>();
                if (attrOrder != null)
                {
                    if (!String.IsNullOrEmpty(attrOrder.GroupKey) && latestGroupKey != attrOrder.GroupKey)
                    {
                        sbMethods.Append(context.RenderTemplate<BasicTemplateSet>(ts => ts.MethodsGroup, method));
                    }
                    latestGroupKey = attrOrder.GroupKey;
                }
                sbMethods.Append(context.RenderTemplate<BasicTemplateSet>(ts => ts.Method, method));
            }
            return sbMethods.ToString();
        }

        private string Page_JsonMethodsList(DocumentorContext context, params object[] parameters)
        {
            StringBuilder sbMethods = new StringBuilder();
            
            foreach (MethodInfo methodInfo in context.RestMethods)
            {
                string title = "";
                string method = "";
                string route = "";
                string id = "";

                MethodAttribute attrMethod = methodInfo.GetRadishAttribute<MethodAttribute>();
                if (attrMethod != null)
                {
                    id = attrMethod.Id;
                    route = attrMethod.Route;
                    method = attrMethod.MethodType;
                }

                MethodTitleAttribute attrTitle = methodInfo.GetRadishAttribute<MethodTitleAttribute>();
                if (attrTitle != null)
                {
                    title = attrTitle.Title;
                }

                sbMethods.Append(
                    String.Format("{{\"title\": \"{0}\", \"method\" : \"{1}\", \"route\": \"{2}\", \"id\" : \"{3}\"}},\n", 
                        title, method, route, id));
            }

            return sbMethods.ToString();
        }

        private string MethodsGroup_Title(DocumentorContext context, params object[] parameters)
        {
            MethodInfo restMethod = parameters[0] as MethodInfo;
            OrderAttribute attr = restMethod.GetRadishAttribute<OrderAttribute>();
            MethodGroup group = context.MethodGroups[attr.GroupKey];
            return group.Title;
        }

        private string MethodsGroup_Description(DocumentorContext context, params object[] parameters)
        {
            MethodInfo restMethod = parameters[0] as MethodInfo;
            OrderAttribute attr = restMethod.GetRadishAttribute<OrderAttribute>();
            MethodGroup group = context.MethodGroups[attr.GroupKey];
            return group.Description;
        }

        private string Method_Method(DocumentorContext context, params object[] parameters)
        {
            MethodInfo restMethod = parameters[0] as MethodInfo;
            MethodAttribute attr = restMethod.GetRadishAttribute<MethodAttribute>();
            return attr.MethodType;
        }

        private string Method_Route(DocumentorContext context, params object[] parameters)
        {
            MethodInfo restMethod = parameters[0] as MethodInfo;
            MethodAttribute attr = restMethod.GetRadishAttribute<MethodAttribute>();
            return attr.Route;
        }

        private string Method_Id(DocumentorContext context, params object[] parameters)
        {
            MethodInfo restMethod = parameters[0] as MethodInfo;
            MethodAttribute attr = restMethod.GetRadishAttribute<MethodAttribute>();
            return (attr != null ? attr.Id : "");
        }

        private string Method_Title(DocumentorContext context, params object[] parameters)
        {
            MethodInfo restMethod = parameters[0] as MethodInfo;
            MethodTitleAttribute attr = restMethod.GetRadishAttribute<MethodTitleAttribute>();
            return (attr != null ? attr.Title : "");
        }

        private string Method_Description(DocumentorContext context, params object[] parameters)
        {
            MethodInfo restMethod = parameters[0] as MethodInfo;
            MethodDescriptionAttribute attr = restMethod.GetRadishAttribute<MethodDescriptionAttribute>();
            return (attr != null ? attr.Description : "");
        }

        private string Method_RequestParametersList(DocumentorContext context, params object[] parameters)
        {
            MethodInfo restMethod = parameters[0] as MethodInfo;
            bool hasParameters = restMethod.GetRadishAttributes<RequestParameterAttribute>().Count() > 0;
            return (hasParameters ? context.RenderTemplate<BasicTemplateSet>(ts => ts.RequestParametersList, restMethod) : "");
        }

        private string Method_RequestBody(DocumentorContext context, params object[] parameters)
        {
            MethodInfo restMethod = parameters[0] as MethodInfo;
            RequestBodyDescriptionAttribute attr = restMethod.GetRadishAttribute<RequestBodyDescriptionAttribute>();
            return (attr != null ? context.RenderTemplate<BasicTemplateSet>(ts => ts.RequestBodyDescription, restMethod) : "");
        }

        private string Method_RequestBodyExample(DocumentorContext context, object[] parameters)
        {
            MethodInfo restMethod = parameters[0] as MethodInfo;
            RequestBodyExampleAttribute attr = restMethod.GetRadishAttribute<RequestBodyExampleAttribute>();
            return (attr != null ? context.RenderTemplate<BasicTemplateSet>(ts => ts.RequestBodyExample, restMethod) : "");
        }

        private string Method_ResponseBody(DocumentorContext context, params object[] parameters)
        {
            MethodInfo restMethod = parameters[0] as MethodInfo;
            ResponseBodyDescriptionAttribute attr = restMethod.GetRadishAttribute<ResponseBodyDescriptionAttribute>();
            return (attr != null ? context.RenderTemplate<BasicTemplateSet>(ts => ts.ResponseBodyDescription, restMethod) : "");
        }

        private string Method_ResponseBodyExample(DocumentorContext context, params object[] parameters)
        {
            MethodInfo restMethod = parameters[0] as MethodInfo;
            ResponseBodyExampleAttribute attr = restMethod.GetRadishAttribute<ResponseBodyExampleAttribute>();
            return (attr != null ? context.RenderTemplate<BasicTemplateSet>(ts => ts.ResponseBodyExample, restMethod) : "");
        }

        private string Method_ResponseCodesList(DocumentorContext context, params object[] parameters)
        {
            MethodInfo restMethod = parameters[0] as MethodInfo;
            bool hasResponseCodes = restMethod.GetRadishAttributes<ResponseCodeAttribute>().Count() > 0;
            return (hasResponseCodes ? context.RenderTemplate<BasicTemplateSet>(ts => ts.ResponseCodesList, restMethod) : "");
        }

        private string RequestParametersList_RequestParametersList(DocumentorContext context, object[] parameters)
        {
            MethodInfo restMethod = parameters[0] as MethodInfo;
            IEnumerable<RequestParameterAttribute> attrRequestParametersList = restMethod.GetRadishAttributes<RequestParameterAttribute>().OrderBy(rp => rp.Order);
            StringBuilder sbRequestParametersList = new StringBuilder();
            foreach (RequestParameterAttribute attr in attrRequestParametersList)
            {
                sbRequestParametersList.Append(context.RenderTemplate<BasicTemplateSet>(ts => RequestParameter, attr));
            }
            return sbRequestParametersList.ToString();
        }

        private string RequestParameter_Name(DocumentorContext context, params object[] parameters)
        {
            RequestParameterAttribute attr = parameters[0] as RequestParameterAttribute;
            return attr.Name;
        }

        private string RequestParameter_Type(DocumentorContext context, params object[] parameters)
        {
            RequestParameterAttribute attr = parameters[0] as RequestParameterAttribute;
            return attr.Type;
        }

        private string RequestParameter_Required(DocumentorContext context, params object[] parameters)
        {
            RequestParameterAttribute attr = parameters[0] as RequestParameterAttribute;
            return attr.Required.ToString();
        }

        private string RequestParameter_Description(DocumentorContext context, params object[] parameters)
        {
            RequestParameterAttribute attr = parameters[0] as RequestParameterAttribute;
            return attr.Description;
        }

        private string RequestBody_Description(DocumentorContext context, params object[] parameters)
        {
            MethodInfo restMethod = parameters[0] as MethodInfo;
            RequestBodyDescriptionAttribute attr = restMethod.GetRadishAttribute<RequestBodyDescriptionAttribute>();
            return (attr != null ? attr.Description : "");
        }

        private string ResponseBody_Description(DocumentorContext context, params object[] parameters)
        {
            MethodInfo restMethod = parameters[0] as MethodInfo;
            ResponseBodyDescriptionAttribute attr = restMethod.GetRadishAttribute<ResponseBodyDescriptionAttribute>();
            return (attr != null ? attr.Description : "");
        }

        private string RequestBody_Data(DocumentorContext context, params object[] parameters)
        {
            MethodInfo restMethod = parameters[0] as MethodInfo;
            RequestBodyExampleAttribute attr = restMethod.GetRadishAttribute<RequestBodyExampleAttribute>();
            return (attr != null ? attr.Data : "");
        }

        private string RequestBody_DataFormat(DocumentorContext context, params object[] parameters)
        {
            MethodInfo restMethod = parameters[0] as MethodInfo;
            RequestBodyExampleAttribute attr = restMethod.GetRadishAttribute<RequestBodyExampleAttribute>();
            return attr.Format.ToString();
        }

        private string ResponseBody_Data(DocumentorContext context, params object[] parameters)
        {
            MethodInfo restMethod = parameters[0] as MethodInfo;
            ResponseBodyExampleAttribute attr = restMethod.GetRadishAttribute<ResponseBodyExampleAttribute>();
            return (attr != null ? attr.Data : "");
        }

        private string ResponseBody_DataFormat(DocumentorContext context, params object[] parameters)
        {
            MethodInfo restMethod = parameters[0] as MethodInfo;
            ResponseBodyExampleAttribute attr = restMethod.GetRadishAttribute<ResponseBodyExampleAttribute>();
            return attr.Format.ToString();
        }

        private string ResponseCodesList_ResponseCodesList(DocumentorContext context, params object[] parameters)
        {
            MethodInfo restMethod = parameters[0] as MethodInfo;
            IEnumerable<ResponseCodeAttribute> attrResponseCodes = restMethod.GetRadishAttributes<ResponseCodeAttribute>().OrderBy(rc => rc.Code);
            StringBuilder sbResponseCodesList = new StringBuilder();
            foreach (ResponseCodeAttribute attr in attrResponseCodes)
            {
                sbResponseCodesList.Append(context.RenderTemplate<BasicTemplateSet>(ts => ts.ResponseCode, attr));
            }
            return sbResponseCodesList.ToString();
        }

        private string ResponseCode_Code(DocumentorContext context, params object[] parameters)
        {
            ResponseCodeAttribute attr = parameters[0] as ResponseCodeAttribute;
            return attr.Code.ToString();
        }

        private string ResponseCode_Description(DocumentorContext context, params object[] parameters)
        {
            ResponseCodeAttribute attr = parameters[0] as ResponseCodeAttribute;
            return attr.Description;
        }

        #endregion Rules Handlers
    }
}
