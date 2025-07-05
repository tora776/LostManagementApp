using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LostManagementApp.Models
{
    [Table("losts")]
    public class Lost
    {
        /// <summary>
        /// 紛失ID
        /// </summary>
        [Key]
        [Column("lost_id")]
        public int LostId { get; set; }
        /// <summary>
        /// ユーザーID
        /// </summary>
        [Column("user_id")]
        public int UserId { get; set; }
        /// <summary>
        /// 紛失ステータス
        /// </summary>
        [Column("is_found")]
        public bool IsFound { get; set; }
        /// <summary>
        /// 紛失日
        /// </summary>
        [Column("lost_date")]
        public DateTime LostDate { get; set; }
        /// <summary>
        /// 発見日
        /// </summary>
        [Column("found_date")]
        public DateTime FoundDate { get; set; }
        /// <summary>
        /// 紛失物
        /// </summary>
        [Column("lost_item")]
        public required string LostItem { get; set; }
        /// <summary>
        /// 紛失場所
        /// </summary>
        [Column("lost_place")]
        public required string LostPlace { get; set; }
        /// <summary>
        /// 紛失した詳細な場所
        /// </summary>
        [Column("lost_detailed_place")]
        public required string LostDetailedPlace { get; set; }
        /// <summary>
        /// 登録日
        /// </summary>
        [Column("registrate_date")]
        public DateTime RegistrateDate { get; set; }
        /// <summary>
        /// 更新日
        /// </summary>
        [Column("update_date")]
        public DateTime UpdateDate { get; set; }
    }
}
