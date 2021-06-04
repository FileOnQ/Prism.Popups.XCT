using Prism.Mvvm;
using PrismPopupsSample.ViewModels;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms.Xaml;

namespace PrismPopupsSample.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SamplePopup : Popup
	{
		public SamplePopup()
		{
			BindingContext = new SampleViewModel();
			InitializeComponent();
		}
	}
}