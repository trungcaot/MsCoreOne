using MsCoreOne.Application.Categories.Queries.Dtos;
using MsCoreOne.Application.Common.Bases;
using MsCoreOne.Application.Common.Exceptions;
using MsCoreOne.Application.Common.Interfaces;
using System.Threading.Tasks;

namespace MsCoreOne.Application.Categories.Commands.UpdateCategory
{
    public class DataConflictValidator : BaseDataConflictValidator<UpdateCategoryDto, CategoryDto>
    {
        private readonly UpdateCategoryDto _updateCategoryDto;
        private readonly IApplicationDbContext _context;

        public DataConflictValidator(UpdateCategoryDto updateCategoryDto,
            IApplicationDbContext context) : base(updateCategoryDto)
        {
            _updateCategoryDto = updateCategoryDto;
            _context = context;
        }

        protected override async Task<CategoryDto> GetLatestDataFromDbAsync()
        {
            var category = await _context.Categories.FindAsync(_updateCategoryDto.Id);
            if (category == null)
            {
                throw new NotFoundException("Category is not exist.");
            }

            return new CategoryDto { Id = category.Id, Name = category.Name };
        }
    }
}
