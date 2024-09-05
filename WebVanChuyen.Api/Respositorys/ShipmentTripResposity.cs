    using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebVanChuyen.Api.Data;
using WebVanChuyen.Models.Models;

namespace WebVanChuyen.Api.Repositorys
{
    public class ShipmentResposity : IShipmentTripResposity
    {
        private readonly DataContextWeb appDbContext;

        public ShipmentResposity(DataContextWeb appDbContext) { this.appDbContext = appDbContext; }

        // ShipmentTrip
        public async Task<bool> CheckShipmentTrip(int shipmentTripId)
        {
            var result = await appDbContext.ShipmentTrip.AsQueryable().FirstOrDefaultAsync(s => s.ShipmentTripId == shipmentTripId);
            if (result != null) return true;
            else return false;
        }

        public async Task<ActionResult<ShipmentTrip>> CreateShipmentTrip(ShipmentTrip shipmentTrip)
        {
            if (shipmentTrip == null) return new BadRequestResult();

            // Drive 1
            var driver = await appDbContext.Employee.AsQueryable().FirstOrDefaultAsync(e => e.EmployeeId == shipmentTrip.Driver1);
            if (driver == null) return new BadRequestObjectResult("Driver not found");
            else
            {
                driver.LocaId = null;
                await appDbContext.SaveChangesAsync();
            }

            // Driver 2
            var coDriver = await appDbContext.Employee.AsQueryable().FirstOrDefaultAsync(e => e.EmployeeId == shipmentTrip.Driver2);
            if (coDriver != null)
            {
                coDriver.LocaId = null;
                await appDbContext.SaveChangesAsync();
            }
            else { shipmentTrip.Driver2 = null;}

            // Truck
            var truck = await appDbContext.Truck.AsQueryable().FirstOrDefaultAsync(truck => truck.TruckId == shipmentTrip.TruckId);
            truck.LocaId = null;
            await appDbContext.SaveChangesAsync();

            // Shipment
            var result = await appDbContext.ShipmentTrip.AddAsync(shipmentTrip);
            await appDbContext.SaveChangesAsync();

            return new CreatedResult(string.Empty, result.Entity);
        }

        public async Task<ActionResult<IEnumerable<ShipmentTrip>>> GetShipmentTrips()
        {
            var result = await appDbContext.ShipmentTrip.AsQueryable().ToListAsync();
            
            return new OkObjectResult(result);
        }

        public async Task<ActionResult<IQueryable<ShipmentTrip>>> GetShipmentTrips(string warehouseId)
        {
            var result = await appDbContext.ShipmentTrip.AsQueryable().Where(
                    s => s.StartWH == warehouseId || s.EndWH == warehouseId
                    && s.Status != (int)ShipmentTripStatus.Complete).ToListAsync();

            return new OkObjectResult(result);
        }

        public async Task<ActionResult<IQueryable<ShipmentTrip>>> GetShipmentTrips(string warehouseId, bool isStart)
        {
            List<ShipmentTrip> result = new();
            if (isStart)
            {
                result = await appDbContext.ShipmentTrip.AsQueryable()
                    .Where(s => s.StartWH == warehouseId && s.Status != (int)ShipmentTripStatus.Complete).ToListAsync();
            }
            else
            {
                result = await appDbContext.ShipmentTrip.AsQueryable()
                    .Where(s => s.EndWH == warehouseId && s.Status != (int)ShipmentTripStatus.Complete).ToListAsync();
            }

            return new OkObjectResult(result);
        }

        public async Task<ActionResult<IQueryable<ShipmentTrip>>> GetShipmentTripsByDriver(int employeeId)
        {
            var result = await appDbContext.ShipmentTrip.AsQueryable().Where(
                s => s.Driver1 == employeeId || s.Driver2 == employeeId && s.Status != (int)ShipmentTripStatus.Complete).ToListAsync();

            return new OkObjectResult(result);
        }

