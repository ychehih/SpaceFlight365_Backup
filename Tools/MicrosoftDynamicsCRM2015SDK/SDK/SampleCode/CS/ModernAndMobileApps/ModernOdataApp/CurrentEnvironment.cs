// =====================================================================
//
//  This file is part of the Microsoft Dynamics CRM SDK Code Samples.
//
//  Copyright (C) Microsoft Corporation.  All rights reserved.
//
//  This source code is intended only as a supplement to Microsoft
//  Development Tools and/or online documentation.  See these other
//  materials for detailed information regarding Microsoft code samples.
//
//  THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
//  KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
//  IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
//  PARTICULAR PURPOSE.
//
// =====================================================================

//<snippetModernOdataApp>
using Microsoft.Preview.WindowsAzure.ActiveDirectory.Authentication;
using System;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.Security.Authentication.Web;

namespace ModernOdataApp
{
    /// <summary>
    /// Manages authentication with the organization web service.
    /// </summary>
    public static class CurrentEnvironment
    {
        # region Class Level Members

        private static AuthenticationContext _authenticationContext;

        // TODO Set these string values as approppriate for your app registration and server.
        // For more information, see the SDK topic "Walkthrough: Register an app with Active Directory".
        private const string _clientID = "7e506c6a-b941-47f2-87d5-10aad78f9ad6"; 
        public const string CrmServiceUrl   = "https://my-org.contoso.com";
        
        // Dyamics CRM IFD OAuth URL.
        // TODO Change contoso.com to your IFD server's address.
        private const string _oauthUrl = "https://sts2.contoso.com/adfs/ls/XRMServices/2011/Organization.svc/web"; 

        # endregion

        // <summary>
        /// Perform any required app initialization.
        /// This is where authentication with Active Directory is performed.        
        /// </summary>
        public static async Task<string> Initialize()
        {
            // Obtain the redirect URI for the app programmatically.
            string redirectUrl = WebAuthenticationBroker.GetCurrentApplicationCallbackUri().ToString();

            // Obtain an authentication token to access the web service. 
            _authenticationContext = new AuthenticationContext(_oauthUrl, false);
            AuthenticationResult result = await _authenticationContext.AcquireTokenAsync(CrmServiceUrl, _clientID);

            // Verify that an access token was successfully acquired.
            if (AuthenticationStatus.Succeeded != result.Status)
            {
                if (result.Error == "authentication_failed")
                {
                    // Clear the token cache and try again.
                    (AuthenticationContext.TokenCache as DefaultTokenCache).Clear();
                    _authenticationContext = new AuthenticationContext(_oauthUrl, false);
                    result = await _authenticationContext.AcquireTokenAsync(CrmServiceUrl, _clientID);
                }
                else
                {
                    DisplayErrorWhenAcquireTokenFails(result);
                }
            }
            return result.AccessToken;
        }

        /// <summary>
        /// Display an error message to the user.
        /// </summary>
        /// <param name="result">The authentication result returned from AcquireTokenAsync().</param>
        private static async void DisplayErrorWhenAcquireTokenFails(AuthenticationResult result)
        {
            MessageDialog dialog;

            switch (result.Error)
            {
                case "authentication_canceled":
                    // User cancelled, so no need to display a message.
                    break;
                case "temporarily_unavailable":
                case "server_error":
                    dialog = new MessageDialog("Please retry the operation. If the error continues, please contact your administrator.", "Sorry, an error has occurred.");
                    await dialog.ShowAsync();
                    break;
                default:
                    // An error occurred when acquiring a token. Show the error description in a MessageDialog. 
                    dialog = new MessageDialog(string.Format("If the error continues, please contact your administrator.\n\nError: {0}\n\nError Description:\n\n{1}", result.Error, result.ErrorDescription), "Sorry, an error has occurred.");
                    await dialog.ShowAsync();
                    break;
            }
        }
    }
}
//</snippetModernOdataApp>