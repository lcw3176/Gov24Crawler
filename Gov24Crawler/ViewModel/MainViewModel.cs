using Gov24Crawler.Command;
using System.Windows.Input;

namespace Gov24Crawler.ViewModel
{
    class MainViewModel : BaseViewModel
    {
        private BaseViewModel _selectedViewModel = new HomeViewModel();

        public BaseViewModel SelectedViewModel
        {
            get { return _selectedViewModel; }
            set
            {
                _selectedViewModel = value;
                OnPropertyUpdate(nameof(SelectedViewModel));
            }
        }

        public ICommand updateViewCommand { get; set; }

        public MainViewModel()
        {
            updateViewCommand = new UpdateViewCommand(this);
        }
    }
}
