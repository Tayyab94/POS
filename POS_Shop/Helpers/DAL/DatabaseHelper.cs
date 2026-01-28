
using POS_Shop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
namespace POS_Shop.Helpers.DAL
{

    public class DatabaseHelper : IDisposable
    {
        private readonly POSDbContext _context;

        public DatabaseHelper()
        {
            _context = new POSDbContext();
        }

        // Get all active product units
        public List<ProductUnit> GetAllProductUnits()
        {
            return _context.ProductUnits
                .Where(pu => pu.IsActive)
                .OrderBy(pu => pu.Id)
                .ToList();
        }

        // Get product by ID
        public Product GetProductById(int productId)
        {
            return _context.Products
                .Include(p => p.SubCategory)
                .FirstOrDefault(p => p.Id == productId);
        }

        // Get all products
        public List<Product> GetAllProducts()
        {
            return _context.Products
                .Where(p => true) // No IsActive field in your model
                .Include(p => p.SubCategory)
                .OrderBy(p => p.ProductEnglishName)
                .ToList();
        }

        // Get product prices by product ID
        public List<ProductPrice> GetProductPrices(int productId)
        {
            return _context.ProductPrices
                .Where(pp => pp.ProductId == productId)
                .Include(pp => pp.ProductUnitType)
                .OrderByDescending(pp => pp.ProductUnitType.Id) // Adjust ordering as needed
                .ToList();
        }

        // Save or update product prices
        public bool SaveProductPrices(int productId, List<ProductPrice> prices)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    // Delete existing prices for this product
                    var existingPrices = _context.ProductPrices
                        .Where(pp => pp.ProductId == productId)
                        .ToList();

                    _context.ProductPrices.RemoveRange(existingPrices);

                    _context.SaveChanges();

                    // Insert new prices
                    foreach (var price in prices.Where(p => p.Price > 0))
                    {

                        price.ProductId = productId;
                        price.CreatedDate = DateTime.Now;
                        _context.ProductPrices.Add(price);
                    }

                    _context.SaveChanges();
                    transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Error saving product prices: " + ex.Message, ex);
                }
            }
        }

        // Save or update product
        public int SaveProduct(Product product)
        {
            if (product.Id == 0)
            {
                // New product
                _context.Products.Add(product);
            }
            else
            {
                // Update existing product
                _context.Entry(product).State = EntityState.Modified;
            }

            _context.SaveChanges();
            return product.Id;
        }

        public Dictionary<int, List<ProductPrice>> GetAllProductsWithPrices()
        {
            var allPrices = new Dictionary<int, List<ProductPrice>>();

            var productsWithPrices = _context.Products
                .Include(p => p.ProductPrices)
                .Include(p => p.ProductPrices.Select(pp => pp.ProductUnitType))
                .ToList();

            foreach (var product in productsWithPrices)
            {
                allPrices[product.Id] = product.ProductPrices?.ToList() ?? new List<ProductPrice>();
            }

            return allPrices;
        }

        // Get product by code
        public Product GetProductByCode(string code)
        {
            return _context.Products
                .FirstOrDefault(p => p.SearchByProductCode == code);
        }

        // Add product and get ID
        public int AddProductAndGetId(Product product)
        {
            // Check if product with same code exists
            var existingProduct = _context.Products
                .FirstOrDefault(p => p.SearchByProductCode == product.SearchByProductCode);

            if (existingProduct != null)
            {
                // Update existing product
                existingProduct.ProductEnglishName = product.ProductEnglishName;
                existingProduct.ProductUrduName = product.ProductUrduName;
                existingProduct.PurchasePrice = product.PurchasePrice;
                existingProduct.SalePrice = product.SalePrice;
                existingProduct.Cost = product.Cost;
                existingProduct.Qty = product.Qty;
                existingProduct.SubcategoryId = product.SubcategoryId;

                _context.SaveChanges();
                return existingProduct.Id;
            }
            else
            {
                // Add new product
                _context.Products.Add(product);
                _context.SaveChanges();
                return product.Id;
            }
        }

        public bool CheckProductCodeExists(string productCode)
        {
            return _context.Products
                .Any(p => p.SearchByProductCode == productCode);
        }

        // Batch operations
        public void SaveProductsBatch(List<Product> products)
        {
            foreach (var product in products)
            {
                var existing = _context.Products
                    .FirstOrDefault(p => p.SearchByProductCode == product.SearchByProductCode);

                if (existing != null)
                {
                    // Update
                    existing.ProductEnglishName = product.ProductEnglishName;
                    existing.ProductUrduName = product.ProductUrduName;
                    existing.PurchasePrice = product.PurchasePrice;
                    existing.SalePrice = product.SalePrice;
                    existing.Cost = product.Cost;
                    existing.Qty = product.Qty;
                }
                else
                {
                    // Add
                    _context.Products.Add(product);
                }
            }

            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
