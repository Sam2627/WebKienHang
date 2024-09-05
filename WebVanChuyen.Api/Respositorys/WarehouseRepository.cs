using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebVanChuyen.Api.Data;
using WebVanChuyen.Models.Models;

namespace WebVanChuyen.Api.Repositorys
{
    public class WarehouseRepository : IWarehouseRepository
    {
        private readonly DataContextWeb appDbContext;

        public WarehouseRepository(DataContextWeb appDbContext) { this.appDbContext = appDbContext; }


        // Warehouse Table
        public async Task<ActionResult<Warehouse>> CreateWarehouse(Warehouse warehouse)
        {
            if (warehouse == null) return new BadRequestResult();

            var result = await appDbContext.Warehouse.AddAsync(warehouse);
            await appDbContext.SaveChangesAsync();

            return new CreatedResult(string.Empty, result.Entity);
        }

        public async Task<ActionResult<Warehouse>> GetWarehouse(string warehouseId)
        {
            var result = await appDbContext.Warehouse.AsQueryable().FirstOrDefaultAsync(w => w.WarehouseId == warehouseId);
            if (result != null) return new OkObjectResult(result);

            return new NotFoundObjectResult($"Warehouse ID: {warehouseId} not exist!");
        }

        public async Task<ActionResult<IEnumerable<Warehouse>>> GetWarehouses()
        {
            var result = await appDbContext.Warehouse.AsQueryable().ToListAsync();
            
            return new OkObjectResult(result);
        }

        public async Task<ActionResult<IQueryable<Warehouse>>> GetWarehouses(string provinceId)
        {
            var district = await appDbContext.Province.AsQueryable().FirstOrDefaultAsync(d => d.ProvinceId == provinceId);
            if( district == null ) { return new BadRequestResult(); }

            var result = await appDbContext.Warehouse.AsQueryable().Where(w => w.ProvinceId == provinceId).ToListAsync();
            
            return new OkObjectResult(result);
        }

        public async Task<ActionResult<IQueryable<Warehouse>>> GetAdjacentWarehouses(string warehouseId)
        {
            var findwarehouse = await appDbContext.Warehouse.AsQueryable().FirstOrDefaultAsync(w => w.WarehouseId == warehouseId);
            if (findwarehouse == null) { return new NotFoundObjectResult($"Warehouse ID: {warehouseId} not exist!"); }

            List<Warehouse> result = new List<Warehouse>();

            // Get list of path contain warehouseId
            var pathList = await appDbContext.WHPath.AsQueryable()
                .Where(p => p.StartPoint == warehouseId || p.EndPoint == warehouseId).ToListAsync();
            foreach(var path in pathList)
            {
                // Get warehouse
                Warehouse warehouse = new Warehouse();
                if(path.StartPoint == warehouseId)          // Get by EndPoint
                {
                    warehouse = await appDbContext.Warehouse.AsQueryable().FirstOrDefaultAsync(w => w.WarehouseId == path.EndPoint);
                }
                else if(path.EndPoint == warehouseId)       // Get by StartPoint
                {
                    warehouse = await appDbContext.Warehouse.AsQueryable().FirstOrDefaultAsync(w => w.WarehouseId == path.StartPoint);
                }

                if (warehouse != null) result.Add(warehouse);
            }

            result = result.Distinct().ToList();
            //warehouseList = warehouseList.DistinctBy(w => w.WarehouseId).ToList();
            
            return new OkObjectResult(result);
        }

        public async Task<ActionResult<Warehouse>> UpdateWarehouse(Warehouse updateWarehouse)
        {
            var result = await appDbContext.Warehouse.AsQueryable().FirstOrDefaultAsync(w => w.WarehouseId == updateWarehouse.WarehouseId);
            if (result != null)
            {
                result.WarehouseName = updateWarehouse.WarehouseName;
                result.WarehouseDesc = updateWarehouse.WarehouseDesc;
                result.WarehouseAddr = updateWarehouse.WarehouseAddr;
                result.GPSCoordinates = updateWarehouse.GPSCoordinates;

                await appDbContext.SaveChangesAsync();

                return new OkObjectResult(result);
            }

            return new BadRequestObjectResult($"Not found warehouse ID: {updateWarehouse.WarehouseId}");
        }

