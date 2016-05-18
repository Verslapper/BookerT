using System.Threading.Tasks;
using BoxKite.Twitter;
using System.Diagnostics;
using System.Security.Cryptography;

namespace BookerT
{
    // (c) 2012-2016 Nick Hodge mailto:nhodge@mungr.com & Brendan Forster
    // License: MS-PL
    // https://github.com/nickhodge/BoxKite.Twitter/blob/master/src/BoxKite.Twitter.Desktop/Helpers/DesktopPlatformAdaptor.cs
    public class DesktopPlatformAdapter : IPlatformAdaptor
    {
        public void DisplayAuthInBrowser(string u)
        {
            Process.Start(u);
        }

        public Task<string> AuthWithBroker(string authuri, string callbackuri)
        {
            throw new System.NotImplementedException();
        }

        private HMACSHA1 _hmacsha1;

        public void AssignKey(byte[] key)
        {
            _hmacsha1 = new HMACSHA1(key);
        }

        public byte[] ComputeHash(byte[] buffer)
        {
            return _hmacsha1.ComputeHash(buffer);
        }
    }
}
