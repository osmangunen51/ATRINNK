using MakinaTurkiye.Entities.Tables.Cultures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakinaTurkiye.Services.Cultures
{
    public interface ILanguageService
    {
        List<Language> GetLanguages();
        Language GetLanguageByCode(string code);

        StaticDefinition GetStaticDefinitionByKeyAndLanguageId(string key, int languageId);


    }
}
