using API.BioFitAdvisor.Domain;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WEB.BioFitAdvisor.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WEB.BioFitAdvisor.Controllers
{
    public class ExerciseController : Controller
    {
        private readonly ApiConsumer _apiConsumer;
        private readonly UserDataManipulator _userDataManipulator;

        public ExerciseController(ApiConsumer apiConsumer, UserDataManipulator userDataManipulator)
        {
            _apiConsumer = apiConsumer;
            _userDataManipulator = userDataManipulator;
        }

        public async Task<IActionResult> Index()
        {
            var userData = _userDataManipulator.GetUserData();

            if (userData == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var apiResponse = await _apiConsumer.GET("/api/ExerciseLibrary/GetAllExercises", userData.Token);
            if (apiResponse.success)
            {
                var exercises = JsonConvert.DeserializeObject<IEnumerable<Exercise>>(apiResponse.response);
                return View(exercises);
            }
            else
            {
                return View(new List<Exercise>());
            }
        }

        public async Task<IActionResult> CreateOrEdit(int id = 0)
        {
            var userData = _userDataManipulator.GetUserData();

            if (userData == null)
            {
                return RedirectToAction("Login", "Account");
            }

            if (id == 0)
            {
                return View(new Exercise());
            }
            else
            {
                var apiResponse = await _apiConsumer.GET($"/api/ExerciseLibrary/GetExerciseById?exerciseId={id}", userData.Token);
                if (apiResponse.success)
                {
                    var exercise = JsonConvert.DeserializeObject<Exercise>(apiResponse.response);
                    return View(exercise);
                }
                else
                {
                    return NotFound();
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateOrEdit(Exercise exercise)
        {
            var userData = _userDataManipulator.GetUserData();

            if (userData == null)
            {
                return RedirectToAction("Login", "Account");
            }

            if (ModelState.IsValid)
            {
                string jsonExercise = JsonConvert.SerializeObject(exercise);
                var apiResponse = await _apiConsumer.POST("/api/ExerciseLibrary/UpsertExercise", userData.Token, jsonExercise);
                if (apiResponse.success)
                {
                    // Redirigir a la ventana Details después de crear o editar el ejercicio
                    return RedirectToAction(nameof(Index), new { id = exercise.ExerciseId });
                }
                else
                {
                    ModelState.AddModelError("", "Error al guardar el ejercicio.");
                }
            }
            return View(exercise);
        }

        // Acción para mostrar los detalles del ejercicio
        public async Task<IActionResult> Details(int id)
        {
            var userData = _userDataManipulator.GetUserData();

            if (userData == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var apiResponse = await _apiConsumer.GET($"/api/ExerciseLibrary/GetExerciseById?exerciseId={id}", userData.Token);
            if (apiResponse.success)
            {
                var exercise = JsonConvert.DeserializeObject<Exercise>(apiResponse.response);
                return View(exercise);
            }
            else
            {
                return NotFound();
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var userData = _userDataManipulator.GetUserData();

            if (userData == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var apiResponse = await _apiConsumer.GET($"/api/ExerciseLibrary/GetExerciseById?exerciseId={id}", userData.Token);
            if (apiResponse.success)
            {
                var exercise = JsonConvert.DeserializeObject<Exercise>(apiResponse.response);
                return View(exercise);
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

            var apiResponse = await _apiConsumer.DELETE($"/api/ExerciseLibrary/DeleteExercise?exerciseId={id}", userData.Token);
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
