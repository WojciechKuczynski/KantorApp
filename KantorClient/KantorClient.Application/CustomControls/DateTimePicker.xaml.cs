using Prism.Commands;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace KantorClient.Application.CustomControls
{
    public partial class DateTimePicker : UserControl, INotifyPropertyChanged
    {
        public DateTimePicker()
        {
            InitializeComponent();
            (Content as FrameworkElement).DataContext = this;
        }

        #region SelectedTime

        public DateTime? SelectedDateTime
        {
            get
            {
                return (DateTime?)GetValue(SelectedDateTimeProperty);
            }
            set
            {
                SetValue(SelectedDateTimeProperty, value);
            }
        }

        public static readonly DependencyProperty
            SelectedDateTimeProperty =
            DependencyProperty.Register("SelectedDateTime",
            typeof(DateTime?),
            typeof(DateTimePicker));

        #endregion

        private bool _isPopupOpen;
        public bool IsPopupOpen
        {
            get { return _isPopupOpen; }
            set
            {
                SetProperty(ref _isPopupOpen, value);
            }
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        protected virtual void SetProperty<T>(ref T member, T val,
           [CallerMemberName] string propertyName = null)
        {
            if (object.Equals(member, val)) return;

            member = val;
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion INotifyPropertyChanged

        #region ChangePopup Command
        private ICommand _changePopupStatusCommand;
        public ICommand ChangePopupStatusCommand
        {
            get { return _changePopupStatusCommand ?? (_changePopupStatusCommand = new DelegateCommand<object>(ChangePopupStatus)); }
        }
        private void ChangePopupStatus(object obj)
        {
            IsPopupOpen = !IsPopupOpen;
        }
        #endregion

        #region ChangeTime Command
        private ICommand _chageTimeCommand;
        public ICommand ChangeTimeCommand
        {
            get { return _chageTimeCommand ?? (_chageTimeCommand = new DelegateCommand<object>(ChangeTime)); }
        }
        private void ChangeTime(object obj)
        {
            if (!SelectedDateTime.HasValue)
                SelectedDateTime = DateTime.Now;
            switch (obj.ToString())
            {
                case "addHour":
                    SelectedDateTime = SelectedDateTime.Value.AddHours(1);
                    break;
                case "addMinute":
                    SelectedDateTime = SelectedDateTime.Value.AddMinutes(1);
                    break;
                case "subHour":
                    SelectedDateTime = SelectedDateTime.Value.AddHours(-1);
                    break;
                case "subMinute":
                    SelectedDateTime = SelectedDateTime.Value.AddMinutes(-1);
                    break;
            }
        }
        #endregion
    }
}