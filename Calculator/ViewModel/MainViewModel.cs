using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.ViewModel
{
    class MainViewModel : INotifyPropertyChanged
    {
        private string _value;

        public RelayCommand Reset { get; set; }
        public RelayCommand Point { get; set; }
        public RelayCommand Sign { get; set; }
        public ParameterizedRelayCommand Number { get; set; }
        public ParameterizedRelayCommand Operation { get; set; }

        public MainViewModel()
        {
            Value = "0";
            Number = new ParameterizedRelayCommand(
                param =>
                {
                    string newChar = param.ToString();
                    if (Value == "0" && newChar == "0")
                    {
                        Value = "0";
                    }
                    if (Value == "0")
                    {
                        Value = newChar;
                    }
                    else if (Value.EndsWith(".0"))
                    {
                        Value = Value.Remove(Value.Length - 1, 1) + newChar;
                    }
                    else
                    {
                        Value = Value + newChar;
                    }
                },
                param => true
                );
            Point = new RelayCommand(
                () =>
                {
                    Value += ".0";
                    Point.RaiseCanExecuteChanged();
                },
                () => (!Value.Contains("."))
                );
            Sign = new RelayCommand(
                () =>
                {
                    if (Value.StartsWith("-"))
                        Value = Value.Substring(1, Value.Length - 1);
                    else
                        Value = "-" + Value;
                },
                () => true
                );
        }
        public string Value
        {
            get
            {
                return Value;
            }
            set
            {
                _value = Value;
                NotifyPropertyChanged();
            }
        }
        #region Notify
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        #endregion 
    }
}
