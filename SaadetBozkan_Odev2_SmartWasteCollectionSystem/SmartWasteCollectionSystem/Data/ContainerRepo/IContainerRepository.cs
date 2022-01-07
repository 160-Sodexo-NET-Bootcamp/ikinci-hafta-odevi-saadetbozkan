using Data.DataModel;
using Data.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ContainerRepo
{
    public interface IContainerRepository : IGenericRepository<Container>
    {
        Task<IEnumerable<Container>> GetByVehicleId(long id);
    }
   
    
 }
