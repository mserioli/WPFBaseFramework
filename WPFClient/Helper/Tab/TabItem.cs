using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFClient.Helper.Tab;
using System.Windows.Input;
using WPFClient.ViewModel;

namespace WPFClient
{
    public class TabItem : ViewModelBase, ITab
    {
        public TabItem()
        {
            CloseCommand = new ActionCommand(p => CloseRequested?.Invoke(this, EventArgs.Empty));
            NavigateTo(ViewModelLocator.Instance.LauncherViewModel);
        }

        private string _tabName;

        public string TabName {
            get
            {
                return _tabName;
            }
            set
            {
                if (_tabName != value)
                {
                    _tabName = value;
                    RaisePropertyChanged(TabName);
                }
            }

        }

        private List<TabbedViewModelBase> _history = new List<TabbedViewModelBase>();

        private TabbedViewModelBase _tabContent;

        public event EventHandler CloseRequested;

        public TabbedViewModelBase TabContent {
            get {
                return _tabContent;
            }
            set
            {
                if (_tabContent != value) { 
                    _tabContent = value;
                    TabName = _tabContent.TabName;
                    RaisePropertyChanged("TabContent");
                }
            }

        }
        

        public ICommand CloseCommand
        {
            get;
        }

        public void Back()
        {
            if (_history.Count > 1)
            {
                _history.Remove(_history.Last());
                TabContent = _history.Last();                
            }
        }

        public void NavigateTo(TabbedViewModelBase to)
        {
            TabContent = to;
            TabName = to.TabName;
            _history.Add(_tabContent);
            if (_history.Count > 5)
            {
                _history.RemoveAt(0);
            }
        }
    }
}