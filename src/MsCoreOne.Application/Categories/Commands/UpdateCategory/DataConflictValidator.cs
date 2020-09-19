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
        private readonly IUnitOfWork _unitOfWork;

        public DataConflictValidator(UpdateCategoryDto updateCategoryDto,
            IUnitOfWork unitOfWork) : base(updateCategoryDto)
        {
            _updateCategoryDto = updateCategoryDto;

            _unitOfWork = unitOfWork;
        }

        protected override async Task<CategoryDto> GetLatestDataFromDbAsync()
        {
            var category = await _unitOfWork.Categories.FirstOrDefaultAsync(c => c.Id == _updateCategoryDto.Id);
            if (category == null)
            {
                throw new NotFoundException("Category is not exist.");
            }

            return new CategoryDto { Id = category.Id, Name = category.Name };
        }
    }
}
