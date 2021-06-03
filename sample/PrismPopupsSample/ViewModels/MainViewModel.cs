using System.Windows.Input;
using Prism.Commands;
using Prism.Services.Dialogs;

namespace PrismPopupsSample.ViewModels
{
	public class MainViewModel
	{
		readonly IDialogService dialogService;
		public MainViewModel(IDialogService dialogService)
		{
			this.dialogService = dialogService;

			OpenPopup = new DelegateCommand(OnOpenPopup);
		}

		public ICommand OpenPopup { get; }

		void OnOpenPopup()
		{
			this.dialogService.ShowDialog("SamplePopup");
		}
	}
}
