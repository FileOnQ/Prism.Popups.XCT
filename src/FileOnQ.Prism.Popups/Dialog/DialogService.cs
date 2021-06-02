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
	public class DialogService : IDialogService
	{
		readonly IApplicationProvider applicationProvider;
		readonly IContainerProvider container;

		public DialogService(IApplicationProvider applicationProvider, IContainerProvider container)
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
			var dialog = container.Resolve<BasePopup>(name);
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

				// TODO - uncomment once merged to XCT
				// dialogAware?.OnDialogClosed(e.IsLightDismissed);

				var result = (IDialogParameters)e.Result ?? null;
				// TODO - uncomment once merged to XCT
				//var dialogResult = new DialogResult { Success = !e.IsLightDismissed, Parameters = result };
				var dialogResult = new DialogResult { Success = true, Parameters = result };

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
