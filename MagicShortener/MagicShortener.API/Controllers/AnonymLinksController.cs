using MagicShortener.API.Inputs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace MagicShortener.API.Controllers
{
    /// <summary>
    /// Для реализации возможности формировать короткие ссылки без аутентификации,
    /// чтобы не городить проверки креденшиалов в каждом методе отдельно и не делать отдельный слой с кастомным авторизационным фильтром
    /// </summary>
    [ApiController]
    [AllowAnonymous]
    [Route("anon/api/links")]
    public class AnonymLinksController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AnonymLinksController(
            IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //проверяем наличие в куках специального токена
            var cookieTempAnonymousToken = _httpContextAccessor.HttpContext.Request.Cookies[Constants.AnonymousTokenCookie];
            
            if(string.IsNullOrEmpty(cookieTempAnonymousToken))
                return Ok();

            // TODO: возвращаем набор ссылок по этому токену
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CreateLinkRequest request)
        {
            //валидация тела запроса
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            // TODO: валидация URL-a

            //например, генерируем такой токен
            var anonymousToken = Guid.NewGuid();

            // TODO: команда создания ссылки - не указываем User-a, вместо этого указываем TempTokenId 
            // (не забыть при этом настроить отдельно задание, которое с какой-то периодичностью такие записи будет тереть)

            //выставляем соответствующую куку в ответ
            CookieOptions options = new CookieOptions
            {
                Expires = DateTime.Now.AddHours(24)
            };
            Response.Cookies.Append(Constants.AnonymousTokenCookie, anonymousToken.ToString(), options);

            return Ok();
        }

        private class Constants
        {
            public const string AnonymousTokenCookie = "anonymousToken";
        }
    }
}
