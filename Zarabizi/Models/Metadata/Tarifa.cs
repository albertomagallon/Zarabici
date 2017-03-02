using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Zarabizi.Resources;

namespace Zarabizi.Models.Metadata
{
    [MetadataType(typeof(TarifaMetaData))]
    public class Tarifa
    {
    }

    public class TarifaMetaData
    {
        [Required]
        [Display(ResourceType = typeof(Messages), Name = "form_Tarifa_idTarifa")]
        public global::System.Int32 idTarifa;

        [Required]
        [Display(ResourceType = typeof(Messages), Name = "form_Tarifa_diasTarifa")]
        public global::System.Int32 diasTarifa;

        [Required]
        [Display(ResourceType = typeof(Messages), Name = "form_Tarifa_nombreTarifa")]
        [StringLength(50)]
        public global::System.String nombreTarifa;

        [Required]
        [Display(ResourceType = typeof(Messages), Name = "form_Tarifa_fechaInicioTarifa")]
        public global::System.DateTime fechaInicioTarifa;

        [Required]
        [Display(ResourceType = typeof(Messages), Name = "form_Tarifa_fechaFinalTarifa")]
        public global::System.DateTime fechaFinalTarifa;

        [Required]
        [Display(ResourceType = typeof(Messages), Name = "form_Tarifa_precioTarifa")]
        public global::System.Decimal precioTarifa;

        [Required]
        [Display(ResourceType = typeof(Messages), Name = "form_Tarifa_idOficina")]
        public global::System.Int32 idOficina;

    }

}