namespace Webshop.Models
{
    public interface IOrderRepository
    {
        void CreateOrder(Order order);
    }
}