        public async Task<ActionResult<ShipmentTrip>> GetShipmentTrip(int shipmentTripId)
        {
            var result = await appDbContext.ShipmentTrip.AsQueryable().FirstOrDefaultAsync(s => s.ShipmentTripId == shipmentTripId);
            if (result != null) return new OkObjectResult(result);

            return new NotFoundResult();
        }

        public async Task<ActionResult<ShipmentTrip>> UpdateShipmentTrip(ShipmentTrip updateShipmentTrip)
        {
            var shipment = await appDbContext.ShipmentTrip.AsQueryable().FirstOrDefaultAsync(s => s.ShipmentTripId == updateShipmentTrip.ShipmentTripId);
            if (shipment == null) return new NotFoundObjectResult($"Not found shipment ID: {updateShipmentTrip.ShipmentTripId}");

            shipment.Driver1 = updateShipmentTrip.Driver1;
            shipment.Driver2 = updateShipmentTrip.Driver2;
            shipment.BeginT = updateShipmentTrip.BeginT;
            shipment.ArrivedT = updateShipmentTrip.ArrivedT;
            shipment.StartWH = updateShipmentTrip.StartWH;
            shipment.EndWH = updateShipmentTrip.EndWH;
            shipment.Status = updateShipmentTrip.Status;

            await appDbContext.SaveChangesAsync();

            return new OkObjectResult(shipment);
        }

