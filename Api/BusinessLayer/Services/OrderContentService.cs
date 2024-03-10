using AutoMapper;
using BusinessLayerAbstractions;
using DataAccessLayerAbstractions;
using Domain;


namespace BusinessLayer;

public class OrderContentService : CrudService<OrderContentDto, OrderContent>, IOrderContentService
{
    public OrderContentService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, unitOfWork.OrderContentRepository, mapper)
    {
    }
}