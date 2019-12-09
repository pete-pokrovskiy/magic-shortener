using System;

namespace MagicShortener.Logic.Services.Authentication
{
    public class UserData : IUserData
    {
        public string Id { get; set; }
        public string Login { get; set; }
    }
}
