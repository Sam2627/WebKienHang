using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebVanChuyen.Api.Data;
using WebVanChuyen.Models.Models;

namespace WebVanChuyen.Api.Repositorys
{
    public class PackageRespository : IPackageRespository
    {
        private readonly DataContextWeb appDbContext;

        public PackageRespository(DataContextWeb appDbContext) { this.appDbContext = appDbContext; }

        // Package
        public async Task<ActionResult<Package>> CreatePackage(Package package)
        {
            if (package == null) return new BadRequestResult();

            var result = await appDbContext.Package.AddAsync(package);
            await appDbContext.SaveChangesAsync();

            return new CreatedResult(string.Empty, result.Entity);
        }

        public async Task<ActionResult<Package>> GetPackage(int packageId)
        {
            var result = await appDbContext.Package.AsQueryable().FirstOrDefaultAsync(p => p.PackageId == packageId);
            if (result != null) return new OkObjectResult(result);

            //return new NotFoundObjectResult($"Not found package ID: {packageId}");
            return new NotFoundResult();
        }

        public async Task<ActionResult<IEnumerable<Package>>> GetPackages()
        {
            var result = await appDbContext.Package.AsQueryable().ToListAsync();
            
            return new OkObjectResult(result);
        }

        public async Task<ActionResult<IQueryable<Package>>> GetPackages(string warehouseId, bool exceptComplete)
        {
            List<Package> result;       
            if(exceptComplete) result = await appDbContext.Package.AsQueryable()
                    .Where(p => p.LocaId == warehouseId && 
                    (p.Status != (int)PackageStatus.Delivered || p.Status == (int)PackageStatus.Complete)).ToListAsync();
            else result = await appDbContext.Package.AsQueryable().Where(p => p.LocaId == warehouseId).ToListAsync();

            return new OkObjectResult(result);
        }

        public async Task<ActionResult<IQueryable<Package>>> GetPackagesCanTransport(string warehouseId)
        {
            var warehouse = await appDbContext.Warehouse.FirstOrDefaultAsync(wh => wh.WarehouseId == warehouseId);
            if (warehouse == null) return new BadRequestResult();

            var packages = await appDbContext.Package.AsQueryable()
                .Where(package => package.LocaId == warehouseId && package.Status == (int)PackageStatus.Stored).ToListAsync();

            return new OkObjectResult(packages);
        }

        public async Task<ActionResult<IQueryable<Package>>> GetPackagesCanDelivery(string warehouseId)
        {
            var warehouse = await appDbContext.Warehouse.FirstOrDefaultAsync(wh => wh.WarehouseId == warehouseId);
            if (warehouse == null) return new BadRequestResult();

            var packages = await appDbContext.Package.AsQueryable().Where(
                package => package.LocaId == warehouseId &&
                package.Status == (int)PackageStatus.WaitingDelivery &&
                package.ShipperId == null).ToListAsync();

            return new OkObjectResult(packages);
        }

        public async Task<ActionResult<IQueryable<Package>>> GetPackagesByShipper(int employeeId)
        {
            var employee = await appDbContext.Employee.FirstOrDefaultAsync(emp => emp.EmployeeId == employeeId);
            if (employee == null) return new BadRequestResult();

            var packages = await appDbContext.Package.AsQueryable().Where(
                package => package.ShipperId == employeeId &&
                package.Status == (int)PackageStatus.WaitingDelivery).ToListAsync();

            return new OkObjectResult(packages);
        }

        public async Task<ActionResult<IQueryable<Package>>> GetPackagesInShipmentTrip(int shipmentTripId)
        {
            var shipmentTrip = await appDbContext.ShipmentTrip.FirstOrDefaultAsync(s => s.ShipmentTripId == shipmentTripId);
            if(shipmentTrip == null) return new BadRequestResult();

            
            var packageLogs = await appDbContext.PackageLog.AsQueryable()
                .Where(pl => pl.ShipmentTripId == shipmentTripId).DistinctBy(pl => pl.ShipmentTripId).ToListAsync();

            List<Package> packages = new();
            foreach(var log in packageLogs)
            {
                var package = await appDbContext.Package.AsQueryable()
                    .FirstOrDefaultAsync(p => p.PackageId == log.PackageId);
                packages.Add(package);
            }
            
            return new OkObjectResult(packages);
        }

