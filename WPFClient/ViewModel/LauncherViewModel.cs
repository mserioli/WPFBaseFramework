using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPFClient.Helper.Tab;
using System.Windows.Input;

namespace WPFClient.ViewModel
{
    public class LauncherViewModel : TabbedViewModelBase
    {
        
        public LauncherViewModel(MainWindowViewModel _parent) : base (_parent)
        {
            NavCommand = new RelayCommand<string>(NavCommandHandler);
            
        }

        private void NavCommandHandler(string destination)
        {
            switch (destination)
            {
                case "TabItem1":
                    NavigateTo(ViewModelLocator.Instance.TabItem1ViewModel);
                    break;
                case "TabItem2":
                    NavigateTo(ViewModelLocator.Instance.TabItem2ViewModel);
                    break;
                default:
                    break;
            }
        }

        public RelayCommand<string> NavCommand { get; private set; }

        

        public override string TabName
        {
            get
            {
                return "Launcher";
            }
        }
        

        public string InstanceHashCode {
           get {
                return this.GetHashCode().ToString();
           }
        } 

    }
    
}
