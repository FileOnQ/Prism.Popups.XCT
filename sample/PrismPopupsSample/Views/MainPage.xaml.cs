using Prism.Mvvm;
using Xamarin.Forms;

namespace PrismPopupsSample.Views
{
	public partial class MainPage : ContentPage
    {
        public MainPage()
        {
			ViewModelLocator.SetAutowireViewModel(this, true);
            InitializeComponent();
        }
    }
}
