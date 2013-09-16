namespace RecepiesClient.ViewModels
{
    using System;
    using System.ComponentModel;
    using System.Windows.Input;

    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}