using System;
using Xamarin.Forms;

namespace ADFSOAuthsample.Presentation
{
	public class HomePage : ContentPage
	{
		Label label;

		public HomePage()
		{
			LoadView();
		}

		void LoadView()
		{
			this.Title = "Home";

			this.label = new Label() {
				XAlign = TextAlignment.Center,
				Text = App.IsLoggedIn ? "Welcome logged in user!" : "Your are not logged in.",
				FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label))
			};
					
			Content = new StackLayout() {
				VerticalOptions = LayoutOptions.StartAndExpand,
				Padding = new Thickness(4, 8),
				Children = { 
					label
				}
			};
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();

			if (!App.IsLoggedIn && !App.CancelledLogin) {
				var adLoginPage = new ADLoginPage();
				adLoginPage.Disappearing += (object sender, EventArgs e) => {
					if (Device.OS == TargetPlatform.Android) {
						// Work-around: OnAppearing called on PopModal only on iOS
						// Thus we call in manually on Android
						// http://forums.xamarin.com/discussion/18781/ios-and-android-different-behaviors-for-onappearing
						this.OnAppearing();
					}
				};
				Navigation.PushModalAsync(adLoginPage);
			} else {
				this.label.Text = App.IsLoggedIn ? "Welcome logged in user!" : "Your are not logged in.";
			}
		}
	}
}

