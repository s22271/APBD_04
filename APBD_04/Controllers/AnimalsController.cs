using Microsoft.AspNetCore.Mvc;
[ApiController]
[Route("api/animals")]
public class AnimalsController : ControllerBase
{
    private IDatabaseService _databaseService;
    public AnimalsController(IDatabaseService databaseService)
    {
        _databaseService = databaseService;
    }
    [HttpGet]
    public IActionResult GetAnimals([FromQuery] string orderBy)
    {
        orderBy = orderBy.ToLower();
        if (orderBy == "Name" || orderBy == null)
        {
            return Ok(_databaseService.GetAnimals("Name"));
        }
        else if (orderBy == "Description")
        {
            return Ok(_databaseService.GetAnimals("Description"));
        }
        else if (orderBy == "Category")
        {
            return Ok(_databaseService.GetAnimals("Category"));
        }

        else if (orderBy == "Area")
        {
            return Ok(_databaseService.GetAnimals("Area"));
        }
        else
        {
            return BadRequest("Invalid column!");
        }
    }
    [HttpPost]
    public IActionResult AddAnimal(Animal animal)
    {
        return Ok(_databaseService.AddAnimal(animal));
    }
    [HttpPut("{idAnimal}")]
    public IActionResult UpdateAnimal([FromRoute] int idAnimal, [FromBody] Animal animal)
    {
        int rowsAffected = _databaseService.UpdateAnimal(idAnimal, animal);

        if (rowsAffected > 0)
            return Ok(rowsAffected);

        return BadRequest("This ID does not exist");
    }
    [HttpDelete("{idAnimal}")]
    public IActionResult DeleteAnimal(int idAnimal)
    {
        int rowsAffected = _databaseService.DeleteAnimal(idAnimal);

        if (rowsAffected > 0)
            return Ok(rowsAffected);

        return BadRequest("This ID does not exist");
    }
}

