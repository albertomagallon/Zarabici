using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Zarabizi.Resources;
using Zarabizi.Models.Validation;

namespace Zarabizi.Models.Metadata
{
    [MetadataType(typeof(Empleado))]
    public class Empleado
    {
    }

    public class EmpleadoMetaData
    {
        [Required]
        [Display(ResourceType = typeof(Messages), Name = "form_Empleado_idEmpleado")]
        public global::System.Int32 idEmpleado;

        [Required]
        [Display(ResourceType = typeof(Messages), Name = "form_Empleado_DNIEmpleado")]
        [DNIValidation(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "err_DNI")]
        [StringLength(10)]
        public global::System.String DNIEmpleado;

        [Required]
        [Display(ResourceType = typeof(Messages), Name = "form_Empleado_nombreEmpleado")]
        [StringLength(15)]
        public global::System.String nombreEmpleado;

        [Required]
        [Display(ResourceType = typeof(Messages), Name = "form_Empleado_apellidosEmpleado")]
        [StringLength(30)]
        public global::System.String apellidosEmpleado;

        [Required]
        [Display(ResourceType = typeof(Messages), Name = "form_Empleado_direccionEmpleado")]
        [StringLength(30)]
        public global::System.String direccionEmpleado;

        [Required]
        [Display(ResourceType = typeof(Messages), Name = "form_Empleado_poblacionEmpleado")]
        [StringLength(20)]
        public global::System.String poblacionEmpleado;

        [Required]
        [Display(ResourceType = typeof(Messages), Name = "form_Empleado_codigopostalEmpleado")]
        [PostalCodeValidation(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "err_CodigoPostal")]
        [StringLength(5)]
        public global::System.String codigopostalEmpleado;

        [Required]
        [Display(ResourceType = typeof(Messages), Name = "form_Empleado_tlfFijoEmpleado")]
        [PhoneValidation(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "err_Teléfono")]
        [StringLength(9)]
        public global::System.String tlfFijoEmpleado;

        [Display(ResourceType = typeof(Messages), Name = "form_Empleado_tlfMovilEmpleado")]
        [PhoneValidation(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "err_Teléfono")]
        [StringLength(9)]
        public global::System.String tlfMovilEmpleado;

        [Display(ResourceType = typeof(Messages), Name = "form_Empleado_emailEmpleado")]
        [StringLength(50)]
        public global::System.String emailEmpleado;

        [Display(ResourceType = typeof(Messages), Name = "form_Empleado_fechaDeAltaEmpleado")]
        public Nullable<global::System.DateTime> fechaDeAltaEmpleado;
        
        [Display(ResourceType = typeof(Messages), Name = "form_Empleado_fechaNacimientoEmpleado")]
        public Nullable<global::System.DateTime> fechaNacimientoEmpleado;

        [Required]
        [Display(ResourceType = typeof(Messages), Name = "form_Empleado_cuentabancariaEmpleado")]
        [AccountValidation(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "err_CodigoBanco")]
        [StringLength(16)]
        public global::System.String cuentabancariaEmpleado;

    }
}