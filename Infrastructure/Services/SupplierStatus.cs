using Application.Interfaces;
using Domain.Interfaces;
using Domain.Entities;
using Infrastructure.Services;
using Application.DTOs;
using Infrastructure.Data;
using AutoMapper;
using Application.Common.Exceptions;

namespace Infrastructure.Services
{
    public class SupplierStatusService : ISupplierStatus
    {
        private readonly IRepository<Domain.Entities.SupplierStatus> _supplierStatusRepository;
        private readonly IMapper _mapper;
        public SupplierStatusService(IRepository<Domain.Entities.SupplierStatus> supplierStatusRepository, IMapper mapper)
        {
            _supplierStatusRepository = supplierStatusRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<SupplierStatusDto>> GetAllSupplierStatusAsync()
        {
            var supplierStatus = await _supplierStatusRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<SupplierStatusDto>>(supplierStatus);
        }
        public async Task<SupplierStatusDto?> GetSupplierStatusByIdAsync(int id)
        {
            var supplierStatus = await _supplierStatusRepository.GetByIdAsync(id);
            if (supplierStatus == null)
                throw new NotFoundException("supplier status not found!");

            return _mapper.Map<SupplierStatusDto>(supplierStatus);
        }
        public async Task AddSupplierStatusAsync(SupplierStatusDto supplierStatusDto)
        {
            var newSupplierStatus = _mapper.Map<SupplierStatus>(supplierStatusDto);
            await _supplierStatusRepository.AddAsync(newSupplierStatus);
        }
        public async Task UpdateSupplierStatusAsync(SupplierStatusDto newSupplierStatusDto)
        {
            var newSupplierStatus = _mapper.Map<SupplierStatus>(newSupplierStatusDto);
            await _supplierStatusRepository.UpdateAsync(newSupplierStatus);
        }
        public async Task RemoveSupplierStatusAsync(int id)
        {
            await _supplierStatusRepository.RemoveAsync(id);
        }
    }
}