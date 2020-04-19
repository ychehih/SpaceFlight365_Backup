﻿// =====================================================================
//  This file is part of the Microsoft Dynamics CRM SDK code samples.
//
//  Copyright (C) Microsoft Corporation.  All rights reserved.
//
//  This source code is intended only as a supplement to Microsoft
//  Development Tools and/or on-line documentation.  See these other
//  materials for detailed information regarding Microsoft code samples.
//
//  THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
//  KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
//  IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
//  PARTICULAR PURPOSE.
// =====================================================================

//<snippetPostUrl>
using System;
using System.Activities;
using System.IO;
using System.Net;
using System.Text;

// These namespaces are found in the Microsoft.Xrm.Sdk.dll assembly
// located in the SDK\bin folder of the SDK download.
using Microsoft.Xrm.Sdk;

// These namespaces are found in the Microsoft.Xrm.Sdk.Workflow.dll assembly
// located in the SDK\bin folder of the SDK download.
using Microsoft.Xrm.Sdk.Workflow;

namespace Microsoft.Crm.Sdk.Samples
{
    /// <summary>
    /// Posts data in a url.
    /// Input arguments:
    ///   "URL". Type: String. Is the URL to which you will be posting data.
    ///   "Account Name". Type: String. Is part of the data to post.
    ///   "Account Num". Type: String. Is part of the data to post.
    /// Output argument:
    ///   None.
    /// </summary>
    public sealed partial class PostUrl : CodeActivity
    {
        /// <summary>
        /// NOTE: When you add this activity to a workflow, you must set the following properties:
        ///
        /// URL - manually add the URL to which you will be posting data.  For example: http://myserver.com/ReceivePostURL.aspx  
        ///		 See this sample's companion file 'ReceivePostURL.aspx' for an example of how the receiving page might look.
        ///
        /// AccountName - populate this property with the Account's 'name' attribute.
        ///
        /// AccountNum - populate this property with the Account's 'account number' attribute.
        /// </summary>
        protected override void Execute(CodeActivityContext executionContext)
        {
            IWorkflowContext context = executionContext.GetExtension<IWorkflowContext>();
            IOrganizationServiceFactory serviceFactory =
                executionContext.GetExtension<IOrganizationServiceFactory>();
            IOrganizationService service =
                serviceFactory.CreateOrganizationService(context.UserId);

            // Build data that will be posted to a URL
            string postData = "Name=" + this.AccountName.Get(executionContext) + "&AccountNum=" + this.AccountNum.Get(executionContext);

            // Encode the data
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] encodedPostData = encoding.GetBytes(postData);

            // Create a request object for posting our data to a URL
            Uri uri = new Uri(this.URL.Get(executionContext));
            HttpWebRequest urlRequest = (HttpWebRequest)WebRequest.Create(uri);
            urlRequest.Method = "POST";
            urlRequest.ContentLength = encodedPostData.Length;
            urlRequest.ContentType = "application/x-www-form-urlencoded";

            // Add the encoded data to the request	
            using (Stream formWriter = urlRequest.GetRequestStream())
            {
                formWriter.Write(encodedPostData, 0, encodedPostData.Length);
            }

            // Post the data to the URL			
            HttpWebResponse urlResponse = (HttpWebResponse)urlRequest.GetResponse();
        }

        // Define Input/Output Arguments
        [Input("URL")]
        [Default("http://localhost:9999/ReceivePostURL.aspx")]
        public InArgument<string> URL { get; set; }

        [Input("Account Name")]
        public InArgument<string> AccountName { get; set; }

        [Input("Account Num")]
        public InArgument<string> AccountNum { get; set; }
    }
}
//</snippetPostUrl>