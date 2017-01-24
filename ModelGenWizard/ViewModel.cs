using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace ModelGenWizard
{
    public class ViewModel : INotifyPropertyChanged
    {
        public ViewModel()
        {
            GoButton = new GoCommand();
        }

        private string _ServerName;
        private string _DatabaseName;
        private string _TableName;

        public string ServerName
        {
            get { return this._ServerName; }
            set { this._ServerName = value; NotifyPropertyChanged(); }
        }
        public string DatabaseName
        {
            get { return this._DatabaseName; }
            set { this._DatabaseName = value; NotifyPropertyChanged(); }
        }
        public string TableName
        {
            get { return this._TableName; }
            set { this._TableName = value; NotifyPropertyChanged(); }
        }

        public string GeneratedCode { get; set; }

        public GoCommand GoButton { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class GoCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            Window window = parameter as Window;
            ViewModel vm = window.DataContext as ViewModel;

            //Call to a very complex database parsing routine here....

            vm.GeneratedCode = @"public int ContactID { get; set; }
        public string ContactName { get; set; }
        public string ContactPhone { get; set; }";

            window.Close();

        }
    }
}
