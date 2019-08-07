using CompanyStores.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyStores.DAL.EntityFramework6.Infrastructure
{
	public class CompanyStoresContext : DbContext
	{
		public CompanyStoresContext(string connectionString) : base(connectionString)
		{
			Database.SetInitializer<CompanyStoresContext>(null);

			// Buildserver cannot find EF Sql Provider
			// Link to fix: http://stackoverflow.com/questions/22478327/buildserver-can-not-find-entity-framework-sql-provider
			var ensureDLLIsCopied = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
		}

		public virtual DbSet<Company> Companies { get; set; }
		public virtual DbSet<Store> Stores { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Company>()
				.Property(x => x.Id)
				.HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

			modelBuilder.Entity<Store>()
				.Property(x => x.Id)
				.HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

			base.OnModelCreating(modelBuilder);
		}
	}
}
