using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShortUrlAPI.Data;
using ShortUrlAPI.DTO;
using ShortUrlAPI.Entities;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ShortUrlAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShortUrlController : ControllerBase
    {
        #region Properties
        private readonly ApplicationDbContext _db;
        #endregion

        #region Constructor
        public ShortUrlController(ApplicationDbContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }
        #endregion

        #region publicMethod
        [HttpPost]
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
            Random random = new Random();
            var randomNumber = random.Next();
            var longUrl = new LongUrl
            {
                UrlId = randomNumber,
                Url = results
            };
            _db.LongUrls.Add(longUrl);
            var longsUrl = longUrl.getLongUrl();
            var responseUri = $"{context.Request.Scheme}://{context.Request.Host}/{longsUrl}";
            context.Response.WriteAsync(responseUri);
            return _db.SaveChangesAsync();
        }
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<LongLinkDTO>> Redirect_Url(string shortUrl)
        {
            HttpContext context = HttpContext;
            var id = LongUrl.GetId(shortUrl.Substring(shortUrl.Length - 6));
            var result = await _db.LongUrls.Where(x => x.UrlId == id).ToListAsync();
            var longLink = new LongLinkDTO
            {
                Url = result.First().Url
            };
            return longLink;
        }
        #endregion

        #region privateMethod

        #endregion
    }
}
