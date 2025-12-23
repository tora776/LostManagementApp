using LostManagementApp.DatabaseContext;
using LostManagementApp.Models;
using LostManagementApp.ViewModels;
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
        /// <param name="LostId">紛失ID</param>
        /// TODO:ユーザーID,紛失物,紛失場所,紛失した詳細な場所が指定されている場合は、該当する紛失物を取得する
        public Lost GetLost(int LostId)
        {
            var LostData = _context.Lost
                .Where(x => x.LostId == LostId)
                .FirstOrDefault()
                ?? new Lost
                {
                    LostId = -1,
                    User = new Users
                    {
                        UserName = string.Empty,
                        Email = string.Empty,
                        Password = string.Empty,
                        RegistrateDate = DateTime.MinValue,
                        Losts = new List<Lost>(),
                        Logins = new List<Login>()
                    }
                };

            return LostData;
        }

        /// <summary>
        /// アプリ起動時・検索処理時に紛失物を取得する
        /// </summary>
        /// <param name="lost">紛失物情報</param>
        /// TODO:ユーザーID,紛失物,紛失場所,紛失した詳細な場所が指定されている場合は、該当する紛失物を取得する
        public List<Lost> GetLostList(LostViewModel model)
        {
            var query = _context.Lost.AsQueryable();

            // UserId が 0 の場合は全ユーザー（必要なら必須に変更）
            if (model.UserId != 0)
                query = query.Where(x => x.UserId == model.UserId);

            if (model.LostDate.HasValue)
            {
                var d = model.LostDate.Value.Date;
                query = query.Where(x => x.LostDate.HasValue && x.LostDate.Value.Date == d);
            }

            if (model.FoundDate.HasValue)
            {
                var d = model.FoundDate.Value.Date;
                query = query.Where(x => x.FoundDate.HasValue && x.FoundDate.Value.Date == d);
            }

            if (!string.IsNullOrWhiteSpace(model.LostItem))
                query = query.Where(x => x.LostItem != null && x.LostItem.Contains(model.LostItem));

            if (!string.IsNullOrWhiteSpace(model.LostPlace))
                query = query.Where(x => x.LostPlace != null && x.LostPlace.Contains(model.LostPlace));

            if (!string.IsNullOrWhiteSpace(model.LostDetailedPlace))
                query = query.Where(x => x.LostDetailedPlace != null && x.LostDetailedPlace.Contains(model.LostDetailedPlace));

            return query.OrderBy(x => x.LostId).ToListAsync().Result;
        }

        /// <summary>
        /// 紛失物を登録する
        /// </summary>
        /// <param name="lost">紛失物情報</param>
        public void InsertLost(LostViewModel model)
        {
            //lost.RegistrateDate = DateTime.UtcNow;
            //lost.UpdateDate = DateTime.UtcNow;
            //// LostIdの最大値 + 1を取得
            //lost.LostId = GetMaxLostId();
            DateTime lostDateValue = model.LostDate ?? DateTime.MinValue;
            DateTime foundDateValue = model.FoundDate ?? DateTime.MinValue;

            var lost = new Lost
            {
                LostId = GetMaxLostId(),
                UserId = model.UserId,
                IsFound = model.FoundDate.HasValue,
                LostDate = DateTime.SpecifyKind(lostDateValue, DateTimeKind.Utc),
                FoundDate = DateTime.SpecifyKind(foundDateValue, DateTimeKind.Utc),
                LostItem = model.LostItem,
                LostPlace = model.LostPlace,
                LostDetailedPlace = model.LostDetailedPlace,
                RegistrateDate = DateTime.UtcNow,
                UpdateDate = DateTime.UtcNow
            };
            _context.Lost.Add(lost);
            _context.SaveChanges();
        }

        /// <summary>
        /// 紛失物を更新する
        /// </summary>
        /// <param name="lost">紛失物情報</param>
        public void UpdateLost(Lost model)
        {
            DateTime lostDateValue = model.LostDate ?? DateTime.MinValue;
            DateTime foundDateValue = model.FoundDate ?? DateTime.MinValue;
            var updateLost = GetLost(model.LostId);
            DateTime registrateDateValue = updateLost.RegistrateDate ?? DateTime.MinValue;
            updateLost.IsFound = model.IsFound;
            updateLost.LostDate = DateTime.SpecifyKind(lostDateValue, DateTimeKind.Utc);
            updateLost.FoundDate = DateTime.SpecifyKind(foundDateValue, DateTimeKind.Utc);
            updateLost.LostItem = model.LostItem;
            updateLost.LostPlace = model.LostPlace;
            updateLost.LostDetailedPlace = model.LostDetailedPlace;
            updateLost.RegistrateDate = DateTime.SpecifyKind(registrateDateValue, DateTimeKind.Utc);
            updateLost.UpdateDate = DateTime.UtcNow;
            _context.Lost.Update(updateLost);
            _context.SaveChanges();
        }

        /// <summary>
        /// 紛失物を削除する
        /// </summary>
        /// <param name="lostIds">紛失IDのリスト</param>
        public void DeleteLostIds(List<int> lostIds)
        {
            _context.Lost.Where(x => lostIds.Contains(x.LostId)).ExecuteDelete();
            _context.SaveChanges();
        }

        /// <summary>
        /// 紛失物を削除する
        /// </summary>
        /// <param name="lostId">紛失ID</param>
        public void DeleteLost(int lostId)
        {
            _context.Lost.Where(x => x.LostId == lostId).ExecuteDelete();
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
