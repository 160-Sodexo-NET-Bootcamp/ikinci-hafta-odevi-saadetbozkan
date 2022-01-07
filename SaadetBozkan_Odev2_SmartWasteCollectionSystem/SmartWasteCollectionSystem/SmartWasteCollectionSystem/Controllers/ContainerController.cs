using Data.DataModel;
using Data.Uow;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SmartWasteCollectionSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContainerController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        private readonly ILogger<ContainerController> logger;

        public ContainerController(ILogger<ContainerController> logger, IUnitOfWork unitOfWork)
        {
            this.logger = logger;
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var listOfContainer = await unitOfWork.Container.GetAll();
            unitOfWork.Complate();
            return Ok(listOfContainer);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var container = await unitOfWork.Container.GetById(id);
            if (container is null)
            {
                return NotFound();
            }
            unitOfWork.Complate();
            return Ok(container);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Container entity)
        {
            var response = await unitOfWork.Container.Add(entity);

            unitOfWork.Complate();

            return CreatedAtAction("Post", response);
        }
        // Update withoud vehicleId
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] Container entity, long id)
        {
            entity.Id = id;

            var response = await unitOfWork.Container.Update(entity);

            unitOfWork.Complate();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var response = await unitOfWork.Container.Delete(id);

            unitOfWork.Complate();

            return Ok();
        }


        [HttpGet("/VehicleId")]
        public async Task<IActionResult> GetByVehicleId([FromQuery] long vehicleId)
        {
            var containers = await unitOfWork.Container.GetByVehicleId(vehicleId);
            unitOfWork.Complate();
            return Ok(containers);
        }

        [HttpGet("/GetGroupOfContainer")]
        public async Task<IActionResult> GetGroupOfContainer([FromQuery] long vehicleId, [FromQuery] int n)
        {
            var containerOfList = await unitOfWork.Container.GetByVehicleId(vehicleId);
            List<List<Container>> resultContainerList = new List<List<Container>>();
            int index = 0;
            for (int i = 0; i< n; i++)
            {
                
                List<Container> contains = new List<Container>();
                for(int j = 0; j< containerOfList.Count()/n; j++)
                {
                    contains.Add(containerOfList.ElementAt<Container>(index));
                    index++;
                }
                resultContainerList.Add(contains);
               
            }
            if (containerOfList.Count() % n != 0)
            {
                List<Container> contains = new List<Container>();
                for (int i = 0; i < containerOfList.Count() % n; i++)
                {
                    contains.Add(containerOfList.ElementAt<Container>(index));
                }
                resultContainerList.Add(contains);
            }

            unitOfWork.Complate();
            return Ok(resultContainerList);
        }
    }
}
