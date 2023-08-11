using GESINV.AthenticationService.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GESINV.AthenticationService.PersistanceAccess.Interface
{
    public interface IAppkeyRepository
    {
        ApplicationKey? GetActiveByUserId(Guid id);
        List<ApplicationKey> GetAllActivesByUserId(Guid id);
        ApplicationKey Create(ApplicationKey applicationKey);
        ApplicationKey Update(ApplicationKey applicationKey);
    }
}
