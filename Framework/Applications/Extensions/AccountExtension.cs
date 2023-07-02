using Aurora.Framework.Data.Entities;
using Aurora.Library.Accounts;

namespace Aurora.Framework.Applications.Extensions
{
    /// <summary>
    /// Extension methods for account at application level.
    /// </summary>
    public static class AccountExtension
    {
        /// <summary>
        /// Convert the account to account information that does not contain any navigation properties.
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public static AccountInformation ToInformation(this Account account)
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
