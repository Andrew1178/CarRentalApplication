namespace BusinessLayer;

public class OrderDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; } // TODO: This should be the username

    public DateTime OrderedOn { get; set; }

    public DateTime? CancelledOn { get; set; }

    public ICollection<OrderContentDto> OrderContents { get; set; } = new List<OrderContentDto>();
}
