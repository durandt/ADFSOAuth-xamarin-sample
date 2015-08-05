# ADFSOAuth-xamarin-sample
Example of authentication using ADFS 3.0 (2012 R2) and OAuth2

# Init
Don't forget to init submodules (Customized Xamarin.Auth)

```
    $ git submodule init
    $ git submodule update
```

# Configure
In file Droid/Presentation/ADLoginPageRenderer.cs and iOS/Presentation/ADLoginPageRenderer.cs, replace the placeholders by your own values for clientId, resource, adfsHost and callbackUrl. 

# Run
Example works with iOS simulator 8.4, Android SDK 22
