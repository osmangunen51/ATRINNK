using MakinaTurkiye.Core.Data;
using MakinaTurkiye.Entities.Tables.Cultures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakinaTurkiye.Services.Cultures
{
    public class LanguageService : ILanguageService
    {
        IRepository<Language> _languageRepository;
        IRepository<StaticDefinition> _staticDefinitionRepository;

        public LanguageService(IRepository<Language> languageRepository,
            IRepository<StaticDefinition> staticDefinitionRepository)
        {
            this._languageRepository = languageRepository;
            this._staticDefinitionRepository = staticDefinitionRepository;
        }
        public Language GetLanguageByCode(string code)
        {
            var query = _languageRepository.Table;
            return query.FirstOrDefault(x => x.LanguageCode == code);
        }

        public List<Language> GetLanguages()
        {
            var query = _languageRepository.Table;
            return query.ToList();
        }

        public StaticDefinition GetStaticDefinitionByKeyAndLanguageId(string key, int languageId)
        {
            var query = _staticDefinitionRepository.Table;
            return query.FirstOrDefault(x => x.Key.ToLower() == key && x.LanguageId == languageId);
        }
    }
}
