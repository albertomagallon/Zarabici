using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Zarabizi.Resources;

namespace Zarabizi.Models.Metadata
{
    [MetadataType(typeof(BicicletaMetaData))]
    public class Bicicleta
    {
    }

    public class BicicletaMetaData
    {
        [Required]
        [Display(ResourceType = typeof(Messages), Name = "form_Bicicleta_idBicicleta")]
        public global::System.Int32 idBicicleta;

        [Required]
        [Display(ResourceType = typeof(Messages), Name = "form_Bicicleta_distanciaTotal")]
        public global::System.Decimal distanciaTotal;

        [Required]
        [Display(ResourceType = typeof(Messages), Name = "form_Bicicleta_distanciaMantenimiento")]
        public global::System.Decimal distanciaMantenimiento;        

    }
}