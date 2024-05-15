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
      return NotFound(new ErrorMessage
      {
        Error = $"Item with id of {id} was not found"
      });
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
  public IActionResult AddItem([FromBody] CreateItemRequest createItemRequest)
  {

    var matchingItem = _context.Items.FirstOrDefault(item => item.Name.ToLower() == createItemRequest.Name.ToLower());
    if(matchingItem != null){
      return Conflict(new ErrorMessage
      {
        Error = $"Item with the name {createItemRequest.Name} already exists"
      });
    }
    _context.Items.Add( new Item
    {
      Name = createItemRequest.Name,
      Category = createItemRequest.Category
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
      return NotFound(new ErrorMessage
      {
        Error = $"Item with id of {id} was not found"
      });
    }
    else 
    {
      _context.Items.Remove(matchingItem);
      _context.SaveChanges();
      return NoContent();
    }
  }
}