using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SpendSmart.Models;

namespace SpendSmart.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly SpendSmartDbContext _context;

    public HomeController(ILogger<HomeController> logger, SpendSmartDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }
    public IActionResult Expenses()
    {
        var allExpenses = _context.Expenses.ToList();
        return View(allExpenses);
    }

    public IActionResult CreateEditExpense(int? id)
    {
        if (id != null)
        {
            var expense = _context.Expenses.Find(id);
            if (expense != null)
            {
                return View(expense);
            }
        }
        // If id is null or expense not found, return a new Expense model
        return View();
    }

    public IActionResult CreateEditExpenseForm(Expense model)
    {
        // If model.Id is not 0, it's an existing expense
        if (model.Id != 0)
        {
            _context.Expenses.Update(model);
        }
        else
        {
            // If model.Id is 0, it's a new expense
            _context.Expenses.Add(model);
        }
        // Save changes to the database
        _context.SaveChanges();
        return RedirectToAction("Expenses");
    }
    public IActionResult DeleteExpense(int id)
    {
        var expense = _context.Expenses.Find(id);
        if (expense != null)
        {
            _context.Expenses.Remove(expense);
            _context.SaveChanges();
        }
        return RedirectToAction("Expenses");
    }
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
