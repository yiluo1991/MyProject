namespace MyProject.Core
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("mine.users")]
    public partial class User
    {
        public int Id { get; set; }

        [Required]
        [StringLength(64)]
        public string LoginName { get; set; }

        [Required]
        [StringLength(70)]
        public string Password { get; set; }

        [StringLength(128)]
        public string Role { get; set; }
    }
}
