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
        private readonly IRepository<City> _cityRepository;
        private readonly IRepository<Locality> _localityRepository;
        private readonly IRepository<Country> _countryRepository;
        private readonly IRepository<Town> _townRepository;
        IRepository<MemberDescription> _memberDescriptionRepository;
        IRepository<User> _userRepository;
        IRepository<Store> _StoreRepository;
        IRepository<Phone> _PhoneRepository;

        public PreRegistirationStoreService(IRepository<PreRegistrationStore> preRegistrationStoreRepository, IRepository<City> cityRepository, IRepository<Locality> localityRepository, IRepository<Country> countryRepository, IRepository<Town> townRepository, IRepository<MemberDescription> memberDescriptionRepository, IRepository<User> userRepository, IRepository<Store> storeRepository, IRepository<Phone> phoneRepository)
        {
            _preRegistrationStoreRepository = preRegistrationStoreRepository;
            _cityRepository = cityRepository;
            _localityRepository = localityRepository;
            _countryRepository = countryRepository;
            _townRepository = townRepository;
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
                var CountryIdListesi = _countryRepository.TableNoTracking.Where(x => x.CountryName.Contains(city)).Select(x=>x.CountryId).ToList();
                var CityIdListesi = _cityRepository.TableNoTracking.Where(x => x.CityName.Contains(city)).Select(x => x.CityId).ToList();
                var LocalityIdListesi = _localityRepository.TableNoTracking.Where(x => x.LocalityName.Contains(city)).Select(x => x.LocalityId).ToList();
                query = query.Where(x => CountryIdListesi.Contains((int)x.CountryId) || CityIdListesi.Contains((int)x.CityId) || LocalityIdListesi.Contains((int)x.LocalityId) || x.City.Contains(city));
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
            var CountryListesi = _countryRepository.TableNoTracking.ToList();
            var CityListesi = _cityRepository.TableNoTracking.ToList();
            var LocalityListesi = _localityRepository.TableNoTracking.ToList();
            foreach (var item in result)
            {
                item.CountryName = CountryListesi.FirstOrDefault(x => x.CountryId == item.CountryId)?.CountryName;
                item.CityName = CityListesi.FirstOrDefault(x => x.CityId == item.CityId)?.CityName;
                item.LocalityName = LocalityListesi.FirstOrDefault(x => x.LocalityId == item.LocalityId)?.LocalityName;
            }
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
