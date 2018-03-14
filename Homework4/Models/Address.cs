using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Homework4.Models
{
    class Address : IDataErrorInfo, INotifyPropertyChanged
    {
        private string zipCode = string.Empty;
        private string zipCodeError;
        private bool zipCodeValid = false;
        private Regex zipCodeRegex = new Regex(@"^(\d{5}(-\d{4})?|([A-Z][0-9]){3})$");

        public override string ToString() => zipCode;

        public string ZipCode
        {
            get => zipCode;
            set
            {
                if (zipCode != value)
                {
                    zipCode = value;
                    OnPropertyChanged("ZipCode");
                }
            }
        }

        public string ZipCodeError
        {
            get => zipCodeError;
            set
            {
                if (zipCodeError != value)
                {
                    zipCodeError = value;
                    OnPropertyChanged("ZipCodeError");
                }
            }
        }

        public bool ZipCodeValid
        {
            get => zipCodeValid;
            set
            {
                zipCodeValid = value;
                OnPropertyChanged("ZipCodeValid");
            }
        }

        public string Error
        {
            get => zipCodeError;
        }

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case "ZipCode":
                        {
                            ZipCodeError = "";
                            ZipCodeValid = false;

                            if (string.IsNullOrEmpty(ZipCode))
                            {
                                ZipCodeError = "Zip Code cannot be empty";
                            }

                            if (!zipCodeRegex.IsMatch(ZipCode))
                            {
                                ZipCodeError = "Invalid US/Canadian Zip Code";
                            }

                            if (ZipCodeError == "")
                            {
                                ZipCodeValid = true;
                            }

                            return ZipCodeError;
                        }
                }
                return string.Empty;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
