using Application.Interfaces;
using Domain.Entities;
using Application.DTOs;
using Domain.Interfaces;
using Application.Common.Exceptions;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace Infrastructure.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly IRepository<Supplier> _supplierRepository;
        private readonly IMapper _mapper;
        public SupplierService(IRepository<Supplier> supplierRepository, IMapper mapper)
        {
            _supplierRepository = supplierRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<SupplierDto>> GetAllSuppliersAsync()
        {
            var suppliers = await _supplierRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<SupplierDto>>(suppliers);
        }
        public async Task<SupplierDto> GetSupplierByIdAsync(int id)
        {
            var supplier = await _supplierRepository.GetByIdAsync(id);
            if (supplier == null)
                return null;

            return _mapper.Map<SupplierDto>(supplier);
        }

        public int GetActiveSuppliersCount()
        {
            var query = _supplierRepository.Query();
            query = query
            .Where(c => c.SupplierStatusId == 1); // active supplier status id

            return query.Count();
        }
        public async Task AddSupplierAsync(SupplierDto supplierDto)
        {
            var supplier = _mapper.Map<Supplier>(supplierDto);
            await _supplierRepository.AddAsync(supplier);
        }
        public async Task UpdateSupplierAsync(SupplierDto dto, int id)
        {
            var existing = await _supplierRepository.GetByIdAsync(id);
            if (existing == null)
                throw new NotFoundException("entity not found!");

            _mapper.Map(dto, existing);

            await _supplierRepository.UpdateAsync(existing);
        }
        public async Task RemoveSupplierAsync(int id)
        {
            await _supplierRepository.RemoveAsync(id);
        }

        public async Task<IEnumerable<SupplierDto>> GetSuppliersInfo()
        {
            var suppliers = await _supplierRepository.Query()
              .Include(s => s.SupplierStatus)
              .Include(s => s.SuppliedItems)
              .ToListAsync();

            return _mapper.Map<List<SupplierDto>>(suppliers);
        }
    }
}