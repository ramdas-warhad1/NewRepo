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
                TempData["successMessage"] = "Product is added successfully";
                return RedirectToAction(nameof(Index));
            }
            catch (InvalidOperationException ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View(productToAdd);
            }
            catch (FileNotFoundException ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View(productToAdd);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "Error on saving data";
                return View(productToAdd);
            }
        }

        public async Task<IActionResult> UpdateProduct(int id)
        {
            var product = await _prodkRepo.GetProductById(id);
            if (product == null)
            {
                TempData["errorMessage"] = $"Product with the id: {id} does not found";
                return RedirectToAction(nameof(Index));
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

                TempData["successMessage"] = "Product is updated successfully";
                return RedirectToAction(nameof(Index));
            }
            catch (InvalidOperationException ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View(prodToUpdate);
            }
            catch (FileNotFoundException ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View(prodToUpdate);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "Error on saving data";
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
                    TempData["errorMessage"] = $"Product with the id: {id} does not found";
                }
                else
                {
                    await _prodkRepo.DeleteProduct(product);
                }
            }
            catch (InvalidOperationException ex)
            {
                TempData["errorMessage"] = ex.Message;
            }
            catch (FileNotFoundException ex)
            {
                TempData["errorMessage"] = ex.Message;
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "Error on deleting the data";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
