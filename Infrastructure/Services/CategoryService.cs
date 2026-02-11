using Application.Interfaces;
using Domain.Interfaces;
using Application.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Infrastructure.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> _categoRepository;
        private readonly IMapper _mapper;
        public CategoryService(IRepository<Category> categoRepository, IMapper mapper)
        {
            _categoRepository = categoRepository;
            _mapper = mapper;
        }

        public async Task CreateCategoryAsync(CategoryDto categoryDto)
        {
            var newCategory = _mapper.Map<Category>(categoryDto);
            await _categoRepository.AddAsync(newCategory);
        }

        public async Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync()
        {
            var categories = await _categoRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CategoryDto>>(categories);
        }

        public async Task<CategoryDto> GetCategoryByIdAsync(int id)
        {
            var category = _categoRepository.GetByIdAsync(id);
            return _mapper.Map<CategoryDto>(category);
        }
    }
}