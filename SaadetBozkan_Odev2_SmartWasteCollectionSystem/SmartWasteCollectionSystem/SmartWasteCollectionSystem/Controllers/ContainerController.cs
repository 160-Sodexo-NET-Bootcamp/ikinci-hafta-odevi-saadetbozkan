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
        public IActionResult GetAll()
        {
            var listOfContainer = unitOfWork.Container.GetAll();
            unitOfWork.Complate();
            return Ok(listOfContainer);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(long id)
        {
            var container = unitOfWork.Container.GetById(id);
            if (container is null)
            {
                return NotFound();
            }
            unitOfWork.Complate();
            return Ok(container);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Container entity)
        {
            var response = unitOfWork.Container.Add(entity);

            unitOfWork.Complate();

            return CreatedAtAction("Post", response);
        }
        // Update withoud vehicleId
        [HttpPut("{id}")]
        public IActionResult Put([FromBody] Container entity, long id)
        {
            entity.Id = id;

            var response = unitOfWork.Container.Update(entity);

            unitOfWork.Complate();

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var response = unitOfWork.Container.Delete(id);

            unitOfWork.Complate();

            return Ok();
        }


        [HttpGet("/VehicleId")]
        public IActionResult GetByVehicleId([FromQuery] long vehicleId)
        {
            var containers = unitOfWork.Container.GetByVehicleId(vehicleId);
            unitOfWork.Complate();
            return Ok(containers);
        }

        [HttpGet("/GetGroupOfContainer")]
        public IActionResult GetGroupOfContainer([FromQuery] long vehicleId, [FromQuery] int n)
        {
            var containerOfList = unitOfWork.Container.GetByVehicleId(vehicleId);
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
                for (int i = 0; i < containerOfList.Count() % n; i++)
                {
                    resultContainerList.ElementAt<List<Container>>(i).Add(containerOfList.ElementAt<Container>(index));
                }
            }

            unitOfWork.Complate();
            return Ok(resultContainerList);
        }
    }
}
