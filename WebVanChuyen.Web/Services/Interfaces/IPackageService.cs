using WebVanChuyen.Models.Models;

namespace WebVanChuyen.Web.Services
{
    public interface IPackageService
    {
        // Package
        Task<Package?> CreatePackage(Package package);
        Task<Package> GetPackage(int packageId);
        Task<List<Package>> GetPackages();
        Task<List<Package>> GetPackages(string warehouseId, bool exceptComplete);
        Task<List<Package>> GetPackagesCanTransport(string warehouseId);
        Task<List<Package>> GetPackagesCanDelivery(string warehouseId);
        Task<List<Package>> GetPackagesByShipper(int employeeId);
        Task<List<Package>> GetPackagesInShipmentTrip(int shipmentTripId);
        Task<List<Package>> GetPackageByNextWarehouse(string start, string end);
        Task UpdatePackageStatus(Package updatePackage);
        Task PackagShipperRegister(PackageRegister registerPackage);
        Task PackageConfirmDelivery(PackageDelivery confirmPackage);
        Task DeletePackage(int packageId);

        // PackageLog
        Task<PackageLog> AddPackageLog(PackageLog packageLog, bool isCreate);
        Task<List<PackageLog>> GetPackageLogsByPackage(int packageId);
        Task<List<PackageLog>> GetPackageLogsByPhone(string phoneNum);
        Task<List<PackageLog>> GetPackageLogsByShipmentTrip(int shipmentTripId);
        Task DeletePackageLog(int packageLogId);


        // PackageWeight
        Task CreatePackageWeight(PackageWeight package);
        Task<PackageWeight> GetPackageWeight(double packageWeightId);
        Task<List<PackageWeight>> GetPackageWeights();
        Task UpdatePackageWeight(PackageWeight updatepackageWeight);
    }
}
