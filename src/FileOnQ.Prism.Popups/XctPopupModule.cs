using FileOnQ.Prism.Popups.XCT.Dialogs;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Services.Dialogs;

namespace FileOnQ.Prism.Popups.XCT
{
	public class XctPopupModule : IModule
	{
		public void OnInitialized(IContainerProvider containerProvider) 
		{
		}

		public void RegisterTypes(IContainerRegistry containerRegistry)
		{
			containerRegistry.Register<IDialogService, XctDialogService>();
		}
	}
}
