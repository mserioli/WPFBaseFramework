using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MvvmDialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TabcontrolTest.Model;
using TabcontrolTest.ViewModel;
using TabcontrolTest.Views;
using WPFClient.Helper.Tab;

namespace WPFClient.ViewModel
{
    public sealed class TabItem1ViewModel :  TabbedViewModelBase
    {
        private readonly IDialogService _dialogService;
        private TabItem1Model _tabItem1Model;
        private TabItem1FilterModel _filterModel;
        public ICommand LoadedCommand { get; private set; }

        public ICommand SearchCommand { get; }
        

        public TabItem1ViewModel(IDialogService _dialogService, MainWindowViewModel _parent) : base(_parent)
        {
            this._dialogService = _dialogService;
            SearchCommand = new RelayCommand(SearchCommandHandler);
            LoadedCommand = new RelayCommand(LoadedCommandHandler);
            _tabItem1Model = new TabItem1Model();

            //TODO avoid calling this in the contructor! It's an ugly Practice
            //ShowDialog(viewModel => _dialogService.ShowDialog<TabItem1FilterView>(ViewModelLocator.Instance.MainWindowViewModel, viewModel));
        }

        private void LoadedCommandHandler()
        {
            ShowDialog(viewModel => _dialogService.ShowDialog<TabItem1FilterView>(this, viewModel));
        }

        private void SearchCommandHandler()
        {   
            NavigateTo(ViewModelLocator.Instance.TabItem1ViewModel);
        }

        private void ShowDialog(Func<TabItem1FilterViewModel, bool?> showDialog)
        {
            if (_filterModel == null)
            {
                _filterModel = new TabItem1FilterModel();
            }
           
            bool? success = showDialog(new TabItem1FilterViewModel(_filterModel));
            if (success == true)
            {
                TabItem1Model tabItem1Model = new TabItem1Model();
                tabItem1Model.Parameter1 = _filterModel.Parameter1;
                tabItem1Model.Parameter2 = _filterModel.Parameter2;
                TabItem1Model = tabItem1Model;
            }
            else {
                Back(); 
            }
        }

        public override string TabName
        {
            get
            {
                return "TabItem1";
            }
        }

        public TabItem1Model TabItem1Model
        {
            get
            {
                return _tabItem1Model;
            }
            set
            {
                if (_tabItem1Model != value)
                {
                    _tabItem1Model = value;
                    RaisePropertyChanged("TabItem1Model");
                }
            }
        }

       
        public string InstanceHashCode {
            get {
                return this.GetHashCode().ToString();
            }
        }

    }
}
