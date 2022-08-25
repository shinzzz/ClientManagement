using Microsoft.EntityFrameworkCore;

namespace ClientManagement
{
    public class ClientDBContext:DbContext
    {
        public ClientDBContext(DbContextOptions<ClientDBContext> options) : base(options)
        {

        }
       
        public virtual DbSet<ClientData> ClientData { get; set; }
    }
}
