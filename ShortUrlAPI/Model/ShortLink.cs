using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShortUrlAPI.Model
{
    public class ShortLink
    {
        #region Properties
        public int urlId { get; set; }
        public string longUrl { get; set; }
        #endregion

        #region Constructor

        #endregion

        #region publicMethod
        public string getLongUrl()
        {
            return WebEncoders.Base64UrlEncode(BitConverter.GetBytes(urlId));
        }
        public static int GetId(string longUrl)
        {
            return BitConverter.ToInt32(WebEncoders.Base64UrlDecode(longUrl));
        }
        #endregion

        #region privateMethod

        #endregion
    }
}
