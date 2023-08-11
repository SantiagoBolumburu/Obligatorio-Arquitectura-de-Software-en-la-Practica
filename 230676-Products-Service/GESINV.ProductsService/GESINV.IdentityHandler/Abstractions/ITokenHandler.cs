using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GESINV.IdentityHandler.Abstractions
{
    public interface ITokenHandler
    {
        string? GetCurrentRequestToken(string header, TokenFormat format);
        bool ValidateToken(string token, string secretKey);
        SessionInfo? DeserializeSessionInfo(string token);
        string GetSessionToken(SessionInfo sessionInfo, string secretKey);
        void SetCurrentSessionInfo(SessionInfo sessionInfo);
        SessionInfo GetCurrentSessionInfo();
        string GetGesinvApplicationToken(TokenFormat format, string secretKey);
    }
}
