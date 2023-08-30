using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Disco
    {
        //get y set
        [DisplayName("Numero de Id")]
        public int Id { get; set; }
        public string Titulo { get; set; }
        public DateTime Fecha { get; set; }

        [DisplayName("Cantidad de canciones")]
        public int CantidadCanciones{get; set;} 
        public string UrlTapa { get; set; }


        [DisplayName("Tipo de Estilo")]
        public Estilo TipoEsti { get; set; }

        [DisplayName("Tipo de Edicion")]
        public TipoEdicion TipoEdi { get; set; }

        
       

        
    }
}
