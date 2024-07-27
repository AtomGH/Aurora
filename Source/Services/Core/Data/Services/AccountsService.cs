using Aurora.Core.Data.Entities;
using Aurora.Library.Accounts;

namespace Aurora.Core.Data.Services
{
    /// <summary>
    /// Entity repository for services.
    /// </summary>
    public class AccountsService
    {
        private readonly DatabaseContext _context;

        /// <summary>
        /// Instantiate a service entity repository.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="idGenerator"></param>
        public AccountsService(DatabaseContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get a specific account by ID.
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        /// <exception cref="InvalidDataException"></exception>
        public async Task<Account> GetAsync(int accountId)
        {
            Account? targetAccount = await _context.Accounts.FindAsync(accountId);
            if (targetAccount == null)
            {
                throw new InvalidDataException("The account does not exist.");
            }
            return targetAccount;
        }

        /// <summary>
        /// Add a new account.
        /// </summary>
        /// <param name="accountName"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public async Task<Account> AddAsync(string accountName, AccountType type)
        {
            Account newAccount = new(_context, accountName, type);
            await _context.Accounts.AddAsync(newAccount);
            return newAccount;
        }
    }
}
