using CRUD_Alumnos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace CRUD_Alumnos.WebServices
{
    public class ServiceAlumno : IServiceAlumno
    {
        public bool AddAlumno(Alumno alumnoContract)
        {
            try
            {
                using (var db = new AlumnosContext())
                {
                    Models.ALUMNO entAlumno = new ALUMNO();
                    entAlumno.MOMENTO = System.DateTime.Now;

                    // PARSEO A MANO
                    entAlumno.NOMBRE = alumnoContract.NOMBRE;
                    entAlumno.APELLIDO = alumnoContract.APELLIDO;
                    entAlumno.EDAD = alumnoContract.EDAD;
                    entAlumno.SEXO = alumnoContract.SEXO;
                    entAlumno.CIUDAD_ID = alumnoContract.CIUDAD_ID;

                    db.ALUMNO.Add(entAlumno);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteAlumno(int id)
        {
            try
            {
                using (var db = new AlumnosContext())
                {
                    Models.ALUMNO a = db.ALUMNO.Find(id);

                    db.ALUMNO.Remove(a);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool EditAlumno(int id, Alumno alumno)
        {
            try
            {
                using (var db = new AlumnosContext())
                {
                    ALUMNO a = db.ALUMNO.Find(id);

                    a.NOMBRE = alumno.NOMBRE;
                    a.APELLIDO = alumno.APELLIDO;
                    a.SEXO = alumno.SEXO;
                    a.EDAD = alumno.EDAD;

                    db.SaveChanges();

                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Alumno GetAlumno(int id)
        {
            try
            {
                using (var db = new AlumnosContext())
                {
                    Models.ALUMNO a = db.ALUMNO.Find(id);
                    Alumno alumnoContract = new Alumno();

                    // PARSEO A MANO
                    alumnoContract.ID = a.ID;
                    alumnoContract.NOMBRE = a.NOMBRE;
                    alumnoContract.APELLIDO = a.APELLIDO;
                    alumnoContract.EDAD = a.EDAD;
                    alumnoContract.SEXO = a.SEXO;
                    alumnoContract.MOMENTO = a.MOMENTO;
                    alumnoContract.CIUDAD_ID = a.CIUDAD_ID;
                    alumnoContract.NombreCiudad = Controllers.AlumnoController.NombreCiudad((int)a.CIUDAD_ID);

                    return alumnoContract;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Alumno> GetAlumnoList()
        {
            try
            {
                using (var db = new AlumnosContext())
                {
                    List<Alumno> listadoAlumnos = new List<Alumno>();
                    foreach (var a in db.ALUMNO.ToList())
                    {
                        Alumno alumnoContract = new Alumno();

                        // PARSEO A MANO
                        alumnoContract.ID = a.ID;
                        alumnoContract.NOMBRE = a.NOMBRE;
                        alumnoContract.APELLIDO = a.APELLIDO;
                        alumnoContract.EDAD = a.EDAD;
                        alumnoContract.SEXO = a.SEXO;
                        alumnoContract.MOMENTO = a.MOMENTO;
                        alumnoContract.CIUDAD_ID = a.CIUDAD_ID;
                        alumnoContract.NombreCiudad = Controllers.AlumnoController.NombreCiudad((int)a.CIUDAD_ID);

                        listadoAlumnos.Add(alumnoContract);
                    }
                    return listadoAlumnos;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
