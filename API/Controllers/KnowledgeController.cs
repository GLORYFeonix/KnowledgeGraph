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
        private readonly IDriver driver;

        public KnowledgeController()
        {
            driver = GraphDatabase.Driver("bolt://localhost:7687", AuthTokens.Basic("neo4j", "wasd12345652"));
        }

        [HttpGet]
        public async Task<List<Relationship>> SearchAllRelationship()
        {
            IAsyncSession session = driver.AsyncSession(o => o.WithDatabase("genealogy"));
            try
            {
                return await session.ReadTransactionAsync(async transaction =>
                {
                    var cursor = await transaction.RunAsync("match(n)-[r]->(m) return n.name,r.name,m.name");

                    return await cursor.ToListAsync(record => new Relationship(source: record[0].ToString(), kind: record[1].ToString(), target: record[2].ToString()));
                });
            }
            finally
            {
                await session.CloseAsync();
            }
        }

        [HttpGet]
        public async Task<List<Relationship>> SearchRelationship(string? source, string? target)
        {
            IAsyncSession session = driver.AsyncSession(o => o.WithDatabase("genealogy"));
            try
            {
                return await session.ReadTransactionAsync(async transaction =>
                {
                    IResultCursor? cursor;
                    if (source == null && target == null)
                    {
                        cursor = await transaction.RunAsync($"match(n)-[r]->(m) return n.name,r.name,m.name");

                    }
                    else if (source != null && target == null)
                    {
                        cursor = await transaction.RunAsync($"match(n)-[r]->(m) where n.name = '{source}' return n.name,r.name,m.name");
                    }
                    else
                    {
                        cursor = await transaction.RunAsync($"match(n)-[r]->(m) where n.name = '{source}' and m.name = '{target}' return n.name,r.name,m.name");
                    }

                    return await cursor.ToListAsync(record => new Relationship(source: record[0].ToString(), kind: record[1].ToString(), target: record[2].ToString()));
                });
            }
            finally
            {
                await session.CloseAsync();
            }
        }

        [HttpGet]
        public async Task<List<Person>> SearchAllNode()
        {
            IAsyncSession session = driver.AsyncSession(o => o.WithDatabase("genealogy"));
            try
            {
                return await session.ReadTransactionAsync(async transaction =>
                {
                    var cursor = await transaction.RunAsync("MATCH (n) RETURN n.name,id(n)");

                    return await cursor.ToListAsync(record => new Person(name: record[0].ToString(), id: int.Parse(record[1].ToString())));
                });
            }
            finally
            {
                await session.CloseAsync();
            }
        }

        [HttpPost]
        public async void AddNode(string name)
        {
            IAsyncSession session = driver.AsyncSession(o => o.WithDatabase("genealogy"));
            try
            {
                await session.WriteTransactionAsync(async transaction =>
                {
                    string query = string.Format("MERGE (n:{0} ", "Character") + @"{name: $name})";
                    await transaction.RunAsync(query, new { name });
                });
            }
            finally
            {
                await session.CloseAsync();
            }
        }

        [HttpPost]
        public async void AddRelation(string SourceNodeName, string Type, string TargetNodeName)
        {
            IAsyncSession session = driver.AsyncSession(o => o.WithDatabase("genealogy"));
            try
            {
                await session.WriteTransactionAsync(async transaction =>
                {
                    string query = string.Format(@"match (n),(m) where n.name=$source and m.name=$target merge (n)-[r:Relationship {{name: '{0}'}}]->(m);", Type);
                    System.Console.WriteLine(query);
                    await transaction.RunAsync(query, new { source = SourceNodeName, type = Type, target = TargetNodeName });
                });
            }
            finally
            {
                System.Console.WriteLine("done");
                await session.CloseAsync();
            }
        }

        [HttpPost]
        public async void SingleSentence(string Sentence)
        {
            FileStream fs = new FileStream("C:/Users/gzy88/Desktop/KnowledgeGraph/API.DAL/NLP/data/single.txt", FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);

            sw.WriteLine(Sentence);
            sw.Flush();
            sw.Close();
            fs.Close();
            System.Console.WriteLine("Writing TXT has been completed!");

            NLP.RunDeepKE();
            System.Console.WriteLine("NLP has been complete!");

            IAsyncSession session = driver.AsyncSession(o => o.WithDatabase("genealogy"));
            try
            {
                await session.WriteTransactionAsync(async transaction =>
                {
                    string query = @"LOAD CSV WITH HEADERS FROM ""file:///single.csv"" AS row MERGE(src: Character { name: row.Source}) MERGE(tgt: Character { name: row.Target}) MERGE(src) <-[r: Relationship { name: row.Type}]->(tgt)";
                    await transaction.RunAsync(query);
                });
            }
            finally
            {
                await session.CloseAsync();
            }
        }

        [HttpPut]
        public async void Update(string SourceNodeName, string Type, string TargetNodeName)
        {
            IAsyncSession session = driver.AsyncSession(o => o.WithDatabase("genealogy"));
            try
            {
                await session.WriteTransactionAsync(async transaction =>
                {
                    string query = @"match (n)-[r]-(m) where n.name=$source and m.name=$target set r.name=$type;";
                    await transaction.RunAsync(query, new { source = SourceNodeName, type = Type, target = TargetNodeName });
                });
            }
            finally
            {
                await session.CloseAsync();
            }
        }

        [HttpDelete]
        public async void Delete(string SourceNodeName, string TargetNodeName)
        {
            IAsyncSession session = driver.AsyncSession(o => o.WithDatabase("genealogy"));
            try
            {
                await session.WriteTransactionAsync(async transaction =>
                {
                    string query = @"match (n)-[r]-(m) where n.name=$source and m.name=$target delete r;";
                    await transaction.RunAsync(query, new { source = SourceNodeName, target = TargetNodeName });
                });
            }
            finally
            {
                await session.CloseAsync();
            }
        }
    }
}
