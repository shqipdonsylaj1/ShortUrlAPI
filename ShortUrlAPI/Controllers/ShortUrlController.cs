using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShortUrlAPI.Model;
using System;
using System.Net;
using System.Threading.Tasks;

namespace ShortUrlAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShortUrlController : ControllerBase
    {
        #region Properties

        #endregion

        #region Constructor

        #endregion

        #region publicMethod
        [HttpPost("/short")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public Task Create_Short_Url(string url)
        {
            HttpContext context = HttpContext;
            if (!Uri.TryCreate(url, UriKind.Absolute, out Uri result))
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                return context.Response.WriteAsync("Could not understand URL.");
            }
            var results = result.ToString();
            var entry = new ShortLink
            {
                urlId = 123_456_789,
                longUrl = results
            };
            var longsUrl = entry.getLongUrl();
            var responseUri = $"{context.Request.Scheme}://{context.Request.Host}/{longsUrl}";
            return context.Response.WriteAsync(responseUri);
        }
        [HttpGet("/redirectUrl")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public Task Redirect_Url(string url)
        {
            HttpContext context = HttpContext;
            var entry = new ShortLink
            {
                urlId = 123_456_789,
                longUrl = url
            };
            var shortUrl = entry.getLongUrl();
            var path = context.Request.Path.ToUriComponent().Trim('/');
            // Call method for find Id in db...
            var id = ShortLink.GetId(shortUrl);
            if (entry != null)
                //context.Response.Redirect(entry.longUrl);
                return context.Response.WriteAsync(entry.longUrl);
            else
                context.Response.Redirect("/");
            return Task.CompletedTask;
        }
        #endregion

        #region privateMethod

        #endregion
    }
}
