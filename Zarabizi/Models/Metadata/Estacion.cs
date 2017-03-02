using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Zarabizi.Resources;

namespace Zarabizi.Models.Metadata
{
    [MetadataType(typeof(EstacionMetaData))]
    public class Estacion
    {
    }

    public class EstacionMetaData
    {
        [Required]
        [Display(ResourceType = typeof(Messages), Name = "form_Estacion_idEstacion")]
        public global::System.Int32 idEstacion;

        [Required]
        [Display(ResourceType = typeof(Messages), Name = "form_Estacion_nombreEstacion")]
        [StringLength(20)]
        public global::System.String nombreEstacion;

        [Required]
        [Display(ResourceType = typeof(Messages), Name = "form_Estacion_ubicacionEstacion")]
        [StringLength(50)]
        public global::System.String ubicacionEstacion;

        [Display(ResourceType = typeof(Messages), Name = "form_Estacion_anclajesEstacion")]
        public global::System.Int32 anclajesEstacion;


    }
}