        public async Task<ActionResult<Warehouse>> DeleteWarehouse(string warehouseId)
        {
            var result = await appDbContext.Warehouse.FirstOrDefaultAsync(w => w.WarehouseId == warehouseId);
            if (result != null)
            {
                appDbContext.Warehouse.Remove(result);
                await appDbContext.SaveChangesAsync();

                return new OkObjectResult(result);
            }

            return new NotFoundObjectResult($"Not found warehouse ID: {warehouseId}");
        }


        // Province
        public async Task<ActionResult<Province>> CreateProvince(Province province)
        {
            if (province == null) return new BadRequestResult();

            var result = await appDbContext.Province.AddAsync(province);
            await appDbContext.SaveChangesAsync();

            return new CreatedResult(string.Empty, result.Entity);
        }

        public async Task<ActionResult<Province>> GetProvince(string provinceId)
        {
            var result = await appDbContext.Province.AsQueryable().FirstOrDefaultAsync(p => p.ProvinceId == provinceId);
            if (result != null) return new OkObjectResult(result);

            return new NotFoundObjectResult($"Province ID: {provinceId} not exist!");
        }

        public async Task<ActionResult<IEnumerable<Province>>> GetProvinces()
        {
            var result = await appDbContext.Province.ToListAsync();
            
            return new OkObjectResult(result);
        }

        public async Task<ActionResult<IQueryable<Province>>> GetProvinces(string warehouseId)
        {
            var warehouse = await appDbContext.Warehouse.AsQueryable().FirstOrDefaultAsync(wh => wh.WarehouseId == warehouseId);
            if (warehouse == null) { return new BadRequestResult(); }

            // Get path connect to warehouse
            var paths = await appDbContext.WHPath.AsQueryable()
                .Where(p => p.StartPoint == warehouseId || p.EndPoint == warehouseId).ToListAsync();
            if (paths == null) { return new NotFoundResult(); }

            // Get list of provinces
            List<Province> provinceList = new List<Province>();
            foreach (var path in paths)
            {
                // Get other warehouse
                // Find with start
                var addwarehouse = await appDbContext.Warehouse.AsQueryable()
                    .FirstOrDefaultAsync(wh => wh.WarehouseId == path.StartPoint && wh.WarehouseId != warehouseId);
                // Find with end
                if (addwarehouse == null)
                {
                    addwarehouse = await appDbContext.Warehouse.AsQueryable()
                        .FirstOrDefaultAsync(wh => wh.WarehouseId == path.EndPoint && wh.WarehouseId != warehouseId);
                }

                // Get and add province to list
                var province = await appDbContext.Province.AsQueryable()
                    .FirstOrDefaultAsync(d => d.ProvinceId == addwarehouse.ProvinceId);
                provinceList.Add(province);
            }

            return new OkObjectResult(provinceList);
        }


        // District
        public async Task<ActionResult<District>> CreateDistrict(District district)
        {
            if (district == null) return new BadRequestResult();

            var result = await appDbContext.District.AddAsync(district);
            await appDbContext.SaveChangesAsync();

            return new CreatedResult(string.Empty, result.Entity);
        }

        public async Task<ActionResult<District>> GetDistrict(int districtId)
        {
            var result = await appDbContext.District.AsQueryable().FirstOrDefaultAsync(d => d.DistrictId == districtId);
            if (result != null) return new OkObjectResult(result);

            return new NotFoundObjectResult($"District ID: {districtId} not exist!");
        }

        public async Task<ActionResult<IEnumerable<District>>> GetDistricts()
        {
            var result = await appDbContext.District.ToListAsync();

            return new OkObjectResult(result);
        }

