using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Zarabizi.Resources;

namespace Zarabizi.Models.Metadata
{
    [MetadataType(typeof(RepararBicicletaMetaData))]
    public class RepararBicicleta
    {
    }

    public class RepararBicicletaMetaData
    {
        [Required]
        [Display(ResourceType = typeof(Messages), Name = "form_RepararBicicleta_idRepararBicicleta")]
        public global::System.Int32 idRepararBicicleta;
        
        [Display(ResourceType = typeof(Messages), Name = "form_RepararBicicleta_idEmpleado")]
        public Nullable<global::System.Int32> idEmpleado;

        [Display(ResourceType = typeof(Messages), Name = "form_RepararBicicleta_idBicicleta")]
        public Nullable<global::System.Int32> idBicicleta;

        [Required]
        [Display(ResourceType = typeof(Messages), Name = "form_RepararBicicleta_descripcionRepararBicicleta")]
        [StringLength(50)]
        public global::System.String descripcionRepararBicicleta;

        [Required]
        [Display(ResourceType = typeof(Messages), Name = "form_RepararBicicleta_fechaRepararBicicleta")]
        public global::System.String fechaRepararBicicleta;


    }

}