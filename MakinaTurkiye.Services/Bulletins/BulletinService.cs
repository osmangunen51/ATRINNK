using MakinaTurkiye.Core.Data;
using MakinaTurkiye.Entities.Tables.Bullettins;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MakinaTurkiye.Services.Bulletins
{
    public class BulletinService:IBulletinService
    {
        IRepository<BulletinMember> _bulletinMembeRepository;
        IRepository<BulletinMemberCategory> _bulletinMemberCategoryRepository;


        public BulletinService(IRepository<BulletinMember> bulletinMemberRepository, 
            IRepository<BulletinMemberCategory> bulletinMemberCategoryRepository)
        {
            this._bulletinMemberCategoryRepository = bulletinMemberCategoryRepository;
            this._bulletinMembeRepository = bulletinMemberRepository;
        }

   

        public BulletinMember GetBulletinMemberByBulletinMemberId(int bulletinMemberId)
        {
            if (bulletinMemberId == 0)
                throw new ArgumentNullException();
            var query = _bulletinMembeRepository.Table;
     
            return query.FirstOrDefault(x => x.BulletinMemberId == bulletinMemberId);
        }

        public List<BulletinMember> GetBulletinMembers()
        {
            var query = _bulletinMembeRepository.Table;
            return query.ToList();
        }

        public List<BulletinMember> GetBulletinMembersByCategoryId(int categoryId)
        {
            if (categoryId == 0)
                throw new ArgumentNullException();
            var bulletinMembercategories = _bulletinMemberCategoryRepository.Table;
            var bulletinMembers = _bulletinMembeRepository.Table;
            var query = from b in bulletinMembers join c in bulletinMembercategories on b.BulletinMemberId equals c.BulletinMemberId
                        where c.CategoryId == categoryId select b;
            return query.ToList();
        }

        public void InsertBulletinMember(BulletinMember bulletinMember)
        {
            if (bulletinMember == null)
                throw new ArgumentNullException();

            _bulletinMembeRepository.Insert(bulletinMember);
        }
        public void DeleteBulletinMember(BulletinMember bulletinMember)
        {
            if (bulletinMember == null)
                throw new ArgumentNullException();

            _bulletinMembeRepository.Delete(bulletinMember);
        }
        public void InsertBulletinMemberCategory(BulletinMemberCategory bulletinMemberCategory)
        {
            if (bulletinMemberCategory == null)
                throw new ArgumentNullException();
        
                _bulletinMemberCategoryRepository.Insert(bulletinMemberCategory);
            
        }

        public void UpdateBulletinMember(BulletinMember bulletinMember)
        {
            if (bulletinMember == null)
                throw new ArgumentNullException();

            _bulletinMembeRepository.Update(bulletinMember);
        }
    }
}
