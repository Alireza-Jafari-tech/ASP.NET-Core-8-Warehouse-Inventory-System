using Domain.Entities;
using Application.DTOs;

namespace Application.Interfaces
{
    public interface ISupplierStatus
    {
        Task<IEnumerable<SupplierStatusDto>> GetAllSupplierStatusAsync();
        Task<SupplierStatusDto?> GetSupplierStatusByIdAsync(int id);
        Task AddSupplierStatusAsync(SupplierStatusDto supplierStatusDto);
        Task UpdateSupplierStatusAsync(SupplierStatusDto newSupplierStatusDto);
        Task RemoveSupplierStatusAsync(int id);
    }
}