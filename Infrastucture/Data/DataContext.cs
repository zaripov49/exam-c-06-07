using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastucture.Data;

public class DataContext(DbContextOptions<DataContext> options) : IdentityDbContext(options)
{
}
