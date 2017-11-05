using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using XMightUICommon;

namespace WPF_MVVM
{
    public class MainWindowViewModel : ViewModelBase
    {
        private Dispatcher _thisDispatcher;

        private RelayCommand _toggleCommand;
        public RelayCommand ToggleCommand { get { return _toggleCommand; } }

        private RelayCommand _canExecuteExampleCommand;
        public RelayCommand CanExecuteExampleCommand { get { return _canExecuteExampleCommand; } }

        private bool _canExecute;
        public bool CanExecute
        {
            get { return _canExecute; }
            protected set
            {
                this.SetProperty(ref _canExecute, value, "CanExecute");
            }
        }

        private String _logMessage;
        public String LogMessage
        {
            get { return _logMessage; }
            protected set
            {
                this.SetProperty(ref _logMessage, value, "LogMessage");
                _logMessage = String.Empty;
            }
        }

        private bool _loadInProgress;
        public bool LoadInProgress
        {
            get { return _loadInProgress; }
            protected set
            {
                SetProperty(ref _loadInProgress, value, "LoadInProgress");
            }
        }

        public MainWindowViewModel()
        {
            _toggleCommand = new RelayCommand(ExecuteToggleCommand);
            _canExecuteExampleCommand = new RelayCommand(ExecuteCanExecute, CanExecuteCanExecute);
            _canExecute = false;
            _thisDispatcher = Dispatcher.CurrentDispatcher;
        }

        private void ExecuteToggleCommand()
        {
            Task.Run(() => { LoadInProgress = true; Thread.Sleep(3000); LoadInProgress = false; }).
                ContinueWith((t) => 
                {
                    _thisDispatcher.Invoke(() =>
                    {
                        LogMessage = "You can now Execute CanExecute!";
                        CanExecute = true;
                        CanExecuteExampleCommand.RaiseCanExecuteChanged();
                    });
                });
        }

        private void ExecuteCanExecute()
        {
            CanExecute = false;
            CanExecuteExampleCommand.RaiseCanExecuteChanged();
            LogMessage = "You will not be able to execute can execute untile you toggle again!";
        }

        private bool CanExecuteCanExecute()
        {
            return CanExecute;
        }
    }
}
