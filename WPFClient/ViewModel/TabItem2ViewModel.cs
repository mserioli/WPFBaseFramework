using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFClient.Helper.Tab;
using System.Windows.Input;
using System.Collections.ObjectModel;
using Data.RepositoryPattern.Services;
using Data;
using GalaSoft.MvvmLight.Command;
using MvvmDialogs;

namespace WPFClient.ViewModel
{
    public sealed class TabItem2ViewModel : TabbedViewModelBase
    {
        private readonly RelayCommand<PHU> _showPHUTubes;
        private readonly PhuService _phuService;
        private readonly IDialogService _dialogService;

        public  TabItem2ViewModel(IDialogService _dialogService, MainWindowViewModel _parent) : base(_parent)
        {
            this._phuService = new PhuService();
            this._loaded = new RelayCommand(LoadedCommandHandler);
            this._dialogService = _dialogService;
            this._showPHUTubes = new RelayCommand<PHU>(ShowPHUTubesCommandHandler);
            
        }

        public RelayCommand<PHU> ShowPHUTubes
        {
            get
            {
                return _showPHUTubes;
            }
            private set { }
        }

        private void ShowPHUTubesCommandHandler(PHU phu)
        {
            _dialogService.ShowMessageBox(this._parent, phu.PHUID);
        }

        private async void LoadedCommandHandler()
        {
            PHUList = new ObservableCollection<PHU>(
                await _phuService.FindAll());
        }

        private string _parameterA;
        private string _parameterB;
        private bool _parameterC;

        private ICommand _loaded;

        public ICommand LoadedCommand 
        {
            get{
                return _loaded;
            }
        }
        

        private ObservableCollection<PHU> _PHUList;

        public ObservableCollection<PHU> PHUList
        {
            get
            {
                return _PHUList;
            }
            set
            {
                if (_PHUList != value)
                {
                    _PHUList = value;
                    RaisePropertyChanged("PHUList");
                }
            }
        }

        public override string TabName
        {
            get
            {
                return "TabItem2";
            }
        }

        public string ParameterA
        {
            get
            {
                return _parameterA;
            }
            set
            {
                if (_parameterA != value)
                {
                    _parameterA = value;
                    RaisePropertyChanged("ParameterB");
                }
            }
        }

        public string ParameterB
        {
            get
            {
                return _parameterB;
            }
            set
            {
                if (_parameterB != value)
                {
                    _parameterB = value;
                    RaisePropertyChanged("ParameterB");
                }
            }
        }

        public bool ParameterC
        {
            get
            {
                return _parameterC;
            }
            set
            {
                if (_parameterC != value)
                {
                    _parameterC = value;
                    RaisePropertyChanged("ParameterC");
                }
            }
        }

        public string InstanceHashCode
        {
            get
            {
                return this.GetHashCode().ToString();
            }
        }

        public override string ToString()
        {
            return GetHashCode().ToString();
        }
    }
}
