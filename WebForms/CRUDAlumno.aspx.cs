using CRUD_Alumnos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace CRUD_Alumnos.WebForms
{
    public partial class CRUDAlumno : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BuscarAlumnoResultado.Visible = false;
                poblarGV();
                poblarDdlCiudades();
                vaciarBusquedaAlumno();
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            if (!(tbNombre.Text == "" || tbApellido.Text == "" || tbEdad.Text == ""))
            {
                using (var db = new AlumnosContext())
                {
                    ALUMNO nuevoAlumno = new ALUMNO
                    {
                        //PARSEO A MANO
                        NOMBRE = tbNombre.Text,
                        APELLIDO = tbApellido.Text,
                        EDAD = Convert.ToInt16(tbEdad.Text),
                        SEXO = ddlSexo.SelectedValue,
                        CIUDAD_ID = Convert.ToInt32(ddlCiudades.SelectedValue),
                        MOMENTO = System.DateTime.Now
                    };

                    db.ALUMNO.Add(nuevoAlumno);
                    db.SaveChanges();

                    poblarGV();
                }

                // REINICIAR CAMPOS
                tbNombre.Text = "";
                tbApellido.Text = "";
                tbEdad.Text = "";
                ddlSexo.SelectedIndex = 0;
                ddlCiudades.SelectedIndex = 0;
            }
        }

        protected void btnBuscarAlumno_Click(object sender, EventArgs e)
        {
            if (!(tbBuscaAlumno.Text == ""))
            {
                try
                {
                    int idAlumno = Convert.ToInt32(tbBuscaAlumno.Text);
                    using (var db = new AlumnosContext())
                    {
                        var data = from a in db.ALUMNO
                                   join c in db.CIUDAD on a.CIUDAD_ID equals c.ID
                                   where a.ID == idAlumno
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

                        //PARSEO A MANO
                        lblAlumnoResultado_ID.Text = data.FirstOrDefault().ID.ToString();
                        lblAlumnoResultado_Nombre.Text = data.FirstOrDefault().NOMBRE;
                        lblAlumnoResultado_Apellido.Text = data.FirstOrDefault().APELLIDO;
                        lblAlumnoResultado_Edad.Text = data.FirstOrDefault().EDAD.ToString();
                        lblAlumnoResultado_Sexo.Text = data.FirstOrDefault().SEXO;
                        lblAlumnoResultado_Ciudad.Text = data.FirstOrDefault().NombreCiudad;

                        BuscarAlumnoResultado.Visible = true;
                        lblNotFound.Visible = false;
                        lblBorradoConExito.Visible = false;
                        btnGuardarEdicion.Visible = false;
                        btnEliminarAlumno.Enabled = true;
                        btnEditarAlumno.Enabled = true;
                        btnGuardarEdicion.Enabled = true;

                    }
                }
                catch (Exception)
                {
                    lblNotFound.Visible = true;
                }

                // REINICIAR CAMPOS
                tbBuscaAlumno.Text = "";
            }
        }

        protected void vaciarBusquedaAlumno()
        {
            lblAlumnoResultado_Nombre.Text = "";
            lblAlumnoResultado_Apellido.Text = "";
            lblAlumnoResultado_Edad.Text = "";
            lblAlumnoResultado_Sexo.Text = "";
            lblAlumnoResultado_Ciudad.Text = "";
        }

        protected void btnEditarAlumno_Click(object sender, EventArgs e)
        {
            // Escondo los Labels
            lblAlumnoResultado_Nombre.Visible = false;
            lblAlumnoResultado_Apellido.Visible = false;
            lblAlumnoResultado_Edad.Visible = false;
            lblAlumnoResultado_Sexo.Visible = false;
            lblAlumnoResultado_Ciudad.Visible = false;

            // Muestro los TextBox
            tbEditarAlumno_Nombre.Visible = true;
            tbEditarAlumno_Apellido.Visible = true;
            tbEditarAlumno_Edad.Visible = true;
            ddlEditarAlumno_Sexo.Visible = true;
            ddlEditarAlumno_Ciudad.Visible = true;

            // Extraigo data de los Labels para rellenar los TextBoxs
            tbEditarAlumno_Nombre.Text = lblAlumnoResultado_Nombre.Text;
            tbEditarAlumno_Apellido.Text = lblAlumnoResultado_Apellido.Text;
            tbEditarAlumno_Edad.Text = lblAlumnoResultado_Edad.Text;

            using (var db = new AlumnosContext())
            {
                int id = Convert.ToInt32(lblAlumnoResultado_ID.Text);
                ALUMNO alumno = db.ALUMNO.Find(id);

                ddlEditarAlumno_Sexo.SelectedValue = alumno.SEXO.ToString();

                ddlEditarAlumno_Ciudad.DataSource = db.CIUDAD.ToList();
                ddlEditarAlumno_Ciudad.DataTextField = "NOMBRE";
                ddlEditarAlumno_Ciudad.DataValueField = "ID";
                ddlEditarAlumno_Ciudad.DataBind();
                ddlEditarAlumno_Ciudad.SelectedValue = alumno.CIUDAD_ID.ToString();
            }

            btnGuardarEdicion.Visible = true;

        }

        protected void btnEliminarAlumno_Click(object sender, EventArgs e)
        {
            try
            {
                int idAlumno = Convert.ToInt32(lblAlumnoResultado_ID.Text);
                using (var db = new AlumnosContext())
                {
                    ALUMNO aluBorrado = db.ALUMNO.Find(idAlumno);

                    db.ALUMNO.Remove(aluBorrado);
                    db.SaveChanges();

                    lblBorradoConExito.ForeColor = System.Drawing.Color.DarkRed;
                    lblBorradoConExito.Visible = true;
                    poblarGV();

                    btnEliminarAlumno.Enabled = false;
                    btnEditarAlumno.Enabled = false;
                    btnGuardarEdicion.Enabled = false;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void btnGuardarEdicion_Click(object sender, EventArgs e)
        {
            if (!(tbEditarAlumno_Nombre.Text == "" || tbEditarAlumno_Apellido.Text == "" || tbEditarAlumno_Edad.Text == ""))
            {
                using (var db = new AlumnosContext())
                {
                    int id = Convert.ToInt32(lblAlumnoResultado_ID.Text);
                    ALUMNO alumnoEditado = db.ALUMNO.Find(id);

                    //PARSEO A MANO
                    alumnoEditado.NOMBRE = tbEditarAlumno_Nombre.Text;
                    alumnoEditado.APELLIDO = tbEditarAlumno_Apellido.Text;
                    alumnoEditado.EDAD = Convert.ToInt16(tbEditarAlumno_Edad.Text);
                    alumnoEditado.SEXO = ddlEditarAlumno_Sexo.SelectedValue;
                    alumnoEditado.CIUDAD_ID = Convert.ToInt32(ddlEditarAlumno_Ciudad.SelectedValue);

                    if (db.SaveChanges() > 0)
                    {
                        // Muestro los Labels
                        lblAlumnoResultado_Nombre.Visible = true;
                        lblAlumnoResultado_Apellido.Visible = true;
                        lblAlumnoResultado_Edad.Visible = true;
                        lblAlumnoResultado_Sexo.Visible = true;
                        lblAlumnoResultado_Ciudad.Visible = true;

                        // Escondo los TextBox
                        tbEditarAlumno_Nombre.Visible = false;
                        tbEditarAlumno_Apellido.Visible = false;
                        tbEditarAlumno_Edad.Visible = false;
                        ddlEditarAlumno_Sexo.Visible = false;
                        ddlEditarAlumno_Ciudad.Visible = false;

                        // Igualo de lado cliente ya que esta validado.
                        lblAlumnoResultado_Nombre.Text = tbEditarAlumno_Nombre.Text;
                        lblAlumnoResultado_Apellido.Text = tbEditarAlumno_Apellido.Text;
                        lblAlumnoResultado_Edad.Text = tbEditarAlumno_Edad.Text;
                        lblAlumnoResultado_Sexo.Text = ddlEditarAlumno_Sexo.SelectedValue;
                        lblAlumnoResultado_Ciudad.Text = ddlEditarAlumno_Ciudad.SelectedValue;

                        poblarGV();
                    }
                }
            }
        }

        protected void poblarGV()
        {
            gvAlumnos.DataSource = getAlumnos();
            gvAlumnos.DataBind();
        }

        protected void poblarDdlCiudades()
        {
            using (var db = new AlumnosContext())
            {
                ddlCiudades.DataSource = db.CIUDAD.ToList();
                ddlCiudades.DataTextField = "NOMBRE";
                ddlCiudades.DataValueField = "ID";
                ddlCiudades.DataBind();
            }
        }

        protected List<AlumnoCE> getAlumnos()
        {
            using (var db = new AlumnosContext())
            {
                var data = from a in db.ALUMNO
                           join c in db.CIUDAD on a.CIUDAD_ID equals c.ID
                           orderby a.ID
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
                return data.ToList();
            }
        }
    }
}