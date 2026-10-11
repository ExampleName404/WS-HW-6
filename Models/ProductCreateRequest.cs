using System.ComponentModel.DataAnnotations;

namespace ProductStoreApi.Models;

public class ProductCreateRequest
{
    [Required, StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [Range(typeof(decimal), "0", "100000000")]
    public decimal Price { get; set; }

    [Required, StringLength(50)]
    public string Category { get; set; } = string.Empty;
}
