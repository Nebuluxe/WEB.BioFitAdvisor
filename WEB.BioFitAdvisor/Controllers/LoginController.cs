using API.BioFitAdvisor.Domain;
using API.BioFitAdvisor.Domain.DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using WEB.BioFitAdvisor.Core;
using WEB.BioFitAdvisor.Models;

namespace WEB.BioFitAdvisor.Controllers
{
    public class LoginController : Controller
    {
        private IHttpContextAccessor _HttpContextAccessor;
        private ApiConsumer _ApiConsumer;
        private UserDataManipulator _UserDataManipulator;

        public LoginController(IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _HttpContextAccessor = httpContextAccessor;
            _ApiConsumer = new ApiConsumer(configuration);
            _UserDataManipulator = new UserDataManipulator(httpContextAccessor);
        }

        [HttpPost]
        public async Task<IActionResult> AutenticateUser([FromBody] Autentication user)
        {
            try
            {
                IList<UserCompany> ListEmpresas = new List<UserCompany>();

                string json = JsonConvert.SerializeObject(user);

                apiDTO apiDTO = await _ApiConsumer.POST("/api/User/Autentication", "", json);

                if (apiDTO.success)
                {
                    if (apiDTO.success)
                    {
                        ResponseToken oToken = JsonConvert.DeserializeObject<ResponseToken>(apiDTO.response);

                        _UserDataManipulator.SetUserData(oToken);

                        apiDTO = await _ApiConsumer.GET("/api/User/GetUserCompanies", oToken.token);

                        ListEmpresas = JsonConvert.DeserializeObject<List<UserCompany>>(apiDTO.response) ?? new List<UserCompany>();

                        _UserDataManipulator.SetUserData(oToken);
                    }
                }

                return Json(new { success = apiDTO.success, userData = _UserDataManipulator.GetUserData(), companies = ListEmpresas });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex });
            }
        }

        [HttpPost]
        public async Task<IActionResult> SelectCompany([FromBody] SelectCompanyApp comp)
        {
            try
            {
                UserData user = _UserDataManipulator.GetUserData();

                apiDTO apiDTO = await _ApiConsumer.POST("/api/User/SelectCompany?CompanyId=" + comp.CompanyId, user.Token, "");

                if (apiDTO.success)
                {
                    ResponseToken oToken = JsonConvert.DeserializeObject<ResponseToken>(apiDTO.response);

                    _UserDataManipulator.SetUserData(oToken, comp.KeepSession, comp.CompanyId);
                }

                return Json(new { success = apiDTO.success, userData = _UserDataManipulator.GetUserData() });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetUserCompanies()
        {
            try
            {
                UserData user = _UserDataManipulator.GetUserData();

                apiDTO apiDTO = await _ApiConsumer.GET("/api/User/GetUserCompanies", user.Token);

                IList<Company> ListEmpresas = new List<Company>();

                if (apiDTO.success)
                {
                    ListEmpresas = JsonConvert.DeserializeObject<List<Company>>(apiDTO.response) ?? new List<Company>();

                }

                return Json(new { success = apiDTO.success, companies = ListEmpresas });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetUsersByCompany()
        {
            try
            {
                UserData user = _UserDataManipulator.GetUserData();

                apiDTO apiDTO = await _ApiConsumer.GET("/api/User/GetUsersByCompany", user.Token);

                IList<User> response = new List<User>();

                if (apiDTO.success)
                {
                    response = JsonConvert.DeserializeObject<List<User>>(apiDTO.response) ?? new List<User>();

                }

                return Json(new { success = apiDTO.success, response = response });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex });
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetUser([FromBody] UserRequest userparam)
        {
            try
            {
                if (userparam.UserID == "0" || userparam.UserID == "")
                {
                    return Json(new { success = false });
                }

                UserData user = _UserDataManipulator.GetUserData();

                Dictionary<string, string> parameters = new Dictionary<string, string>();
                parameters.Add("user_ID", userparam.UserID.ToString());

                apiDTO apiDTO = await _ApiConsumer.GET("/api/User/GetUser", user.Token, parameters);

                User response = new User();

                if (apiDTO.success)
                {
                    response = JsonConvert.DeserializeObject<User>(apiDTO.response) ?? new User();

                }

                return Json(new { success = apiDTO.success, response = response });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex });
            }
        }

        [HttpPost]
        public async Task<IActionResult> setUserData([FromBody] UserData USER)
        {
            try
            {
                _UserDataManipulator.SetUserData(USER);

                return Json(new { success = true, userData = _UserDataManipulator.GetUserData() });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex });
            }
        }

        [HttpPost]
        public async Task<IActionResult> ReLogin([FromBody] UserData user)
        {
            try
            {
                if (user.ExpieredDate <= DateTime.Now)
                {
                    string json = JsonConvert.SerializeObject(user);

                    apiDTO apiDTO = await _ApiConsumer.POST("/User/RefreshToken", user.Token, "");

                    if (apiDTO.success)
                    {
                        ResponseToken oToken = JsonConvert.DeserializeObject<ResponseToken>(apiDTO.response);

                        user.Token = oToken.token;
                        user.ExpieredDate = oToken.ExpirationDate;

                        _UserDataManipulator.SetUserData(user);
                    }
                }
                else
                {
                    _UserDataManipulator.SetUserData(user);
                }

                return Json(new { success = true, userData = _UserDataManipulator.GetUserData() });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex });
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetVerificationCode([FromBody] string email)
        {
            try
            {
                Dictionary<string, string> Params = new Dictionary<string, string>();
                Params.Add("email", email);

                apiDTO apiDTO = await _ApiConsumer.GET("/learn/User/GetVerificationCode", "");

                if (apiDTO.success)
                {
                    Code oCode = JsonConvert.DeserializeObject<Code>(apiDTO.response);

                    _HttpContextAccessor.HttpContext.Session.SetString("VerificationCode", oCode.code);
                }

                return Json(new { success = apiDTO.success });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex });
            }
        }

        [HttpPut]
        public async Task<IActionResult> RecoverPassword([FromBody] RecoverPassword newpass)
        {
            try
            {
                string VerifCodeToken = _HttpContextAccessor.HttpContext.Session.GetString("VerificationCode");
                string json = JsonConvert.SerializeObject(newpass);

                apiDTO apiDTO = await _ApiConsumer.PUT("/learn/User/RecoverPassword", VerifCodeToken, json);

                if (apiDTO.success)
                {
                    _HttpContextAccessor.HttpContext.Session.Clear();
                }

                return Json(new { success = apiDTO.success });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex });
            }
        }

        [HttpPost]
        public IActionResult LogOuth()
        {
            try
            {
                _HttpContextAccessor.HttpContext.Session.Clear();

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex });
            }
        }

        [HttpPost]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

    public class UserRequest
    {
        public string UserID { get; set; }
    }
}
