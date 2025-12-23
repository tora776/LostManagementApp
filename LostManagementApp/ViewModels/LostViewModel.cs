using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LostManagementApp.ViewModels
{
    public class LostViewModel
    {
        /// <summary>
        /// 紛失ID
        /// </summary>
        public int LostId { get; set; }
        /// <summary>
        /// ユーザーID
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 紛失ステータス
        /// </summary>
        public bool IsFound { get; set; }
        /// <summary>
        /// 紛失日
        /// </summary>
        public DateTime? LostDate { get; set; }
        /// <summary>
        /// 発見日
        /// </summary>
        public DateTime? FoundDate { get; set; }
        /// <summary>
        /// 紛失物
        /// </summary>
        public string? LostItem { get; set; }
        /// <summary>
        /// 紛失場所
        /// </summary>
        public string? LostPlace { get; set; }
        /// <summary>
        /// 紛失した詳細な場所
        /// </summary>
        public string? LostDetailedPlace { get; set; }
        /// <summary>
        /// 登録日
        /// </summary>
        public DateTime? RegistrateDate { get; set; }
        /// <summary>
        /// 更新日
        /// </summary>
        public DateTime? UpdateDate { get; set; }
        /// <summary>
        /// メッセージ
        /// </summary>
        public string? Message { get; set; }
    }
}
