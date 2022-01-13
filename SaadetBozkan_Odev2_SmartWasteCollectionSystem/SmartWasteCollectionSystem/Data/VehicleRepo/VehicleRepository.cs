using Data.Context;
using Data.DataModel;
using Data.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.ContainerRepo
{
    public class VehicleRepository : GenericRepository<Vehicle>, IVehicleRepository
    {
        public VehicleRepository(CollectionSystemDbContext context, ILogger logger) : base(context, logger)
        {
        }
        public bool Delete(long id)
        {
            var vehicle = dbSet.Find(id);
            //delete vehicle with its containers
            DbSet<Container> dbSet2 = context.Set<Container>();
            var listOfContainer = dbSet2.Where(s => s.VehicleId == vehicle.Id);
            dbSet.Remove(vehicle);           
            dbSet2.RemoveRange(listOfContainer);
            return true;
        }
    }
}
