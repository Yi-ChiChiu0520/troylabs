using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TroyLabs.Models;
using TroyLabs.Data;
using Microsoft.EntityFrameworkCore;
using TroyLabs.ViewModels;

namespace TroyLabs.Controllers;

public class SurveyController : Controller
{
    private ApplicationDbContext _db;

    public SurveyController(ApplicationDbContext db){
        _db = db;
    }
  
public IActionResult Index(string searchTerm, int page = 1, int pageSize = 8)
{
    // Start with the base query
    var surveysQuery = _db.Surveys.AsQueryable();

    // Apply search filter if a search term is provided
    if (!string.IsNullOrEmpty(searchTerm))
    {
        surveysQuery = surveysQuery.Where(s => s.CompanyName.Contains(searchTerm));

        // Check if no results were found
        if (!surveysQuery.Any())
        {
            ViewBag.ErrorMessage = $"Company: {searchTerm} not found, please enter the correct company name.";
        }

        ViewBag.SearchTerm = searchTerm;
    }
    else
    {
        ViewBag.SearchTerm = "";
    }

    // Pagination logic
    var totalItems = surveysQuery.Count();
    var paginatedSurveys = surveysQuery
                        .OrderBy(s => s.CustomerName)  // Order by CustomerName or any other field
                        .Skip((page - 1) * pageSize)
                        .Take(pageSize)
                        .ToList();

    var viewModel = new PagedViewModel<Survey>
    {
        Items = paginatedSurveys,
        PageNumber = page,
        PageSize = pageSize,
        TotalItems = totalItems
    };

    return View(viewModel);
}

[HttpPost]
public IActionResult SubmitFeedback(Survey survey)
{
    // Check if the posted model is valid
    if (ModelState.IsValid)
    {
        // Add the survey feedback to the database
        _db.Surveys.Add(survey);
        _db.SaveChanges();

        // Set a success message in ViewBag
        ViewBag.SuccessMessage = "Thank you for your feedback!";

        // Return the same feedback page with the success message
        return View("SubmitFeedback", new Survey()); // Return a new Survey instance to clear the form
    }

    // If the model is invalid, return the same view with validation errors
    return View("SubmitFeedback", survey); // Return the form with the validation errors and the user's data
}


[HttpGet]
public IActionResult SubmitFeedback()
{
    return View();
}

}