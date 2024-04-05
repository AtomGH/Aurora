using Assets.Data;
using Assets.Data.Entities;
using Assets.Extensions;
using Aurora.Library.Assets;
using Aurora.Library.Common;
using Microsoft.EntityFrameworkCore;

namespace Aurora.Services.Assets.Applications
{
    public class AssetsApplication
    {
        private readonly DatabaseContext _context;

        public AssetsApplication(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<RangeQueryResult<AssetInformation>> QueryAsync(QueryAssetParameters parameters)
        {
            IQueryable<Asset> queryRoot = _context.Assets.AsQueryable();
            if (parameters.ProjectId != 0)
            {
                queryRoot = queryRoot.Where(a => a.ProjectId == parameters.ProjectId);
            }
            if (parameters.Name != string.Empty)
            {
                queryRoot = queryRoot.Where(a => a.Name.Contains(parameters.Name));
            }
            if (parameters.TypeId != 0)
            {
                queryRoot = queryRoot.Where(a => a.Type.Id == parameters.TypeId);
            }
            if (parameters.OperatorId != 0)
            {
                queryRoot = queryRoot.Where(a => a.OperatorId == parameters.OperatorId);
            }

            List<Asset> listOfAssets = await queryRoot.LongSkip(parameters.Start - 1).Take(parameters.Limit).Include(a => a.Type).ToListAsync();

            long totalQuantity = await queryRoot.CountAsync();

            List<AssetInformation> listOfAssetInformations = new();
            listOfAssets.ForEach(a => listOfAssetInformations.Add(a.ToInformation()));

            RangeQueryResult<AssetInformation> result = new(totalQuantity, listOfAssetInformations);
            return result;
        }

        public async Task<AssetInformation> GetAsync(long id)
        {
            Asset? targetAsset = await _context.Assets.Where(a => a.Id == id).Include(a => a.Type).FirstOrDefaultAsync();
            if (targetAsset == null)
            {
                throw new Exception("Asset not found.");
            }
            return targetAsset.ToInformation();
        }

        public async Task<AssetInformation> CreateAssetAsync(CreateAssetParameters parameters)
        {
            AssetType? targetType = await _context.AssetTypes.Where(t => t.Id == parameters.TypeId).SingleOrDefaultAsync();
            if (targetType == null)
            {
                throw new Exception("The asset type does not exist.");
            }
            Asset newAsset = new(
                parameters.Name,
                parameters.Description,
                targetType,
                parameters.ProjectId,
                parameters.OperatorId,
                parameters.Token);
            await _context.Assets.AddAsync(newAsset);
            await _context.SaveChangesAsync();
            return newAsset.ToInformation();
        }

        public async Task DeleteAssetAsync(long id)
        {
            Asset? targetAsset = await _context.Assets.Where(a => a.Id == id).FirstOrDefaultAsync();
            if (targetAsset == null)
            {
                throw new Exception("The asset does not exist.");
            }
            _context.Assets.Remove(targetAsset);
            await _context.SaveChangesAsync();
        }
    }
}
