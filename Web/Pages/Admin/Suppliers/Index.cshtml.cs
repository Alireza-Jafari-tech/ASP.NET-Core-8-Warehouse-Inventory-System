using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace Web.Pages.Admin.Supplier
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly ISupplierService _supplierService;
        private readonly ISupplierStatus _supplierStatusService;
        public IndexModel(ISupplierService supplierService, ISupplierStatus supplierStatus)
        {
            _supplierService = supplierService;
            _supplierStatusService = supplierStatus;
        }

        public IEnumerable<SupplierDto> Suppliers { get; set; }
        [BindProperty]
        public SupplierDto SupplierDto { get; set; }

        public List<SelectListItem> SuppliersStatusDropList { get; set; }
        public async Task OnGetAsync()
        {
            // fill in select dropdown in create supplier section
            var SuppliersStatus = await _supplierStatusService.GetAllSupplierStatusAsync();
            SuppliersStatusDropList = SuppliersStatus.Select(s => new SelectListItem()
            {
                Text = s.Name,
                Value = s.Id.ToString()
            }).ToList();

            Suppliers = await _supplierService.GetSuppliersInfo();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // fill in select dropdown in create supplier section
            var SuppliersStatus = await _supplierStatusService.GetAllSupplierStatusAsync();
            SuppliersStatusDropList = SuppliersStatus.Select(s => new SelectListItem()
            {
                Text = s.Name,
                Value = s.Id.ToString()
            }).ToList();

            if (!ModelState.IsValid)
                return Page();

            if (SupplierDto.Id == 0 || SupplierDto.Id == null) // new record, need to add to db
                await _supplierService.AddSupplierAsync(SupplierDto);
            else // record already exists, update it in db
                await _supplierService.UpdateSupplierAsync(SupplierDto, SupplierDto.Id);

            return RedirectToPage();
        }
    }
}