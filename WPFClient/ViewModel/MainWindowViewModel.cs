
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MvvmDialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TabcontrolTest.ViewModel;
using TabcontrolTest.Views;
using WPFClient.Helper.Tab;
using WPFClient.Model;
using WPFLocalizeExtension.Engine;

namespace WPFClient.ViewModel
{
    public sealed class MainWindowViewModel : ViewModelBase
    {
        #region Fields
        private readonly ObservableCollection<ITab> _tabs;
        private readonly ObservableCollection<Locale> _locales;

        private TabItem _selectedTab;
        private Locale _selectedLocale;
        private readonly IDialogService _dialogService;
        #endregion


        public MainWindowViewModel(IDialogService _dialogService)
        {

            this._dialogService = _dialogService;

            _tabs = new ObservableCollection<ITab>();
            _tabs.CollectionChanged += Tabs_CollectionChanged;

            _locales = new ObservableCollection<Locale>();
            _locales.Add(new Locale { LocaleCode = "it-IT", LocaleName = "Italiano" });
            _locales.Add(new Locale { LocaleCode = "en-US", LocaleName = "English" });

            _selectedLocale = _locales.ElementAt(0);
            LocalizeDictionary.Instance.Culture = new System.Globalization.CultureInfo(_selectedLocale.LocaleCode);

            NewTab = new RelayCommand(AddNewTab);
            LoadedCommand = new RelayCommand(LoadedHandler);
            AboutCommand = new RelayCommand(
                         () =>
                         {
                             ShowDialog(viewModel => _dialogService.ShowDialog<AboutView>(this, viewModel));
                         });
            BackCommand = new RelayCommand(BackCommandMethod);
        }

        public TabItem SelectedTab
        {
            get { return _selectedTab; }
            set
            {
                if (value != _selectedTab)
                {
                    _selectedTab = value;
                    RaisePropertyChanged("SelectedTab");
                }
            }
        }


        public Locale SelectedLocale
        {
            get { return _selectedLocale; }
            set
            {
                if (value != _selectedLocale)
                {
                    _selectedLocale = value;
                    RaisePropertyChanged("SelectedLocale");
                    LocalizeDictionary.Instance.Culture = new System.Globalization.CultureInfo(_selectedLocale.LocaleCode);
                }
            }
        }
        
        public ICommand NewTab { get; private set; }
        public ICommand LoadedCommand { get; private set; }

        public ICommand AboutCommand { get; private set; }

        private void ShowDialog(Func<AboutViewModel, bool?> showDialog)
        {
            var dialogViewModel = new AboutViewModel();
            showDialog(dialogViewModel);

        }

        public ICommand BackCommand
        {
            get; private set;
        }

        private ICommand _homeCommand;

        public ICommand HomeCommand
        {
            get
            {
                return _homeCommand
                    ?? (_homeCommand = new RelayCommand(
                         () =>
                         {
                             HomeCommandMethod();
                         }));
            }
        }

        private void HomeCommandMethod()
        {
            SelectedTab.NavigateTo(ViewModelLocator.Instance.LauncherViewModel);
        }

        private void BackCommandMethod()
        {
            _selectedTab.Back();
        }

        
        public ICommand WindowClosing
        {
            get
            {
                return new RelayCommand<CancelEventArgs>(
                    (args) => {
                        var result = _dialogService.ShowMessageBox(
                            this,
                            Translations.MainWindow.AreYouSureToExit,
                            Translations.MainWindow.Exit,
                            MessageBoxButton.YesNo
                            );

                        if (result == MessageBoxResult.No)
                        {
                            args.Cancel = true;
                        }
                    });
            }
        }

        private ICommand _closeTab;

        public ICommand CloseTab
        {
            get
            {
                return _closeTab
                    ?? (_closeTab = new RelayCommand(
                         () =>
                         {
                             CloseTabMethod();
                         }));
            }
        }

        public void CloseTabMethod()
        {
            if (null != _selectedTab)
            {
                _selectedTab.CloseCommand.Execute(null);
            }
        }


        private void Tabs_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            ITab tab;

            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    tab = (ITab)e.NewItems[0];
                    tab.CloseRequested += OnTabCloseRequested;
                    break;
                case NotifyCollectionChangedAction.Remove:
                    tab = (ITab)e.OldItems[0];
                    tab.CloseRequested -= OnTabCloseRequested;
                    break;
            }

        }


        private void AddNewTab()
        {
            TabItem tabItem = new TabItem();
            tabItem.TabName = "Launcher";
            Tabs.Add(tabItem);
            SelectedTab = tabItem;            
        }

        private void LoadedHandler()
        {
            AddNewTab();
        }

        public ObservableCollection<ITab> Tabs
        {
            get {
                return _tabs;
            }
            private set { }
        }

        public ObservableCollection<Locale> Locales
        {
            get
            {
                return _locales;
            }
            private set { }
        }

        private void OnTabCloseRequested(object sender, EventArgs e)
        {
            MessageBoxResult result = _dialogService.ShowMessageBox(
                this, 
                Translations.MainWindow.AreYouSureToCloseTheSelectedTab, 
                Translations.MainWindow.CloseTab, 
                MessageBoxButton.YesNo);

            if(result == MessageBoxResult.Yes)
            { 
                Tabs.Remove((ITab)sender);
            }
        }
    }
}