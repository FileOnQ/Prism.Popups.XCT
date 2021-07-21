using System;
using Prism.Services.Dialogs;
using Xamarin.Forms;

namespace FileOnQ.Prism.Popups.XCT
{
	public static class IDialogParametersExtensions
	{
		public static IDialogParameters SetSize(this IDialogParameters dialogParameters, double widthAndHeight) =>
			dialogParameters.SetSize(widthAndHeight, widthAndHeight);

		public static IDialogParameters SetSize(this IDialogParameters dialogParameters, double width, double height) =>
			dialogParameters.SetSize(new Size(width, height));

		public static IDialogParameters SetSize(this IDialogParameters dialogParameters, Size size)
		{
			if (dialogParameters == null)
				throw new ArgumentNullException(nameof(dialogParameters));

			dialogParameters.Add("Size", size);
			return dialogParameters;
		}

		public static IDialogParameters SetColor(this IDialogParameters dialogParameters, Color color)
		{
			if (dialogParameters == null)
				throw new ArgumentNullException(nameof(dialogParameters));

			dialogParameters.Add("Color", color);
			return dialogParameters;
		}

		public static IDialogParameters SetVerticalOptions(this IDialogParameters dialogParameters, LayoutOptions verticalOptions)
		{
			if (dialogParameters == null)
				throw new ArgumentNullException(nameof(dialogParameters));

			dialogParameters.Add("VerticalOptions", verticalOptions);
			return dialogParameters;
		}

		public static IDialogParameters SetHorizontalOptions(this IDialogParameters dialogParameters, LayoutOptions horizontalOptions)
		{
			if (dialogParameters == null)
				throw new ArgumentNullException(nameof(dialogParameters));

			dialogParameters.Add("HorizontalOptions", horizontalOptions);
			return dialogParameters;
		}

		public static IDialogParameters SetIsLightDismissEnabled(this IDialogParameters dialogParameters, bool isLightDismissEnabled)
		{
			if (dialogParameters == null)
				throw new ArgumentNullException(nameof(dialogParameters));

			dialogParameters.Add("IsLightDismissEnabled", isLightDismissEnabled);

			return dialogParameters;
		}
	}
}
