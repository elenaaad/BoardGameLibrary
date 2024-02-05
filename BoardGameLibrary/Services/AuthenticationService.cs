using DAL.Models;
using DAL.Models.DTO;
using DAL.Repositories.UnitOfWork;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using BCryptNet = BCrypt.Net.BCrypt;
using System.Text;
using AutoMapper;
using sib_api_v3_sdk.Api;
using sib_api_v3_sdk.Client;
using sib_api_v3_sdk.Model;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using Microsoft.Extensions.Configuration;

namespace BoardGameLibrary.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        public IMapper _mapper;

        public AuthenticationService(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _mapper = mapper;
        }
        public async Task<Token?> Authenticate(UserAuthRequestDto? user)
        {
            if (user == null || user.Email == null || user.Password == null
                || user.Email == "" || user.Password == "")
            {
                throw new Exception("You must provide an email and password");
            }

            var userInDb = _unitOfWork.Users.FindByEmail(user.Email);
            if (user == null || !BCryptNet.Verify(user.Password, userInDb.PasswordHash))
            {
                throw new Exception();
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userInDb.Id.ToString()),
                    new Claim(ClaimTypes.Email, userInDb.Email),
                    new Claim(ClaimTypes.Role,  userInDb.Role.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(100000),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey),
                SecurityAlgorithms.HmacSha256Signature),
                Issuer = _configuration["JWT:Issuer"],
                Audience = _configuration["JWT:Audience"]
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            //var refreshToken = GenerateRefreshToken();
            //SetRefreshToken(refreshToken);
            return new Token { TokenString = tokenHandler.WriteToken(token) };
        }
        /*
        private RefreshToken GenerateRefreshToken()
        {
            var refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.Now.AddMinutes(20),
                Created = DateTime.Now
            };
            return refreshToken;
        }
        private void SetRefreshToken(RefreshToken refreshToken)
        {
            _unitOfWork.RefreshTokens.Add(refreshToken);
            _unitOfWork.Complete();
        }
        */

        public async Task<User> Register(UserAuthRequestDto user)
        {
            if (user == null || user.Email == "" || user.Password == ""
                || user.Email == null || user.Password == null)
            {
                throw new Exception("Must enter an email and password");
            }

            var userInDb = _unitOfWork.Users.FindByEmail(user.Email);

            if (userInDb != null)
            {
                throw new Exception("User with this email already exists");
            }
            var newUser = _mapper.Map<User>(user);
            newUser.PasswordHash = BCryptNet.HashPassword(user.Password);
            newUser.Role = Role.User;

            await _unitOfWork.Users.CreateAsync(newUser);
            await _unitOfWork.Users.SaveAsync();

            SendEmai(user.Email, user.Name);
            return newUser;
        }


        public object GetById(int userId)
        {
            var user = _unitOfWork.Users.FindById(userId);
            if (user == null)
            {
                return null;
            }
            return user;
        }
        public void SendEmai(string email, string name)
        {
            string apiKey = "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";
            Configuration.Default.ApiKey["api-key"] = apiKey;
            var apiInstance = new TransactionalEmailsApi();

            string SenderName = "Go Out Bucharest";
            string SenderEmail = "isachecatalina0409@gmail.com";

            SendSmtpEmailSender EmailSender = new SendSmtpEmailSender(SenderName, SenderEmail);

            SendSmtpEmailTo smtpEmailTo = new SendSmtpEmailTo(email, name);
            List<SendSmtpEmailTo> To = new List<SendSmtpEmailTo>();
            To.Add(smtpEmailTo);

            string HtmlContent = null;
            string TextContent = "Welcome to our site";
            string Subject = "Welcome";
            try
            {
                var sendSmtpEmail = new SendSmtpEmail(EmailSender, To, null, null, HtmlContent, TextContent, Subject);
                CreateSmtpEmail result = apiInstance.SendTransacEmail(sendSmtpEmail);

                Console.WriteLine(result.ToJson());

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadLine();
            }
            return;
        }

    }
}