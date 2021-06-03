using Prism.Mvvm;
using PrismPopupsSample.ViewModels;
using Xamarin.Forms.Xaml;

namespace PrismPopupsSample.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SamplePopup
	{
		public SamplePopup()
		{
			BindingContext = new SampleViewModel();
			InitializeComponent();
		}
	}
}