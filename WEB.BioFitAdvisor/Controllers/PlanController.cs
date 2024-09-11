using API.BioFitAdvisor.Domain;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WEB.BioFitAdvisor.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WEB.BioFitAdvisor.Controllers
{
    public class PlanController : Controller
    {
        private readonly ApiConsumer _apiConsumer;
        private readonly UserDataManipulator _userDataManipulator;

        public PlanController(ApiConsumer apiConsumer, UserDataManipulator userDataManipulator)
        {
            _apiConsumer = apiConsumer;
            _userDataManipulator = userDataManipulator;
        }

        // Vista principal para listar planes
        public async Task<IActionResult> Index()
        {
            var userData = _userDataManipulator.GetUserData();
            if (userData == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var apiResponse = await _apiConsumer.GET("/api/TrainingPlan/GetAllPlans", userData.Token);
            if (apiResponse.success)
            {
                var plans = JsonConvert.DeserializeObject<IEnumerable<Plan>>(apiResponse.response);
                return View(plans);
            }
            else
            {
                return View(new List<Plan>());
            }
        }

        // Vista para crear o editar un plan
        public async Task<IActionResult> CreateOrEdit(int id = 0)
        {
            var userData = _userDataManipulator.GetUserData();
            if (userData == null)
            {
                return RedirectToAction("Login", "Account");
            }

            if (id == 0)
            {
                return View(new Plan());
            }
            else
            {
                var apiResponse = await _apiConsumer.GET($"/api/TrainingPlan/GetPlanById?planId={id}", userData.Token);
                if (apiResponse.success)
                {
                    var plan = JsonConvert.DeserializeObject<Plan>(apiResponse.response);
                    return View(plan);
                }
                else
                {
                    return NotFound();
                }
            }
        }

        // Acción POST para manejar la creación/edición de Plan
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateOrEdit(Plan plan)
        {
            var userData = _userDataManipulator.GetUserData();
            if (userData == null)
            {
                return RedirectToAction("Login", "Account");
            }

            if (ModelState.IsValid)
            {
                string jsonPlan = JsonConvert.SerializeObject(plan);
                var apiResponse = await _apiConsumer.POST("/api/TrainingPlan/UpsertPlan", userData.Token, jsonPlan);
                if (apiResponse.success)
                {
                    return RedirectToAction(nameof(Details), new { id = plan.PlanId });
                }
                else
                {
                    ModelState.AddModelError("", "Error al guardar el plan.");
                }
            }
            return View(plan);
        }

        // Vista para ver los detalles de un plan
        public async Task<IActionResult> Details(int id)
        {
            var userData = _userDataManipulator.GetUserData();
            if (userData == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var apiResponse = await _apiConsumer.GET($"/api/TrainingPlan/GetPlanById?planId={id}", userData.Token);
            if (apiResponse.success)
            {
                var plan = JsonConvert.DeserializeObject<Plan>(apiResponse.response);
                return View(plan);
            }
            else
            {
                return NotFound();
            }
        }

        // Vista para eliminar un plan
        public async Task<IActionResult> Delete(int id)
        {
            var userData = _userDataManipulator.GetUserData();
            if (userData == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var apiResponse = await _apiConsumer.GET($"/api/TrainingPlan/GetPlanById?planId={id}", userData.Token);
            if (apiResponse.success)
            {
                var plan = JsonConvert.DeserializeObject<Plan>(apiResponse.response);
                return View(plan);
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

            var apiResponse = await _apiConsumer.DELETE($"/api/TrainingPlan/DeletePlan?planId={id}", userData.Token);
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
