﻿// =====================================================================
//
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
//
// =====================================================================

//<snippetAssignQueueItemWorker>
using System;
using System.ServiceModel;
using System.ServiceModel.Description;

// These namespaces are found in the Microsoft.Xrm.Sdk.dll assembly
// found in the SDK\bin folder.
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;


// This namespace is found in Microsoft.Crm.Sdk.Proxy.dll assembly
// found in the SDK\bin folder.
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Client;

namespace Microsoft.Crm.Sdk.Samples
{
    /// <summary>
    /// This sample shows how to assign queue item work to some user.
    /// </summary>
    public class AssignQueueItemWorker
    {
        #region Class Level Members


        /// <summary>
        /// Stores the organization service proxy.
        /// </summary>
        private OrganizationServiceProxy _serviceProxy;


        // Define the IDs needed for this sample.
        private Guid _queueItemId;
        private Guid _letterId;
        private Guid _queueId;
        private Guid _userId;
        #endregion Class Level Members

        #region How To Sample Code
        /// <summary>
        /// This method first connects to the Organization service. 
        /// Initiate method to create any entity records that this sample requires.
        /// Retrieves new owner's details. 
        /// Update the queue item record to assign it to new owner.
        /// Optionally delete any entity records that were created for this sample.
        /// <para name="organizationFriendlyName">The friendly name of the 
        /// target organization.</para>
        /// <para name="discoveryServer">The name of the discovery server.</para>
        /// <param name="promptForDelete">Indicates whether to prompt the user to 
        /// delete the records created in this sample.</param>
        /// </summary>
        public void Run(ServerConnection.Configuration serverConfig, bool promptForDelete)
        {
            try
            {
                // Connect to the Organization service. 
                // The using statement assures that the service proxy will be properly disposed.
                using (_serviceProxy = new OrganizationServiceProxy(serverConfig.OrganizationUri, serverConfig.HomeRealmUri,
                                                                     serverConfig.Credentials, serverConfig.DeviceCredentials))
                {
                    // This statement is required to enable early-bound type support.
                    _serviceProxy.EnableProxyTypes();


                    // Call the method to create any data that this sample requires.
                    CreateRequiredRecords();

                    //<snippetAssignQueueItemWorker1>
                    // Retrieve the current user information.
                    WhoAmIRequest whoAmIRequest = new WhoAmIRequest();
                    WhoAmIResponse whoAmIResponse = (WhoAmIResponse)_serviceProxy.Execute(
                        whoAmIRequest);

                    ColumnSet columnSet = new ColumnSet("fullname");
                    SystemUser currentUser = (SystemUser)_serviceProxy.Retrieve(
                        SystemUser.EntityLogicalName,
                        whoAmIResponse.UserId, columnSet);
                    String currentUserName = currentUser.FullName;
                    _userId = currentUser.Id;

                    // Create an instance of an existing queueitem in order to specify 
                    // the user that will be working on it using PickFromQueueRequest.

                    PickFromQueueRequest pickFromQueueRequest = new PickFromQueueRequest
                    {
                        QueueItemId = _queueItemId,
                        WorkerId = _userId
                    };

                    _serviceProxy.Execute(pickFromQueueRequest);
                    //</snippetAssignQueueItemWorker1>  

                    Console.WriteLine("The letter queue item is queued for new owner {0}.",
                        currentUserName);

                    DeleteRequiredRecords(promptForDelete);
                }
            }
            // Catch any service fault exceptions that Microsoft Dynamics CRM throws.
            catch (FaultException<Microsoft.Xrm.Sdk.OrganizationServiceFault>)
            {
                // You can handle an exception here or pass it back to the calling method.
                throw;
            }
        }

        /// <summary>
        /// This method creates any entity records that this sample requires.
        /// Create a queue record.
        /// Create a letter record.
        /// Create a queue item for queue record.
        /// </summary>
        public void CreateRequiredRecords()
        {
            // Create a private queue instance and set its property values.
            Queue newQueue = new Queue
            {
                Name = "Example Queue.",
                Description = "This is an example queue.",
                QueueViewType=new OptionSetValue((int)QueueQueueViewType.Private)
            };

            // Create a new queue and store its returned GUID in a variable 
            // for later use.
            _queueId = _serviceProxy.Create(newQueue);

            Console.WriteLine("Created {0}.", newQueue.Name);

            Letter newLetter = new Letter
            {
                Description = "Example Letter"
            };

            _letterId = _serviceProxy.Create(newLetter);

            Console.WriteLine("Created {0}.", newLetter.Description);

            // Create a new instance of a queueitem and initialize its 
            // properties.
            QueueItem item = new QueueItem
            {
                QueueId = new EntityReference(Queue.EntityLogicalName, _queueId),
                ObjectId = new EntityReference(Letter.EntityLogicalName, _letterId)
            };

            // Create the queueitem on the server, which will associate 
            // the letter with the queue.
            _queueItemId = _serviceProxy.Create(item);

            Console.WriteLine("Created the letter queue item for the queue.");

            return;
        }

        /// <summary>
        /// Deletes any entity records that were created for this sample.
        /// <param name="prompt">Indicates whether to prompt the user to 
        /// delete the records created in this sample.</param>
        /// </summary>
        public void DeleteRequiredRecords(bool prompt)
        {
            bool deleteRecords = true;

            if (prompt)
            {
                Console.WriteLine("\nDo you want these entity records deleted? (y/n)");
                String answer = Console.ReadLine();

                deleteRecords = (answer.StartsWith("y") || answer.StartsWith("Y"));
            }

            if (deleteRecords)
            {
                _serviceProxy.Delete(QueueItem.EntityLogicalName, _queueItemId);
                _serviceProxy.Delete(Letter.EntityLogicalName, _letterId);
                _serviceProxy.Delete(Queue.EntityLogicalName, _queueId);

                Console.WriteLine("Entity records have been deleted.");
            }
        }

        #endregion How To Sample Code

        #region Main
        /// <summary>
        /// Main. Runs the sample and provides error output.
        /// <param name="args">Array of arguments to Main method.</param>
        /// </summary>
        static public void Main(string[] args)
        {
            try
            {
                // Obtain the target organization's Web address and client logon 
                // credentials from the user.
                ServerConnection serverConnect = new ServerConnection();
                ServerConnection.Configuration config = serverConnect.GetServerConfiguration();

                AssignQueueItemWorker app = new AssignQueueItemWorker();
                app.Run(config, true);
            }

            catch (FaultException<Microsoft.Xrm.Sdk.OrganizationServiceFault> ex)
            {
                Console.WriteLine("The application terminated with an error.");
                Console.WriteLine("Timestamp: {0}", ex.Detail.Timestamp);
                Console.WriteLine("Code: {0}", ex.Detail.ErrorCode);
                Console.WriteLine("Message: {0}", ex.Detail.Message);
                Console.WriteLine("Plugin Trace: {0}", ex.Detail.TraceText);
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

                    FaultException<Microsoft.Xrm.Sdk.OrganizationServiceFault> fe =
                        ex.InnerException
                        as FaultException<Microsoft.Xrm.Sdk.OrganizationServiceFault>;
                    if (fe != null)
                    {
                        Console.WriteLine("Timestamp: {0}", fe.Detail.Timestamp);
                        Console.WriteLine("Code: {0}", fe.Detail.ErrorCode);
                        Console.WriteLine("Message: {0}", fe.Detail.Message);
                        Console.WriteLine("Plugin Trace: {0}", fe.Detail.TraceText);
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
        #endregion Main
    }
}
//</snippetAssignQueueItemWorker>

