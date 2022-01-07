using Data.ContainerRepo;
using Data.Context;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Uow
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ILogger logger;
        private readonly IConfiguration configuration;
        private readonly CollectionSystemDbContext context;

        public IContainerRepository Container { get; private set; } 
        public IVehicleRepository Vehicle { get; private set; }

        public UnitOfWork(CollectionSystemDbContext context, ILoggerFactory logger, IConfiguration configuration)
        {
            this.context = context;
            this.logger = logger.CreateLogger("patika");
            this.configuration = configuration;

            Container = new ContainerRepository(context, this.logger);
            Vehicle = new VehicleRepository(context, this.logger);
        }

        public int Complate()
        {
            return context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
