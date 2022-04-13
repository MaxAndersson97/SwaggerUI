using Swagger.Models;
using System.Collections.Generic;
using MongoDB.Driver;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using IdentityModel.Client;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Swagger.Services
{
    public class UserService
    {

        private DiscoveryDocumentResponse _discoveryDocument { get; set; }
        private readonly IMongoCollection<Users> _user;
        private readonly IMongoCollection<oAuthRefreshTokens> _userToken;

        public UserService(IConfiguration configuration)
        {
            var Client = new MongoClient(configuration.GetConnectionString("DB"));
            var database = Client.GetDatabase("stocko_db");
            var apiUrl = configuration.GetSection("Api_Url");

            _user = database.GetCollection<Users>("Users");
            _userToken = database.GetCollection<oAuthRefreshTokens>("oAuthRefreshTokens");
            using var client = new HttpClient();
            _discoveryDocument = client.GetDiscoveryDocumentAsync(apiUrl.Value + "/.well-known/openid-configuration").Result;
        }

        public UserService() { }

        public JsonResult GetUsers()
        {
            List<Users> user = _user.Find(User => true).ToList();
            JObject json = new();
            json.Add("statut", 200);
            json.Add("message", "Success");
            json.Add("users", JArray.FromObject(user));
            return new JsonResult(user);
        }

        public JsonResult GetUser(string id)
        {
            JObject keyValuePairs = new JObject();
            if (!string.IsNullOrEmpty(id))
            {
                Users users = _user.Find(User => User.id == id).FirstOrDefault();

                if (users != null)
                {

                    keyValuePairs["statut"] = 200;
                    keyValuePairs.Add("data", JObject.FromObject(users));
                }
                else
                {
                    keyValuePairs["statut"] = 400;
                    keyValuePairs["message"] = "Invalid Id";
                    keyValuePairs["data"] = null;
                }
            }
            else
            {
                keyValuePairs["statut"] = 400;
                keyValuePairs["message"] = "Invalid Id / Null id";
                keyValuePairs["data"] = null;
            }
            return new JsonResult(keyValuePairs);
        }


        public async Task<JsonResult> LoginAsync(string email, string password,int role_id)
        {
            JObject keyValuePairs = new();
            if (IsValidEmail(email))
            {
                Users user = _user.Find(User => User.email == email && User.password == password && User.role_id==role_id).FirstOrDefault();
                if (user != null)
                {
                    var OAuth2Token = await GetToken("getToken");

                    oAuthRefreshTokens token = new();
                    token.access_token = OAuth2Token.AccessToken;
                    token.expires_at = OAuth2Token.ExpiresIn.ToString();
                    token.userId = user.id;
                    _userToken.InsertOne(token);
                    keyValuePairs.Add("statut", 200);
                    keyValuePairs.Add("message", "Successfully Login");
                    keyValuePairs.Add("token", OAuth2Token.AccessToken);
                }
                else
                {
                    keyValuePairs["statut"] = 400;
                    keyValuePairs["message"] = "Wrong Email or password or selected role is wrong ";
                }
            }
            else
            {
                keyValuePairs["statut"] = 400;
                keyValuePairs["message"] = "Kindly enter a valid email address";

            }
            return new JsonResult(keyValuePairs);
        }

        public JsonResult PostUser(Users user)
        {
            JObject keyValuePairs = new();
            if (IsValidEmail(user.email))
            {
                if (_user.Find(User => User.email == user.email).FirstOrDefault() == null)
                {
                    _user.InsertOne(user);
                    keyValuePairs.Add("statut", 200);
                    keyValuePairs.Add("message", "Successfully Added");
                }
                else
                {
                    keyValuePairs["statut"] = 400;
                    keyValuePairs["message"] = "This email is already registered";
                }
            }
            else
            {
                keyValuePairs["statut"] = 400;
                keyValuePairs["message"] = "Kindly enter a valid email address";
            }
            return new JsonResult(keyValuePairs);
        }

        public JsonResult PutUser(string id, Users user)
        {
            JObject keyValuePairs = new();
            if (_user.Find(User => User.id == id).FirstOrDefault() != null)
            {
                _user.ReplaceOne(user => user.id == id, user);
                keyValuePairs.Add("statut", 200);
                keyValuePairs.Add("message", "Successfully Uploaded");
            }
            else
            {
                keyValuePairs["statut"] = 400;
                keyValuePairs["message"] = "No account is associated with this id";
            }
            return new JsonResult(keyValuePairs);
        }

        public JsonResult DeleteUser(string id)
        {
            JObject keyValuePairs = new();
            var user = _user.Find(user => user.id == id).FirstOrDefault();
            if (user != null)
            {
                _user.DeleteOne(User => User.id == id);
                keyValuePairs.Add("statut", 200);
                keyValuePairs.Add("message", "Successfully Delete");
            }
            else
            {
                keyValuePairs["statut"] = 400;
                keyValuePairs["message"] = "No account is associated with this id";
            }
            return new JsonResult(keyValuePairs);
        }

        public async Task<TokenResponse> GetToken(string apiScope)
        {
            using (var client = new HttpClient())
            {
                var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
                {
                    Address = _discoveryDocument.TokenEndpoint,
                    ClientId = "inventoryApi",
                    Scope = apiScope,
                    ClientSecret = "ProCodeGuide"
                });
                return tokenResponse;
            }
        }
        public JsonResult ForgetPassword(JObject json)
        {
            JObject keyValuePairs = new();
            if (_user.Find(User => User.email == json["email"].ToString()).FirstOrDefault() != null)
            {
                _user.ReplaceOne(User => User.email == json["email"].ToString(), new Users { email = json["email"].ToString(), password = json["password"].ToString() });
                keyValuePairs.Add("statut", 200);
                keyValuePairs.Add("message", "Successfully Sent");
            }
            else
            {
                keyValuePairs["statut"] = 400;
                keyValuePairs["message"] = "No account is associated with this email";
            }
            return new JsonResult(keyValuePairs);
        }
        bool IsValidEmail(string email)
        {
            if (email.Trim().EndsWith("."))
            {
                return false; // suggested by @TK-421
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        
    }
}