using Microsoft.EntityFrameworkCore;

namespace PetStore.API.Db
{
    public partial class PetStoreDBContext : DbContext
    {
        public PetStoreDBContext()
        {
        }

        public PetStoreDBContext(DbContextOptions<PetStoreDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Comment> Comment { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<OrderItem> OrderItem { get; set; }
        public virtual DbSet<Toy> Toy { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category", "petstore");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.ToTable("Comment", "petstore");

                entity.HasIndex(e => e.ToyId)
                    .HasDatabaseName("fki_Comment_Toy");

                entity.Property(e => e.Author)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.DatePosted)
                    .HasColumnType("date")
                    .HasDefaultValueSql("CURRENT_DATE");

                entity.Property(e => e.Text).IsRequired();

                entity.HasOne(d => d.Toy)
                    .WithMany(p => p.Comment)
                    .HasForeignKey(d => d.ToyId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("Comment_Toy");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order", "petstore");

                entity.Property(e => e.CustomerName)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.CustomerSurname)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.ExternalReferenceId).HasMaxLength(255);

                entity.Property(e => e.IpinfoAddress)
                    .IsRequired()
                    .HasColumnName("IPInfoAddress")
                    .HasMaxLength(255);

                entity.Property(e => e.OrderDate)
                    .HasColumnType("date")
                    .HasDefaultValueSql("CURRENT_DATE");

                entity.Property(e => e.OrderStatus)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.ShippingAddress)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.ToTable("OrderItem", "petstore");

                entity.HasIndex(e => e.OrderId)
                    .HasDatabaseName("fki_OrderItem_Order");

                entity.HasIndex(e => e.ToyId)
                    .HasDatabaseName("fki_OrderItem_Toy");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderItem)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("OrderItem_Order");

                entity.HasOne(d => d.Toy)
                    .WithMany(p => p.OrderItem)
                    .HasForeignKey(d => d.ToyId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("OrderItem_Toy");
            });

            modelBuilder.Entity<Toy>(entity =>
            {
                entity.ToTable("Toy", "petstore");

                entity.HasIndex(e => e.CategoryId)
                    .HasDatabaseName("fki_OrderItem_Category");

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.Name).HasMaxLength(30);

                entity.Property(e => e.Price).HasColumnType("money");

                entity.Property(e => e.ShortDescription)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Toy)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("Toy_Category");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
