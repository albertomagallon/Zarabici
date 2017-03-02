using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Zarabizi.Resources;
using Zarabizi.Models.Validation;

namespace Zarabizi.Models.Metadata
{
    [MetadataType(typeof(SocioMetaData))]
    public class Socio
    {
    }

    public class SocioMetaData
    {
        [Required]
        [Display(ResourceType = typeof(Messages), Name = "form_Socio_idSocio")]
        public global::System.Int32 idSocio;

        [Required]
        [Display(ResourceType = typeof(Messages), Name = "form_Socio_nombreSocio")]
        [StringLength(15)]
        public global::System.String nombreSocio;

        [Required]
        [Display(ResourceType = typeof(Messages), Name = "form_Socio_apellidosSocio")]
        [StringLength(30)]
        public global::System.String apellidosSocio;

        [Required]
        [Display(ResourceType = typeof(Messages), Name = "form_Socio_direccionSocio")]
        [StringLength(30)]
        public global::System.String direccionSocio;

        [Required]
        [Display(ResourceType = typeof(Messages), Name = "form_Socio_poblacionSocio")]
        [StringLength(20)]
        public global::System.String poblacionSocio;

        [Required]
        [Display(ResourceType = typeof(Messages), Name = "form_Socio_codigopostalSocio")]
        [PostalCodeValidation(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "err_CodigoPostal")]
        [StringLength(5)]
        public global::System.String codigopostalSocio;

        [Required]
        [Display(ResourceType = typeof(Messages), Name = "form_Socio_tlfFijoSocio")]
        [PhoneValidation(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "err_Teléfono")]
        [StringLength(9)]
        public global::System.String tlfFijoSocio;

        [Display(ResourceType = typeof(Messages), Name = "form_Socio_tlfMovilSocio")]
        [PhoneValidation(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "err_Teléfono")]
        [StringLength(9)]
        public global::System.String tlfMovilSocio;

        [Required]
        [Display(ResourceType = typeof(Messages), Name = "form_Socio_fechaAltaSocio")]
        public Nullable<global::System.DateTime> fechaAltaSocio;

        [Required]
        [Display(ResourceType = typeof(Messages), Name = "form_Socio_fechaNacimientoSocio")]
        public Nullable<global::System.DateTime> fechaNacimientoSocio;

        [Required]
        [Display(ResourceType = typeof(Messages), Name = "form_Socio_CuentaBancariaSocio")]
        [AccountValidation(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "err_CodigoBanco")]
        [StringLength(16)]
        public global::System.String CuentaBancariaSocio;

        [Display(ResourceType = typeof(Messages), Name = "form_Socio_idOficina")]
        public Nullable<global::System.Int32> idOficina;

        [Required]
        [Display(ResourceType = typeof(Messages), Name = "form_Socio_idUsuario")]
        public Nullable<global::System.Guid> idUsuario;

    }

}