// =====================================================================
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

//<snippetSOAPLogger>
using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.IO;

// These namespaces are found in the Microsoft.Xrm.Sdk.dll assembly
// located in the SDK\bin folder of the SDK download.
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;

namespace Microsoft.Crm.Sdk.Samples
{
 /// <summary>
 /// Use this project to capture the SOAP Requests and Responses for code in the Run method.</summary>
 /// <remarks>
 /// When run this project in Visual Studio the SOAP messages will be written to a file. </remarks>
 public class SOAPLogger
 {
  #region Class Level Members

  private OrganizationServiceProxy _serviceProxy;

  #endregion Class Level Members

  #region CodeToCapture
  /// <summary>
  /// This method connects to the Organization service. 
  /// </summary>
  /// <param name="serverConfig">Contains server connection information.</param>
  //<snippetSOAPLogger1>
  public void Run(ServerConnection.Configuration serverConfig)
  {
   try
   {
    
    // Connect to the Organization service. 
    // The using statement assures that the service proxy will be properly disposed.
       using (_serviceProxy = new OrganizationServiceProxy(serverConfig.OrganizationUri, serverConfig.HomeRealmUri, serverConfig.Credentials, serverConfig.DeviceCredentials))
    {
     // This statement is required to enable early-bound type support.
     _serviceProxy.EnableProxyTypes();

     IOrganizationService service = (IOrganizationService)_serviceProxy;


     using (StreamWriter output = new StreamWriter("output.txt"))
     {

      SoapLoggerOrganizationService slos = new SoapLoggerOrganizationService(serverConfig.OrganizationUri, service, output);

      //Add the code you want to test here:
      // You must use the SoapLoggerOrganizationService 'slos' proxy rather than the IOrganizationService proxy you would normally use.



     }


    }

   }

   // Catch any service fault exceptions that Microsoft Dynamics CRM throws.
   catch (FaultException<Microsoft.Xrm.Sdk.OrganizationServiceFault>)
   {
    // You can handle an exception here or pass it back to the calling method.
    throw;
   }
  }
  //</snippetSOAPLogger1>


  #endregion CodeToCapture

  #region Main method

  /// <summary>
  /// Based on the Main() method used by most SDK samples.
  /// </summary>
  /// <param name="args"></param>
  static public void Main(string[] args)
  {
   try
   {
    // Obtain the target organization's Web address and client logon 
    // credentials from the user.
    ServerConnection serverConnect = new ServerConnection();
    ServerConnection.Configuration config = serverConnect.GetServerConfiguration();

    SOAPLogger app = new SOAPLogger();
    app.Run(config);
   }

   catch (FaultException<Microsoft.Xrm.Sdk.OrganizationServiceFault> ex)
   {
    Console.WriteLine("The application terminated with an error.");
    Console.WriteLine("Timestamp: {0}", ex.Detail.Timestamp);
    Console.WriteLine("Code: {0}", ex.Detail.ErrorCode);
    Console.WriteLine("Message: {0}", ex.Detail.Message);
    Console.WriteLine("Trace: {0}", ex.Detail.TraceText);
    Console.WriteLine("Inner Fault: {0}",
        null == ex.Detail.InnerFault ? "No Inner Fault" : "Has Inner Fault");
   }
   catch (System.TimeoutException ex)
   {
    Console.WriteLine("The application terminated with an error.");
    Console.WriteLine("Message: {0}", ex.Message);
    Console.WriteLine("Stack Trace: {0}", ex.StackTrace);
    Console.WriteLine("Inner Fault: {0}",
        null == ex.InnerException.Message ? "No Inner Fault" : ex.InnerException.Message);
   }
   catch (System.Exception ex)
   {
    Console.WriteLine("The application terminated with an error.");
    Console.WriteLine(ex.Message);

    // Display the details of the inner exception.
    if (ex.InnerException != null)
    {
     Console.WriteLine(ex.InnerException.Message);

     FaultException<Microsoft.Xrm.Sdk.OrganizationServiceFault> fe = ex.InnerException
         as FaultException<Microsoft.Xrm.Sdk.OrganizationServiceFault>;
     if (fe != null)
     {
      Console.WriteLine("Timestamp: {0}", fe.Detail.Timestamp);
      Console.WriteLine("Code: {0}", fe.Detail.ErrorCode);
      Console.WriteLine("Message: {0}", fe.Detail.Message);
      Console.WriteLine("Trace: {0}", fe.Detail.TraceText);
      Console.WriteLine("Inner Fault: {0}",
          null == fe.Detail.InnerFault ? "No Inner Fault" : "Has Inner Fault");
     }
    }
   }

   // Additional exceptions to catch: SecurityTokenValidationException, ExpiredSecurityTokenException,
   // SecurityAccessDeniedException, MessageSecurityException, and SecurityNegotiationException.

   finally
   {
    Console.WriteLine("Press <Enter> to exit.");
    Console.ReadLine();
   }
  }
  #endregion Main method
 }
}
//</snippetSOAPLogger>