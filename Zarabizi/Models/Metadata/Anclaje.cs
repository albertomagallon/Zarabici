using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Zarabizi.Resources;

namespace Zarabizi.Models.Metadata
{
    [MetadataType(typeof(AnclajeMetaData))]
    public partial class Anclaje
    {
    }

    public class AnclajeMetaData
    {
        [Required]
        [Display(ResourceType = typeof(Messages), Name = "form_Anclaje_estadoAnclaje")]    
        public global::System.Boolean estadoAnclaje;

        [Required]
        [Display(ResourceType = typeof(Messages), Name = "form_Anclaje_ocupadoAnclaje")]
        public global::System.Boolean ocupadoAnclaje;

        [Required]
        [Display(ResourceType = typeof(Messages), Name = "form_Anclaje_idEstacion")]
        public global::System.Int32 idEstacion;
  
        [Display(ResourceType = typeof(Messages), Name = "form_Anclaje_idBicicleta")]
        public global::System.Int32 idBicicleta;
        

    }
}