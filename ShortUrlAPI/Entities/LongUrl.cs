using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShortUrlAPI.Entities
{
    public class LongUrl
    {
        #region Properties
        [Key]
        public int Id { get; set; }
        [Required]
        public int UrlId { get; set; }
        [Required]
        public string Url { get; set; }
        #endregion

        #region publicMethod
        public string getLongUrl()
        {
            return WebEncoders.Base64UrlEncode(BitConverter.GetBytes(UrlId));
        }
        public static int GetId(string longUrl)
        {
            return BitConverter.ToInt32(WebEncoders.Base64UrlDecode(longUrl));
        }
        #endregion
    }
}
