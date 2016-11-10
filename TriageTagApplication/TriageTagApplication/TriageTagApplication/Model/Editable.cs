using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace TriageTagApplication.Model
{
    class Editable: INotifyPropertyChanged
    {
        bool isEditable;

        public event PropertyChangedEventHandler PropertyChanged;

        public bool IsEditable {
            get {
                return isEditable;
            }
            set {
                if(isEditable != value ) {
                    isEditable = value;
                    OnPropertyChanged( "IsEditable" );
                }
            }
        }

        public void OnPropertyChanged(string propertyName ) {
            if(PropertyChanged != null ) {
                PropertyChanged( this, new PropertyChangedEventArgs( propertyName ) );
            }
        }
    }
}
