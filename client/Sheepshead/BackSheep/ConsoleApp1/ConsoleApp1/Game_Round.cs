namespace ConsoleApp1
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Game_Round
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Game_Round()
        {
            Cards = new HashSet<Card>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Round_ID { get; set; }

        public int Game_ID { get; set; }

        public int? Round_Num { get; set; }

        public int Player_Turn { get; set; }

        public byte? Leading_Card { get; set; }

        public virtual Card Card { get; set; }

        public virtual Game Game { get; set; }

        public virtual Player Player { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Card> Cards { get; set; }
    }
}
