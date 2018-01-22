using Inventory.DataAccess;
using Inventory.DataAccess.Entities;
using InventoryManagement.ViewModels;
using System;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Web;
using System.Web.Mvc;

namespace InventoryManagement.Controllers
{
    public class InventoryController : Controller
    {
        private readonly InventoryContextBase dbContext;

        public object FileName { get; private set; }

        public InventoryController()
        {
            this.dbContext = new InventoryContextBase();
        }
        // GET: Inventory
        public ViewResult List(string sortOrder, string SearchString)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.TypeSortParm = sortOrder == "Type" ? "Type_desc" : "Type";
            ViewBag.QuantitySortParm = sortOrder == "Quantity" ? "Quantity_desc" : "Quantity";
            ViewBag.SinglePriceSortParm = sortOrder == "SinglePrice" ? "SinglePrice_desc" : "SinglePrice";


            var inventories = from s in dbContext.Inventories
                              select s;

            if (!String.IsNullOrEmpty(SearchString))
            {
                inventories = inventories.Where(s => s.Name.ToUpper().Contains(SearchString.ToUpper())
                                       || s.Type.ToUpper().Contains(SearchString.ToUpper()));
            }
            
            switch (sortOrder)
            {
                case "name_desc":
                    inventories = inventories.OrderByDescending(s => s.Name);
                    break;
                case "Type":
                    inventories = inventories.OrderBy(s => s.Type);
                    break;
                case "Type_desc":
                    inventories = inventories.OrderByDescending(s => s.Type);
                    break;
                case "Quantity_desc":
                    inventories = inventories.OrderByDescending(s => s.Quantity);
                    break;
                case "Quantity":
                    inventories = inventories.OrderBy(s => s.Quantity);
                    break;
                case "SinglePrice_desc":
                    inventories = inventories.OrderByDescending(s => s.SinglePrice);
                    break;
                case "SinglePrice":
                    inventories = inventories.OrderBy(s => s.SinglePrice);
                    break;
                default:
                    inventories = inventories.OrderBy(s => s.Name);
                    break;
            }
            //var inventories = dbContext.Inventories.ToList();
            var viewModel = new ListModel<InventoriesManagement>()
            {
                Items = inventories.ToList()
            };

            return View(viewModel);

        }
        public ActionResult Details(int id)
        {
            var inventory = dbContext.Inventories.SingleOrDefault(x => x.InventoryID == id);
            if (inventory == null)
            {
                return HttpNotFound();
            }

            return View(inventory);
        }
        [HttpGet]
        public ViewResult Create()
        {
            var model = new InventoriesManagement();
            return View(model);
        }


        [HttpPost]
        public ActionResult Create(InventoriesManagement inventory, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {

                    inventory.Picture = new byte[image.ContentLength];
                    image.InputStream.Read(inventory.Picture, 0, image.ContentLength);
                }
                dbContext.Inventories.Add(inventory);
                dbContext.SaveChanges();
                return RedirectToAction("List");
            }
            return View(inventory);
        }
        [HttpGet]
        public ActionResult Update(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var inventory = dbContext.Inventories.SingleOrDefault(x => x.InventoryID == id.Value);
            if (inventory == null)
            {
                return HttpNotFound();
            }
            
            return View(inventory);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(InventoriesManagement inventory, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                var inventoryToUpdate = dbContext.Inventories.SingleOrDefault(x => x.InventoryID == inventory.InventoryID);

                
                if (image != null)
                {

                    inventory.Picture = new byte[image.ContentLength];
                    image.InputStream.Read(inventory.Picture, 0, image.ContentLength);

                }
                if (image == null && inventoryToUpdate != null)
                {
                    inventory.Picture = inventoryToUpdate.Picture;
                    
                }
                dbContext.Set<InventoriesManagement>().AddOrUpdate(inventory);
                //dbContext.Entry(inventory).State = System.Data.Entity.EntityState.Modified;
                dbContext.SaveChanges();
                return RedirectToAction("List");
            }
            return View(inventory);
        }
        public ActionResult Delete(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var inventory = dbContext.Inventories.SingleOrDefault(x => x.InventoryID == id.Value);
            if (inventory == null)
            {
                return HttpNotFound();
            }
            return View(inventory);

        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCategory(int id)
        {

            var inventory = dbContext.Inventories.Find(id);
            dbContext.Inventories.Remove(inventory);
            dbContext.SaveChanges();
            return RedirectToAction("List");
        }
    }
}