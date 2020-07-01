using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using WebAppSite.Infrastructure;
using WebAppSite.Models;

namespace WebAppSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUsersApiClient _usersApiClient;
        private readonly ILogger<HomeController> _logger;
        public HomeController(IUsersApiClient usersApiClient, ILogger<HomeController> logger)
        {
            _usersApiClient = usersApiClient;
            _logger = logger;
        }
        // Get all users
        public async Task<IActionResult> Index()
        {
            var users = Enumerable.Empty<User>();
            try
            {
                string usersInfo = await _usersApiClient.GetData("api/users");
                users = JsonConvert.DeserializeObject<IEnumerable<User>>(usersInfo);
                _logger.LogInformation("Запрос к users.api на получение всех пользователей выполнен");
            }
            catch (System.Net.Http.HttpRequestException e)
            {
                if (e.Message != "Name or service not known")
                {
                    _logger.LogError("Ошибка с обращением в базу данных: {0}", e.GetBaseException() + e.Message);
                    ViewBag.ErrorType = "DB_Error";
                }
                else
                {
                    _logger.LogError("Служба users.api недоступна! {0}", e.GetBaseException() + e.Message);
                    ViewBag.ErrorType = "API_Error";
                }
            }
            return View(users);
        }
        //Get user for edit
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var user = new User();
            try
            {
                string userInfo = await _usersApiClient.GetData("api/users/" + id.ToString());
                user = JsonConvert.DeserializeObject<User>(userInfo);
                _logger.LogInformation("Запрос к users.api на получение пользователя с ID {0} выполнен.", id.ToString());
            }
            catch (System.Net.Http.HttpRequestException e)
            {
                if (e.Message != "Name or service not known")
                {
                    _logger.LogError("Ошибка с обращением в базу данных при попытке получить пользователя {0}: {1}", id.ToString(), e.GetBaseException());
                    ViewBag.ErrorType = "DB_Error";
                }
                else
                {
                    _logger.LogError("Служба users.api недоступна! {0}", e.GetBaseException() + e.Message);
                    ViewBag.ErrorType = "API_Error";
                }
            }
            return View(user);
        }
        // Edit user
        [HttpPost]
        public async Task<IActionResult> Edit(User user)
        {
            if (ModelState.IsValid)
            {
                string data = JsonConvert.SerializeObject(user);
                await _usersApiClient.PutData(data, "api/users/" + user.Id.ToString());
                _logger.LogInformation("Пользователь {0} успешно сохранен!", user.Fio);
                ViewBag.Good = "Изменения сохранены!";
            }
            else
            {
                _logger.LogWarning("Пользователь {0} не сохранен по причине неуспешного прохождения валидации.", user.Fio);
                ViewBag.Bad = "Проверьте корректность введенных данных!";
            }
            return View(user);
        }
        public async Task<JsonResult> GetUsers()
        {
            _logger.LogWarning("Запрос на получение пользователей для карты выполнен.");
            return Json(await _usersApiClient.GetData("api/users"));
        }
    }
}