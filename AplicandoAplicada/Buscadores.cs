using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AplicandoAplicada
{
    public class Buscadores
    {
        public cliente ocliente(vehiculo ovehiculo)
        {
            cliente ocliente = new cliente();
            
            using (aplicadaBDEntities2 DBF = new aplicadaBDEntities2())
            {

                ocliente = (from q in DBF.cliente where q.id == ovehiculo.id_cliente select q).FirstOrDefault();

                

            }
            return ocliente;
        }

        public vehiculo buscarvehiculo(string patente)
        {
            vehiculo objvehiculo = new vehiculo();
            using (aplicadaBDEntities2 DBF = new aplicadaBDEntities2())
            {

                objvehiculo = (from q in DBF.vehiculo where q.patente == patente select q).FirstOrDefault();

                

            }
            return objvehiculo;
        }
        public modelo buscarmodelo(vehiculo objvehiculo)
        {
            modelo objmodelo= new modelo();
            using (aplicadaBDEntities2 DBF = new aplicadaBDEntities2())
            {
                
                objmodelo = (from q in DBF.modelo where q.id_modelo == objvehiculo.id_modelo select q).FirstOrDefault();
    

            }
            return objmodelo;
        }

        public marca buscarmarca(modelo objmodelo)
        {
            marca objmarca = new marca();
            using (aplicadaBDEntities2 DBF = new aplicadaBDEntities2())
            {

                objmarca = (from q in DBF.marca where q.id_marca == objmodelo.id_marca select q).FirstOrDefault();


            }
            return objmarca;
        }

        
        public List<empleado> Lempleado()
        {
            List<empleado> Lempleados = new List<empleado>();

            using (aplicadaBDEntities2 DBF = new aplicadaBDEntities2())
            {
                IQueryable<empleado> lista = (from q in DBF.empleado select q);
                Lempleados = lista.ToList() ;

            }
            return Lempleados;
        }

       

        public List<stock> Lstock()
        {
            List<stock> Lstock = new List<stock>();

            using (aplicadaBDEntities2 DBF = new aplicadaBDEntities2())
            {
                IQueryable<stock> lista = (from q in DBF.stock select q);
                Lstock = lista.ToList();

            }
            return Lstock;
        }

        public List<serviciostock> Lstockservi()
        {
            List<serviciostock> Lstockservi = new List<serviciostock>();

            using (aplicadaBDEntities2 DBF = new aplicadaBDEntities2())
            {
                IQueryable<serviciostock> lista = (from q in DBF.serviciostock select q);
                Lstockservi = lista.ToList();

            }
            return Lstockservi;
        }

        




    }
}