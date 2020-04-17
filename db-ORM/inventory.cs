namespace db_ORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("public.inventory")]
    public partial class inventory
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public inventory()
        {
            number_of_inventory_in_branch = new HashSet<number_of_inventory_in_branch>();
        }

        [Key]
        public int inventory_id { get; set; }

        [Required]
        public string inventory_name { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<number_of_inventory_in_branch> number_of_inventory_in_branch { get; set; }
    }
}
