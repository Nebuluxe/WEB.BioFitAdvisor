using API.BioFitAdvisor.Domain;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WEB.BioFitAdvisor.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WEB.BioFitAdvisor.Controllers
{
    public class RoutineController : Controller
    {
        private readonly ApiConsumer _apiConsumer;
        private readonly UserDataManipulator _userDataManipulator;

        public RoutineController(ApiConsumer apiConsumer, UserDataManipulator userDataManipulator)
        {
            _apiConsumer = apiConsumer;
            _userDataManipulator = userDataManipulator;
        }

        // Vista principal para listar rutinas
        public async Task<IActionResult> Index()
        {
            var userData = _userDataManipulator.GetUserData();
            if (userData == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var apiResponse = await _apiConsumer.GET("/api/Routine/GetAllRoutines", userData.Token);
            if (apiResponse.success)
            {
                var routines = JsonConvert.DeserializeObject<IEnumerable<Routine>>(apiResponse.response);
                return View(routines);
            }
            else
            {
                return View(new List<Routine>());
            }
        }

        // Vista para crear o editar una rutina
        public async Task<IActionResult> CreateOrEdit(int id = 0)
        {
            var userData = _userDataManipulator.GetUserData();
            if (userData == null)
            {
                return RedirectToAction("Login", "Account");
            }

            if (id == 0)
            {
                return View(new Routine());
            }
            else
            {
                var apiResponse = await _apiConsumer.GET($"/api/Routine/GetRoutineById?routineId={id}", userData.Token);
                if (apiResponse.success)
                {
                    var routine = JsonConvert.DeserializeObject<Routine>(apiResponse.response);
                    return View(routine);
                }
                else
                {
                    return NotFound();
                }
            }
        }

        // Acción POST para manejar la creación/edición de Rutina
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateOrEdit(Routine routine)
        {
            var userData = _userDataManipulator.GetUserData();
            if (userData == null)
            {
                return RedirectToAction("Login", "Account");
            }

            if (ModelState.IsValid)
            {
                string jsonRoutine = JsonConvert.SerializeObject(routine);
                var apiResponse = await _apiConsumer.POST("/api/Routine/UpsertRoutine", userData.Token, jsonRoutine);
                if (apiResponse.success)
                {
                    return RedirectToAction(nameof(Details), new { id = routine.RoutineId });
                }
                else
                {
                    ModelState.AddModelError("", "Error al guardar la rutina.");
                }
            }
            return View(routine);
        }

        // Vista para ver los detalles de una rutina
        public async Task<IActionResult> Details(int id)
        {
            var userData = _userDataManipulator.GetUserData();
            if (userData == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var apiResponse = await _apiConsumer.GET($"/api/Routine/GetRoutineById?routineId={id}", userData.Token);
            if (apiResponse.success)
            {
                var routine = JsonConvert.DeserializeObject<Routine>(apiResponse.response);
                return View(routine);
            }
            else
            {
                return NotFound();
            }
        }

        // Vista para eliminar una rutina
        public async Task<IActionResult> Delete(int id)
        {
            var userData = _userDataManipulator.GetUserData();
            if (userData == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var apiResponse = await _apiConsumer.GET($"/api/Routine/GetRoutineById?routineId={id}", userData.Token);
            if (apiResponse.success)
            {
                var routine = JsonConvert.DeserializeObject<Routine>(apiResponse.response);
                return View(routine);
            }
            else
            {
                return NotFound();
            }
        }

        // Acción POST para confirmar la eliminación
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userData = _userDataManipulator.GetUserData();
            if (userData == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var apiResponse = await _apiConsumer.DELETE($"/api/Routine/DeleteRoutine?routineId={id}", userData.Token);
            if (apiResponse.success)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View();
            }
        }

        // Método privado para cargar componentes de la rutina (Diets, Workouts, SupplementPlans)
        private async Task LoadRoutineComponents(Routine routine, string token)
        {
            // Cargar Dietas
            var dietResponse = await _apiConsumer.GET($"/api/Diets/SearchDietsByName?name={routine.Name}", token);
            if (dietResponse.success)
            {
                routine.Diets = JsonConvert.DeserializeObject<IList<Diet>>(dietResponse.response);
            }

            // Cargar Workouts
            var workoutResponse = await _apiConsumer.GET($"/api/Workouts/SearchWorkoutsByRoutineId?routineId={routine.RoutineId}", token);
            if (workoutResponse.success)
            {
                routine.Exercises = JsonConvert.DeserializeObject<IList<Workout>>(workoutResponse.response);
            }

            // Cargar Supplement Plans
            var supplementResponse = await _apiConsumer.GET($"/api/SupplementPlans/SearchSupplementPlansByName?name={routine.Name}", token);
            if (supplementResponse.success)
            {
                routine.SupplementPlans = JsonConvert.DeserializeObject<IList<SupplementPlan>>(supplementResponse.response);
            }
        }

        // **Acciones para manejar Diets, Workouts y SupplementPlans en la misma ventana de Routine**

        // Agregar o editar una Diet dentro de una Rutina
        [HttpPost]
        public async Task<IActionResult> AddOrUpdateDiet(int routineId, Diet diet)
        {
            var userData = _userDataManipulator.GetUserData();
            if (userData == null)
            {
                return RedirectToAction("Login", "Account");
            }

            if (ModelState.IsValid)
            {
                string jsonDiet = JsonConvert.SerializeObject(diet);
                var apiResponse = await _apiConsumer.POST("/api/Diets/UpsertDiet", userData.Token, jsonDiet);
                if (apiResponse.success)
                {
                    return RedirectToAction(nameof(Details), new { id = routineId });
                }
                else
                {
                    ModelState.AddModelError("", "Error al guardar la dieta.");
                }
            }

            return RedirectToAction(nameof(Details), new { id = routineId });
        }

        // Agregar o editar un Workout dentro de una Rutina
        [HttpPost]
        public async Task<IActionResult> AddOrUpdateWorkout(int routineId, Workout workout)
        {
            var userData = _userDataManipulator.GetUserData();
            if (userData == null)
            {
                return RedirectToAction("Login", "Account");
            }

            if (ModelState.IsValid)
            {
                string jsonWorkout = JsonConvert.SerializeObject(workout);
                var apiResponse = await _apiConsumer.POST("/api/Workouts/UpsertWorkout", userData.Token, jsonWorkout);
                if (apiResponse.success)
                {
                    return RedirectToAction(nameof(Details), new { id = routineId });
                }
                else
                {
                    ModelState.AddModelError("", "Error al guardar el workout.");
                }
            }

            return RedirectToAction(nameof(Details), new { id = routineId });
        }

        // Agregar o editar un Supplement Plan dentro de una Rutina
        [HttpPost]
        public async Task<IActionResult> AddOrUpdateSupplementPlan(int routineId, SupplementPlan supplementPlan)
        {
            var userData = _userDataManipulator.GetUserData();
            if (userData == null)
            {
                return RedirectToAction("Login", "Account");
            }

            if (ModelState.IsValid)
            {
                string jsonSupplementPlan = JsonConvert.SerializeObject(supplementPlan);
                var apiResponse = await _apiConsumer.POST("/api/SupplementPlans/UpsertSupplementPlan", userData.Token, jsonSupplementPlan);
                if (apiResponse.success)
                {
                    return RedirectToAction(nameof(Details), new { id = routineId });
                }
                else
                {
                    ModelState.AddModelError("", "Error al guardar el plan de suplementos.");
                }
            }

            return RedirectToAction(nameof(Details), new { id = routineId });
        }
    }
}
