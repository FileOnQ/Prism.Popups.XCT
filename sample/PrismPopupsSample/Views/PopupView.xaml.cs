using PrismPopupsSample.ViewModels;
using Xamarin.Forms.Xaml;

namespace PrismPopupsSample.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PopupView
	{
		public PopupView()
		{
			BindingContext = new PopupViewModel();
			InitializeComponent();
		}
	}
}