        public async Task<ActionResult<List<Package>>> GetPackagesByNextWarehouse(string start, string end)
        {
            var startWarehouse = await appDbContext.Warehouse.AsQueryable().FirstOrDefaultAsync(s => s.WarehouseId == start);
            var endWarehouse = await appDbContext.Warehouse.AsQueryable().FirstOrDefaultAsync(s => s.WarehouseId == end);
            if (startWarehouse == null || endWarehouse == null) return new BadRequestResult();

            // Get all packages in start warehouse
            var packages = await appDbContext.Package.AsQueryable().Where(
                p => p.Status == (int)PackageStatus.Stored && p.LocaId == start).ToListAsync();
            if (packages == null) return new NotFoundResult();

            List<Package> result = new List<Package>();

            // Add package
            foreach (var item in packages)
            {
                // Check if package is in destination warehouse
                var route = await appDbContext.WHRoute.FirstOrDefaultAsync(r => r.RouteId == item.RouteId);
                if (route.EndPoint == end) continue;

                // Create array of node for check start and end index
                string[] array = route.StopPointsList.Split(',');

                // Get index value of start and end warehouses
                int start_index = Array.IndexOf(array, start);
                int end_index = Array.IndexOf(array, end);

                // Check if start and end next to each other
                if (end_index - start_index == 1) result.Add(item);
            }

            return new OkObjectResult(result);
        }

        public async Task<ActionResult<Package>> UpdatePackage(Package updatePackage)
        {
            var result = await appDbContext.Package.FirstOrDefaultAsync(s => s.PackageId == updatePackage.PackageId);
            if (result == null) return new BadRequestObjectResult($"Not found shipment with ID: {updatePackage.PackageId}");

            result.PackageDesc = updatePackage.PackageDesc;
            result.ReceptionistId = updatePackage.ReceptionistId;
            result.ShipperId = updatePackage.ShipperId;
            result.Status = updatePackage.Status;
            result.LocaId = updatePackage.LocaId;
            result.Weight = updatePackage.Weight;
            result.RouteId = updatePackage.RouteId;
            result.SenderName = updatePackage.SenderName;
            result.SenderPhone = updatePackage.SenderPhone;
            result.SenderAddr = updatePackage.SenderAddr;
            result.ReceiverName = updatePackage.ReceiverName;
            result.ReceiverPhone = updatePackage.ReceiverPhone;
            result.ReceiverAddr = updatePackage.ReceiverAddr;
            result.Collect = updatePackage.Collect;
            result.ShipCost = updatePackage.ShipCost;
            result.PayStatus = updatePackage.PayStatus;

            await appDbContext.SaveChangesAsync();

            return new OkObjectResult(result);
        }

        public async Task<ActionResult> PackagShipperRegister(PackageRegister registerPackage)
        {
            var package = await appDbContext.Package.FirstOrDefaultAsync(p => p.PackageId == registerPackage.PackageId);
            if (package == null) return new BadRequestResult();

            // Add shipper to package
            package.ShipperId = registerPackage.ShipperId;

            // Create package log
            PackageLog newPackLog = new()
            {
                PackageId = package.PackageId,
                LogNote = "Kien hang dang duoc giao, vui long chu y dien thoai"
            };

            await appDbContext.PackageLog.AddAsync(newPackLog);
            await appDbContext.SaveChangesAsync();

            return new OkResult();
        }

