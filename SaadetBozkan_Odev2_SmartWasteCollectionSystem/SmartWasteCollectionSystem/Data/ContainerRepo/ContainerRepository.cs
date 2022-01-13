using Data.Context;
using Data.DataModel;
using Data.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ContainerRepo
{
    public class ContainerRepository : GenericRepository<Container>, IContainerRepository
    {
        public ContainerRepository(CollectionSystemDbContext context, ILogger logger) : base(context, logger)
        {
        }
   
        public bool Update(Container entity)
        {
            var id = entity.Id;
            DbSet<Container> dbSet2 = context.Set<Container>();

            var container = dbSet2.Find(id);
            container.Longitude = entity.Longitude == 0 ? container.Longitude : entity.Longitude;
            container.Latitude = entity.Latitude == 0 ? container.Latitude: entity.Latitude;
            container.ContainerName = entity.ContainerName == null ? container.ContainerName : entity.ContainerName;

            dbSet.Update(container);
            return true;
        }
        public IEnumerable<Container> GetByVehicleId(long id)
        {
            var listOfContainer = dbSet.Where(s => s.VehicleId == id);
            return listOfContainer;
        }
    }
}
