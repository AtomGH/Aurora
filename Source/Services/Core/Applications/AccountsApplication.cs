using Aurora.Core.Data;
using Aurora.Core.Data.Entities;
using Aurora.Library.Common;
using Aurora.Library.Accounts;
using Aurora.Core.Applications.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Aurora.Core.Applications
{
    /// <summary>
    /// The interact to interact with accounts, contains all the business logic for accounts.
    /// </summary>
    public class AccountsApplication
    {
        private readonly DataService _data;
        private readonly DatabaseContext _context;

        /// <summary>
        /// Instantiate an instance.
        /// </summary>
        /// <param name="dataService"></param>
        public AccountsApplication(DataService dataService, DatabaseContext context)
        {
            _data = dataService;
            _context = context;

        }

        /// <summary>
        /// Get a list of all accounts.
        /// </summary>
        /// <param name="parameters">The parameters used to query multiple items.</param>
        /// <returns>A range query result that contains a slice of result and the total quantity of the result.</returns>
        public async Task<RangeQueryResult<AccountInformation>> GetAllAccounts(RangeQueryParameter parameters)
        {
            List<Account> listOfAccounts = await _context.Accounts.LongSkip(parameters.Start - 1).Take(parameters.Limit).ToListAsync();
            List<AccountInformation> listOfAccountInformations = new();
            listOfAccounts.ForEach(a => listOfAccountInformations.Add(a.ToInformation()));
            long totalQuantity = await _context.Accounts.CountAsync();

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
