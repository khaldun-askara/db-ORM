namespace db_ORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("public.branch")]
    public partial class branch
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public branch()
        {
            number_of_inventory_in_branch = new HashSet<number_of_inventory_in_branch>();
        }

        [Key]
        public int branch_id { get; set; }

        [Required]
        public string branch_address { get; set; }

        [Required]
        public string branch_phone { get; set; }

        public int branch_area { get; set; }

        [Required]
        public string branch_working_hours { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<number_of_inventory_in_branch> number_of_inventory_in_branch { get; set; }
    }
}
