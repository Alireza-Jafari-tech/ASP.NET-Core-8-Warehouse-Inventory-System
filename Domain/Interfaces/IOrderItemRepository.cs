namespace Domain.Interfaces
{
	public interface IOrderItemRepository
	{
		Task<bool> UpdateOrderItemQuantityAsync(int orderId, int itemId, int newQuantity);
		Task<bool> OrderItemExistsAsync(int itemId, int orderId);
	}
}