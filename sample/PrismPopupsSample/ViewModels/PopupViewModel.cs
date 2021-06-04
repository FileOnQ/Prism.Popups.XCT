using System;
using Prism.Services.Dialogs;

namespace PrismPopupsSample.ViewModels
{
	public class PopupViewModel : IDialogAware
	{
		public PopupViewModel()
		{

		}

		public event Action<IDialogParameters> RequestClose;

		public bool CanCloseDialog() => true;

		public void OnDialogClosed()
		{
		}

		public void OnDialogOpened(IDialogParameters parameters)
		{
		}
	}
}
