using Data_Library.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Model_Library.Models;
using Model_Library;

namespace WebApplication_ECommerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IUnitOfWork unitofwork, IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            _unitOfWork = unitofwork;
        }
        public IActionResult Index()
        {
            return View();
        }
        #region APIs
        public IActionResult Getall()
        {
            var mohi = _unitOfWork.Product.GetAll();
            return Json(new { data = mohi });

        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var cat = _unitOfWork.Product.get(id);
            if (cat == null) return Json(new
            {
                success = false,
                message = "Something went !!!"
            });
            var wabRootPath = _webHostEnvironment.WebRootPath;
            var imgePath = Path.Combine(wabRootPath, cat.ImageUrl.Trim('\\'));
            if (System.IO.File.Exists(imgePath))
            {
                System.IO.File.Exists(imgePath);
            }
            _unitOfWork.Product.Remove(cat);
            _unitOfWork.Save();
            return Json(new
            {
                success = true,
                Message = "Finally DATA delete"
            });
        }
        #endregion

        public IActionResult upsert(int? id)
        {
            ProductVM productVM = new ProductVM()
            {
                Product = new Product(),
                CategoryList = _unitOfWork.Category.GetAll().Select(cl => new SelectListItem()
                {
                    Text = cl.Name,
                    Value = cl.Id.ToString()
                }),
                covertypeList = _unitOfWork.CoverType.GetAll().Select(ct => new SelectListItem()
                {
                    Text = ct.Name,
                    Value = ct.Id.ToString()
                })

            };
            if (id == null) return View(productVM);
            productVM.Product = _unitOfWork.Product.get(id.GetValueOrDefault());

            return View(productVM);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]

        public IActionResult upsert(ProductVM productVM)
        {
            if (!ModelState.IsValid)
            {
                var mo = _webHostEnvironment.WebRootPath;
                var f = HttpContext.Request.Form.Files;
                if (f.Count() > 0)
                {
                    var filename = Guid.NewGuid().ToString();
                    var extension = Path.GetExtension(f[0].FileName);
                    var uploads = Path.Combine(mo, @"Image\Product");
                    if (productVM.Product.Id != 0)
                    {
                        var ImagerExists = _unitOfWork.Product.get(productVM.Product.Id).ImageUrl;
                        productVM.Product.ImageUrl = ImagerExists;
                    }
                    if (productVM.Product.ImageUrl != null)
                    {
                        var imagepath = Path.Combine(mo, productVM.Product.ImageUrl.Trim('\\'));
                        if (System.IO.File.Exists(imagepath))
                        {
                            System.IO.File.Delete(imagepath);
                        }
                    }
                    using (var filestream = new FileStream(Path.Combine(uploads, filename + extension), FileMode.Create))
                    {
                        f[0].CopyTo(filestream);
                    }
                    productVM.Product.ImageUrl = @"\Image\Product\" + filename + extension;
                }
                else
                {
                    if (productVM.Product.Id != 0)
                    {
                        var imageexists = _unitOfWork.Product.get(productVM.Product.Id).ImageUrl;
                        productVM.Product.ImageUrl = imageexists;
                    }
                }
                if (productVM.Product.Id == 0)
                    _unitOfWork.Product.Add(productVM.Product);
                else


                    _unitOfWork.Product.Update(productVM.Product);
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            else
            {


                productVM = new ProductVM()
                {
                    Product = new Product(),
                    CategoryList = _unitOfWork.Category.GetAll().Select(cl => new SelectListItem()
                    {
                        Text = cl.Name,
                        Value = cl.Id.ToString()
                    }),
                    covertypeList = _unitOfWork.CoverType.GetAll().Select(ct => new SelectListItem()
                    {
                        Text = ct.Name,
                        Value = ct.Id.ToString()
                    })

                };
                if (productVM.Product.Id != 0)
                    productVM.Product = _unitOfWork.Product.get(productVM.Product.Id);

                return View(productVM);
            }
        }

    }





}