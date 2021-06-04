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
			// Note - Andrew Hoefling (6/4/2021)
			// The module is invoked via extension method and 
			// the OnInitialized method is not used or invoked.
		}

		public void RegisterTypes(IContainerRegistry containerRegistry)
		{
			containerRegistry.Register<IDialogService, XctDialogService>();
		}
	}
}
