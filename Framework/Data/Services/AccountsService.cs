using Aurora.Framework.Data.Entities;
using Aurora.Library.Common;
using Aurora.Library.Accounts;
using Microsoft.EntityFrameworkCore;

namespace Aurora.Framework.Data.Services
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
        public AccountsService(DatabaseContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get a list of accounts.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public async Task<List<Account>> GetAsync(long start, int limit)
        {
            return await _context.Accounts.LongSkip(start - 1).Take(limit).ToListAsync();
        }

        /// <summary>
        /// Get a specific account by ID.
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        /// <exception cref="InvalidDataException"></exception>
        public async Task<Account> GetAsync(long accountId)
        {
            Account? targetAccount = await _context.Accounts.FindAsync(accountId);
            if (targetAccount == null)
            {
                throw new InvalidDataException("The account does not exist.");
            }
            return targetAccount;
        }

        /// <summary>
        /// Count how many accounts exist.
        /// </summary>
        /// <returns></returns>
        public async Task<long> CountAsync()
        {
            return await _context.Accounts.LongCountAsync();
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
