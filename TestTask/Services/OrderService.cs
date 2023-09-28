using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services;

public class OrderService : IOrderService
{
    private readonly ApplicationDbContext _ctx;
    private const int MIN_QUANTITY = 10;

    public OrderService(ApplicationDbContext ctx)
    {
        _ctx = ctx;
    }

    public async Task<Order> GetOrder()
    {
        var maxPrice = await _ctx.Orders.MaxAsync(order => order.Price);
        return (await _ctx.Orders.FirstOrDefaultAsync(order => order.Price == maxPrice))!;
    }

    public async Task<List<Order>> GetOrders()
    {
        return await _ctx.Orders.Where(_ => _.Quantity > MIN_QUANTITY).ToListAsync();
    }
}