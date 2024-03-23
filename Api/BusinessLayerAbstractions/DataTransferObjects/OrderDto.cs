namespace BusinessLayerAbstractions;

public class OrderDto
{
    public Guid Id { get; set; }
    public string EmailAddress { get; set; }

    public DateTime OrderedOn { get; set; }

    public DateTime? CancelledOn { get; set; }

    public ICollection<OrderContentDto> OrderContents { get; set; } = new List<OrderContentDto>();
}
