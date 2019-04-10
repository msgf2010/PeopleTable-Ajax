using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Class57.Data;
using Class57_Homework.Models;

namespace Class57_Homework.Controllers
{
    //Create an application that manages a list of people. All the interactions with the back-end will be done via Ajax.
    //Here's the basic structure:
    //When the page loads, the user will see three textboxes and a button. These textboxes will be used to add a new person to the
    //database(via ajax). Beneath these buttons, there should be a table that displays a list of all the people in the database.
    //The simplest way to populate this table is to always clear it out, and just get all the people from the db and repopulate.
    //(The table should also be filled in as soon as the page loads the first time).
    //On each row of the table, there should also be an Edit button. When the edit button is clicked,
    //a modal should be displayed with all the textboxes prefilled with that persons information.
    //On the modal, there should be an update button. When clicked, the modal should close and that person should be updated,
    //and the table should refresh.
    //Also on every line, there should be a delete button that when clicked, will delete that person from the DB.
    //Again, the table should refresh once this is done.

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Manager mgr = new Manager(Properties.Settings.Default.ConStr);
            PeopleModelView mv = new PeopleModelView();
            mv.People = mgr.GetPeople();

            return View(mv);
        }

        public ActionResult AddPerson(Person person)
        {
            Manager mgr = new Manager(Properties.Settings.Default.ConStr);
            mgr.AddPerson(person);

            List<Person> people = mgr.GetPeople();

            return Json(people, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdatePerson(Person person)
        {
            Manager mgr = new Manager(Properties.Settings.Default.ConStr);
            mgr.UpdatePerson(person);

            List<Person> people = mgr.GetPeople();

            return Json(people, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeletePerson(int id)
        {
            Manager mgr = new Manager(Properties.Settings.Default.ConStr);
            mgr.DeletePerson(id);

            List<Person> people = mgr.GetPeople();

            return Json(people, JsonRequestBehavior.AllowGet);
        }
    }
}