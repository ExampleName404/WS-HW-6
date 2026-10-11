using ProductStoreApi.Models;

namespace ProductStoreApi.Services;

public class ProductRepository
{
    private readonly object _lock = new();
    private int _nextId = 5;

    private readonly List<Product> _products = new()
    {
        new Product { Id = 1, Name = "Ноутбук Lenovo IdeaPad", Price = 320000, Category = "Компьютеры" },
        new Product { Id = 2, Name = "Смартфон Samsung A55", Price = 180000, Category = "Телефоны" },
        new Product { Id = 3, Name = "Наушники JBL Tune 520", Price = 25000, Category = "Аудио" },
        new Product { Id = 4, Name = "Мышь Logitech M185", Price = 7000, Category = "Аксессуары" }
    };

    public List<Product> GetAll()
    {
        lock (_lock)
        {
            return _products.ToList();
        }
    }

    public Product? GetById(int id)
    {
        lock (_lock)
        {
            return _products.FirstOrDefault(p => p.Id == id);
        }
    }

    public Product Add(ProductCreateRequest request)
    {
        lock (_lock)
        {
            var product = new Product
            {
                Id = _nextId++,
                Name = request.Name.Trim(),
                Price = request.Price,
                Category = request.Category.Trim()
            };
            _products.Add(product);
            return product;
        }
    }

    public bool Delete(int id)
    {
        lock (_lock)
        {
            return _products.RemoveAll(p => p.Id == id) > 0;
        }
    }
}
