namespace NetSheep.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Blind")]
    public partial class Blind
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Blind_Game { get; set; }

        [Key]
        [Column(Order = 1)]
        public byte Blind_Card { get; set; }
    }
}
