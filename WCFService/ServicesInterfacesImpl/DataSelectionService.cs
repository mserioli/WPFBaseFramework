using ClientServerContract.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace WCFService.ServicesInterfacesImpl
{

    [ServiceBehavior(MaxItemsInObjectGraph = int.MaxValue, IncludeExceptionDetailInFaults = true)]
    class DataSelectionService : IDataSelectionService
    {
        private static ServiceHost host;

        public static void StartService()
        {
            //Create the Service host
            host = new ServiceHost(typeof(DataSelectionService));

            // Set binding max size and timeout
            NetTcpBinding netTcpBinding = new NetTcpBinding();
            netTcpBinding.SendTimeout = netTcpBinding.ReceiveTimeout = TimeSpan.FromMinutes(120);
            netTcpBinding.MaxBufferPoolSize = int.MaxValue; // 2147483647;
            netTcpBinding.MaxReceivedMessageSize = int.MaxValue; // 2147483647;
            netTcpBinding.MaxBufferSize = int.MaxValue; // 2147483647;

            netTcpBinding.ReaderQuotas.MaxArrayLength = int.MaxValue;
            netTcpBinding.ReaderQuotas.MaxBytesPerRead = int.MaxValue;
            netTcpBinding.ReaderQuotas.MaxDepth = int.MaxValue;
            netTcpBinding.ReaderQuotas.MaxNameTableCharCount = int.MaxValue;

            //Add the endpoint
            try
            {
                host.AddServiceEndpoint(new ServiceEndpoint(
                ContractDescription.GetContract(typeof(IDataSelectionService)),
                netTcpBinding,
                new EndpointAddress(Properties.Settings.Default.DataSelectionServiceAddress)));

                foreach (ServiceEndpoint ep in host.Description.Endpoints)
                {
                    foreach (OperationDescription op in ep.Contract.Operations)
                    {
                        DataContractSerializerOperationBehavior dataContractBehavior = op.Behaviors[typeof(DataContractSerializerOperationBehavior)] as DataContractSerializerOperationBehavior;

                        if (dataContractBehavior != null)
                        {
                            dataContractBehavior.MaxItemsInObjectGraph = int.MaxValue;
                        }
                    }
                }

                //if (Logger.IsDebugEnabled())
                //    Logger.Log(LogLevel.DEBUG, "EndPoint added to Service Host");
            }
            catch (Exception e)
            {
                //if (Logger.IsErrorEnabled())
                //    Logger.Log(LogLevel.ERROR, "An error occurred adding Service Host EndPoint", e);
                throw;
            }

            //Open the service
            try
            {
                host.Open();
                //if (Logger.IsInfoEnabled())
                //    Logger.Log(LogLevel.INFO, "Service Host opened");
            }
            catch (Exception e)
            {
                //if (Logger.IsErrorEnabled())
                //    Logger.Log(LogLevel.ERROR, "An error occurred opening Service Host", e);
                throw;
            }
        }

        public static void CloseService()
        {
            if (host.State != CommunicationState.Closed)
            {
                try
                {
                    host.Close();
                    //if (Logger.IsInfoEnabled())
                    //    Logger.Log(LogLevel.INFO, "Service Host closed");
                }
                catch (Exception e)
                {
                    //if (Logger.IsErrorEnabled())
                    //    Logger.Log(LogLevel.ERROR, "An error occurred closing Service Host", e);
                    throw;
                }
            }
        }


        public void Ping()
        {
            
        }
    }
}
