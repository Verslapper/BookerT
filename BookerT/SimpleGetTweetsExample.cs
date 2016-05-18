using BoxKite.Twitter;
using BoxKite.Twitter.Authentication;
using System;

namespace BookerT
{
    public class SimpleGetTweetsExample
    {
        public async void ShowLatestMentions()
        {
            System.Console.WriteLine("Welcome to BoxKite.Twitter from Console");

            // Provide a platform specific adaptor for Web browser display and HMACSHA1
            // example: https://github.com/nickhodge/BoxKite.Twitter/blob/master/src/Boxkite.Twitter.Console/Helpers/DesktopPlatformAdaptor.cs
            // this class need to implement BoxKite.Twitter.IPlatformAdaptor
            // source: https://github.com/nickhodge/BoxKite.Twitter/blob/master/src/BoxKite.Twitter/Interfaces/IPlatformAdaptor.cs
            var desktopPlatformAdaptor = new DesktopPlatformAdapter();

            // Using consumerkey and consumersecret, start the OAuth1.0a process
            var twitterauth = new TwitterAuthenticator("consumerkeyhere", "consumersecrethere", desktopPlatformAdaptor);
            var authstartok = await twitterauth.StartAuthentication();

            // if OK
            if (authstartok)
            {
                // Now the User will see a Web browser asking them to log in to Twitter, authorise your app
                // and remember a PIN
                // This PIN is then entered here to complete the Authentication of the Credentials
                System.Console.Write("pin: ");
                var pin = System.Console.ReadLine();
                var twittercredentials = await twitterauth.ConfirmPin(pin);

                // Credentials OK, now OK to communicate with Twitter
                if (twittercredentials.Valid)
                {
                    // Create a new "session" using the OK'd credentials and the platform specific stuff
                    var session = new UserSession(twittercredentials, desktopPlatformAdaptor);

                    // Let's check the user is OK
                    var checkUser = await session.GetVerifyCredentials();
                    if (checkUser.OK)
                    {
                        System.Console.WriteLine(twittercredentials.ScreenName +
                            " is authorised to use BoxKite.Twitter.");

                        // START GOOD STUFF HAPPENS HERE    
                        // grab the latest 10 mentions for the Auth'd user
                        var mentionslist = await session.GetMentions(count: 10);

                        foreach (var tweet in mentionslist)
                        {
                            System.Console.WriteLine(String.Format("ScreenName: {0}, Tweet: {1}", tweet.User.ScreenName,
                                tweet.Text));
                        }
                        // END GOOD STUFF HAPPENS HERE
                    }
                    else
                    {
                        System.Console.WriteLine("Credentials could not be verified");
                    }
                }
                else
                {
                    System.Console.WriteLine(
                        "Authenticator could not start. Do you have the correct Client/Consumer IDs and secrets?");
                }
                System.Console.WriteLine("Press return to exit");
                System.Console.ReadLine();
            }
        }
    }
}
