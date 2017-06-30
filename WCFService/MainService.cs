using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using WCFService.ServicesInterfacesImpl;

namespace WCFService
{
    public partial class MainService : ServiceBase
    {
        public MainService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
#if DEBUG
            Thread.Sleep(15000);
#endif


            DataSelectionService.StartService();

        }

        protected override void OnStop()
        {
            DataSelectionService.CloseService();
        }
    }
}
