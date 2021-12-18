using Bank.Transfers.Models;
using Microsoft.EntityFrameworkCore;

namespace Bank.Transfers.Data
{
    public class ContaContext : DbContext
    {
        public ContaContext(DbContextOptions<ContaContext> options)
           : base(options) { }

        public DbSet<Conta> Conta { get; set; }
    }
}