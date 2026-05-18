using YellowBook.API.Models;

namespace YellowBook.API.Interfaces;

public interface ICompanyRepository
{
    Task<IEnumerable<Company>> GetAllAsync();
    Task<Company?> GetByIdAsync(int id);
    Task<IEnumerable<Company>> GetByCategoryAsync(int categoryId);
    Task<IEnumerable<Company>> SearchAsync(string searchTerm);
    Task<Company> CreateAsync(Company company);
    Task<Company?> UpdateAsync(int id, Company company);
    Task<bool> DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
    Task<(IEnumerable<Company>, int)> GetPagedAsync(int page, int pageSize, string? searchTerm = null, int? categoryId = null);
}