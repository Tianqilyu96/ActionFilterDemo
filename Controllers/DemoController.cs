using System.Transactions;
using Microsoft.AspNetCore.Mvc;

namespace ActionFilterDemo.Controllers;
[ApiController]
[Route("[controller]/[action]")]
public class DemoController(MyDbContext myDbContext) : ControllerBase
{
    private readonly MyDbContext dbContext = myDbContext;

    [HttpPost]
    public string Test1()
    {
        using (TransactionScope tx = new())
        {
            //task1
            dbContext?.Books.Add(new Book { Title = ".Net Book" });
            dbContext?.SaveChanges();
            //task2
            dbContext?.People.Add(new Person { Name = "AL" });
            dbContext?.SaveChanges();
            tx.Complete();
            return "Success Added";
        }
    }

    [HttpPost]
    public async Task<string> Test2()
    {
        using (TransactionScope tx = new(TransactionScopeAsyncFlowOption.Enabled))
        {
            //task1
            dbContext.Books.Add(new Book { Title = ".Net Book" });
            await dbContext.SaveChangesAsync();
            //task2
            dbContext.People.Add(new Person { Name = "AL" });
            await dbContext.SaveChangesAsync();
            tx.Complete();
            return "Success Added";
        }
    }
}
