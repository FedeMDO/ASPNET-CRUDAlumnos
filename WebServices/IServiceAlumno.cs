using CRUD_Alumnos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace CRUD_Alumnos.WebServices
{
    [ServiceContract]
    public interface IServiceAlumno
    {
        [OperationContract]
        bool AddAlumno(Alumno alumno);

        [OperationContract]
        bool EditAlumno(int id, Alumno alumno);

        [OperationContract]
        Alumno GetAlumno(int id);

        [OperationContract]
        List<Alumno> GetAlumnoList();

        [OperationContract]
        bool DeleteAlumno(int id);
    }

    [DataContract]
    public class Alumno
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public string NOMBRE { get; set; }

        [DataMember]
        public string APELLIDO { get; set; }

        [DataMember]
        public int EDAD { get; set; }

        [DataMember]
        public string SEXO { get; set; }

        [DataMember]
        public System.DateTime MOMENTO { get; set; }

        [DataMember]
        public Nullable<int> CIUDAD_ID { get; set; }

        [DataMember]
        public string NombreCiudad { get; set; }
    }

}
