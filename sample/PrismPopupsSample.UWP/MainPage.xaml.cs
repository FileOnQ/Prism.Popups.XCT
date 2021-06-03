using Prism;
using Prism.Ioc;

namespace PrismPopupsSample.UWP
{
	public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();

            LoadApplication(new PrismPopupsSample.App(new AppInitializer()));
        }

		class AppInitializer : IPlatformInitializer
		{
			public void RegisterTypes(IContainerRegistry containerRegistry)
			{
			}
		}
	}
}
