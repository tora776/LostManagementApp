using LostManagementApp.DatabaseContext;
using LostManagementApp.Models;
using Microsoft.EntityFrameworkCore;

namespace LostManagementApp.Dao
{
    public class LostDao : ILostDao
    {
        private readonly LostContext _context;
        public LostDao(LostContext context) 
        {
            _context = context;
        }

        /// <summary>
        /// 詳細画面遷移時、紛失物を取得する
        /// </summary>
        /// <param name="lost">検索条件</param>
        /// TODO:ユーザーID,紛失物,紛失場所,紛失した詳細な場所が指定されている場合は、該当する紛失物を取得する
        public Lost GetLost(int LostId)
        {
            var LostData = _context.Lost
                .Where(x => x.LostId == LostId)
                .FirstOrDefault()
                ?? new Lost { LostId = -1};

            return LostData;
        }

        /// <summary>
        /// アプリ起動時・検索処理時に紛失物を取得する
        /// </summary>
        /// <param name="lost">検索条件</param>
        /// TODO:ユーザーID,紛失物,紛失場所,紛失した詳細な場所が指定されている場合は、該当する紛失物を取得する
        public List<Lost> GetLostList(Lost lost)
        {
            var query = _context.Lost.AsQueryable();
            // UserIdは必須
            query = query.Where(x => x.UserId == lost.UserId);

            // nullまたは空でなければ条件を追加
            if (!string.IsNullOrEmpty(lost.LostDate?.ToString("yyyy/MM/dd")))
            {
                query = query.Where(x => x.LostDate == lost.LostDate);
            }

            if (!string.IsNullOrEmpty(lost.FoundDate?.ToString("yyyy/MM/dd")))
            {
                query = query.Where(x => x.FoundDate == lost.FoundDate);
            }

            if (!string.IsNullOrEmpty(lost.LostItem))
            {
                query = query.Where(x => x.LostItem == lost.LostItem);
            }

            if (!string.IsNullOrEmpty(lost.LostPlace))
            {
                query = query.Where(x => x.LostPlace == lost.LostPlace);
            }

            if (!string.IsNullOrEmpty(lost.LostDetailedPlace))
            {
                query = query.Where(x => x.LostDetailedPlace == lost.LostDetailedPlace);
            }
            
            return query.ToList();
        }

        /// <summary>
        /// 紛失物を登録する
        /// </summary>
        /// <param name="lost">紛失物情報</param>
        public void InsertLost(Lost lost)
        {
            lost.RegistrateDate = DateTime.UtcNow;
            lost.UpdateDate = DateTime.UtcNow;
            // LostIdの最大値 + 1を取得
            lost.LostId = GetMaxLostId();
            _context.Lost.Add(lost);
            _context.SaveChanges();
        }

        /// <summary>
        /// 紛失物を更新する
        /// </summary>
        /// <param name="lost">紛失物情報</param>
        public void UpdateLost(Lost lost)
        {
            lost.UpdateDate = DateTime.UtcNow;
            _context.Lost.Update(lost);
            _context.SaveChanges();
        }

        /// <summary>
        /// 紛失物を削除する
        /// </summary>
        /// <param name="lost">紛失物情報</param>
        public void DeleteLost(Lost lost)
        {
            lost.UpdateDate = DateTime.UtcNow;
            _context.Lost.Where(x => x.LostId == lost.LostId).ExecuteDelete();
            _context.SaveChanges();
        }

        /// <summary>
        /// 紛失IDの最大値を取得する
        /// </summary>
        public int GetMaxLostId()
        {
            int maxLostId = _context.Lost
                .Select(x => x.LostId).Max();
            return maxLostId + 1;
        }
    }
}
