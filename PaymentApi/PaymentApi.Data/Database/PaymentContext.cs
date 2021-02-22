using System;
using Microsoft.EntityFrameworkCore;
using PaymentApi.Domain;

namespace PaymentApi.Data.Database
{
    public class PaymentContext : DbContext
    {
        public PaymentContext()
        {
        }

        public PaymentContext(DbContextOptions<PaymentContext> options)
            : base(options)
        {
            var payments = new[]
            {
                new Payment
                {
                    Id = Guid.Parse("9f35b48d-cb87-4783-bfdb-21e36012930a"),
                    PaymentState = 1,
                    PaymentGuid = Guid.Parse("d3e3137e-ccc9-488c-9e89-50ba354738c2"),
                    CardNumber = "1111 1111 1111 1111",
                    ExpiryMonth = 12,
                    ExpiryYear = 2021,
                    Amount = 12.34,
                    Currency = "EUR",
                    CVV = "111"
                },
                new Payment
                {
                    Id = Guid.Parse("bffcf83a-0224-4a7c-a278-5aae00a02c1e"),
                    PaymentState = 1,
                    PaymentGuid = Guid.Parse("4a2f1e35-f527-4136-8b12-138a57e1ba08"),
                    CardNumber = "1111 1111 2222 2222",
                    ExpiryMonth = 11,
                    ExpiryYear = 2021,
                    Amount = 123.40,
                    Currency = "USD",
                    CVV = "222"
                },
                new Payment
                {
                    Id = Guid.Parse("58e5cd7d-856b-4224-bdff-bd8f85bf5a6d"),
                    PaymentState = 2,
                    PaymentGuid = Guid.Parse("334feb16-d7bb-4ca9-ab56-f4fadeb88d21"),
                    CardNumber = "1111 1111 3333 3333",
                    ExpiryMonth = 10,
                    ExpiryYear = 2021,
                    Amount = 35.00,
                    Currency = "EUR",
                    CVV = "333"
                }
            };

            Payment.AddRange(payments);
            SaveChanges();
        }

        public virtual DbSet<Payment> Payment { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CardNumber).IsRequired().HasMaxLength(19); ;
                entity.Property(e => e.ExpiryMonth).IsRequired();
                entity.Property(e => e.ExpiryYear).IsRequired();
                entity.Property(e => e.Amount).IsRequired();
                entity.Property(e => e.Currency).IsRequired().HasMaxLength(3);
                entity.Property(e => e.CVV).IsRequired().HasMaxLength(3);
            });
        }
    }
}
