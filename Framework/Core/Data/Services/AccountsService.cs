using Framework.Core.Data.Models;
using Framework.Library.Accounts;
using Framework.Library.Common;
using Microsoft.EntityFrameworkCore;

namespace Framework.Core.Data.Services
{
    public class AccountsService
    {
        private readonly DatabaseContext _context;

        public AccountsService(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<List<Account>> GetAsync(long start, int limit)
        {
            return await _context.Accounts.LongSkip(start - 1).Take(limit).ToListAsync();
        }

        public async Task<Account> GetAsync(long accountId)
        {
            Account? targetAccount = await _context.Accounts.FindAsync(accountId);
            if (targetAccount == null)
            {
                throw new InvalidDataException("The account does not exist.");
            }
            return targetAccount;
        }

        public async Task<long> CountAsync()
        {
            return await _context.Accounts.LongCountAsync();
        }

        public async Task<Account> AddAsync(string accountName, AccountType type)
        {
            Account newAccount = new(_context, accountName, type);
            await _context.Accounts.AddAsync(newAccount);
            return newAccount;
        }
    }
}
