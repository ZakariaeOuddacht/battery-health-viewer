using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Battery_Health_Viewer.Views
{
    public class BatteryProperty : INotifyPropertyChanged
    {
        // A class that generates each property of the battery in a "Description | Value" format. Used by BatteryInfoProvider.cs
        public string Description { get; set; }

        private string _value;
        public string Value
        {
            get => _value;
            set
            {
                _value = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}