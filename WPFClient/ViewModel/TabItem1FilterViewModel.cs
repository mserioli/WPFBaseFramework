using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MvvmDialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Input;
using TabcontrolTest.Model;

namespace TabcontrolTest.ViewModel
{
    public sealed class TabItem1FilterViewModel : ViewModelBase, IModalDialogViewModel
    {

        private TabItem1FilterModel _filter;

        public TabItem1FilterViewModel(TabItem1FilterModel _filter)
        {
            ConfirmCommand = new RelayCommand(ConfirmCommandHandler, ConfirmCommandCanExecuteHandler);
            CancelCommand = new RelayCommand(CancelCommandHandler);
            Filter = _filter;
            
        }

        private bool ConfirmCommandCanExecuteHandler()
        {
            return IsFilterValid();
        }

        private bool IsFilterValid()
        {
            /*Type type = _filter.GetType();
            PropertyInfo[] properties = type.GetProperties();
            foreach (PropertyInfo property in properties)
            {
                if (null != property.GetValue(_filter))
                {
                    return true;
                }
            }
            return false;*/
            return true;
        }

        public RelayCommand ConfirmCommand { get; }
        public ICommand CancelCommand { get; }

        private bool? dialogResult;

        public TabItem1FilterModel Filter
            
        {
            get { return _filter; }
            set
            {
                if (value != _filter)
                {
                    _filter = value;
                    RaisePropertyChanged("Filter");
                }
            }
        }
        

        private void CancelCommandHandler()
        {
            //if (!string.IsNullOrEmpty(Text))
            {
                DialogResult = false;
            }
        }

        private void ConfirmCommandHandler()
        {
            //if (!string.IsNullOrEmpty(Text))
            {
                DialogResult = true;
            }
        }

        public bool? DialogResult
        {
            get { return dialogResult; }
            private set { Set(nameof(DialogResult), ref dialogResult, value); }
        }

    }
}
