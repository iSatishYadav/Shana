using Shana.Models.Instargam;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Shana.Controllers
{
    public class InstagramController : Controller
    {
        private readonly string client_id = ConfigurationManager.AppSettings["ClientId:Instagram"];
        private readonly string client_secret = ConfigurationManager.AppSettings["ClientSecret:Instagram"];
        private const string grant_type = "authorization_code";
        private HttpClient _client = new HttpClient();
        private static string _url = "https://localhost:44341/";
        private string _redirectUri = (_url.EndsWith("/") ? _url.TrimEnd('/') : _url) + "/Instagram/Code";

        public ActionResult Index()
        {
            return Redirect($"https://api.instagram.com/oauth/authorize/?client_id={client_id}&redirect_uri={_redirectUri}&response_type=code");
        }

        public ActionResult Actions()
        {
            var actions = new Actions[] {
                new Actions { Name = "Basic Info", Key = nameof(Users) }
            };
            return View("Actions", actions);
        }

        public async Task<ActionResult> Users(string id)
        {
            id = id ?? "self";
            var response = await _client.GetAsync($"https://api.instagram.com/v1/users/{id}?access_token={GetAccessToken()}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsAsync<InstagramResopnse<InstagramUser>>();
                //return Json(result.Data, JsonRequestBehavior.AllowGet);
                return View(result.Data);
            }
            else
            {
                throw new HttpException(response.ReasonPhrase);
            }
        }


        private async Task<string> GetAccessTokenAsync(string code)
        {
            var response = await _client.PostAsFormEncoded("https://api.instagram.com/oauth/access_token", new
            {
                client_id = client_id,
                client_secret = client_secret,
                grant_type = grant_type,
                redirect_uri = _redirectUri,
                code = code
            });
            response.EnsureSuccessStatusCode();
            var token = await response.Content.ReadAsAsync<Token>();
            return token.AccessToken;

        }

        public async Task<ActionResult> Code(string code)
        {
            var accessToken = await GetAccessTokenAsync(code);
            SaveAccessToken(accessToken);
            return RedirectToAction(nameof(Actions));
        }

        private string GetAccessToken()
        {
            return Session["AccessToken"]?.ToString();
        }

        private void SaveAccessToken(string accessToken)
        {
            Session["AccessToken"] = accessToken;
        }

        public async Task<ActionResult> Follows(string token)
        {
            var response = await _client.GetAsync($"https://api.instagram.com/v1/users/self/follows?access_token={token}");
            return Json(await response.Content.ReadAsStringAsync(), JsonRequestBehavior.AllowGet);
        }
    }
}