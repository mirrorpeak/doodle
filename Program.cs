using Doodle;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

var serviceCollection = new ServiceCollection();

serviceCollection.AddDbContext<DoodleDbContext>(options => options.UseNpgsql("Host=localhost;Port=55000;Database=doodle;Username=postgres;Password=postgrespw"));

var serviceProvider = serviceCollection.BuildServiceProvider().CreateScope().ServiceProvider;

var dbContext = serviceProvider.GetRequiredService<DoodleDbContext>();
await dbContext.Database.EnsureCreatedAsync();

// for (int i = 0; i < 10; i++)
// {
//     var a1 = new Account()
//     {
//         AccountId = Guid.NewGuid(),
//         Name = $"a{i}"
//     };

//     var q1 = new Quote()
//     {
//         QuoteId = Guid.NewGuid(),
//         Account = a1,
//         //CustomerId = a1.AccountId
//     };

//     //await dbContext.Accounts.AddAsync(a1);
//     await dbContext.Quotes.AddAsync(q1);
// }

// await dbContext.SaveChangesAsync();

var queryText = await File.ReadAllTextAsync("Query.sql");

var result = await dbContext.Quotes.FromSqlRaw(queryText).ToListAsync();

foreach(var q in result)
{
    Console.WriteLine($"{q.QuoteId}, {q.Account.Name}");
}

var quotes = 
await 
(
    from q in dbContext.Quotes
    join a in dbContext.Accounts
    on q.CustomerId equals a.AccountId
    select new 
    Quote
    {
        Account = new Account(){
            AccountId = a.AccountId,
            Name = a.Name
        },

        CustomerId = a.AccountId,
        QuoteId = q.QuoteId
    }

).ToListAsync();


foreach(var q in quotes)
{
    Console.WriteLine($"{q.QuoteId}, {q.Account.Name}");
}
