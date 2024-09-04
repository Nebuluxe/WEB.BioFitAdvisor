using API.BioFitAdvisor.Domain;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using WEB.BioFitAdvisor.Core; // Ajusta con tu espacio de nombres real
using WEB.BioFitAdvisor.Models; // Ajusta con tu espacio de nombres real

namespace WEB.BioFitAdvisor.Controllers
{
    public class UserController : Controller
    {
        private readonly ApiConsumer _apiConsumer;
        private readonly UserDataManipulator _userDataManipulator;

        public UserController(ApiConsumer apiConsumer, UserDataManipulator userDataManipulator)
        {
            _apiConsumer = apiConsumer;
            _userDataManipulator = userDataManipulator;
        }

        // Vista principal para listar usuarios
        public async Task<IActionResult> Index()
        {
            var userData = _userDataManipulator.GetUserData(); // Obtener token del usuario desde la sesión

            if (userData == null)
            {
                // Manejar el caso en que el usuario no está autenticado
                return RedirectToAction("Login", "Account");
            }

            var apiResponse = await _apiConsumer.GET("Users/GetUsers", userData.Token);
            if (apiResponse.success)
            {
                var users = JsonConvert.DeserializeObject<IEnumerable<User>>(apiResponse.response);
                return View(users);
            }
            else
            {
                // Manejar el error de la API aquí
                return View(new List<User>()); // Retornar una lista vacía o mostrar un mensaje de error
            }
        }

        // Vista para ver detalles de un usuario
        public async Task<IActionResult> Details(int id)
        {
            var userData = _userDataManipulator.GetUserData();

            if (userData == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var apiResponse = await _apiConsumer.GET($"Users/GetUser?userId={id}", userData.Token);
            if (apiResponse.success)
            {
                var user = JsonConvert.DeserializeObject<User>(apiResponse.response);
                return View(user);
            }
            else
            {
                return NotFound();
            }
        }

        // Vista para crear o editar un usuario
        public async Task<IActionResult> GetUser(int id = 0)
        {
            var userData = _userDataManipulator.GetUserData();

            if (userData == null)
            {
                return RedirectToAction("Login", "Account");
            }

            if (id == 0)
            {
                return View(new User());
            }
            else
            {
                var apiResponse = await _apiConsumer.GET($"Users/GetUser?userId={id}", userData.Token);
                if (apiResponse.success)
                {
                    var user = JsonConvert.DeserializeObject<User>(apiResponse.response);
                    return View(user);
                }
                else
                {
                    return NotFound();
                }
            }
        }

        // Acción para manejar el POST de creación/edición
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateOrEdit(User user)
        {
            var userData = _userDataManipulator.GetUserData();

            if (userData == null)
            {
                return RedirectToAction("Login", "Account");
            }

            if (ModelState.IsValid)
            {
                string jsonUser = JsonConvert.SerializeObject(user);
                var apiResponse = await _apiConsumer.POST("Users/UpsertUser", userData.Token, jsonUser);
                if (apiResponse.success)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", "Error al guardar el usuario.");
                }
            }
            return View(user);
        }

        // Acción para eliminar un usuario
        public async Task<IActionResult> Delete(int id)
        {
            var userData = _userDataManipulator.GetUserData();

            if (userData == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var apiResponse = await _apiConsumer.GET($"Users/GetUser?userId={id}", userData.Token);
            if (apiResponse.success)
            {
                var user = JsonConvert.DeserializeObject<User>(apiResponse.response);
                return View(user);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userData = _userDataManipulator.GetUserData();

            if (userData == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var apiResponse = await _apiConsumer.DELETE($"Users/DeleteUser?userId={id}", userData.Token);
            if (apiResponse.success)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View();
            }
        }
    }
}
