using System;
using System.Collections.Generic;
using System.Text;

namespace AdvancedLinqDay2.Models
{
    internal record OrderItem(
    int Id,
    string ProductName,
    int Quantity,
    decimal UnitPrice
    );
}
