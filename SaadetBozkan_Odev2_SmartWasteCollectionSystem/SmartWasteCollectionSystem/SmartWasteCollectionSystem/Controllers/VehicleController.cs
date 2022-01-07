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
        public async Task<IActionResult> GetAll()
        {
            var listOfVehicle = await unitOfWork.Vehicle.GetAll();
            unitOfWork.Complate();
            return Ok(listOfVehicle);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var vehicle = await unitOfWork.Vehicle.GetById(id);
            if (vehicle is null)
            {
                return NotFound();
            }
            unitOfWork.Complate();
            return Ok(vehicle);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Vehicle entity)
        {
            var response = await unitOfWork.Vehicle.Add(entity);

            unitOfWork.Complate();

            return CreatedAtAction("Post", response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] Vehicle entity, long id)
        {
            entity.Id = id;
            var response = await unitOfWork.Vehicle.Update(entity);

            unitOfWork.Complate();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var response = await unitOfWork.Vehicle.Delete(id);

            unitOfWork.Complate();

            return Ok();
        }
    }
}