        public async Task<ActionResult<ShipmentTrip>> UpdateShipmentTripStatus(UpdateShipmentTripStatus updateShipmentTrip)
        {
            var shipmentTrip = await appDbContext.ShipmentTrip.AsQueryable().FirstOrDefaultAsync(s => s.ShipmentTripId == updateShipmentTrip.ShipmentTripId);
            if (shipmentTrip == null) return new BadRequestResult();

            // Update shipment status, set truck, drivers, packages location to null
            if (updateShipmentTrip.Status == (int)ShipmentTripStatus.Transporting)
            {
                // Update shipment trip status
                shipmentTrip.Status = updateShipmentTrip.Status;
                await appDbContext.SaveChangesAsync();

                // Update location of truck
                var truck = await appDbContext.Truck.AsQueryable()
                    .FirstOrDefaultAsync(truck => truck.TruckId == shipmentTrip.TruckId);
                truck.LocaId = null;
                await appDbContext.SaveChangesAsync();

                // Update drivers location
                var driver1 = await appDbContext.Employee.AsQueryable()
                    .FirstOrDefaultAsync(emp => emp.EmployeeId == shipmentTrip.Driver1);
                driver1.LocaId = null;
                await appDbContext.SaveChangesAsync();

                if (shipmentTrip.Driver2 != null)
                {
                    var driver2 = await appDbContext.Employee.AsQueryable()
                        .FirstOrDefaultAsync(emp => emp.EmployeeId == shipmentTrip.Driver2);
                    driver2.LocaId = null;
                    await appDbContext.SaveChangesAsync();
                }

                // Update new location for packages
                var packageLogs = await appDbContext.PackageLog.AsQueryable()
                    .Where(packageLog => packageLog.ShipmentTripId == shipmentTrip.ShipmentTripId).ToListAsync();
                if (packageLogs.Any())
                {
                    foreach (var packageLog in packageLogs)
                    {
                        // Get package and set local to null
                        var package = await appDbContext.Package.AsQueryable()
                            .FirstOrDefaultAsync(package => package.PackageId == packageLog.PackageId);
                        package.LocaId = null;
                        await appDbContext.SaveChangesAsync();
                    }
                }
            }
            // Update shipment status and arrived time
            else if (updateShipmentTrip.Status == (int)ShipmentTripStatus.Arrived)
            {
                // Update shipment trip
                shipmentTrip.Status = updateShipmentTrip.Status;
                shipmentTrip.ArrivedT = DateTime.Now;
                await appDbContext.SaveChangesAsync();
            }
            // Update shipment status, set truck, drivers, packages, package logs location to destination
            else if (updateShipmentTrip.Status == (int)ShipmentTripStatus.Complete)
            {
                // Update shipment trip
                shipmentTrip.Status = updateShipmentTrip.Status;

                await appDbContext.SaveChangesAsync();

                // Update new location of truck
                var truck = await appDbContext.Truck.AsQueryable()
                    .FirstOrDefaultAsync(truck => truck.TruckId == shipmentTrip.TruckId);
                truck.LocaId = shipmentTrip.EndWH;

                await appDbContext.SaveChangesAsync();

                // Update drivers location
                var driver1 = await appDbContext.Employee.AsQueryable()
                    .FirstOrDefaultAsync(emp => emp.EmployeeId == shipmentTrip.Driver1);
                driver1.LocaId = shipmentTrip.EndWH;

                await appDbContext.SaveChangesAsync();

                if (shipmentTrip.Driver2 != null)
                {
                    var driver2 = await appDbContext.Employee.AsQueryable()
                        .FirstOrDefaultAsync(emp => emp.EmployeeId == shipmentTrip.Driver2);
                    driver2.LocaId = shipmentTrip.EndWH;

                    await appDbContext.SaveChangesAsync();
                }

                // Update new location for packages and add package logs
                var packageLogs = await appDbContext.PackageLog.AsQueryable()
                    .Where(packageLog => packageLog.ShipmentTripId == shipmentTrip.ShipmentTripId).ToListAsync();
                if (packageLogs.Any())
                {
                    foreach (var packageLog in packageLogs)
                    {
                        // Get package
                        var package = await appDbContext.Package.AsQueryable()
                            .FirstOrDefaultAsync(package => package.PackageId == packageLog.PackageId);
                        // Get route of package
                        var route = await appDbContext.WHRoute.AsQueryable()
                            .FirstOrDefaultAsync(route => route.RouteId == package.RouteId);
                        // Get warehouse name
                        var warehouseName = (await appDbContext.Warehouse
                            .FirstOrDefaultAsync(wh => wh.WarehouseId == shipmentTrip.EndWH)).WarehouseName;

                        // Add arrived log to package
                        PackageLog arrivedLog = new PackageLog()
                        {
                            ShipmentTripId = shipmentTrip.ShipmentTripId,
                            TruckId = shipmentTrip.TruckId,
                            PackageId = package.PackageId,
                            LogNote = "Da den kho " + warehouseName
                        };

                        await appDbContext.PackageLog.AddAsync(arrivedLog);
                        await appDbContext.SaveChangesAsync();

                        // Set status for arrived package
                        var destWarehouse = shipmentTrip.EndWH;
                        if (destWarehouse == route.StartPoint || destWarehouse == route.EndPoint)
                        {
                            // Update package
                            package.LocaId = shipmentTrip.EndWH;
                            package.Status = (int)PackageStatus.WaitingDelivery;

                            await appDbContext.SaveChangesAsync();
                        }
                        else
                        {
                            // Update package
                            package.LocaId = shipmentTrip.EndWH;
                            package.Status = (int)PackageStatus.Stored;

                            await appDbContext.SaveChangesAsync();
                        }
                    }
                }
            }
            
            return new OkObjectResult(shipmentTrip);
        }


        // Truck
        public async Task<ActionResult<IEnumerable<Truck>>> GetTrucks()
        {
            var result = await appDbContext.Truck.AsQueryable().ToListAsync();

            return new OkObjectResult(result);
        }

        public async Task<ActionResult<IQueryable<Truck>>> GetTrucks(string warehouseId)
        {
            var warehouse = await appDbContext.Warehouse.FirstOrDefaultAsync(wh => wh.WarehouseId == warehouseId);
            if (warehouse == null) { return new BadRequestResult(); }

            var result = await appDbContext.Truck.AsQueryable().Where(t => t.LocaId == warehouseId).ToListAsync();
            if (result.Count > 0) { return new OkObjectResult(result); }

            return new NotFoundResult();
        }
    }
}
