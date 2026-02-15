using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LostManagementApp.ViewModels
{
    public class LoginViewModel
    {
        /// <summary>
        /// ユーザーID
        /// </summary>
        public required string UserId { get; set; }
        /// <summary>
        /// パスワード
        /// </summary>
        public required string Password { get; set; }
        /// <summary>
        /// メッセージ
        /// </summary>
        public string? Message { get; set; }
    }
}
