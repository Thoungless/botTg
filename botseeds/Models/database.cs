using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;

namespace botseeds.Models
{
    public class database
    {
        static string connectionString = ""; // твоя строка подключения
        static MongoClient client = new MongoClient(connectionString);

        public async Task SaveUser(long userId, string nickName)
        {
            try
            {
                var basedata = client.GetDatabase("User");
                var collection = basedata.GetCollection<BsonDocument>("userId");
                var filter = new BsonDocument("_id", $"{userId}");
                var results = await collection.Find(filter).ToListAsync();



                if (results.Count == 0)
                {
                    BsonDocument user = new BsonDocument() { { "_id", $"{userId}" }, { "NickName", $"{nickName}" } };
                    await collection.InsertOneAsync(user);
                }
            }
            catch(Exception ex)
            {
                Exception d = ex;
            }
        }

        public async Task AddToCartUser(long userId, string orderName)
        {
            try
            {
                var basedata = client.GetDatabase("User");
                var collection = basedata.GetCollection<BsonDocument>("userId");
                var filter = Builders<BsonDocument>.Filter.Eq("_id", $"{userId}");
                var update = Builders<BsonDocument>.Update.AddToSet("Cart", $"{orderName}");
                var result = await collection.UpdateOneAsync(filter, update);
            }
            catch(Exception ex)
            {
                Exception d = ex;
            }
        }

        public async Task AddToCartUserMany(long userId, string orderName)
        {
            try
            {
                var basedata = client.GetDatabase("User");
                var collection = basedata.GetCollection<BsonDocument>("userId");
                var filter = Builders<BsonDocument>.Filter.Eq("_id", $"{userId}");
                var update = Builders<BsonDocument>.Update.Push("Cart", $"{orderName}");
                var result = await collection.UpdateOneAsync(filter, update);
            }
            catch (Exception ex)
            {
                Exception d = ex;
            }
        }

        public async Task<List<string>> OpenCart(long userId)
        {
            List<string> cart = new List<string>();
            try
            {
                var basedata = client.GetDatabase("User");
                var collection = basedata.GetCollection<BsonDocument>("userId");
                var filter = new BsonDocument("_id", $"{userId}");
                var projection = Builders<BsonDocument>.Projection.Include("Cart").Exclude("_id");
                var results = await collection.Find(filter).Project(projection).ToListAsync();
                if(results.Count != 0)
                {
                   var j = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(results[0].ToJson());
                    var eee = j["Cart"];
                    foreach (var vrt in eee)
                    {
                        cart.Add(vrt.ToString());
                    }
                }
                return cart;
            }
            catch (Exception ex)
            {
                Exception d = ex;
                return cart;
            }
        }

        public async Task ClearCart(long userId)
        {
            var basedata = client.GetDatabase("User");
            var collection = basedata.GetCollection<BsonDocument>("userId");
            var filter = new BsonDocument("_id", $"{userId}");
            var update = Builders<BsonDocument>.Update.Unset("Cart");
            var result = await collection.UpdateOneAsync(filter, update);
        }

        public async Task AddDateTimeUser(long userId, DateTime dt)
        {
            try
            {
                var basedata = client.GetDatabase("User");
                var collection = basedata.GetCollection<BsonDocument>("userId");
                var filter = Builders<BsonDocument>.Filter.Eq("_id", $"{userId}");
                var update = Builders<BsonDocument>.Update.Set("DateTimeUser", $"{dt}");
                var result = await collection.UpdateOneAsync(filter, update);
            }
            catch (Exception ex)
            {
                Exception d = ex;
            }
        }

