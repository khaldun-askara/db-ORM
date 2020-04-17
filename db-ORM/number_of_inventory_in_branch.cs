namespace db_ORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("public.number_of_inventory_in_branch")]
    public partial class number_of_inventory_in_branch
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int branch_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int inventory_id { get; set; }

        public int number { get; set; }

        public virtual branch branch { get; set; }

        public virtual inventory inventory { get; set; }
    }
}
