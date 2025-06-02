
using csharpPb9.utils;
using pass;

namespace restApi
{
    public class LoginService : ILoginService
    {
        private readonly IArbitruRepository _arbitruRepository;
        public LoginService(IArbitruRepository arbitruRepository)
        {
            this._arbitruRepository = arbitruRepository;
        }

        public Arbitru login(string username, string password)
        {
            var user = _arbitruRepository.FindByUser(username);

            if (user == null)
            {
                throw new Exception("User not found");
            }

            if (PasswordEncrypt.Encrypt(password) != user.Parola)
            {
                throw new Exception("Invalid password");
            }

            return user;
        }
    }
}