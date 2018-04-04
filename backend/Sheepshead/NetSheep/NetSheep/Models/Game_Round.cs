namespace NetSheep.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Game_Round
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Round_ID { get; set; }

        public int Game_ID { get; set; }

        public int? Round_Num { get; set; }

        public int Player_Turn { get; set; }

        public byte? Leading_Card { get; set; }
    }
}
