using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Application.Interfaces;
using Application.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Web.Pages.Admin.Orders
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public IEnumerable<OrderDto> Orders { get; set; }

        public IndexModel(
            IOrderService orderService,
            UserManager<ApplicationUser> userManager,
            IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
            _userManager = userManager;
        }
        public async Task OnGetAsync()
        {
            var orderEntites = await _orderService.GetAllOrdersAsync();
            Orders = _mapper.Map<IEnumerable<OrderDto>>(orderEntites);
        }

        public void OnPost()
        {
            // Logic to handle form submission can be added here
        }
    }
}