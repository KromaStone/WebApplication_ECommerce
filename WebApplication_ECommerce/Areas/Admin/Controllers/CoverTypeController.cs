using Dapper;
using Data_Library.IRepository;
using Data_Library.Repository;
using Microsoft.AspNetCore.Mvc;
using Model_Library;
using Utility_Library;

namespace WebApplication_ECommerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CoverTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CoverTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult upsert(int? id)
        {
            CoverType covertype = new CoverType();
            if (id == null) return View(covertype);
            covertype = _unitOfWork.CoverType.get(
                id.GetValueOrDefault());
            //var param.add("@id",id getvalu)
            if (covertype == null) return NotFound();
            return View(covertype);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult upsert(CoverType covertype)
        {
            if (covertype == null) return NotFound();
            if (!ModelState.IsValid) return View(covertype);
            var ms = new DynamicParameters();
            ms.Add("Name", covertype.Name);
            if (covertype.Id == 0)
            {
                _unitOfWork.SpCall.excecute(SD.proc_CreateCoverType, ms);
            }
            else
            {
                ms.Add("@id", covertype.Id);
                _unitOfWork.SpCall.excecute(SD.proc_UpdateCoverType, ms);
                _unitOfWork.Save();
            }
            return RedirectToAction("Index");
        }
        #region APIs
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _unitOfWork.SpCall.List<CoverType>(SD.proc_GetCoverType) });
            // return Json(new { data = _unitfowork.Covertype.Getall() });
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var parm = new DynamicParameters();
            parm.Add("@id", id);
            var coer = _unitOfWork.SpCall.oneRecord<CoverType>(SD.proc_GetCoverType, parm);
            //  var cat = _unitfowork.Covertype.get(id);
            if (parm == null) return Json(new
            {
                success = false,
                message = "Something went !!!"
            });
            //   _unitfowork.Covertype.Remove(cat);
            //_unitfowork.save();
            _unitOfWork.SpCall.excecute(SD.proc_DeleteCoverTypes, parm);

            return Json(new { success = true, message = "data is delete sucaee" });

        }
        #endregion

    }
}