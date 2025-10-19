using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LostManagementApp.Models
{
    [Table("users")]
    public class Users
    {
        /// <summary>
        /// ユーザーID
        /// </summary>
        [Key]
        [Column("user_id")]
        public int UserId { get; set; }
        /// <summary>
        /// ユーザー名
        /// </summary>
        [Column("user_name")]
        public required string UserName { get; set; }
        /// <summary>
        /// メールアドレス
        /// </summary>
        [Column("email")]
        public required string Email { get; set; }
        /// <summary>
        /// パスワード
        /// </summary>
        [Column("login_password")]
        public required string Password { get; set; }
        /// <summary>
        /// 登録日
        /// </summary>
        [Column("registrate_date")]
        public DateTime RegistrateDate { get; set; }
        /// <summary>
        /// 更新日
        /// </summary>
        [Column("update_date")]
        public DateTime? UpdateDate { get; set; }

        /// <summary>
        /// ナビゲーションプロパティ: ユーザー情報
        /// </summary>
        public ICollection<Lost> Losts { get; set; } = new List<Lost>();

        /// <summary>
        /// ナビゲーションプロパティ: ログイン情報
        /// </summary>
        public ICollection<Login> Logins { get; set; } = new List<Login>();
    }
}
