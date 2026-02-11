using Application.DTOs;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface ISupplierService
    {
        Task<IEnumerable<SupplierDto>> GetAllSuppliersAsync();
        Task<SupplierDto> GetSupplierByIdAsync(int id);
        int GetActiveSuppliersCount();
        Task AddSupplierAsync(SupplierDto supplierDto);
        Task UpdateSupplierAsync(SupplierDto dto, int id);
        Task RemoveSupplierAsync(int id);
        Task<IEnumerable<SupplierDto>> GetSuppliersInfo();
    }
}