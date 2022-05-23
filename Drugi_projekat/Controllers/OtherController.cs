#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Drugi_projekat.models;
using Drugi_projekat.Models;

namespace Drugi_projekat.Controllers
{
    public class OtherController : Controller
    {
        private readonly Northwind21Context _context;

        public OtherController(Northwind21Context context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Products(string id)
        {
            int order_id;
            if (!int.TryParse(id, out order_id))
                return View(await _context.Products.Include(p => p.Category).Include(p => p.Supplier).OrderBy(p=>p.Supplier.CompanyName).ToListAsync());

            ViewData["Order"] = await _context.Orders.Include(o=>o.Customer).SingleOrDefaultAsync(o=>o.OrderId.Equals(order_id));
            var products = _context.OrderDetails.Include(od => od.Product).ThenInclude(p=>p.Category)
                                                .Include(od => od.Product).ThenInclude(p=>p.Supplier)
                            .Where(o => o.OrderId.Equals(order_id)).Select(o=>o.Product);
            return View(await products.ToListAsync());
        }

        // GET: Order/id
        public async Task<IActionResult> Orders(string id)
        {
            ViewData["Customers"] = await _context.Customers.ToListAsync();

            if (id is null)
            {
                return View();
            }

            var orders = _context.Orders.Include(o=>o.ShipViaNavigation).Include(o=>o.Employee).Where(o => o.CustomerId.Equals(id));
            return View(await orders.ToListAsync());
        }

        // GET: Other/Create
        // Add products for supplier page
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName");
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "SupplierId", "CompanyName");
            return View();
        }

        //Add product for supplier
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add()
        {
            if (ModelState.IsValid && Request.Form.Keys.Count>1)
            {

                var formData = Request.Form.Where(el => !el.Key.ToLower().Contains("verificationtoken"))
                        .ToDictionary(el => el.Key, el => el.Value);
                var len = formData.First().Value.Count;
                var products = new Product[len];

                for (int i = 0; i < len; i++)
                {
                    var p = new Product()
                    {
                        ProductName = formData["ProductName"][i],
                        SupplierId = int.Parse(formData["SupplierId"][i]),
                        CategoryId = int.Parse(formData["CategoryId"][i]),
                        QuantityPerUnit = formData["QuantityPerUnit"][i],
                        UnitPrice = String.IsNullOrEmpty(formData["UnitPrice"][i]) ? 0 : decimal.Parse(formData["UnitPrice"][i]),
                        UnitsInStock = String.IsNullOrEmpty(formData["UnitsInStock"][i]) ? (short)0 : short.Parse(formData["UnitsInStock"][i]),
                        UnitsOnOrder = String.IsNullOrEmpty(formData["UnitsOnOrder"][i]) ? (short)0 : short.Parse(formData["UnitsOnOrder"][i]),
                        ReorderLevel = String.IsNullOrEmpty(formData["ReorderLevel"][i]) ? (short)0 : short.Parse(formData["ReorderLevel"][i]),
                        Discontinued = bool.Parse(formData["Discontinued"][i]),
                    };
                    products[i] = p;
                }

                _context.AddRange(products);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return NotFound();
        }

        // GET: Other/Count
        public async Task<IActionResult> Count()
        {
            var orderDetails = _context.OrderDetails.Include(o => o.Order).Include(o => o.Product).ThenInclude(p => p.Supplier);
            var result = orderDetails.GroupBy(od => new { od.OrderId, od.Product.Supplier.SupplierId, })
                    .Select(g => new ProductsByOrderAndSupplier
                    {
                        Order = _context.Orders.SingleOrDefault(o => o.OrderId.Equals(g.Key.OrderId)),
                        Supplier = _context.Suppliers.SingleOrDefault(s => s.SupplierId.Equals(g.Key.SupplierId)),
                        Count = g.Select(el => el).ToList().Count
                    });

            //var products = _context.Products.Include(el => el.Supplier).Include(el => el.OrderDetails);
            //var result = products.GroupBy(p => new { p.SupplierId }).Select(g => new ProductsByOrderAndSupplier
            //{
            //    Supplier = _context.Suppliers.SingleOrDefault(s => s.SupplierId.Equals(g.Key.SupplierId)),
            //    Count = g.Count()
            //});

            return View(await result.ToListAsync());
        }
    }
}
