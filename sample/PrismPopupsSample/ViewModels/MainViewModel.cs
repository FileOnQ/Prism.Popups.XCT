using System.Windows.Input;
using Prism.Commands;
using PrismPopupsSample.Views;
using Xamarin.CommunityToolkit.Extensions;

namespace PrismPopupsSample.ViewModels
{
	public class MainViewModel
	{
		public MainViewModel()
		{
			OpenPopup = new DelegateCommand(OnOpenPopup);
		}

		public ICommand OpenPopup { get; }

		void OnOpenPopup()
		{
			// TODO - update this to use IDialog
			App.Current.MainPage.Navigation.ShowPopup(new SamplePopup());
		}
	}
}
