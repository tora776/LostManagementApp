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
        public List<Lost> GetLostList(LostDto lostDto)
        {
            var query = _context.Lost.AsQueryable();

            // UserId が 0 の場合は全ユーザー（必要なら必須に変更）
            if (lostDto.UserId != 0)
                query = query.Where(x => x.UserId == lostDto.UserId);

            if (lostDto.LostDate.HasValue)
            {
                var d = lostDto.LostDate.Value.Date;
                query = query.Where(x => x.LostDate.HasValue && x.LostDate.Value.Date == d);
            }

            if (lostDto.FoundDate.HasValue)
            {
                var d = lostDto.FoundDate.Value.Date;
                query = query.Where(x => x.FoundDate.HasValue && x.FoundDate.Value.Date == d);
            }

            if (!string.IsNullOrWhiteSpace(lostDto.LostItem))
                query = query.Where(x => x.LostItem != null && x.LostItem.Contains(lostDto.LostItem));

            if (!string.IsNullOrWhiteSpace(lostDto.LostPlace))
                query = query.Where(x => x.LostPlace != null && x.LostPlace.Contains(lostDto.LostPlace));

            if (!string.IsNullOrWhiteSpace(lostDto.LostDetailedPlace))
                query = query.Where(x => x.LostDetailedPlace != null && x.LostDetailedPlace.Contains(lostDto.LostDetailedPlace));

            return query.OrderBy(x => x.LostId).ToListAsync().Result;
        }

        /// <summary>
        /// 紛失物を登録する
        /// </summary>
        /// <param name="lost">紛失物情報</param>
        public void InsertLost(LostDto lostDto)
        {
            //lost.RegistrateDate = DateTime.UtcNow;
            //lost.UpdateDate = DateTime.UtcNow;
            //// LostIdの最大値 + 1を取得
            //lost.LostId = GetMaxLostId();
            var lost = new Lost
            {
                LostId = GetMaxLostId(),
                UserId = lostDto.UserId,
                IsFound =  lostDto.FoundDate.HasValue,
                LostDate = lostDto.LostDate,
                FoundDate = lostDto.FoundDate,
                LostItem = lostDto.LostItem,
                LostPlace = lostDto.LostPlace,
                LostDetailedPlace = lostDto.LostDetailedPlace,
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
        public void UpdateLost(Lost lost)
        {
            lost.UpdateDate = DateTime.UtcNow;
            _context.Lost.Update(lost);
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
