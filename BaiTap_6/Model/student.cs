namespace BaiTap_6.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("student")]
    public partial class student
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int studentID { get; set; }

        [Required]
        [StringLength(50)]
        public string FullName { get; set; }

        [Required]
        [StringLength(50)]
        public string AverageScore { get; set; }

        [Required]
        [StringLength(50)]
        public string FacultyID { get; set; }
    }
}
