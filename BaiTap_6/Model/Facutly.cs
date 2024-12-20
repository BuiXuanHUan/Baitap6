namespace BaiTap_6.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Facutly")]
    public partial class Facutly
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FacutlyID { get; set; }

        [Required]
        [StringLength(50)]
        public string FacultyName { get; set; }
    }
}
