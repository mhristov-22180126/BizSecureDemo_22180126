using BizSecureDemo_22180126.Data;
using BizSecureDemo_22180126.Models;
using BizSecureDemo_22180126.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BizSecureDemo.Controllers;

[Authorize]
public class OrdersController : Controller
{
    private readonly AppDbContext _db;
    public OrdersController(AppDbContext db) => _db = db;

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateOrderVm vm)
    {
        if (!ModelState.IsValid) return RedirectToAction("Index", "Home");

        var uid = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        _db.Orders.Add(new Order
        {
            UserId = uid,
            Title = vm.Title,
            Amount = vm.Amount
        });

        await _db.SaveChangesAsync();
        return RedirectToAction("Index", "Home");
    }
    public async Task<IActionResult> Details(int id)
    {

        //Unsafe variant - allows viewing all orders without authorization
        ////  Please note: we search by Id only, no ownership verification
        //var order = await _db.Orders.FirstOrDefaultAsync(o => o.Id == id);
        //if (order == null) return NotFound();
        //return View(order);


        //safe variant
        var uid = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var order = await _db.Orders.FirstOrDefaultAsync(o => o.Id == id && o.UserId == uid);
        if (order == null) return NotFound();
        return View(order);
    }

}
