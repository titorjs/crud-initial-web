using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace FirstCrud.Data
{
    public class ProductContextFactory : IDesignTimeDbContextFactory<ProductContext>
    {
        public ProductContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ProductContext>();

            // Obtener la configuración desde appsettings.json
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            // Obtener la cadena de conexión desde la configuración
            var connectionString = configuration.GetConnectionString("ProductContext");

            // Configurar el DbContext para usar PostgreSQL
            optionsBuilder.UseNpgsql(connectionString);

            return new ProductContext(optionsBuilder.Options);
        }
    }
}
