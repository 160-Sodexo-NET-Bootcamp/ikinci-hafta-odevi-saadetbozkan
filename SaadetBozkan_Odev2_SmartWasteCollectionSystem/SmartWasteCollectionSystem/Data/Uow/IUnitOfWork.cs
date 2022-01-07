using Data.ContainerRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Uow
{
    public interface IUnitOfWork
    {
        IContainerRepository Container { get; }
        IVehicleRepository Vehicle { get; }

        int Complate();
    }
}
