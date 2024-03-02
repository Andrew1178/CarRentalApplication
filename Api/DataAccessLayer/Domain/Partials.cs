using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataAccessLayer.Domain.Model;

public partial class CarRentalContext : DbContext
{
    private readonly IConfiguration _configuration;
    public CarRentalContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public CarRentalContext(DbContextOptions<CarRentalContext> options, IConfiguration configuration)
        : base(options)
    {
        _configuration = configuration;
    }

      protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_configuration.GetConnectionString("CarRentalContext"));
    }
}