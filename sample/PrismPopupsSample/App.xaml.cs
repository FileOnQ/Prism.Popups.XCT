using System;
using System.Globalization;
using System.Reflection;
using FileOnQ.Prism.Popups.XCT;
using Prism;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Unity;
using PrismPopupsSample.Views;

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
			containerRegistry.UseXctPopups();
			containerRegistry.RegisterForNavigation<MainPage>(nameof(MainPage));
			containerRegistry.RegisterDialog<SamplePopup>("Sample");
			containerRegistry.RegisterDialog<PopupView>("ContentView");
			ViewModelLocationProvider.SetDefaultViewTypeToViewModelTypeResolver(FindViewModel);
		}

		Type FindViewModel(Type viewType)
		{
			var viewName = string.Empty;

			if (viewType.FullName.EndsWith("Page"))
			{
				viewName = viewType.FullName
					.Replace("Page", string.Empty)
					.Replace("Views", "ViewModels");
			}
			else if (viewType.FullName.EndsWith("Popup"))
			{
				viewName = viewType.FullName
					.Replace("Popup", string.Empty)
					.Replace("Views", "ViewModels");
			}

			var viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;
			var viewModelName = string.Format(CultureInfo.InvariantCulture, "{0}ViewModel, {1}", viewName, viewAssemblyName);

			return Type.GetType(viewModelName);
		}
	}
}
