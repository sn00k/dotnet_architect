using CompanyStores.DAL.Interfaces.Repositories;
using CompanyStores.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyStores.BLL.Interfaces;
using CompanyStores.DAL.Interfaces.Infrastructure;
using CompanyStores.BLL.Services.Exceptions;

namespace CompanyStores.BLL.Services
{
	// a huge improvement would be to make use of AutoMapper
	// to take care of the mapping of object properties
	// instead of having to do it manually.
    public class CompanyService : ICompanyService
    {
		private readonly ICompanyRepository _companyRepository;
		private readonly IUnitOfWork _unitOfWork;

		public CompanyService(ICompanyRepository companyRepository, IUnitOfWork unitOfWork)
		{
			if (companyRepository == null) { throw new ArgumentNullException(nameof(companyRepository)); }
			if (unitOfWork == null) { throw new ArgumentNullException(nameof(unitOfWork)); }

			_companyRepository = companyRepository;

			_unitOfWork = unitOfWork;
		}

		public async Task<IList<Company>> GetAllAsync()
		{
			return (await _companyRepository.GetAllAsync()).Select(x => new Company
			{
				Id = x.Id,
				Name = x.Name,
				Notes = x.Notes,
				OrganizationNumber = x.OrganizationNumber
			}).ToList();
		}

		public async Task<Company> GetByIdAsync(Guid id)
		{
			var model = await _companyRepository.GetByIdAsync(id);

			var company = new Company()
			{
				Id = model.Id,
				Name = model.Name,
				Notes = model.Notes,
				OrganizationNumber = model.OrganizationNumber
			};

			return company;
		}

		public async Task<IList<Company>> GetByNameAsync(string name)
		{
			return (await _companyRepository.GetByNameAsync(name)).Select(x => new Company
			{
				Id = x.Id,
				Name = x.Name,
				Notes = x.Notes,
				OrganizationNumber = x.OrganizationNumber
			}).ToList();
		}

		public async Task SaveEditedCompanyAsync(Company company)
		{
			if (company == null)
			{
				throw new ArgumentNullException(nameof(company) + " is null");
			}

			var model = await _companyRepository.GetByIdAsync(company.Id);

			if (model == null)
			{
				throw new CompanyNotFoundException(nameof(model) + " is null");
			}

			model.Name = company.Name;
			model.Notes = company.Notes;
			model.OrganizationNumber = company.OrganizationNumber;

			_companyRepository.Update(model);

			await _unitOfWork.CommitAsync();
		}

		public async Task CreateCompanyAsync(Company company)
		{
			if (company == null)
			{
				throw new ArgumentNullException(nameof(company) + " is null");
			}
			
			var newCompany = new DAL.Models.Company
			{
				Name = company.Name,
				Notes = company.Notes,
				OrganizationNumber = company.OrganizationNumber
			};

			_companyRepository.Add(newCompany);

			await _unitOfWork.CommitAsync();
		}

		public async Task DeleteCompanyAsync(Company company)
		{
			var model = await _companyRepository.GetByIdAsync(company.Id);

			_companyRepository.Remove(model);

			await _unitOfWork.CommitAsync();
		}
	}
}
