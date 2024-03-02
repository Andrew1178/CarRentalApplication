using AutoMapper;
using DataAccessLayer;
using DataAccessLayer.Domain.Model;

namespace BusinessLayer;

public class OrderContentService : CrudService<OrderContentDto, OrderContent>, IOrderContentService
{
    public OrderContentService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, unitOfWork.OrderContentRepository, mapper)
    {
    }
}