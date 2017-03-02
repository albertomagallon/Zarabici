using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Zarabizi.Resources;

namespace Zarabizi.Models.Metadata
{
    [MetadataType(typeof(RepararEstacionMetaData))]
    public class RepararEstacion
    {
    }

    public class RepararEstacionMetaData
    {
        [Required]
        [Display(ResourceType = typeof(Messages), Name = "form_RepararEstacion_idRepararEstacion")]
        public global::System.Int32 idRepararEstacion;

        [Display(ResourceType = typeof(Messages), Name = "form_RepararEstacion_idEmpleado")]
        public Nullable<global::System.Int32> idEmpleado;

        [Display(ResourceType = typeof(Messages), Name = "form_RepararEstacion_idEstacion")]
        public Nullable<global::System.Int32> idEstacion;

        [Required]
        [Display(ResourceType = typeof(Messages), Name = "form_RepararEstacion_descripcionRepararEstacion")]
        [StringLength(50)]
        public global::System.String descripcionRepararEstacion;

        [Required]
        [Display(ResourceType = typeof(Messages), Name = "form_RepararBicicleta_fechaRepararEstacion")]
        public global::System.DateTime fechaRepararEstacion;


    }
}