        public async Task<ActionResult<Package>> PackageConfirmDelivery(PackageDelivery confirmPackage)
        {
            var package = await appDbContext.Package.FirstOrDefaultAsync(p => p.PackageId == confirmPackage.PackageId);
            if (package == null) return new BadRequestResult();

            package.Status = (int)PackageStatus.Delivered;
            package.ReceiverId = confirmPackage.ReceiverId;

            // Create package log
            PackageLog newPackLog = new()
            {
                PackageId = package.PackageId,
                LogNote = "Kien hang da duoc giao"
            };

            await appDbContext.PackageLog.AddAsync(newPackLog);
            await appDbContext.SaveChangesAsync();

            return new OkObjectResult(package);
        }

        public async Task<ActionResult<Package>> DeletePackage(int packageId)
        {
            var result = await appDbContext.Package.FirstOrDefaultAsync(p => p.PackageId == packageId);
            if (result != null)
            {
                var packageLogs = await appDbContext.PackageLog.AsQueryable().Where(pl => pl.PackageId == packageId).ToListAsync();
                foreach (var item in packageLogs)
                {
                    appDbContext.PackageLog.Remove(item);
                    await appDbContext.SaveChangesAsync();
                }

                appDbContext.Package.Remove(result);
                await appDbContext.SaveChangesAsync();

                return new OkObjectResult(result);
            }

            //return new NotFoundObjectResult($"Not found package ID: {packageId}");
            return new BadRequestResult();
        }


        // PackageDetails
        public async Task<ActionResult<PackageDetails>> AddPackageDetails(PackageDetails packageDetails)
        {
            if (packageDetails == null) return new BadRequestResult();

            var result = await appDbContext.PackageDetail.AddAsync(packageDetails);
            await appDbContext.SaveChangesAsync();

            return new CreatedResult(string.Empty, result.Entity);
        }

        public async Task<ActionResult<IQueryable<PackageDetails>>> GetPackageDetailsByPackage(int packageId)
        {
            var package = appDbContext.Package.FirstOrDefaultAsync(p => p.PackageId == packageId);
            if (package == null) { return new BadRequestObjectResult($"Not found package ID: {packageId}"); }

            var result = appDbContext.PackageDetail.AsQueryable().Where(pd => pd.PackageId == packageId);
            
            return new OkObjectResult(result);
        }

        public async Task<ActionResult<PackageDetails>> UpdatePackageDetails(PackageDetails newPackageDetails)
        {
            var result = await appDbContext.PackageDetail.FirstOrDefaultAsync(pd => pd.PackageDetsId == newPackageDetails.PackageDetsId);
            if (result != null)
            {
                result.ProductId = newPackageDetails.ProductId;
                result.Quantity = newPackageDetails.Quantity;

                await appDbContext.SaveChangesAsync();

                return new OkObjectResult(result);
            }

            //return new BadRequestObjectResult($"Not found package details ID: {newPackageDetails.PackageDetsId}");
            return new BadRequestResult();
        }

        public async Task<ActionResult<PackageDetails>> DeletePackageDetails(int packageDetailsId)
        {
            var result = await appDbContext.PackageDetail.FirstOrDefaultAsync(pd => pd.PackageDetsId == packageDetailsId);
            if (result != null)
            {
                appDbContext.PackageDetail.Remove(result);
                await appDbContext.SaveChangesAsync();

                return new OkObjectResult(result);
            }

            //return new NotFoundObjectResult($"Not found package details ID: {packageDetailsId}");
            return new BadRequestResult();
        }


        // PackageLog
        public async Task<ActionResult<PackageLog>> AddPackageLog(PackageLog packageLog, bool isCreate)
        {
            // Get Package and change status
            var package = await appDbContext.Package.FirstOrDefaultAsync(p => p.PackageId == packageLog.PackageId);
            if (package == null) return new BadRequestResult();

            if (!isCreate) package.Status = (int)PackageStatus.Tranporting;
            var result = await appDbContext.PackageLog.AddAsync(packageLog);
            await appDbContext.SaveChangesAsync();

            return new CreatedResult(string.Empty, result.Entity);
        }

