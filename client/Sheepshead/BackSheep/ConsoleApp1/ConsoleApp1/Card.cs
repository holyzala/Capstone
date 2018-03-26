namespace ConsoleApp1
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Card")]
    public partial class Card
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Card()
        {
            Game_Round = new HashSet<Game_Round>();
            Hands = new HashSet<Hand>();
            Games = new HashSet<Game>();
            Game_Round1 = new HashSet<Game_Round>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte Card_ID { get; set; }

        [Required]
        [StringLength(2)]
        public string Face { get; set; }

        [Required]
        [StringLength(10)]
        public string Suit { get; set; }

        public bool is_Trump { get; set; }

        public int Trump_Power { get; set; }

        public int Card_Value { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Game_Round> Game_Round { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Hand> Hands { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Game> Games { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Game_Round> Game_Round1 { get; set; }
    }
}
