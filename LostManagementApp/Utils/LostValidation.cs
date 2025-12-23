namespace LostManagementApp.Utils
{
    public class LostValidation
    {
        /// <summary>
        /// 発見日が紛失日以降であることを確認する
        /// </summary>
        /// <param name="lostDate">紛失日</param>
        /// <param name="foundDate">発見日</param>
        public static bool FoundDateValidation(DateTime? lostDate, DateTime? foundDate)
        {
            // TODO:存在チェックはメソッドの外で行うべきか検討
            if(lostDate.HasValue && foundDate.HasValue)
            {
                return foundDate >= lostDate;
            }
            return false;
        }
    }
}
