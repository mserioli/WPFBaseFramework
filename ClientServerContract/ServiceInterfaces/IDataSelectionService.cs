using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace ClientServerContract.ServiceInterfaces
{
    [ServiceContract]
    public interface IDataSelectionService
    {
        [OperationContract]
        void Ping();
    }
}
