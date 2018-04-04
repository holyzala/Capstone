namespace NetSheep.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Table_Match
    {
        [Key]
        public int Table_ID { get; set; }

        public int Dealer { get; set; }

        public int Host_Player { get; set; }

        public int? Game_Number { get; set; }
    }
}
