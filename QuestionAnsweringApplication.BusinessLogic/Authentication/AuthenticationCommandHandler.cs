using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using QuestionAnsweringApplication.Domain;
using QuestionAnsweringApplication.Persistence;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace QuestionAnsweringApplication.BusinessLogic.Authentication
{
    public sealed class AuthenticationCommandHandler : IRequestHandler<UserCommand, AuthenticationResponse>
    {
        private readonly IRepository _repository;
        private readonly IConfiguration _configuration;

        public AuthenticationCommandHandler(IRepository repository, IConfiguration config)
        {
            _repository = repository;
            _configuration = config;
        }

        public async Task<AuthenticationResponse> Handle(UserCommand request, CancellationToken cancellationToken)
        {
            AuthenticationResponse response = new();

            if (string.IsNullOrEmpty(request.UserName))
            {
                response.Error = "User name is required";
                return response;
            }

            if (string.IsNullOrEmpty(request.Password))
            {
                response.Error = "Password is required";
                return response;
            }

            var user = await _repository.FirstAsync<User>(x => x.UserName == request.UserName && x.Password == request.Password);

            if (user == null)
            {
                response.Error = "Invalid user name or password.";
                return response;
            }

            //create claims details based on the user information
            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.GivenName, user.FirstName!),
                new Claim(ClaimTypes.Surname, user.LastName!),
                new Claim(ClaimTypes.Name, user.UserName!)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: signIn);

            var accessToken = new JwtSecurityTokenHandler().WriteToken(token);
            response.AccessToken = accessToken;

            return response;
        }
    }
}
