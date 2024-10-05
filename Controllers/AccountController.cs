using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FirstCrud.Controllers
{
    public class AccountController : Controller
    {
        // Acción para mostrar la página de login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // Acción para procesar el login
        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            // Aquí debes implementar la lógica para verificar las credenciales
            // Esto es un ejemplo simplificado
            if (username == "admin" && password == "password")
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, username)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true // Mantener la sesión activa
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                return RedirectToAction("Index", "Products"); // Redirigir a la página principal
            }

            // Si las credenciales no son válidas, mostrar el formulario nuevamente con un mensaje de error
            ViewBag.Error = "Invalid username or password";
            return View();
        }

        // Acción para cerrar la sesión
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}
