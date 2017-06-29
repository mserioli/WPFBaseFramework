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
    public class TabItem1ViewModel :  TabbedViewModelBase
    {
        private readonly IDialogService _dialogService;
        private TabItem1Model _tabItem1Model;
        private TabItem1FilterModel filterModel = new TabItem1FilterModel();

        public ICommand SearchCommand { get; }
        

        public TabItem1ViewModel(IDialogService _dialogService, MainWindowViewModel _parent) : base(_parent)
        {
            this._dialogService = _dialogService;
            SearchCommand = new RelayCommand(SearchCommandHandler);
            ShowDialog(viewModel => _dialogService.ShowDialog<TabItem1FilterView>(ViewModelLocator.Instance.MainWindowViewModel, viewModel));
        }

        private void SearchCommandHandler()
        {
            //ShowDialog(viewModel => _dialogService.ShowDialog<TabItem1Filter>(ViewModelLocator.Instance.MainWindowViewModel, viewModel));
            NavigateTo(ViewModelLocator.Instance.TabItem1ViewModel);
        }

        private void ShowDialog(Func<TabItem1FilterViewModel, bool?> showDialog)
        {
            var dialogViewModel = new TabItem1FilterViewModel(filterModel);

            bool? success = showDialog(dialogViewModel);
            if (success == true)
            {
                _tabItem1Model = new TabItem1Model();
                _tabItem1Model.Parameter1 = filterModel.Parameter1;
                _tabItem1Model.Parameter2 = filterModel.Parameter2;
            }
            else {
                    
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
