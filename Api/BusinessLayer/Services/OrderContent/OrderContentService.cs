using AutoMapper;
using DataAccessLayer;

namespace BusinessLayer;

public class OrderContentService : CrudService<OrderContentDto, OrderContent>, IOrderContentService
{
    public OrderContentService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, unitOfWork.OrderContentRepository, mapper)
    {
    }
}