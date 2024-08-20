using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace WEB.BioFitAdvisor.Core
{
    public class SessionValidatorAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Session.GetString("UserData") == null)
            {
                // Si UserData no existe en la sesión, redirige a otra ventana
                context.Result = new RedirectToActionResult("Auth", "Login", null);
            }
            else
            {
                base.OnActionExecuting(context);
            }
        }
    }
}
