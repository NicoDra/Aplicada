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
            
            using (aplicadaBDEntities DBF = new aplicadaBDEntities())
            {

                ocliente = (from q in DBF.cliente where q.id == ovehiculo.id_cliente select q).FirstOrDefault();

                

            }
            return ocliente;
        }

        public vehiculo buscarvehiculo(string patente)
        {
            vehiculo objvehiculo = new vehiculo();
            using (aplicadaBDEntities DBF = new aplicadaBDEntities())
            {

                objvehiculo = (from q in DBF.vehiculo where q.patente == patente select q).FirstOrDefault();

                

            }
            return objvehiculo;
        }
        public modelo buscarmodelo(vehiculo objvehiculo)
        {
            modelo objmodelo= new modelo();
            using (aplicadaBDEntities DBF = new aplicadaBDEntities())
            {
                
                objmodelo = (from q in DBF.modelo where q.id_modelo == objvehiculo.id_modelo select q).FirstOrDefault();
    

            }
            return objmodelo;
        }

        public List<marca> Lmarca()
        {
            List<marca> Lmarca;

            using (aplicadaBDEntities DBF = new aplicadaBDEntities())
            {
                IQueryable<marca> lista = (from q in DBF.marca select q);
                Lmarca = lista.ToList();
                



            }
            return Lmarca;
        }




    }
}