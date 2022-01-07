using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.DataModel
{
    public class Vehicle
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string VehicleName { get; set; }
        public string VehiclePlate { get; set; }
        public IEnumerable<Container> Container { get; set; }
    }
}
