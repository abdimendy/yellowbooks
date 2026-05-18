using YellowBook.API.Data;
using YellowBook.API.Interfaces;
using YellowBook.API.Models;
using Microsoft.EntityFrameworkCore;

namespace YellowBook.API.Repositories;

public class CompanyRepository : ICompanyRepository
{
    private readonly ApplicationDbContext _context;

    public CompanyRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Company>> GetAllAsync()
    {
        return await _context.Companies
            .Include(c => c.Category)
            .OrderBy(c => c.CompanyName)
            .ToListAsync();
    }

    public async Task<Company?> GetByIdAsync(int id)
    {
        return await _context.Companies
            .Include(c => c.Category)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<IEnumerable<Company>> GetByCategoryAsync(int categoryId)
    {
        return await _context.Companies
            .Include(c => c.Category)
            .Where(c => c.CategoryId == categoryId)
            .OrderBy(c => c.CompanyName)
            .ToListAsync();
    }

    public async Task<IEnumerable<Company>> SearchAsync(string searchTerm)
    {
        return await _context.Companies
            .Include(c => c.Category)
            .Where(c => c.CompanyName.Contains(searchTerm) ||
                       c.PhoneNumber.Contains(searchTerm) ||
                       c.Email.Contains(searchTerm) ||
                       c.Address.Contains(searchTerm))
            .OrderBy(c => c.CompanyName)
            .ToListAsync();
    }

    public async Task<Company> CreateAsync(Company company)
    {
        _context.Companies.Add(company);
        await _context.SaveChangesAsync();
        return company;
    }

    public async Task<Company?> UpdateAsync(int id, Company company)
    {
        var existingCompany = await _context.Companies.FindAsync(id);
        if (existingCompany == null)
            return null;

        existingCompany.CompanyName = company.CompanyName;
        existingCompany.PhoneNumber = company.PhoneNumber;
        existingCompany.Email = company.Email;
        existingCompany.Address = company.Address;
        existingCompany.Website = company.Website;
        existingCompany.Description = company.Description;
        existingCompany.Logo = company.Logo;
        existingCompany.CategoryId = company.CategoryId;

        await _context.SaveChangesAsync();
        return existingCompany;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var company = await _context.Companies.FindAsync(id);
        if (company == null)
            return false;

        _context.Companies.Remove(company);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Companies.AnyAsync(c => c.Id == id);
    }

    public async Task<(IEnumerable<Company>, int)> GetPagedAsync(int page, int pageSize, string? searchTerm = null, int? categoryId = null)
    {
        var query = _context.Companies
            .Include(c => c.Category)
            .AsQueryable();

        if (!string.IsNullOrEmpty(searchTerm))
        {
            query = query.Where(c => c.CompanyName.Contains(searchTerm) ||
                                    c.PhoneNumber.Contains(searchTerm) ||
                                    c.Email.Contains(searchTerm) ||
                                    c.Address.Contains(searchTerm));
        }

        if (categoryId.HasValue)
        {
            query = query.Where(c => c.CategoryId == categoryId.Value);
        }

        var totalCount = await query.CountAsync();
        var companies = await query
            .OrderBy(c => c.CompanyName)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (companies, totalCount);
    }
}