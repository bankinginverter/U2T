using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.IO;

namespace U2T
{
    public class BackendManager
    {
        MongoClient mongoClient;
        IMongoDatabase database;
        IMongoCollection<BsonDocument> collection;

        private void Initialize()
        {
            mongoClient = new MongoClient("mongodb+srv://mndb1234:mndb1234@cluster0.njcbd.mongodb.net/testDB?retryWrites=true&w=majority");
            database = mongoClient.GetDatabase("mndb1234");
            collection = database.GetCollection<BsonDocument>("testDB");
        }

        public void CreateCharacter(int id, bool status)
        {
            var document = new BsonDocument { { "id", id }, { "plant", status } };
            collection.InsertOne(document);
        }

        private void GetDataPlayer()
        {
            collection.Find(new BsonDocument()).ForEachAsync(X => Debug.Log(X));
        }
    }
}
