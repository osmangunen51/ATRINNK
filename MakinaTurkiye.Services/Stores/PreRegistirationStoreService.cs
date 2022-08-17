using AutoMapper;
using MakinaTurkiye.Core;
using MakinaTurkiye.Core.Data;
using MakinaTurkiye.Entities.Tables.Common;
using MakinaTurkiye.Entities.Tables.Members;
using MakinaTurkiye.Entities.Tables.Stores;
using MakinaTurkiye.Entities.Tables.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MakinaTurkiye.Services.Stores
{
    public class PreRegistirationStoreService : IPreRegistirationStoreService
    {
        IRepository<PreRegistrationStore> _preRegistrationStoreRepository;
        IRepository<MemberDescription> _memberDescriptionRepository;
        IRepository<User> _userRepository;
        IRepository<Store> _StoreRepository;
        IRepository<Phone> _PhoneRepository;

        public PreRegistirationStoreService(IRepository<PreRegistrationStore> preRegistrationStoreRepository, IRepository<MemberDescription> memberDescriptionRepository, IRepository<User> userRepository, IRepository<Store> storeRepository, IRepository<Phone> phoneRepository)
        {
            _preRegistrationStoreRepository = preRegistrationStoreRepository;
            _memberDescriptionRepository = memberDescriptionRepository;
            _userRepository = userRepository;
            _StoreRepository = storeRepository;
            _PhoneRepository = phoneRepository;
        }

        public void InsertPreRegistrationStore(PreRegistrationStore preRegistirationStore)
        {
            if (preRegistirationStore == null)
                throw new ArgumentNullException("preRegistirationStore");
            _preRegistrationStoreRepository.Insert(preRegistirationStore);
        }

        public void UpdatePreRegistrationStore(PreRegistrationStore preRegistirationStore)
        {
            if (preRegistirationStore == null)
                throw new ArgumentNullException("preRegistirationStore");
            _preRegistrationStoreRepository.Update(preRegistirationStore);
        }
        public void DeletePreRegistrationStore(PreRegistrationStore preRegistirationStore)
        {
            if (preRegistirationStore == null)
                throw new ArgumentNullException("preRegistirationStore");
            _preRegistrationStoreRepository.Delete(preRegistirationStore);
        }

        public PreRegistrationStore GetPreRegistirationStoreByPreRegistrationStoreId(int preRegistraionStoreId)
        {
            if (preRegistraionStoreId == 0)
                throw new ArgumentNullException("preRegistraionId");
            var query = _preRegistrationStoreRepository.Table;
            return query.FirstOrDefault(x => x.PreRegistrationStoreId == preRegistraionStoreId);
        }

        public IPagedList<PreRegistrationStoreResponse> GetPreRegistirationStores(int page, int pageSize, string storeName, string email, string city = "",string user="", bool notcalling=false)
        {
            int totalRecord = 0;
            var query = _preRegistrationStoreRepository.TableNoTracking;
            if (!string.IsNullOrEmpty(storeName))
            {
                query = query.Where(x => x.StoreName.ToLower().Contains(storeName.ToLower()));
            }
            if (!string.IsNullOrEmpty(email))
            {
                query = query.Where(x => x.Email.ToLower().Contains(email.ToLower()));
            }
            if (!string.IsNullOrEmpty(city))
            {
                query = query.Where(x => x.City.ToLower().Contains(city.ToLower()));
            }

            List<PreRegistrationStoreResponse> result = new List<PreRegistrationStoreResponse>();
            List<int> PreRegistrationStoreIdList=query.Select(x=>x.PreRegistrationStoreId).Distinct().ToList();
            var memberDescription = _memberDescriptionRepository.TableNoTracking.Where(x=> PreRegistrationStoreIdList.Contains((int)x.PreRegistrationStoreId)).Select(x => new {
                PreRegistrationStoreId = x.PreRegistrationStoreId,
                date=x.Date,
                title = x.Title,
                userId=x.UserId
            }).ToList();


            List<string> ControlList = new List<string>() {
            "Teknik Servis",
            "Aksesuar Parça",
            "Hizmet",
            "Satişa Uygun Değil"
            };
            var UserList = _userRepository.TableNoTracking.ToList();
            List<PreRegistrationStore> liste = query.ToList();
            var config = new MapperConfiguration(cfg =>
                cfg.CreateMap<PreRegistrationStore, PreRegistrationStoreResponse>().ReverseMap()
            );
            var mapper = new Mapper(config);
            Parallel.For(0, liste.Count, index=> {
                var item = liste[index];
                PreRegistrationStoreResponse PreRegistrationStoreResponse=mapper.Map<PreRegistrationStoreResponse>(item);               
                bool state = false;
                var prestorememberDescription = memberDescription.Where(x => x.PreRegistrationStoreId == item.PreRegistrationStoreId).OrderByDescending(x => x.date).FirstOrDefault();
                if (prestorememberDescription != null)
                {
                    var Usr= UserList.FirstOrDefault(x => x.UserId == prestorememberDescription.userId);
                    if (Usr != null)
                    {
                        PreRegistrationStoreResponse.AtananUser = Usr.UserName;
                    }
                    else
                    {
                        PreRegistrationStoreResponse.AtananUser = "User Yok";
                    }
                    state = ControlList.Contains(prestorememberDescription.title);
                }
                else
                {
                    PreRegistrationStoreResponse.AtananUser = "-";
                }
                if (notcalling && state)
                {
                    lock (this)
                    {
                        result.Add(PreRegistrationStoreResponse);
                    }
                }
                else if (!notcalling && !state)
                {
                    lock (this)
                    {
                        result.Add(PreRegistrationStoreResponse);
                    }
                }
                else
                {

                }
            });
            if (!string.IsNullOrEmpty(user))
            {
                string txtUserName = "-";

                if (user == "0")
                {
                    result = result.Where(x => x.AtananUser == txtUserName).ToList();
                }
                else
                {
                    txtUserName = UserList.FirstOrDefault(x => x.UserId.ToString() == user)?.UserName;
                    result = result.Where(x => x.AtananUser.Contains(txtUserName)).ToList();
                }
            }
            totalRecord = result.Count;
            result = result.OrderByDescending(x => x.PreRegistrationStoreId).Skip(page * pageSize - pageSize).Take(pageSize).ToList();
            var preRegistrationStores = result.ToList();
            return new PagedList<PreRegistrationStoreResponse>(preRegistrationStores, page, pageSize, totalRecord);
        }


        public IList<Store> GetPreRegistrationStoreSearchByPhone(params string[] Phones)
        {
            Phones = Phones.Where(x => !string.IsNullOrEmpty(x)).ToArray();
            for (int Don = 0; Don < Phones.Length; Don++)
            {
                Phones[Don] = Phones[Don].Replace(" ", "");
            }
            List<int> vs = new List<int>();
            var query = _PhoneRepository.Table;
            foreach (string phone in Phones)
            {
                var tmpquery = query.Where(x => Phones.Contains(phone));
                if (tmpquery.Count() > 0)
                { 
                    vs.AddRange(tmpquery.Select(x=>(int)x.MainPartyId).ToList());
                }
            }
            var querystore = _StoreRepository.Table;
            querystore = querystore.Where(x => vs.Contains(x.MainPartyId));
            return querystore.ToList();
        }

        public IList<PreRegistrationStore> GetPreRegistrationStoreSearchByName(string storeName)
        {
            var query = _preRegistrationStoreRepository.Table;
            query = query.Where(x => x.StoreName.ToLower().Contains(storeName.ToLower()));
            return query.ToList();
        }
        public IList<PreRegistrationStore> GetPreRegistrationStores()
        {
            var query = _preRegistrationStoreRepository.TableNoTracking;
            return query.ToList();
        }
    }
}
