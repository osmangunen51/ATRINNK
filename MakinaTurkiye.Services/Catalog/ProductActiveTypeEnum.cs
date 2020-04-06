using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakinaTurkiye.Services.Catalog
{
    public enum ProductActiveTypeEnum:byte
    {
        Inceleniyor,
        Onaylandi,
        Onaylanmadi,
        Silindi,
        CopKutusunda = 6,
        Tumu = 7,
        CopKutusuYeni = 8
    }
}