        public async Task<DateTime> OpenDateTime(long userId)
        {
            DateTime datetime = new DateTime();
            try
            {
                var basedata = client.GetDatabase("User");
                var collection = basedata.GetCollection<BsonDocument>("userId");
                var filter = Builders<BsonDocument>.Filter.Eq("_id", $"{userId}");
                var projection = Builders<BsonDocument>.Projection.Include("DateTimeUser").Exclude("_id");
                var results = await collection.Find(filter).Project(projection).ToListAsync();
                if (results.Count != 0)
                {
                    var dt = JsonConvert.DeserializeObject<Dictionary<string, DateTime>>(results[0].ToJson());
                    datetime = dt["DateTimeUser"];
                    return datetime;
                }
                else
                {
                    return datetime;
                }
            }
            catch (Exception ex)
            {
                Exception d = ex;
                return datetime;
            }
        }

        public async Task RemoveDateTime(long userId)
        {
            try
            {
                var basedata = client.GetDatabase("User");
                var collection = basedata.GetCollection<BsonDocument>("userId");
                var filter = Builders<BsonDocument>.Filter.Eq("_id", $"{userId}");
                var update = Builders<BsonDocument>.Update.Unset("DateTimeUser");
                var result = await collection.UpdateOneAsync(filter, update);
            }
            catch (Exception ex)
            {
                Exception d = ex;
            }
        }

        public async Task SaveUserFio(long userId, string fio)
        {
            try
            {
                var basedata = client.GetDatabase("User");
                var collection = basedata.GetCollection<BsonDocument>("userId");
                var filter = Builders<BsonDocument>.Filter.Eq("_id", $"{userId}");
                var update = Builders<BsonDocument>.Update.Set("FIO", $"{fio}");
                var result = await collection.UpdateOneAsync(filter, update);
            }
            catch (Exception ex)
            {
                Exception d = ex;
            }
        }

        public async Task SaveUserIndex(long userId, string index)
        {
            try
            {
                var basedata = client.GetDatabase("User");
                var collection = basedata.GetCollection<BsonDocument>("userId");
                var filter = Builders<BsonDocument>.Filter.Eq("_id", $"{userId}");
                var update = Builders<BsonDocument>.Update.Set("Index", $"{index}");
                var result = await collection.UpdateOneAsync(filter, update);
            }
            catch (Exception ex)
            {
                Exception d = ex;
            }
        }

        public async Task SaveUserAdress(long userId, string Adress)
        {
            try
            {
                var basedata = client.GetDatabase("User");
                var collection = basedata.GetCollection<BsonDocument>("userId");
                var filter = Builders<BsonDocument>.Filter.Eq("_id", $"{userId}");
                var update = Builders<BsonDocument>.Update.Set("Adress", $"{Adress}");
                var result = await collection.UpdateOneAsync(filter, update);
            }
            catch (Exception ex)
            {
                Exception d = ex;
            }
        }

        public async Task SaveUserCountry(long userId, string Country)
        {
            try
            {
                var basedata = client.GetDatabase("User");
                var collection = basedata.GetCollection<BsonDocument>("userId");
                var filter = Builders<BsonDocument>.Filter.Eq("_id", $"{userId}");
                var update = Builders<BsonDocument>.Update.Set("Country", $"{Country}");
                var result = await collection.UpdateOneAsync(filter, update);
            }
            catch (Exception ex)
            {
                Exception d = ex;
            }
        }

        public async Task SaveUserSum(long userId, int sum)
        {
            try
            {
                var basedata = client.GetDatabase("User");
                var collection = basedata.GetCollection<BsonDocument>("userId");
                var filter = Builders<BsonDocument>.Filter.Eq("_id", $"{userId}");
                var update = Builders<BsonDocument>.Update.Set("Sum", $"{sum}");
                var result = await collection.UpdateOneAsync(filter, update);
            }
            catch (Exception ex)
            {
                Exception d = ex;
            }
        }

