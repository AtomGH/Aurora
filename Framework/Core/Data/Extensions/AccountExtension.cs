using Framework.Core.Data.Models;
using Framework.Library.Accounts;

namespace Framework.Core.Data.Extensions
{
    public static class AccountExtension
    {
        public static AccountInformation GetInformation(this Account account)
        {
            return new()
            {
                Id = account.Id,
                Name = account.Name,
                Type = account.Type,
            };
        }
    }
}
