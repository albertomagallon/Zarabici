using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Zarabizi.Resources;

namespace Zarabizi.Models.Metadata
{
    [MetadataType(typeof(BonoMetaData))]
    public class Bono
    {
    }

    public class BonoMetaData
    {
        [Required]
        [Display(ResourceType = typeof(Messages), Name = "form_Bono_idBono")]
        public global::System.Int32 idBono;
        
        [Display(ResourceType = typeof(Messages), Name = "form_Bono_fechaCompraBono")]
        public Nullable<global::System.DateTime> fechaCompraBono;

        [Required]
        [Display(ResourceType = typeof(Messages), Name = "form_Bono_fechaInicioBono")]
        public global::System.DateTime fechaInicioBono;

        [Required]
        [Display(ResourceType = typeof(Messages), Name = "form_Bono_fechaFinalBono")]
        public global::System.DateTime fechaFinalBono;

        [Required]
        [Display(ResourceType = typeof(Messages), Name = "form_Bono_idSocio")]
        public global::System.Int32 idSocio;

        [Required]
        [Display(ResourceType = typeof(Messages), Name = "form_Bono_idTarifa")]
        public global::System.Int32 idTarifa;      

    }
}