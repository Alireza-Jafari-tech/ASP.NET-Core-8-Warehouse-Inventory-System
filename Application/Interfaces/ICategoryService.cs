using Application.DTOs;

namespace Application.Interfaces
{
    public interface ICategoryService
    {
        Task CreateCategoryAsync(CategoryDto categoryDto);
        Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync();
        Task<CategoryDto> GetCategoryByIdAsync(int id);
    }
}