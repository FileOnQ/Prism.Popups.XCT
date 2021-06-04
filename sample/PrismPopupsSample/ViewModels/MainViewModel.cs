using System.Windows.Input;
using Prism.Commands;
using Prism.Services.Dialogs;
using Xamarin.Forms;

namespace PrismPopupsSample.ViewModels
{
	public class MainViewModel
	{
		readonly IDialogService dialogService;
		public MainViewModel(IDialogService dialogService)
		{
			this.dialogService = dialogService;

			OpenPopup = new DelegateCommand(() => OnOpenPopup("Sample"));
			OpenContentView = new DelegateCommand(() => OnOpenPopup("ContentView"));
		}

		public ICommand OpenPopup { get; }
		public ICommand OpenContentView { get; }

		void OnOpenPopup(string name)
		{
			var parameters = new DialogParameters
			{
				{ "Size", new Size(125, 250) }
			};

			this.dialogService.ShowDialog(name, parameters);
		}
	}
}
