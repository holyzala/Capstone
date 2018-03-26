namespace ConsoleApp1
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Bot")]
    public partial class Bot
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Player_ID { get; set; }

        [Required]
        [StringLength(15)]
        public string Name { get; set; }

        public virtual Player Player { get; set; }
    }
}
