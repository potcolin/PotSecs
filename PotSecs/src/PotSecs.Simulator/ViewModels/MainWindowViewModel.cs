using PotSecs.Simulator.Services;
using Prism.Mvvm;

namespace PotSecs.Simulator.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly IMessageService MessageService;
        private string _title = "PotSecs[Simulator]";

        public MainWindowViewModel(IMessageService messageService)
        {
            this.MessageService = messageService;
        }

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }
    }
}
