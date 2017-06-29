namespace WPFClient.Helper.Tab
{
    using System;
    using System.Windows.Input;

    public interface ITab
    {
        string TabName { get; set; }

        TabbedViewModelBase TabContent { get; set; }

        ICommand CloseCommand { get; }

        void NavigateTo(TabbedViewModelBase to);

        void Back();

        event EventHandler CloseRequested;
    }
}