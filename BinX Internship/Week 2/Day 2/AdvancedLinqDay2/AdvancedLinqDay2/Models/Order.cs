using System;
using System.Collections.Generic;
using System.Text;

namespace AdvancedLinqDay2.Models
{
    internal record Order(
    int Id,
    int CustomerId,
    decimal Amount,
    List<OrderItem> Items
    );
}
