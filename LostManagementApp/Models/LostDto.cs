using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LostManagementApp.Models
{
    public class LostDto
    {
        /// <summary>
        /// ユーザーID
        /// </summary>
        [Display(Name = "ユーザーID")]
        [Required(ErrorMessage = "{0}は必須です。")]
        [Column("user_id")]
        public int UserId { get; set; }
        /// <summary>
        /// 紛失日
        /// </summary>
        [Column("lost_date")]
        public DateTime? LostDate { get; set; }
        /// <summary>
        /// 発見日
        /// </summary>
        [Column("found_date")]
        public DateTime? FoundDate { get; set; }
        /// <summary>
        /// 紛失物
        /// </summary>
        [Display(Name = "紛失物")]
        [StringLength(100)]
        [Column("lost_item")]
        public string? LostItem { get; set; }
        /// <summary>
        /// 紛失場所
        /// </summary>
        [Display(Name = "紛失した場所")]
        [StringLength(100)]
        [Column("lost_place")]
        public string? LostPlace { get; set; }
        /// <summary>
        /// 紛失した詳細な場所
        /// </summary>
        [Display(Name = "紛失した詳細な場所")]
        [StringLength(100)]
        [Column("lost_detailed_place")]
        public string? LostDetailedPlace { get; set; }
    }
}