        public async Task<string> GetUserFio(long userId)
        {
            string fio = "";
            try
            {
                var basedata = client.GetDatabase("User");
                var collection = basedata.GetCollection<BsonDocument>("userId");
                var filter = Builders<BsonDocument>.Filter.Eq("_id", $"{userId}");
                var projection = Builders<BsonDocument>.Projection.Include("FIO").Exclude("_id");
                var results = await collection.Find(filter).Project(projection).ToListAsync();
                if (results.Count != 0)
                {
                    var fiodic = JsonConvert.DeserializeObject<Dictionary<string, string>>(results[0].ToJson());
                    fio = fiodic["FIO"];
                    return fio;
                }
                else
                {
                    return fio;
                }
            }
            catch (Exception ex)
            {
                Exception d = ex;
                return fio;
            }
        }

        public async Task<string> GetUserIndex(long userId)
        {
            string Index = "";
            try
            {
                var basedata = client.GetDatabase("User");
                var collection = basedata.GetCollection<BsonDocument>("userId");
                var filter = Builders<BsonDocument>.Filter.Eq("_id", $"{userId}");
                var projection = Builders<BsonDocument>.Projection.Include("Index").Exclude("_id");
                var results = await collection.Find(filter).Project(projection).ToListAsync();
                if (results.Count != 0)
                {
                    var Indexdic = JsonConvert.DeserializeObject<Dictionary<string, string>>(results[0].ToJson());
                    Index = Indexdic["Index"];
                    return Index;
                }
                else
                {
                    return Index;
                }
            }
            catch (Exception ex)
            {
                Exception d = ex;
                return Index;
            }
        }

        public async Task<string> GetUserAdress(long userId)
        {
            string Adress = "";
            try
            {
                var basedata = client.GetDatabase("User");
                var collection = basedata.GetCollection<BsonDocument>("userId");
                var filter = Builders<BsonDocument>.Filter.Eq("_id", $"{userId}");
                var projection = Builders<BsonDocument>.Projection.Include("Adress").Exclude("_id");
                var results = await collection.Find(filter).Project(projection).ToListAsync();
                if (results.Count != 0)
                {
                    var Adressdic = JsonConvert.DeserializeObject<Dictionary<string, string>>(results[0].ToJson());
                    Adress = Adressdic["Adress"];
                    return Adress;
                }
                else
                {
                    return Adress;
                }
            }
            catch (Exception ex)
            {
                Exception d = ex;
                return Adress;
            }
        }

        public async Task<string> GetUserCountry(long userId)
        {
            string Country = "";
            try
            {
                var basedata = client.GetDatabase("User");
                var collection = basedata.GetCollection<BsonDocument>("userId");
                var filter = Builders<BsonDocument>.Filter.Eq("_id", $"{userId}");
                var projection = Builders<BsonDocument>.Projection.Include("Country").Exclude("_id");
                var results = await collection.Find(filter).Project(projection).ToListAsync();
                if (results.Count != 0)
                {
                    var Countrydic = JsonConvert.DeserializeObject<Dictionary<string, string>>(results[0].ToJson());
                    Country = Countrydic["Country"];
                    return Country;
                }
                else
                {
                    return Country;
                }
            }
            catch (Exception ex)
            {
                Exception d = ex;
                return Country;
            }
        }

        public async Task<int> GetUserSum(long userId)
        {
            int sum = 0;
            try
            {
                var basedata = client.GetDatabase("User");
                var collection = basedata.GetCollection<BsonDocument>("userId");
                var filter = Builders<BsonDocument>.Filter.Eq("_id", $"{userId}");
                var projection = Builders<BsonDocument>.Projection.Include("Sum").Exclude("_id");
                var results = await collection.Find(filter).Project(projection).ToListAsync();
                if (results.Count != 0)
                {
                    var Sumdic = JsonConvert.DeserializeObject<Dictionary<string, int>>(results[0].ToJson());
                    sum = Sumdic["Sum"];
                    return sum;
                }
                else
                {
                    return sum;
                }
            }
            catch (Exception ex)
            {
                Exception d = ex;
                return sum;
            }
        }

    }
}