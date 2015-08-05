using System;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Auth;

using ADFSOAuthsample.Presentation;
using ADFSOAuthsample.iOS.Presentation;

[assembly: ExportRenderer(typeof(ADLoginPage), typeof(ADLoginPageRenderer))]

namespace ADFSOAuthsample.iOS.Presentation
{
	public class ADLoginPageRenderer : PageRenderer
	{
		public ADLoginPageRenderer()
		{
		}

		public override void ViewDidAppear(bool animated)
		{
			base.ViewDidAppear(animated);

			/**
			 * clientId, resource and callbackUrl have to match with the AD application
			 * you created in your AD.
			 * 		clientId can be any string, something like your app identifier 
			 * 		resource can also more or less be any string, has to do with the "Relying Party" in ADFS
			 * 			in our case we went for something like https://domain/some/path but it could really be anything
			 * 		callbackUrl is the URL that OAuth2 sends you to after you have authenticated. The URL contains
			 * 			a code you have to send to AD FS to claim get a token. This is convenient flow for web apps
			 * 			but in our case we have to catch the request to this url and trigger the token request
			 * 			(That's what ADFSOAuth2Authenticator does)
			 * 			In my case callbackUrl was http://localhost/something, it does not really matter either. 
			 * 
			 * adfsHost is of course where your adfs is running. 
			 * */

			string clientId = "your_client_id_812635";
			string resource = "https://domain/some/path";
			string adfsHost = "your.adfs.hostname";

			string callbackUrl = "http://localhost/some/callback/url";

			var auth = new ADFSOAuth2Authenticator(
				clientId: clientId,
				resource: resource,
				authorizeUrl: new Uri("https://" + adfsHost + "/adfs/oauth2/authorize"),
				redirectUrl: new Uri(callbackUrl),
				accessTokenUrl: new Uri("https://" + adfsHost + "/adfs/oauth2/token")) {
			};

			auth.Completed += (sender, eventArgs) => 
			{
				if (eventArgs.IsAuthenticated) {
					string token = eventArgs.Account.Properties["access_token"];
					string expiresInString = eventArgs.Account.Properties["expires_in"];
					long expiresIn = long.Parse(expiresInString);
					DateTime expiresDateTime = DateTime.Now.AddSeconds(expiresIn);
					Console.WriteLine("Token: " + token);
					Console.WriteLine("Expires: " + expiresDateTime);
					App.SaveToken(token);
					App.SuccessfulLoginAction();
				} else {
					App.CancelledLoginAction();
				}
			};

			auth.Error += (object sender, AuthenticatorErrorEventArgs e) => {
				Console.WriteLine("Error " + e.Message);
				if (e.Exception != null) 
				{
					Console.WriteLine(e.Exception.ToString());
				}
			};

			AddChildViewController(auth.GetUI());
			View.AddSubview(auth.GetUI().View);
		}
	}
}

