using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using employeeCrudOpertaions.Models;
using System.Net.Http;

namespace employeeCrudOpertaions.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            IEnumerable<MvcModel> emplist;

            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Employees").Result;
            emplist = response.Content.ReadAsAsync<IEnumerable<MvcModel>>().Result;
            return View(emplist);
        }

        public ActionResult AddorEdit(int id=0) {
            if (id == 0)
                return View(new MvcModel());
            else {
                HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Employees/" + id.ToString()).Result;
                return View(response.Content.ReadAsAsync<MvcModel>().Result);
            }
        }

        [HttpPost]
        public ActionResult AddorEdit(MvcModel emp)
        {
            if (emp.EmployeeID == 0)
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsJsonAsync("Employees", emp).Result;
                TempData["successMsg"] = "Saved Successfully";
            }
            else {
                HttpResponseMessage response = GlobalVariables.WebApiClient.PutAsJsonAsync("Employees/"+emp.EmployeeID, emp).Result;
                TempData["successMsg"] = "Updated Successfully";
            }

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id) {
            HttpResponseMessage response = GlobalVariables.WebApiClient.DeleteAsync("Employees/"+id.ToString()).Result;
            TempData["successMsg"] = "Deleted Successfully";
            return RedirectToAction("Index");
        }
    }
}