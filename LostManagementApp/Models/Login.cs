using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LostManagementApp.Models
{
    [Table("login_check")]
    public class Login
    {
        /// <summary>
        /// ログインID
        /// </summary>
        [Key]
        [Column("login_id")]
        public int LoginId { get; set; }
        /// <summary>
        /// ユーザーID
        /// </summary>
        [Column("user_id")]
        public int UserId { get; set; }
        /// <summary>
        /// トークン
        /// </summary>
        [Column("token")]
        public required string Token { get; set; }
        /// <summary>
        /// 登録日
        /// </summary>
        [Column("login_date")]
        public DateTime LoginDate { get; set; }
        /// <summary>
        /// 更新日
        /// </summary>
        [Column("expire_date")]
        public DateTime ExpireDate { get; set; }
    }
}


