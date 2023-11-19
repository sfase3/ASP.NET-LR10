using Microsoft.AspNetCore.Mvc;

namespace LR10.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Products = new List<string> { "JavaScript", "C#", "Java", "Python", "Основи" };
            return View("Index");
        }

        [HttpPost]
        public ActionResult Register(string name, string email, DateTime consultationDate, string selectedProduct)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email) || consultationDate == DateTime.MinValue || string.IsNullOrEmpty(selectedProduct))
            {
                ViewBag.Error = "Fill all fields";
                ViewBag.Products = new List<string> { "JavaScript", "C#", "Java", "Python", "Основи" };
                return View("Index");
            }

            if (!IsValidEmail(email))
            {
                ViewBag.Error = "Invalid Email.";
                ViewBag.Products = new List<string> { "JavaScript", "C#", "Java", "Python", "Основи" };
                return View("Index");
            }

            if (consultationDate <= DateTime.Now || consultationDate.DayOfWeek == DayOfWeek.Saturday || consultationDate.DayOfWeek == DayOfWeek.Sunday)
            {
                ViewBag.Error = "Please select a correct consultation date in the future, not a holiday.";
                ViewBag.Products = new List<string> { "JavaScript", "C#", "Java", "Python", "Основи" };
                return View("Index");
            }

            if (selectedProduct == "Основи" && consultationDate.DayOfWeek == DayOfWeek.Monday)
            {
                ViewBag.Error = "Consultations on the 'Основи' product are not held on Mondays.";
                ViewBag.Products = new List<string> { "JavaScript", "C#", "Java", "Python", "Основи" };
                return View("Index");
            }

            ViewBag.Success = "You have successfully registered for a consultation!";
            return View("Index");
        }

        private bool IsValidEmail(string email)
        {
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
