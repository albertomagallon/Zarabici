using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Zarabizi.Resources;

namespace Zarabizi.Models.Metadata
{
    [MetadataType(typeof(OficinaMetaData))]
    public class Oficina
    {
    }

    public class OficinaMetaData
    {
        [Required]
        [Display(ResourceType = typeof(Messages), Name = "form_Oficina_idOficina")]
        public global::System.Int32 idOficina;

        [Required]
        [Display(ResourceType = typeof(Messages), Name = "form_Oficina_ubicacionOficina")]
        [StringLength(20)]
        public global::System.String ubicacionOficina;

        [Required]
        [Display(ResourceType = typeof(Messages), Name = "form_Oficina_ciudadOficina")]
        [StringLength(50)]
        public global::System.String ciudadOficina;

        [Display(ResourceType = typeof(Messages), Name = "form_Oficina_provinciaOficina")]
        [StringLength(20)]
        public global::System.String provinciaOficina;


    }
}