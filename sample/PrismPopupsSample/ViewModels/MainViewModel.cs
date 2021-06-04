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

			OpenPopup = new DelegateCommand(() => OnOpenPopup("SamplePopup"));
			OpenContentView = new DelegateCommand(() => OnOpenPopup("PopupView"));
		}

		public ICommand OpenPopup { get; }
		public ICommand OpenContentView { get; }

		void OnOpenPopup(string name)
		{
			this.dialogService.ShowDialog(name);
		}
	}
}
