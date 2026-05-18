
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YellowBook.API.DTOs;
using YellowBook.API.Interfaces;
using YellowBook.API.Models;

namespace YellowBook.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CompaniesController : ControllerBase
{
    private readonly ICompanyRepository _companyRepository;

    public CompaniesController(ICompanyRepository companyRepository)
    {
        _companyRepository = companyRepository;
    }

    // GET: api/companies
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var companies = await _companyRepository.GetAllAsync();
        var companyDtos = companies.Select(c => new CompanyDto
        {
            Id = c.Id,
            CompanyName = c.CompanyName,
            PhoneNumber = c.PhoneNumber,
            Email = c.Email,
            Address = c.Address,
            Website = c.Website,
            Description = c.Description,
            Logo = c.Logo,
            CategoryId = c.CategoryId,
            CategoryName = c.Category.Name,
            CreatedAt = c.CreatedAt
        });

        return Ok(companyDtos);
    }

    // GET: api/companies/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var company = await _companyRepository.GetByIdAsync(id);
        if (company == null)
            return NotFound();

        var companyDto = new CompanyDto
        {
            Id = company.Id,
            CompanyName = company.CompanyName,
            PhoneNumber = company.PhoneNumber,
            Email = company.Email,
            Address = company.Address,
            Website = company.Website,
            Description = company.Description,
            Logo = company.Logo,
            CategoryId = company.CategoryId,
            CategoryName = company.Category.Name,
            CreatedAt = company.CreatedAt
        };

        return Ok(companyDto);
    }

    // GET: api/companies/search?term={searchTerm}
    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string term)
    {
        if (string.IsNullOrEmpty(term))
            return BadRequest("Search term is required");

        var companies = await _companyRepository.SearchAsync(term);
        var companyDtos = companies.Select(c => new CompanyDto
        {
            Id = c.Id,
            CompanyName = c.CompanyName,
            PhoneNumber = c.PhoneNumber,
            Email = c.Email,
            Address = c.Address,
            Website = c.Website,
            Description = c.Description,
            Logo = c.Logo,
            CategoryId = c.CategoryId,
            CategoryName = c.Category.Name,
            CreatedAt = c.CreatedAt
        });

        return Ok(companyDtos);
    }

    // GET: api/companies/paged?page=1&pageSize=10&searchTerm=&categoryId=
    [HttpGet("paged")]
    public async Task<IActionResult> GetPaged([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] string? searchTerm = null, [FromQuery] int? categoryId = null)
    {
        if (page < 1) page = 1;
        if (pageSize < 1 || pageSize > 100) pageSize = 10;

        var (companies, totalCount) = await _companyRepository.GetPagedAsync(page, pageSize, searchTerm, categoryId);
        var companyDtos = companies.Select(c => new CompanyDto
        {
            Id = c.Id,
            CompanyName = c.CompanyName,
            PhoneNumber = c.PhoneNumber,
            Email = c.Email,
            Address = c.Address,
            Website = c.Website,
            Description = c.Description,
            Logo = c.Logo,
            CategoryId = c.CategoryId,
            CategoryName = c.Category.Name,
            CreatedAt = c.CreatedAt
        });

        var result = new
        {
            Data = companyDtos,
            TotalCount = totalCount,
            Page = page,
            PageSize = pageSize,
            TotalPages = (int)Math.Ceiling((double)totalCount / pageSize)
        };

        return Ok(result);
    }

    // POST: api/companies
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] CreateCompanyDto createDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var company = new Company
        {
            CompanyName = createDto.CompanyName,
            PhoneNumber = createDto.PhoneNumber,
            Email = createDto.Email,
            Address = createDto.Address,
            Website = createDto.Website,
            Description = createDto.Description,
            Logo = createDto.Logo,
            CategoryId = createDto.CategoryId
        };

        var createdCompany = await _companyRepository.CreateAsync(company);
        var companyDto = new CompanyDto
        {
            Id = createdCompany.Id,
            CompanyName = createdCompany.CompanyName,
            PhoneNumber = createdCompany.PhoneNumber,
            Email = createdCompany.Email,
            Address = createdCompany.Address,
            Website = createdCompany.Website,
            Description = createdCompany.Description,
            Logo = createdCompany.Logo,
            CategoryId = createdCompany.CategoryId,
            CategoryName = createdCompany.Category.Name,
            CreatedAt = createdCompany.CreatedAt
        };

        return CreatedAtAction(nameof(GetById), new { id = companyDto.Id }, companyDto);
    }

    // PUT: api/companies/{id}
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateCompanyDto updateDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var company = new Company
        {
            CompanyName = updateDto.CompanyName,
            PhoneNumber = updateDto.PhoneNumber,
            Email = updateDto.Email,
            Address = updateDto.Address,
            Website = updateDto.Website,
            Description = updateDto.Description,
            Logo = updateDto.Logo,
            CategoryId = updateDto.CategoryId
        };

        var updatedCompany = await _companyRepository.UpdateAsync(id, company);
        if (updatedCompany == null)
            return NotFound();

        var companyDto = new CompanyDto
        {
            Id = updatedCompany.Id,
            CompanyName = updatedCompany.CompanyName,
            PhoneNumber = updatedCompany.PhoneNumber,
            Email = updatedCompany.Email,
            Address = updatedCompany.Address,
            Website = updatedCompany.Website,
            Description = updatedCompany.Description,
            Logo = updatedCompany.Logo,
            CategoryId = updatedCompany.CategoryId,
            CategoryName = updatedCompany.Category.Name,
            CreatedAt = updatedCompany.CreatedAt
        };

        return Ok(companyDto);
    }

    // DELETE: api/companies/{id}
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _companyRepository.DeleteAsync(id);
        if (!deleted)
            return NotFound();

        return NoContent();
    }
}
