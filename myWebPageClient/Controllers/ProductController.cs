using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using myWebPageClient.Models;
using System;


namespace myWebPageClient.Controllers
{
    public class ProductController : Controller
    {
        // GET: ProductController
        public async Task<ActionResult> Index()
        {
            List<Product> products = new List<Product>();
            using (var client = new HttpClient())
            {
                using (var response = await client.GetAsync("https://localhost:7038/api/Products"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    products = JsonConvert.DeserializeObject<List<Product>>(apiResponse);
                }
            }
            return View(products);
        }

        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {

            return View();
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Product product, IFormFile Image)
        {
            try
            {
                if (Image != null && Image.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        await Image.CopyToAsync(ms);
                        product.ImageData = Convert.ToBase64String(ms.ToArray()); // Artık string türüyle uyumlu
                        product.ImageType = Image.ContentType; // MIME türünü ekle
                    }
                }
                else
                {
                    ModelState.AddModelError("Image", "Lütfen bir resim yükleyin.");
                    return View(product);
                }

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7038/api/");
                    var response = await client.PostAsJsonAsync("Products", product);

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index)); // Başarılı ise listeye dön
                    }
                    else
                    {
                        var errorMessage = await response.Content.ReadAsStringAsync();
                        ModelState.AddModelError(string.Empty, $"API Hatası: {response.StatusCode} - {errorMessage}");
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Beklenmedik bir hata oluştu: {ex.Message}");
            }

            return View(product); // Hata durumunda formu tekrar göster
        }






        // GET: ProductController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
