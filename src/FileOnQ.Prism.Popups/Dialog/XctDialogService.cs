using System;
using System.Threading.Tasks;
using Prism.Common;
using Prism.Ioc;
using Prism.Navigation;
using Prism.Services.Dialogs;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.CommunityToolkit.UI.Views;

namespace FileOnQ.Prism.Popups.XCT.Dialogs
{
	public class XctDialogService : IDialogService
	{
		readonly IApplicationProvider applicationProvider;
		readonly IContainerProvider container;

		public XctDialogService(IApplicationProvider applicationProvider, IContainerProvider container)
		{
			this.applicationProvider = applicationProvider;
			this.container = container;
		}

		public void ShowDialog(string name)
		{
			ShowDialog(name, null, null);
		}

		public void ShowDialog(string name, IDialogParameters parameters)
		{
			ShowDialog(name, parameters, null);
		}

		public void ShowDialog(string name, IDialogParameters parameters, Action<IDialogResult> callback)
		{
			// todo - see if we can resolve as `BasePopup`
			// resolve any View and if it isn't a BasePopup wrap it into one
			var dialog = (BasePopup)container.Resolve<object>(name);
			if (dialog == null)
				return;

			IDialogAware dialogAware = null;
			if (dialog.BindingContext is IDialogAware)
			{
				dialogAware = (IDialogAware)dialog.BindingContext;
				dialogAware?.OnDialogOpened(parameters);
				dialogAware.RequestClose += Dialog_RequestClose;
			}

			dialog.Dismissed += Dialog_Dismissed;
			applicationProvider.MainPage.Navigation.ShowPopup(dialog);

			void Dialog_Dismissed(object sender, PopupDismissedEventArgs e)
			{
				// todo - check hardware back button if light dismissed.
				//		  maybe it shouldn't close

				// todo - Andrew Hoefling (6/3/2021)
				// We don't use the IsLightDismissed in the VMs but it still
				// may be useful. Let's revisit this later
				//dialogAware?.OnDialogClosed(e.IsLightDismissed);
				dialogAware?.OnDialogClosed();

				var result = (IDialogParameters)e.Result ?? null;
				var dialogResult = new DialogResult { Success = !e.IsLightDismissed, Parameters = result };
				
				if (callback != null)
					Task.Run(() => callback.Invoke(dialogResult));

				if (dialogAware != null)
					dialogAware.RequestClose -= Dialog_RequestClose;

				dialog.Dismissed -= Dialog_Dismissed;
			}

			void Dialog_RequestClose(IDialogParameters currentParameters)
			{
				if (dialogAware.CanCloseDialog())
				{
					if (dialog is Popup popup)
						popup.Dismiss(currentParameters);
					else if (dialog is Popup<IDialogParameters> prismPopup)
						prismPopup.Dismiss(currentParameters);
				}
			}
		}

		class DialogResult : NavigationResult, IDialogResult
		{
			public IDialogParameters Parameters { get; set; }
		}
	}
}
