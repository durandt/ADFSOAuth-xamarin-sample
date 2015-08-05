using System;

using Xamarin.Forms;

using ADFSOAuthsample.Presentation;

namespace ADFSOAuthsample
{
	public class App : Application
	{
		public App()
		{
			var profilePage = new HomePage();

			_NavPage = new NavigationPage(profilePage);

			MainPage = _NavPage;
		}

		static NavigationPage _NavPage;

		public static Page GetMainPage ()
		{
			var profilePage = new HomePage();

			_NavPage = new NavigationPage(profilePage);

			return _NavPage;
		}

		public static bool IsLoggedIn {
			get { return !string.IsNullOrWhiteSpace(_Token); }
		}

		static string _Token;
		public static string Token {
			get { return _Token; }
		}

		public static void SaveToken(string token)
		{
			_Token = token;
		}

		public static Action SuccessfulLoginAction
		{
			get {
				return new Action (() => {
					_NavPage.Navigation.PopModalAsync();
				});
			}
		}

		static bool _CancelledLogin = false;
		public static bool CancelledLogin {
			get { return _CancelledLogin; }
		}
		public static Action CancelledLoginAction
		{
			get {
				return new Action (() => {
					_CancelledLogin = true;
					_NavPage.Navigation.PopModalAsync();
				});
			}
		}

		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}

