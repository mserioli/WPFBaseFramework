using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using MvvmDialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabcontrolTest.Model;
using TabcontrolTest.ViewModel;

namespace WPFClient.ViewModel
{
    public class ViewModelLocator
    {

        private static ViewModelLocator _instance;

        public static ViewModelLocator Instance
        {
            get
            {
                return _instance;
            }
        }

        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<IDialogService>(() => new DialogService());
            SimpleIoc.Default.Register<MainWindowViewModel>();
            SimpleIoc.Default.Register<LauncherViewModel>();
            SimpleIoc.Default.Register<AboutViewModel>();
            SimpleIoc.Default.Register<TabItem1FilterViewModel>();
            SimpleIoc.Default.Register<TabItem1ViewModel>();
            SimpleIoc.Default.Register<TabItem2ViewModel>();

            SimpleIoc.Default.Register<TabItem1FilterModel>();

            _instance = this;

        }

        public MainWindowViewModel MainWindowViewModel
        {
            get
            {
                return SimpleIoc.Default.GetInstance<MainWindowViewModel>();                
            }
        }

        public LauncherViewModel LauncherViewModel
        {
            get
            {
                return new LauncherViewModel(MainWindowViewModel);
            }
        }

        public TabItem1ViewModel TabItem1ViewModel
        {
            get
            {
                return new TabItem1ViewModel(SimpleIoc.Default.GetInstance<IDialogService>(), MainWindowViewModel);
            }
        }

        public TabItem2ViewModel TabItem2ViewModel
        {
            get
            {
                return new TabItem2ViewModel(SimpleIoc.Default.GetInstance<IDialogService>(), MainWindowViewModel);                
            }
        }

        public AboutViewModel AboutWindowViewModel
        {
            get
            {
                return SimpleIoc.Default.GetInstance<AboutViewModel>();
            }
        }

        public TabItem1FilterViewModel TabItem1FilterViewModel
        {
            get
            {
                return SimpleIoc.Default.GetInstance<TabItem1FilterViewModel>();
            }
        }
    }
}