        public async Task<ActionResult<IEnumerable<PackageLog>>> GetAllPackageLog()
        {
            var result = await appDbContext.PackageLog.AsQueryable().ToListAsync();

            return new OkObjectResult(result);
        }

        public async Task<ActionResult<IQueryable<PackageLog>>> GetPackageLogsByPackage(int packageId)
        {
            var package = await appDbContext.Package.AsQueryable().FirstOrDefaultAsync(e => e.PackageId == packageId);
            if (package == null) { return new NotFoundObjectResult($"Not found package with Id: {packageId}"); }

            var result = appDbContext.PackageLog.AsQueryable().Where(pl => pl.PackageId == packageId);
            
            return new OkObjectResult(result);
        }

        public async Task<ActionResult<IQueryable<PackageLog>>> GetPackageLogsByPhone(string phoneNum)
        {
            var packages = await appDbContext.Package.AsQueryable().Where(e => e.ReceiverPhone == phoneNum).ToListAsync();
            if (packages == null) { return new NotFoundResult(); }

            List<PackageLog> result = new ();
            foreach (var package in packages)
            {
                var packagelog = appDbContext.PackageLog.AsQueryable().Where(pl => pl.PackageId == package.PackageId);
                result.AddRange(packagelog);
            }

            return new OkObjectResult(result);
        }

        public async Task<ActionResult<IQueryable<PackageLog>>> GetPackageLogsByShipmentTrip(int shipmentTripId)
        {
            var shipmentTrip = appDbContext.ShipmentTrip.AsQueryable().Where(s => s.ShipmentTripId == shipmentTripId);
            if (shipmentTrip == null) { return new BadRequestResult(); }

            var result = appDbContext.PackageLog.AsQueryable().Where(pl => pl.ShipmentTripId == shipmentTripId);
            
            return new OkObjectResult(result);
        }

        public async Task<ActionResult<PackageLog>> DeletePackageLog(int packageLogId)
        {
            var result = await appDbContext.PackageLog.FirstOrDefaultAsync(pl => pl.PackageLogId == packageLogId);
            if (result != null)
            {
                // Update package status
                var package = await appDbContext.Package.FirstOrDefaultAsync(pack => pack.PackageId == result.PackageId);
                package.Status = (int)PackageStatus.Stored;
                //await appDbContext.SaveChangesAsync();

                // Delete package log
                appDbContext.PackageLog.Remove(result);
                await appDbContext.SaveChangesAsync();

                return new OkObjectResult(result);
            }

            return new BadRequestResult();
        }


        // PackageWeight
        public async Task<ActionResult<PackageWeight>> CreatePackageWeight(PackageWeight packageWeight)
        {
            if (packageWeight == null) return new BadRequestResult();

            var result = await appDbContext.PackageWeight.AddAsync(packageWeight);
            await appDbContext.SaveChangesAsync();

            return new CreatedResult(string.Empty, result.Entity);
        }

        public async Task<ActionResult<IEnumerable<PackageWeight>>> GetAllPackageWeights()
        {
            var result = await appDbContext.PackageWeight.ToListAsync();
            
            return new OkObjectResult(result);
        }

        public async Task<ActionResult<PackageWeight>> GetPackageWeightById(double packageWeightId)
        {
            var result = await appDbContext.PackageWeight.AsQueryable().FirstOrDefaultAsync(p => p.Weight == packageWeightId);
            if (result != null) return new OkObjectResult(result);

            return new NotFoundResult();
        }

        public async Task<ActionResult<PackageWeight>> UpdatePackageWeight(PackageWeight updatepackageWeight)
        {
            var result = await appDbContext.PackageWeight.FirstOrDefaultAsync(s => s.Weight == updatepackageWeight.Weight);
            if (result == null) return new NotFoundObjectResult($"Not found shipment with ID: {updatepackageWeight.Weight}");

            result.WCost = updatepackageWeight.WCost;
            await appDbContext.SaveChangesAsync();

            return new OkObjectResult(result);
        }
    }
}
