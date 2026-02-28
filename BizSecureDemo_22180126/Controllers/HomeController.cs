using BizSecureDemo_22180126.Data;
using BizSecureDemo_22180126.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Claims;

namespace BizSecureDemo.Controllers;

[Authorize]
public class HomeController : Controller
{
    private readonly AppDbContext _db;
    public HomeController(AppDbContext db) => _db = db;

    public async Task<IActionResult> Index()
    {

        // From task 1
        //var uid = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        //var orders = await _db.Orders
        //    .Where(o => o.UserId == uid)
        //    .OrderByDescending(o => o.Id)
        //    .ToListAsync();
        // return View(orders);



        // Task 2
        var uid = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        //Get's the Id from the user that is logged in

        var myOrders = await _db.Orders
            .Where(o => o.UserId == uid)
            .OrderByDescending(o => o.Id)
            .ToListAsync();
        //Reads orders from the database according to the logged in user

        var allOrders = await _db.Orders
            .OrderByDescending(o => o.Id)
            .ToListAsync();
        //Reads all orders from the database

        ViewBag.AllOrders = allOrders;
        //Submits all orders to the View through the ViewBag.The ViewBag is a "bag" for additional data to the View.Thus, the View can also display a public list of orders.


        return View(myOrders);
    }
}
