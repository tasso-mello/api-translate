namespace domain.translate.Security
{
    using domain.translate.Configurations;
    using domain.translate.Contracts.Security;
    using domain.translate.Utilities;
    using Microsoft.IdentityModel.Tokens;
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Security.Claims;
    using System.Text;

    public class Token : IToken
    {
        private readonly TokenConfigurations _tokenConfiguration;

        public Token() { }

        public Token(TokenConfigurations tokenConfiguration)
        {
            _tokenConfiguration = tokenConfiguration;
        }

        public object GenerateTokenAuthorization(string username, string[] userGroups, bool permission = false)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_tokenConfiguration.Secret);
                var time = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_tokenConfiguration.TimeExpire)).AddHours(-3);

                var distinctGroups = string.Empty;
                distinctGroups += username + ',';

                foreach (var item in userGroups)
                {
                    var vetGroups = item.Split(',');
                    foreach (var group in vetGroups)
                    {
                        if (!group.Contains("DC") && !distinctGroups.ToLower().Contains(group.Split('=')[1].ToLower()))
                            distinctGroups += $"{group.Split('=')[1]},";
                    }
                }

                distinctGroups = distinctGroups.Substring(0, distinctGroups.Length - 1);


                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    // Caso precise de informações dentro do token
                    #region Add Claims
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        //new Claim("isAdmin", userGroups.Any(cn => cn.ToLower().Contains(_tokenConfiguration.GrupoAdminstradores.ToLower())).ToString()),
                        //new Claim("CN", distinctGroups)
                    }),
                    #endregion

                    Expires = time,
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);

                return new { token = tokenHandler.WriteToken(token), expiresIn = time };
            }
            catch (Exception e)
            {
                return Messages.GenerateGenericErrorMessage(e.Message ?? e.InnerException.Message);
            }
        }

        public object GenerateNTLMCredentials(string username, string password)
        {
            try
            {
                return Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes($@"{username}:{password}"));
            }
            catch (Exception e)
            {
                return Messages.GenerateGenericErrorMessage(e.Message ?? e.InnerException.Message);
            }
        }

        public object GenerateNTLMCredentials(string domain, string username, string password)
        {
            try
            {
                return new NetworkCredential($"{domain}\\{username}", password);
            }
            catch (Exception e)
            {
                return Messages.GenerateGenericErrorMessage(e.Message ?? e.InnerException.Message);
            }
        }

        public object GenerateNetworkCredentials(NetworkCredential networkCredential)
        {
            try
            {
                return new HttpClientHandler { UseDefaultCredentials = true, Credentials = networkCredential, PreAuthenticate = true };
            }
            catch (Exception e)
            {
                return Messages.GenerateGenericErrorMessage(e.Message ?? e.InnerException.Message);
            }
        }

        public string ReadAuthorizationToken(string token, string tag)
        {
            var stream = token;
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadJwtToken(stream).Claims.ToList().Where(c => c.Type == tag).FirstOrDefault().Value;
            return jsonToken;
        }

    }
}
