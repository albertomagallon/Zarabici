using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Zarabizi.Resources;

namespace Zarabizi.Models.Metadata
{
    [MetadataType(typeof(IncidenciaMetaData))]
    public class Incidencia
    {
    }

    public class IncidenciaMetaData
    {
        [Required]
        [Display(ResourceType = typeof(Messages), Name = "form_Incidencia_idIncidencia")]
        public global::System.Int32 idIncidencia;

        [Required]
        [Display(ResourceType = typeof(Messages), Name = "form_Incidencia_textoIncidencia")]
        [StringLength(50)]
        public global::System.String textoIncidencia;

        [Required]
        [Display(ResourceType = typeof(Messages), Name = "form_Incidencia_fechaIncidencia")]
        public global::System.DateTime fechaIncidencia;

        [Display(ResourceType = typeof(Messages), Name = "form_Incidencia_idSocio")]
        public Nullable<global::System.Int32> idSocio;

    }
}