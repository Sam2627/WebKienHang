using Microsoft.AspNetCore.Mvc;
using WebVanChuyen.Models.Models;

namespace WebVanChuyen.Api.Repositorys
{
    public interface IPackageRespository
    {
        // Package
        Task<ActionResult<Package>> CreatePackage(Package package);
        Task<ActionResult<Package>> GetPackage(int packageId);
        Task<ActionResult<IEnumerable<Package>>> GetPackages();
        Task<ActionResult<IQueryable<Package>>> GetPackages(string warehouseId, bool exceptComplete);
        Task<ActionResult<IQueryable<Package>>> GetPackagesCanTransport(string warehouseId);
        Task<ActionResult<IQueryable<Package>>> GetPackagesCanDelivery(string warehouseId);
        Task<ActionResult<IQueryable<Package>>> GetPackagesByShipper(int employeeId);
        Task<ActionResult<IQueryable<Package>>> GetPackagesInShipmentTrip(int shipmentTripId);
        Task<ActionResult<List<Package>>> GetPackagesByNextWarehouse(string start, string end);
        Task<ActionResult<Package>> UpdatePackage(Package updatePackage);
        Task<ActionResult> PackagShipperRegister(PackageRegister registerPackage);
        Task<ActionResult<Package>> PackageConfirmDelivery(PackageDelivery confirmPackage);
        Task<ActionResult<Package>> DeletePackage(int packageId);


        // PackageDetails
        Task<ActionResult<PackageDetails>> AddPackageDetails(PackageDetails packageDetails);
        Task<ActionResult<IQueryable<PackageDetails>>> GetPackageDetailsByPackage(int packageId);
        Task<ActionResult<PackageDetails>> UpdatePackageDetails(PackageDetails newPackageDetails);
        Task<ActionResult<PackageDetails>> DeletePackageDetails(int packageDetailsId);


        // PackageLog
        Task<ActionResult<PackageLog>> AddPackageLog(PackageLog packageLog, bool isCreate);
        Task<ActionResult<IEnumerable<PackageLog>>> GetAllPackageLog();
        Task<ActionResult<IQueryable<PackageLog>>> GetPackageLogsByPackage(int packageId);
        Task<ActionResult<IQueryable<PackageLog>>> GetPackageLogsByPhone(string phoneNum);
        Task<ActionResult<IQueryable<PackageLog>>> GetPackageLogsByShipmentTrip(int shipmentTripId);
        Task<ActionResult<PackageLog>> DeletePackageLog(int packageLogId);


        // PackageWeight
        Task<ActionResult<PackageWeight>> CreatePackageWeight(PackageWeight packageWeight);
        Task<ActionResult<IEnumerable<PackageWeight>>> GetAllPackageWeights();
        Task<ActionResult<PackageWeight>> GetPackageWeightById(double packageWeightId);
        Task<ActionResult<PackageWeight>> UpdatePackageWeight(PackageWeight updatepackageWeight);
    }
}
