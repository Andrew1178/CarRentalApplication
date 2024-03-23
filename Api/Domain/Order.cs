using System;
using System.Collections.Generic;

namespace Domain;

public partial class Order
{
    public Guid Id { get; set; }

    public string EmailAddress { get; set; }

    public DateTime OrderedOn { get; set; }

    public DateTime? CancelledOn { get; set; }

    public virtual ICollection<OrderContent> OrderContents { get; set; } = new List<OrderContent>();
}
