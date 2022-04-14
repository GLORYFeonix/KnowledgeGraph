using Microsoft.AspNetCore.Mvc;
using Api.BLL;
using Api.DAL;
using Microsoft.AspNetCore.Cors;
using Neo4j.Driver;

namespace Api.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    [EnableCors("Local")]
    public class KnowledgeController : ControllerBase
    {
        [HttpGet]
        public async Task<List<Person>> Search()
        {
            IDriver driver = GraphDatabase.Driver("bolt://localhost:7687", AuthTokens.Basic("neo4j", "wasd12345652"));
            IAsyncSession session = driver.AsyncSession(o => o.WithDatabase("genealogy"));
            try
            {
                return await session.ReadTransactionAsync(async transaction =>
                {
                    var cursor = await transaction.RunAsync("MATCH (n) RETURN n");

                    return await cursor.ToListAsync(record => new Person(name: record[0].As<INode>().Properties["name"].ToString()));
                });
            }
            finally
            {
                await session.CloseAsync();
            }

            await driver.CloseAsync();
        }

    }
}
