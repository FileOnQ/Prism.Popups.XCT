using System;
using System.Windows.Input;
using Prism.Commands;
using Prism.Services.Dialogs;

namespace PrismPopupsSample.ViewModels
{
	public class SampleViewModel : IDialogAware
	{
		public SampleViewModel()
		{
			Close = new DelegateCommand(OnClose);
		}

		public ICommand Close { get; }

		void OnClose() => RequestClose(null);


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