        public async Task<ActionResult<IQueryable<District>>> GetDistrictsByWarehouse(string warehouseId)
        {
            var warehouse = await appDbContext.Warehouse.AsQueryable().FirstOrDefaultAsync(wh => wh.WarehouseId == warehouseId);
            if (warehouse == null) { return new BadRequestResult(); }

            // Get path connect to warehouse
            var paths = await appDbContext.WHPath.AsQueryable()
                .Where(p => p.StartPoint == warehouseId || p.EndPoint == warehouseId).ToListAsync();
            if (paths == null) { return new NotFoundResult(); }

            // Get list of provinces
            List<Province> provinceList = new();
            foreach (var path in paths)
            {
                // Get other warehouse
                // Find with start
                var addwarehouse = await appDbContext.Warehouse.AsQueryable()
                    .FirstOrDefaultAsync(wh => wh.WarehouseId == path.StartPoint && wh.WarehouseId != warehouseId);
                // Find with end
                if (addwarehouse == null)
                {
                    addwarehouse = await appDbContext.Warehouse.AsQueryable()
                        .FirstOrDefaultAsync(wh => wh.WarehouseId == path.EndPoint && wh.WarehouseId != warehouseId);
                }

                // Get and add province to list
                var province = await appDbContext.Province.AsQueryable()
                    .FirstOrDefaultAsync(d => d.ProvinceId == addwarehouse.ProvinceId);
                provinceList.Add(province);
            }

            // Get list of districts
            List<District> districtList = new();
            foreach(var province in provinceList)
            {
                var districts = await appDbContext.District.AsQueryable()
                    .Where(d => d.ProvinceId == province.ProvinceId).ToListAsync();

                foreach(var district in districts) districtList.Add(district);
            }

            return new OkObjectResult(districtList);
        }

        public async Task<ActionResult<IQueryable<District>>> GetDistrictsByProvince(string provinceId)
        {
            var province = await appDbContext.Province.AsQueryable().FirstOrDefaultAsync(p => p.ProvinceId == provinceId);
            if (province == null) { return new BadRequestResult(); }

            var result = await appDbContext.District.AsQueryable().Where(d => d.ProvinceId == provinceId).ToListAsync();

            return new OkObjectResult(result);
        }


        // Commune
        public async Task<ActionResult<IQueryable<Commune>>> GetCommunesByWarehouse(string warehouseId)
        {
            var warehouse = await appDbContext.Warehouse.AsQueryable().FirstOrDefaultAsync(wh => wh.WarehouseId == warehouseId);
            if (warehouse == null) { return new BadRequestResult(); }

            // Get path connect to warehouse
            var paths = await appDbContext.WHPath.AsQueryable()
                .Where(p => p.StartPoint == warehouseId || p.EndPoint == warehouseId).ToListAsync();
            if (paths == null) { return new NotFoundResult(); }

            // Get list of provinces
            List<Province> provinceList = new();
            foreach (var path in paths)
            {
                // Get other warehouse
                // Find with start
                var addwarehouse = await appDbContext.Warehouse.AsQueryable()
                    .FirstOrDefaultAsync(wh => wh.WarehouseId == path.StartPoint && wh.WarehouseId != warehouseId);
                // Find with end
                if (addwarehouse == null)
                {
                    addwarehouse = await appDbContext.Warehouse.AsQueryable()
                        .FirstOrDefaultAsync(wh => wh.WarehouseId == path.EndPoint && wh.WarehouseId != warehouseId);
                }

                // Get and add province to list
                var province = await appDbContext.Province.AsQueryable()
                    .FirstOrDefaultAsync(d => d.ProvinceId == addwarehouse.ProvinceId);
                provinceList.Add(province);
            }

            // Get list of districts
            List<District> districtList = new();
            foreach (var province in provinceList)
            {
                var districts = await appDbContext.District.AsQueryable()
                    .Where(d => d.ProvinceId == province.ProvinceId).ToListAsync();

                foreach (var district in districts) districtList.Add(district);
            }

            // Get list of communes
            List<Commune> communeList = new();
            foreach (var district in districtList)
            {
                var commnunes = await appDbContext.Commune.AsQueryable()
                    .Where(c => c.DistrictId == district.DistrictId).ToListAsync();

                foreach (var commnune in commnunes) communeList.Add(commnune);
            }

            return new OkObjectResult(communeList);
        }

        public async Task<ActionResult<IQueryable<Commune>>> GetCommunesByDistrict(int districtId)
        {
            var district = await appDbContext.District.AsQueryable().FirstOrDefaultAsync(district => district.DistrictId == districtId);
            if (district == null) { return new BadRequestResult(); }

            var ressult = await appDbContext.Commune.AsQueryable().Where(commune => commune.DistrictId == districtId).ToListAsync();

            return new OkObjectResult(ressult);
        }
    }
}
