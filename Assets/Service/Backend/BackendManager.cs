using System.Text;
using System.Security.Cryptography;
using UnityEngine;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using System;

namespace U2T
{
    public class BackendManager
    {
        MongoClient mongoClient;
        IMongoDatabase database;
        IMongoCollection<BsonDocument> collection;

        public void Initialize()
        {
            mongoClient = new MongoClient("mongodb+srv://mndb1234:mndb1234@cluster0.njcbd.mongodb.net/testDB?retryWrites=true&w=majority");
            database = mongoClient.GetDatabase("mndb1234");
            collection = database.GetCollection<BsonDocument>("testDB");
        }

        public void AddData(string username, string password)
        {
            var document = new BsonDocument { { "username", username }, { "password", EncodePasswordToHAS256(password)} };
            collection.InsertOne(document);
        }


        public BsonDocument FilterData(string fill, string value)
        {
            //collection.Find(new BsonDocument()).ForEachAsync(X => Debug.Log(X));   
            var filter = Builders<BsonDocument>.Filter.Eq(fill, value);
            var result = collection.Find(filter).FirstOrDefault();
            return result;
        }

        public void RemoveData(string username, string password)
        {
            var document = new BsonDocument { { "name", "bank" }};
            collection.DeleteOne(document);
        }

        private string EncodePasswordToHAS256(string password)
        {
            var sha256 = SHA256.Create();
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

            var stringbuilder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                stringbuilder.Append(bytes[i].ToString());
            }
            return stringbuilder.ToString();
        }
    }
}
