using Microsoft.AspNetCore.Mvc;

namespace ActionFilterDemo.Controllers;
[ApiController]
[Route("[controller]/[action]")]
public class DemoController(MyDbContext myDbContext) : ControllerBase
{
    private readonly MyDbContext? dbContext = myDbContext;

    [HttpPost]
    public string Test1()
    {
        dbContext?.Books.Add(new Book { Title = ".Net Book", Id = 0 });
        return "Success Added";
    }
}
