using System.Windows.Input;
using FileOnQ.Prism.Popups.XCT;
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
			var parameters = new DialogParameters()
				.SetSize(125, 250)
				.SetColor(Color.Purple)
				.SetHorizontalOptions(LayoutOptions.CenterAndExpand)
				.SetVerticalOptions(LayoutOptions.CenterAndExpand)
				.SetIsLightDismissEnabled(true);

			this.dialogService.ShowDialog(name, parameters);
		}
	}
}
