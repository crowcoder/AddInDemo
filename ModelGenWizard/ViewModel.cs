using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

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
}
