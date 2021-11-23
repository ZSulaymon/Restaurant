using Restaurant.Models.Restaurant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Models.Interfaces
{
    public interface IAllOrders
    {
        void createOrder(Order order);
    }
}
