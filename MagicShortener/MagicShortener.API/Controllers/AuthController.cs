using MagicShortener.API.Infrastructure.Authentication;
using MagicShortener.API.Inputs;
using MagicShortener.Common.Security;
using MagicShortener.DataAccess.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicShortener.API.Controllers
{
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly IUsersRepository _usersRepository;
        //private readonly IJwtFactory _jwtFactory;

        public AuthController(IUsersRepository usersRepository
            //,IJwtFactory jwtFactory
            )
        {
            _usersRepository = usersRepository;
            //_jwtFactory = jwtFactory;
        }

        // TODO: метод SignUp - регистрации пользователя
        [HttpPost("signin")]
        public async Task<IActionResult> SignIn([FromBody]SignInData signInData)

        {
            if (signInData == null || string.IsNullOrEmpty(signInData.Login) || string.IsNullOrEmpty(signInData.Password))
                return BadRequest(Constants.InvalidInputError);
            
            var user = await _usersRepository.GetByLogin(signInData.Login);

            if (user == null)
            {
                return BadRequest(Constants.InvalidCredentialsError);
            }

            var passwordHasher = new PasswordHasher();
            if (!passwordHasher.Check(user.Password, user.Salt, signInData.Password))
            {
                return Forbid(Constants.InvalidCredentialsError);
            }

            // TODO: формируем токен и возвращаем в ответе + идентификатор
            //var claimsIdentity = _jwtFactory.GenerateClaimsIdentity(user.Id.ToString());

            //var jwt = await Tokens.GenerateJwt(claimsIdentity, _jwtFactory, signInData.Login, _jwtOptions.Value, new Newtonsoft.Json.JsonSerializerSettings
            //{
            //    Formatting = Newtonsoft.Json.Formatting.Indented
            //});

            //return Ok(new
            //{
            //    token = jwt,
            //    userId = user.Id
            //});

            return Ok();
        }

        private class Constants
        {
            public const string InvalidInputError = "Некорректные входные данные";
            public const string InvalidCredentialsError = "Неверный логин или пароль";
        }
    }
}
