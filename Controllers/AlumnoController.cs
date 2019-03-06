using CRUD_Alumnos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRUD_Alumnos.Controllers
{
    public class AlumnoController : Controller
    {
        // GET: Alumno
        public ActionResult Index()
        {
            try
            {
                using (var db = new AlumnosContext())
                {
                    var data = from a in db.ALUMNO
                               join c in db.CIUDAD on a.CIUDAD_ID equals c.ID
                               select new AlumnoCE
                               {
                                   ID = a.ID,
                                   NOMBRE = a.NOMBRE,
                                   APELLIDO = a.APELLIDO,
                                   EDAD = a.EDAD,
                                   SEXO = a.SEXO,
                                   MOMENTO = a.MOMENTO,
                                   CIUDAD_ID = a.CIUDAD_ID,
                                   NombreCiudad = c.NOMBRE,
                               };

                    //List<ALUMNO> alumnos = db.ALUMNO.Where(a => a.EDAD > 18).ToList();
                    return View(data.ToList());
                }
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public static string NombreCiudad(int id)
        {
            try
            {
                using (var db = new AlumnosContext())
                {
                    return db.CIUDAD.Find(id).NOMBRE;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ActionResult AlumnoServiceDescription()
        {
            return View();
        }



        public ActionResult AlumnoWebForms()
        {
            return View();
        }

        public ActionResult Agregar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Agregar(ALUMNO al)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                using (var db = new AlumnosContext())
                {
                    al.MOMENTO = System.DateTime.Now;
                    db.ALUMNO.Add(al);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al registrar alumno - " + ex.Message);
                return View();
            }

        }

        public ActionResult ListaCiudades()
        {
            try
            {
                using (var db = new AlumnosContext())
                {
                    return PartialView(db.CIUDAD.ToList());
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ActionResult Edit(int id)
        {
            try
            {
                using (var db = new AlumnosContext())
                {
                    ALUMNO a = db.ALUMNO.Find(id);
                    return View(a);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ALUMNO pAlumno)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                using (var db = new AlumnosContext())
                {
                    ALUMNO a = db.ALUMNO.Find(pAlumno.ID);
                    a.NOMBRE = pAlumno.NOMBRE;
                    a.APELLIDO = pAlumno.APELLIDO;
                    a.SEXO = pAlumno.SEXO;
                    a.EDAD = pAlumno.EDAD;

                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ActionResult Details(int id)
        {
            try
            {
                using (var db = new AlumnosContext())
                {
                    ALUMNO a = db.ALUMNO.Find(id);
                    return View(a);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                using (var db = new AlumnosContext())
                {
                    ALUMNO a = db.ALUMNO.Find(id);
                    db.ALUMNO.Remove(a);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}