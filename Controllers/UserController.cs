using CRUD_application_2.Models;
using System.Linq;
using System.Web.Mvc;
 
namespace CRUD_application_2.Controllers
{
    public class UserController : Controller
    {
        public static System.Collections.Generic.List<User> userlist = new System.Collections.Generic.List<User>();
        // GET: User
        public ActionResult Index()
        {
            // Retrieve all users from the userlist
            var users = userlist.ToList();

            // Pass the list of users to the Index view
            return View(users);
        }

        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            // Implement the details method here
            // Retrieve the user with the specified ID from the userlist
            var user = userlist.FirstOrDefault(u => u.Id == id);

            // If the user is found, pass it to the Details view
            if (user != null)
            {
                return View(user);
            }

            // If no user is found, return a HttpNotFoundResult
            return HttpNotFound();
        }

        // GET: User/Create
        public ActionResult Create()
        {
            // Implement the Create method here
            return View();
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult Create(User user)
        {
            // Implement the Create method (POST) here
            // Add the new user to the userlist
            userlist.Add(user);

            // Redirect to the Index action to display the updated list of users
            return RedirectToAction("Index");
        }

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            // This method is responsible for displaying the view to edit an existing user with the specified ID.
            // It retrieves the user from the userlist based on the provided ID and passes it to the Edit view.
            // Retrieve the user with the specified ID from the userlist
            var user = userlist.FirstOrDefault(u => u.Id == id);

            // If the user is found, pass it to the Edit view
            if (user != null)
            {
                return View(user);
            }

            // If no user is found, return a HttpNotFoundResult
            return HttpNotFound();
        }

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, User user)
        {
            // This method is responsible for handling the HTTP POST request to update an existing user with the specified ID.
            // It receives user input from the form submission and updates the corresponding user's information in the userlist.
            // If successful, it redirects to the Index action to display the updated list of users.
            // If no user is found with the provided ID, it returns a HttpNotFoundResult.
            // If an error occurs during the process, it returns the Edit view to display any validation errors.
            // Retrieve the user with the specified ID from the userlist
            var existingUser = userlist.FirstOrDefault(u => u.Id == id);

            // If the user is found, update its information
            if (existingUser != null)
            {
                existingUser.Name = user.Name;
                existingUser.Email = user.Email;
                existingUser.Age = user.Age;

                // Redirect to the Index action to display the updated list of users
                return RedirectToAction("Index");
            }

            // If no user is found, return a HttpNotFoundResult
            return HttpNotFound();
        }

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            // Implement the Delete method here
            // Retrieve the user with the specified ID from the userlist
            var user = userlist.FirstOrDefault(u => u.Id == id);

            // If the user is found, pass it to the Delete view
            if (user != null)
            {
                return View(user);
            }

            // If no user is found, return a HttpNotFoundResult
            return HttpNotFound();
        }

        // POST: User/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            // Implement the Delete method (POST) here
            // Retrieve the user with the specified ID from the userlist
            var user = userlist.FirstOrDefault(u => u.Id == id);

            // If the user is found, remove it from the userlist
            if (user != null)
            {
                userlist.Remove(user);

                // Redirect to the Index action to display the updated list of users
                return RedirectToAction("Index");
            }

            // If no user is found, return a HttpNotFoundResult
            return HttpNotFound();
        }

        // GET: User/Reset
        public ActionResult Reset()
        {
            // Implement the Reset method here
            // Clear the userlist
            userlist.Clear();

            // Redirect to the Index action to display the empty list of users
            return RedirectToAction("Index");
        }
    }
}
