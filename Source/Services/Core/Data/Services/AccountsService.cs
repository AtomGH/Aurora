using Aurora.Core.Data.Entities;
using Aurora.Library.Common;
using Aurora.Library.Accounts;
using Microsoft.EntityFrameworkCore;

namespace Aurora.Core.Data.Services
{
    /// <summary>
    /// Entity repository for services.
    /// </summary>
    public class AccountsService
    {
        private readonly DatabaseContext _context;
        private readonly IdentifierGenerator _idGenerator;

        /// <summary>
        /// Instantiate a service entity repository.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="idGenerator"></param>
        public AccountsService(DatabaseContext context, IdentifierGenerator idGenerator)
        {
            _context = context;
            _idGenerator = idGenerator;
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
        /// Add a new account.
        /// </summary>
        /// <param name="accountName"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public async Task<Account> AddAsync(string accountName, AccountType type)
        {
            Account newAccount = new(_context, _idGenerator.Get(), accountName, type);
            await _context.Accounts.AddAsync(newAccount);
            return newAccount;
        }
    }
}
