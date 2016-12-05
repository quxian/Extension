using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace StringExtensionForYongsheng
{
    public static class Extension
    {
        private static readonly MD5 _md5 = MD5.Create();
        private static readonly SpinLock _spinLock = new SpinLock();
        private static bool _lockTaken = false;
        public static string GetMD5HashCode(this string str)
        {
            try
            {
                _spinLock.Enter(ref _lockTaken);
                return string.Join("", _md5.ComputeHash(Encoding.UTF8.GetBytes(str)).Select(x => x.ToString("x2")));
            }
            finally
            {
                if (_lockTaken)
                    _spinLock.Exit();
            }
        }
    }
}
