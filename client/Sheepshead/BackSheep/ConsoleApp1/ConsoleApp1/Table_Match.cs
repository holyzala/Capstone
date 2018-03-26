namespace ConsoleApp1
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Table_Match
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Table_Match()
        {
            Scoresheets = new HashSet<Scoresheet>();
            Players = new HashSet<Player>();
        }

        [Key]
        public int Table_ID { get; set; }

        public int Dealer { get; set; }

        public int Host_Player { get; set; }

        public int? Game_Number { get; set; }

        public virtual Player Player { get; set; }

        public virtual Player Player1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Scoresheet> Scoresheets { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Player> Players { get; set; }
    }
}
