namespace NetSheep.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Card")]
    public partial class Card
    {
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
    }
}
