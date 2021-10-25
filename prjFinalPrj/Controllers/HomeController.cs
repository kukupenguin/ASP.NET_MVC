using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Web.Security;
using prjFinalPrj.Models;
using PagedList;

namespace prjFinalPrj.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        
        dbCookEntities db = new dbCookEntities();
        int pageSize = 10;

        // GET: Home
        public ActionResult Index(int page = 1)
        {
            int currentPage = page < 1 ? 1 : page;
            var r = db.TableRecipes1081655.OrderBy(m=>m.rId).ToList();
            CookRecipeModel cr = new CookRecipeModel()
            {

                Cook = db.TableCooks1081655.ToList(),
                //Recipe = db.TableRecipes1081655.ToList()
                Recipe = db.TableRecipes1081655.OrderBy(m => m.rId).ToList().ToPagedList(currentPage, pageSize)
            };
            return View(cr);
        }
        [AllowAnonymous]
        public ActionResult Create()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Create(TableCooks1081655 cook)  //註冊
        {
            var temp = db.TableCooks1081655
                .Where(m => m.name == cook.name)
                .FirstOrDefault();
            if (temp != null)
            {
                return View(cook);
            }
            db.TableCooks1081655.Add(cook);
            db.SaveChanges();
            return RedirectToAction("Login");
        }
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(string name, string pwd)
        {
            var result = db.TableCooks1081655.Where(m => m.name == name && m.password == pwd).FirstOrDefault();
            
            if (result == null)
            {
                ViewBag.Err = "帳密錯誤!"; 
            }
            else
            {
                FormsAuthentication.RedirectFromLoginPage(name, true);
                return RedirectToAction("Index");
            }
            return View();
        
        }
        
        public ActionResult Show(int rId)  //顯示完整食譜
        {
            var recipe = db.TableRecipes1081655.Where(m => m.rId == rId).ToList();
            ViewBag.rId = recipe[0].rId;
            ViewBag.pic = recipe[0].pic;
            ViewBag.rName = recipe[0].rName;
            ViewBag.ingredient = recipe[0].ingredient;
            ViewBag.direction = recipe[0].direction;
            return View();
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
        public ActionResult Edit (string name)
        {
            var cook = db.TableCooks1081655
                .Where(m => m.name == name).FirstOrDefault();
            return View(cook);

        }
        [HttpPost]
        public ActionResult Edit(TableCooks1081655 cook)  //編輯廚師資料
        {
            var temp = db.TableCooks1081655
                .Where(m => m.name == cook.name).FirstOrDefault();
            temp.password = cook.password;
            temp.phone = cook.phone;
            temp.age = cook.age;
            temp.city = cook.city;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int rId)
        {
            var recipe = db.TableRecipes1081655
                .Where(m => m.rId == rId).FirstOrDefault();
            db.TableRecipes1081655.Remove(recipe);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}