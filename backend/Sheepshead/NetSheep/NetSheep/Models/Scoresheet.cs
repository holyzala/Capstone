namespace NetSheep.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Scoresheet")]
    public partial class Scoresheet
    {
        [Key]
        public int Score_ID { get; set; }

        public int Table_ID { get; set; }

        public int Player_ID { get; set; }

        public int? Score { get; set; }
    }
}
