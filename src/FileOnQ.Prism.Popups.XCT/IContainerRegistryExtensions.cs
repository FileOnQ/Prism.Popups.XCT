using Prism.Ioc;

namespace FileOnQ.Prism.Popups.XCT
{
	public static class IContainerRegistryExtensions
	{
		public static void UseXctPopups(this IContainerRegistry containerRegistry)
		{
			var module = new XctPopupModule();
			module.RegisterTypes(containerRegistry);
		}
	}
}
