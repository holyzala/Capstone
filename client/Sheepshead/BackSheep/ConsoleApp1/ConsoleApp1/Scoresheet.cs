namespace ConsoleApp1
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

        public virtual Player Player { get; set; }

        public virtual Table_Match Table_Match { get; set; }
    }
}
