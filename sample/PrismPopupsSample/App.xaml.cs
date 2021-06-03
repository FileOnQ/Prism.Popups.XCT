using Prism;
using Prism.Ioc;
using Prism.Unity;

namespace PrismPopupsSample
{
	public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null) : base(initializer) =>
            InitializeComponent();

		protected override void OnInitialized()
		{
			NavigationService.NavigateAsync(nameof(MainPage));
		}

		protected override void RegisterTypes(IContainerRegistry containerRegistry)
		{
			containerRegistry.RegisterForNavigation<MainPage>(nameof(MainPage));
		}
	}
}
