using Data_Library.IRepository;
using Data_Library.Repository;
using Microsoft.AspNetCore.Mvc;
using Model_Library;

namespace WebApplication_ECommerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }
        #region APIs
        [HttpGet]
        public IActionResult GetAll()
        {
            var categoryList = _unitOfWork.Category.GetAll();
            return Json(new { data = categoryList });
        }


        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var cat = _unitOfWork.Category.get(id);

            if (cat == null) return Json(new
            {
                success = false,
                message = "Something went !!!"
            });
            _unitOfWork.Category.Remove(cat);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Data is Delete suceess!!!" });
        }
        #endregion
        public IActionResult upsert(int? id)
        {
            Category category = new Category();
            if (id == null) return View(category);
            category = _unitOfWork.Category.get(id.GetValueOrDefault());
            if (category == null) return View(category);
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult upsert(Category category)
        {
            if (category == null) return NotFound();
            if (!ModelState.IsValid) return View(category);
            if (category.Id == 0)
                _unitOfWork.Category.Add(category);
            else
                _unitOfWork.Category.Update(category);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }


    }
}
