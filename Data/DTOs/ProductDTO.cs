using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Data.DTOs;
public class ProductDTO
{
    public int Id { get; set; }

    [Required]
    [MaxLength(40)]
    public string? Name { get; set; }

    
    [Required]
    public double Price { get; set; }
}
