namespace Data.DTOs;

public record TopNSoldProductModel(string Name, int TotalUnitSold);
public record TopNSoldProductsVm(DateTime StartDate, DateTime EndDate, IEnumerable<TopNSoldProductModel> TopNSoldProducts);