namespace NetSheep.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Game")]
    public partial class Game
    {
        [Key]
        public int Game_ID { get; set; }

        public int Table_ID { get; set; }

        public int Picker { get; set; }

        public int Partner { get; set; }
    }
}
