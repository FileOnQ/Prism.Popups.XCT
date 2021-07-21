using System;
using System.Threading.Tasks;
using Prism.Common;
using Prism.Ioc;
using Prism.Navigation;
using Prism.Services.Dialogs;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;

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
			var element = container.Resolve<object>(name);
			
			BasePopup dialog;
			object bindingContext;
			if (element != null && element is BasePopup basePopup)
			{
				dialog = basePopup;
				bindingContext = dialog.BindingContext;
			}
			else if (element != null && element is View view)
			{
				dialog = new Popup 
				{
					Content = view,
					Size = new Size(250, 250)
				};

				bindingContext = view.BindingContext;
			}
			else
				return;

			if (parameters != null)
			{
				if (parameters.ContainsKey("Size"))
					dialog.Size = parameters.GetValue<Size>("Size");

				if (parameters.ContainsKey("Color"))
					dialog.Color = parameters.GetValue<Color>("Color");

				if (parameters.ContainsKey("VerticalOptions"))
					dialog.VerticalOptions = parameters.GetValue<LayoutOptions>("VerticalOptions");

				if (parameters.ContainsKey("HorizontalOptions"))
					dialog.HorizontalOptions = parameters.GetValue<LayoutOptions>("HorizontalOptions");

				if (parameters.ContainsKey("IsLightDismissEnabled"))
					dialog.IsLightDismissEnabled = parameters.GetValue<bool>("IsLightDismissEnabled");
			}

			IDialogAware dialogAware = null;
			if (bindingContext is IDialogAware)
			{
				dialogAware = (IDialogAware)bindingContext;
				dialogAware?.OnDialogOpened(parameters);
				dialogAware.RequestClose += Dialog_RequestClose;
			}

			dialog.Dismissed += Dialog_Dismissed;
			applicationProvider.MainPage.Navigation.ShowPopup(dialog);

			void Dialog_Dismissed(object sender, PopupDismissedEventArgs e)
			{
				// todo - check hardware back button if light dismissed.
				//		  maybe it shouldn't close
				// note - This is something that needs to be handled upstrea
				//        with a new API in the XCT Popup Renderer.

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
