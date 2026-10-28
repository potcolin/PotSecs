using PotSecs.Simulator.Services;
using Prism.Mvvm;

namespace PotSecs.Simulator.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly IMessageService _messageService;
        private string _title = "PotSecs/Simulator";
        private string _welcomeText;

        public MainWindowViewModel(IMessageService messageService)
        {
            _messageService = messageService;
            _welcomeText = _messageService.GetWelcomeText();
        }

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public string WelcomeText
        {
            get => _welcomeText;
            set => SetProperty(ref _welcomeText, value);
        }
    }
}
