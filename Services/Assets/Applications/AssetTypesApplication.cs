using Assets.Data;
using Assets.Data.Entities;
using Aurora.Library.Assets;
using Aurora.Library.Common;
using Aurora.Services.Assets.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Aurora.Services.Assets.Applications
{
    public class AssetTypesApplication
    {
        private readonly DatabaseContext _context;

        public AssetTypesApplication(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<RangeQueryResult<AssetTypeInformation>> QueryAsync(RangeQueryParameter parameters)
        {
            List<AssetType> listOfAssetTypes = await _context.AssetTypes.LongSkip(parameters.Start - 1).Take(parameters.Limit).ToListAsync();
            List<AssetTypeInformation> listOfTypeInformations = new();
            listOfAssetTypes.ForEach(a => listOfTypeInformations.Add(a.ToInformation()));

            long totalQuantity = await _context.AssetTypes.CountAsync();

            RangeQueryResult<AssetTypeInformation> queryResult = new(totalQuantity, listOfTypeInformations);
            return queryResult;
        }

        public async Task<AssetTypeInformation> CreateAsync(CreateAssetTypeParameters parameters)
        {
            AssetType newType = new(parameters.Name, parameters.Description);
            await _context.AssetTypes.AddAsync(newType);
            await _context.SaveChangesAsync();

            return newType.ToInformation();
        }

        public async Task DeleteAsync(long id)
        {
            AssetType? targetAssetType = await _context.AssetTypes.Where(a => a.Id == id).FirstOrDefaultAsync();
            if (targetAssetType == null)
            {
                throw new Exception("Asset type not found.");
            }
            if (await _context.Assets.Where(a => a.Type.Id == id).AnyAsync())
            {
                throw new Exception("Asset type is in use.");
            }
            _context.AssetTypes.Remove(targetAssetType);
            await _context.SaveChangesAsync();
        }
    }
}
