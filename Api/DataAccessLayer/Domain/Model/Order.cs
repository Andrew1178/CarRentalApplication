using System;
using System.Collections.Generic;

namespace DataAccessLayer;

public partial class Order
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public DateTime OrderedOn { get; set; }

    public DateTime? CancelledOn { get; set; }

    public virtual ICollection<OrderContent> OrderContents { get; set; } = new List<OrderContent>();
}
