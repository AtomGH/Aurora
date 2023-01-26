using Framework.Core.Data;
using Framework.Core.Data.Extensions;
using Framework.Core.Data.Models;
using Framework.Library.Accounts;
using Framework.Library.Common;

namespace Framework.Core.Applications
{
    public class AccountsApplication
    {
        private readonly DataService _data;

        public AccountsApplication(DataService dataService)
        {
            _data = dataService;
        }

        public async Task<RangeQueryResult<AccountInformation>> GetListOfAllAccounts(RangeQueryParameter parameters)
        {
            List<Account> listOfAccounts = await _data.Accounts.GetAsync(parameters.Start, parameters.Limit);
            List<AccountInformation> listOfAccountInformations = new();
            listOfAccounts.ForEach(a => listOfAccountInformations.Add(a.GetInformation()));
            long totalQuantity = await _data.Accounts.CountAsync();

            return new RangeQueryResult<AccountInformation>(totalQuantity, listOfAccountInformations);
        }

        public async Task<AccountInformation> CreateAccountAsync(CreateAccountParameter parameters)
        {
            Account newAccount = await _data.Accounts.AddAsync(parameters.Name, parameters.Type);
            return newAccount.GetInformation();
        }
    }
}
