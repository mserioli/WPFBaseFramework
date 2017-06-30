using ClientServerContract.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading;

namespace WCFClientProvider
{
    public class DataSelectionServiceProvider
    {
        private static NetTcpBinding netTcpBinding;

        private static readonly int maxRetries = 3;

        private static EndpointAddress dataSelectionServiceEndPointAddress;

        private static readonly int retriesInterval = 3000;

        private static object lockObj = new Object();

        private static ChannelFactory<ClientServerContract.ServiceInterfaces.IDataSelectionService> dataSelectionServiceProxy = null;
        private static ClientServerContract.ServiceInterfaces.IDataSelectionService dataSelectionService = null;

        public static void Initialize(string str_WcfUniformResourceIdentifier)
        {
            netTcpBinding = new NetTcpBinding();

            // Set binding max size and timeout            
            netTcpBinding.SendTimeout = netTcpBinding.ReceiveTimeout = TimeSpan.FromMinutes(120);
            netTcpBinding.MaxBufferPoolSize = 2147483647;
            netTcpBinding.MaxReceivedMessageSize = 2147483647;
            netTcpBinding.MaxBufferSize = 2147483647;

            netTcpBinding.ReaderQuotas.MaxStringContentLength = int.MaxValue;
            netTcpBinding.ReaderQuotas.MaxArrayLength = int.MaxValue;
            netTcpBinding.ReaderQuotas.MaxBytesPerRead = int.MaxValue;
            netTcpBinding.ReaderQuotas.MaxDepth = int.MaxValue;
            netTcpBinding.ReaderQuotas.MaxNameTableCharCount = int.MaxValue;

            dataSelectionServiceEndPointAddress = new EndpointAddress(str_WcfUniformResourceIdentifier);

            //string hostName = str_WcfUniformResourceIdentifier.Split(':')[1].Replace("/", String.Empty);
            //SMSTClient2Server.Util.PingHelper.PingHost(hostName);
        }

        private static ClientServerContract.ServiceInterfaces.IDataSelectionService DataSelectionService
        {
            get
            {
                lock (lockObj)
                {
                    // If channel is null or channel status is different from OPENED, the same one will be created and opened
                    if (dataSelectionService == null || dataSelectionServiceProxy == null || ((ICommunicationObject)dataSelectionService).State != System.ServiceModel.CommunicationState.Opened)
                    {

                        /*
                        if (isAuthenticationRequired)
                        {

                            dataSelectionServiceProxy = new ChannelFactory<SMSTClient2Server.ServiceInterfaces.IDataSelectionService>(netTcpBinding);
                            dataSelectionServiceProxy.Credentials.Windows.ClientCredential.UserName = userName;
                            dataSelectionServiceProxy.Credentials.Windows.ClientCredential.Password = password;

                            foreach (OperationDescription op in dataSelectionServiceProxy.Endpoint.Contract.Operations)
                            {
                                DataContractSerializerOperationBehavior dataContractBehavior = op.Behaviors[typeof(DataContractSerializerOperationBehavior)] as DataContractSerializerOperationBehavior;

                                if (dataContractBehavior != null)
                                {
                                    dataContractBehavior.MaxItemsInObjectGraph = int.MaxValue;
                                }
                            }

                            dataSelectionService = dataSelectionServiceProxy.CreateChannel(dataSelectionServiceEndPointAddress);
                            IClientChannel contextChannel = dataSelectionService as IClientChannel;
                            contextChannel.OperationTimeout = TimeSpan.FromMinutes(120);


                            if (dataSelectionServiceProxy.State != CommunicationState.Opened)
                                dataSelectionServiceProxy.Open();
                        }
                        else */
                        {
                            dataSelectionServiceProxy = new ChannelFactory<ClientServerContract.ServiceInterfaces.IDataSelectionService>(netTcpBinding);

                            foreach (OperationDescription op in dataSelectionServiceProxy.Endpoint.Contract.Operations)
                            {
                                DataContractSerializerOperationBehavior dataContractBehavior = op.Behaviors[typeof(DataContractSerializerOperationBehavior)] as DataContractSerializerOperationBehavior;

                                if (dataContractBehavior != null)
                                {
                                    dataContractBehavior.MaxItemsInObjectGraph = int.MaxValue;
                                }
                            }

                            //// Set the maxItemsInObjectGraph
                            //foreach (OperationDescription op in dataSelectionServiceProxy.Endpoint.Contract.Operations)
                            //{
                            //    DataContractSerializerOperationBehavior dataContractBehavior = op.Behaviors.Find<DataContractSerializerOperationBehavior>() as DataContractSerializerOperationBehavior;
                            //    if (dataContractBehavior != null)
                            //    {
                            //        dataContractBehavior.MaxItemsInObjectGraph = 2147483647;
                            //    }
                            //}

                            dataSelectionService = dataSelectionServiceProxy.CreateChannel(dataSelectionServiceEndPointAddress);
                            IClientChannel contextChannel = dataSelectionService as IClientChannel;
                            contextChannel.OperationTimeout = TimeSpan.FromMinutes(120);

                            if (dataSelectionServiceProxy.State != CommunicationState.Opened)
                                dataSelectionServiceProxy.Open();
                        }
                    }
                }
                return dataSelectionService;
            }
        }

        public static void Ping()
        {
            Exception e = null;
            for (int i = 0; i < maxRetries; i++)
            {
                try
                {
                    DataSelectionService.Ping();
                    return;
                }
                catch (Exception ex)
                {
                    e = ex;
                }
                Thread.Sleep(retriesInterval);
            }
            throw e;
        }
    }
}
