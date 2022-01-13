using Data.DataModel;
using Data.Uow;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SmartWasteCollectionSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        private readonly ILogger<VehicleController> logger;

        public VehicleController(ILogger<VehicleController> logger, IUnitOfWork unitOfWork)
        {
            this.logger = logger;
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var listOfVehicle = unitOfWork.Vehicle.GetAll();
            unitOfWork.Complate();
            return Ok(listOfVehicle);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(long id)
        {
            var vehicle = unitOfWork.Vehicle.GetById(id);
            if (vehicle is null)
            {
                return NotFound();
            }
            unitOfWork.Complate();
            return Ok(vehicle);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Vehicle entity)
        {
            var response = unitOfWork.Vehicle.Add(entity);

            unitOfWork.Complate();

            return CreatedAtAction("Post", response);
        }

        [HttpPut("{id}")]
        public IActionResult Put([FromBody] Vehicle entity, long id)
        {
            entity.Id = id;
            var response = unitOfWork.Vehicle.Update(entity);

            unitOfWork.Complate();

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var response = unitOfWork.Vehicle.Delete(id);

            unitOfWork.Complate();

            return Ok();
        }
    }
}
