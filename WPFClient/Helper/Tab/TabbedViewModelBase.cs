using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFClient.ViewModel;

namespace WPFClient.Helper.Tab
{
    public abstract class TabbedViewModelBase : ViewModelBase
    {
        protected readonly MainWindowViewModel _parent;

        public TabbedViewModelBase(MainWindowViewModel _parent) {
            this._parent = _parent;
        }
        public abstract string TabName { get; }

        public void NavigateTo(TabbedViewModelBase to) {
            this._parent.SelectedTab.NavigateTo(to);
            this._parent.RaisePropertyChanged("Tabs");
        }

        public void Back()
        {
            this._parent.SelectedTab.Back();
        }
    }
}
