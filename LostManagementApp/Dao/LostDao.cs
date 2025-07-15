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
        /// 紛失物を取得する
        /// </summary>
        /// <param name="lost">
        /// TODO:ユーザーID,紛失物,紛失場所,紛失した詳細な場所が指定されている場合は、該当する紛失物を取得する
        public List<Lost> GetLost(Lost lost)
        {
            List<Lost> losts = _context.Lost
                // .Where(x => x.UserId == lost.UserId && x.LostItem == lost.LostItem && x.LostPlace == lost.LostPlace && x.LostDetailedPlace == lost.LostDetailedPlace)
                .ToList();
            return losts;
        }

        /// <summary>
        /// 紛失物を登録する
        /// </summary>
        /// <param name="lost">紛失物情報</param>
        public void InsertLost(Lost lost)
        {
            lost.RegistrateDate = DateTime.UtcNow;
            lost.UpdateDate = DateTime.UtcNow;
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
    }
}
