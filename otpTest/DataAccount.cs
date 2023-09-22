namespace otpTest
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DataAccount")]
    public partial class DataAccount
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(10)]
        public string UID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string Email { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(20)]
        public string MatKhau { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(50)]
        public string TenNguoiDung { get; set; }
    }
}
