using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Zarabizi.Resources;

namespace Zarabizi.Models.Metadata
{


    [MetadataType(typeof(RecorridoMetaData))]
    public class Recorrido
    {
        public int IdRecorrido { get; set; }
        public Double DistanciaRecorrido { get; set; }
        public DateTime FechaSalidaRecorrido { get; set; }
        public DateTime FechaLlegadaRecorrido { get; set; }
        public int IdEstacionInicio { get; set; }
        public int IdEstacionFinal { get; set; }
        public int IdBicicleta { get; set; }
        public int IdSocio { get; set; }
        
        public Recorrido(int idRecorrido, Double distanciaRecorrido, DateTime fechaSalidaRecorrido, 
            DateTime fechaLlegadaRecorrido, int idEstacionInicio, int idEstacionFinal, int idBicicleta, int idSocio)
        {
            IdRecorrido = idRecorrido;
            DistanciaRecorrido = distanciaRecorrido;
            FechaSalidaRecorrido = fechaSalidaRecorrido;
            FechaLlegadaRecorrido = fechaLlegadaRecorrido;
            IdEstacionInicio = idEstacionInicio;
            IdEstacionFinal = idEstacionFinal;
            IdBicicleta = idBicicleta;
            IdSocio = idSocio;
        }

    }

    public class RecorridoMetaData
    {
        [Required]
        [Display(ResourceType = typeof(Messages), Name = "form_Recorrido_idRecorrido")]
        public global::System.Int32 idRecorrido;

        [Display(ResourceType = typeof(Messages), Name = "form_Recorrido_distanciaRecorrido")]
        public Nullable<global::System.Double> distanciaRecorrido;

        [Required]
        [Display(ResourceType = typeof(Messages), Name = "form_Recorrido_fechaSalidaRecorrido")]
        public global::System.DateTime fechaSalidaRecorrido;

        [Display(ResourceType = typeof(Messages), Name = "form_Recorrido_fechaLlegadaRecorrido")]
        public Nullable<global::System.DateTime> fechaLlegadaRecorrido;

        [Required]
        [Display(ResourceType = typeof(Messages), Name = "form_Recorrido_idEstacionInicio")]
        public global::System.Int32 idEstacionInicio;

        [Display(ResourceType = typeof(Messages), Name = "form_Recorrido_idEstacionFinal")]
        public Nullable<global::System.Int32> idEstacionFinal;

        [Display(ResourceType = typeof(Messages), Name = "form_Recorrido_idBicicleta")]
        public Nullable<global::System.Int32> idBicicleta;

        [Display(ResourceType = typeof(Messages), Name = "form_Recorrido_idSocio")]
        public Nullable<global::System.Int32> idSocio;

    }
}