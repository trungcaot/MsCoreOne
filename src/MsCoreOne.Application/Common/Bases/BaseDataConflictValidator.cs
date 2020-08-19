using MsCoreOne.Application.Common.Exceptions;
using MsCoreOne.Application.Common.Extensions;
using MsCoreOne.Application.Common.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MsCoreOne.Application.Common.Bases
{
    public class BaseDataConflict<T>
    {
        public bool IsOverride { get; set; }

        public T Original { get; set; }
    }

    public abstract class BaseDataConflictValidator<TOfUpdateDto, TOfOriginalObject> where TOfUpdateDto : BaseDataConflict<TOfOriginalObject>
    {
        private readonly TOfUpdateDto _updateDto;

        protected BaseDataConflictValidator(TOfUpdateDto updateDto)
        {
            _updateDto = updateDto;
        }

        public async Task<bool> ValidateDataConflictAsync()
        {
            if (_updateDto.IsOverride)
            {
                return true;
            }

            var latestDataFromDb = await GetLatestDataFromDbAsync();

            var differences = new List<Difference>();
            if (!latestDataFromDb.IsDefault())
            {
                differences = latestDataFromDb.Compare(_updateDto.Original).ToList();
            }

            var compareNested = await CompareNestedObjectAsync();
            if (!compareNested.IsDefault())
            {
                differences.AddRange(compareNested);
            }

            differences = HandleInternalRules(latestDataFromDb, differences);

            if (differences.Any())
            {
                throw new DataConflictException(differences);
            }
            return true;
        }

        protected virtual List<Difference> HandleInternalRules(TOfOriginalObject model, List<Difference> differences)
        {
            return differences;
        }

        protected virtual async Task<TOfOriginalObject> GetLatestDataFromDbAsync()
        {
            return await Task.FromResult(default(TOfOriginalObject));
        }

        protected virtual async Task<IList<Difference>> CompareNestedObjectAsync()
        {
            return await Task.FromResult(default(IList<Difference>));
        }
    }
}
