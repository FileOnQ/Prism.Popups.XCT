using System;
using Prism.Services.Dialogs;

namespace FileOnQ.Prism.Popups.Dialog
{
	class DialogService : IDialogService
	{
		public void ShowDialog(string name, IDialogParameters parameters, Action<IDialogResult> callback) => throw new NotImplementedException();
	}
}
