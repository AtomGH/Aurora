using Aurora.Framework.Data;
using Aurora.Framework.Data.Extensions;
using Aurora.Framework.Data.Entities;
using Aurora.Library.Common;
using Aurora.Library.Accounts;
using Aurora.Framework.Applications.Extensions;

namespace Aurora.Framework.Applications
{
    /// <summary>
    /// The interact to interact with accounts, contains all the business logic for accounts.
    /// </summary>
    public class AccountsApplication
    {
        private readonly DataService _data;

        /// <summary>
        /// Instantiate an instance.
        /// </summary>
        /// <param name="dataService"></param>
        public AccountsApplication(DataService dataService)
        {
            _data = dataService;
        }

        /// <summary>
        /// Get a list of all accounts.
        /// </summary>
        /// <param name="parameters">The parameters used to query multiple items.</param>
        /// <returns>A range query result that contains a slice of result and the total quantity of the result.</returns>
        public async Task<RangeQueryResult<AccountInformation>> GetAllAccounts(RangeQueryParameter parameters)
        {
            List<Account> listOfAccounts = await _data.Accounts.GetAsync(parameters.Start, parameters.Limit);
            List<AccountInformation> listOfAccountInformations = new();
            listOfAccounts.ForEach(a => listOfAccountInformations.Add(a.ToInformation()));
            long totalQuantity = await _data.Accounts.CountAsync();

            return new RangeQueryResult<AccountInformation>(totalQuantity, listOfAccountInformations);
        }

        /// <summary>
        /// Create an account.
        /// </summary>
        /// <param name="parameters">Parameters must be filled to create an account.</param>
        /// <returns></returns>
        public async Task<AccountInformation> CreateAccountAsync(CreateAccountParameters parameters)
        {
            Account newAccount = await _data.Accounts.AddAsync(parameters.Name, parameters.Type);
            return newAccount.ToInformation();
        }
    }
}
