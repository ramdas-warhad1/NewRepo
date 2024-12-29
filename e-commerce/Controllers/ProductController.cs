using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using ProductShoppingCartMvcUI.Repositories;
using Data.DTOs;
using Data;
using Microsoft.AspNetCore.Authorization;

namespace e_commerce.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private readonly IProductRepository _prodkRepo;


        public ProductController(IProductRepository prodRepo)
        {
            _prodkRepo = prodRepo;

        }

        public async Task<IActionResult> Index()
        {
            var products = await _prodkRepo.GetProducts();
            return View(products);
        }

        public async Task<IActionResult> AddProduct()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductDTO productToAdd)
        {

            if (!ModelState.IsValid)
                return View(productToAdd);

            try
            {

                // manual mapping of ProductDTO -> Product
                Product product = new()
                {
                    Id = productToAdd.Id,
                    Name = productToAdd.Name,
                    Price = productToAdd.Price
                };
                await _prodkRepo.AddProduct(product);
                return RedirectToAction(nameof(Index));
            }
            catch (InvalidOperationException ex)
            {
                
                return View(productToAdd);
            }
            catch (FileNotFoundException ex)
            {
               
                return View(productToAdd);
            }
            catch (Exception ex)
            {
                
                return View(productToAdd);
            }
        }

        public async Task<IActionResult> UpdateProduct(int id)
        {
            var product = await _prodkRepo.GetProductById(id);
            if (product == null)
            {
               
                return RedirectToAction(nameof(Index), product);
            }

            ProductDTO ToUpdate = new()
            {
                Name = product.Name,
                Price = product.Price,
            };
            return View(ToUpdate);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProduct(ProductDTO prodToUpdate)
        {


            if (!ModelState.IsValid)
                return View(prodToUpdate);

            try
            {

                Product product = new()
                {
                    Id = prodToUpdate.Id,
                    Name = prodToUpdate.Name,
                    Price = prodToUpdate.Price,
                };
                await _prodkRepo.UpdateProduct(product);

               
                return RedirectToAction(nameof(Index));
            }
            catch (InvalidOperationException ex)
            {
               
                return View(prodToUpdate);
            }
            catch (FileNotFoundException ex)
            {
              
                return View(prodToUpdate);
            }
            catch (Exception ex)
            {
               
                return View(prodToUpdate);
            }
        }

        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                var product = await _prodkRepo.GetProductById(id);
                if (product == null)
                {
                    return RedirectToAction(nameof(Index), product);
                }
                else
                {
                    await _prodkRepo.DeleteProduct(product);
                }
            }
            catch (InvalidOperationException ex)
            {
               
            }
            catch (FileNotFoundException ex)
            {
               
            }
            catch (Exception ex)
            {
               
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
