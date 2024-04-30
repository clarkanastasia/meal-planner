using MealPlanner.Models.Data;
using MealPlanner.Models.Request;
using MealPlanner.Models.Response;
using Microsoft.AspNetCore.Mvc;

namespace MealPlanner.Controllers;

[ApiController]
[Route("/items")]
public class ItemController(MealPlannerContext context): ControllerBase 
{
  private readonly MealPlannerContext _context = context;

  [HttpGet("all")]
  public IActionResult GetAllItems()
  {
    var allItems = _context.Items.Select(item => new ItemResponse
    {
      Id = item.Id,
      Name = item.Name,
      Category = item.Category
    }).ToList();

    return Ok(allItems);
  }

  [HttpGet("{id}")]
  public IActionResult GetItemById([FromRoute] int id)
  {
    var matchingItem = _context.Items.SingleOrDefault(item => item.Id == id);

    if(matchingItem == null)
    {
      return NotFound();
    }

    return Ok(new ItemResponse
      {
        Id = matchingItem.Id,
        Name = matchingItem.Name,
        Category = matchingItem.Category
      }
    );
  }

  [HttpPost]
  public IActionResult AddItem([FromBody] CreateItemRequest createItemREquest)
  {
    _context.Items.Add(new Item 
    {
      Name = createItemREquest.Name,
      Category = createItemREquest.Category
    });
    _context.SaveChanges();

    return Ok();
  }

  [HttpDelete("{id}")]
  public IActionResult DeleteItem([FromRoute] int id)
  {
    var matchingItem = _context.Items.FirstOrDefault(item => item.Id == id);
    if (matchingItem == null)
    {
      return NotFound();
    }
    else 
    {
      _context.Items.Remove(matchingItem);
      _context.SaveChanges();
      return NoContent();
    }
  }
}