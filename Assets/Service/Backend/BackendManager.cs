using System;
using System.IO;
using System.Security.Cryptography;
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

        public void Initialize()
        {
            mongoClient = new MongoClient("mongodb+srv://mndb1234:mndb1234@cluster0.njcbd.mongodb.net/testDB?retryWrites=true&w=majority");
            database = mongoClient.GetDatabase("mndb1234");
            collection = database.GetCollection<BsonDocument>("testDB");
        }

        public void AddData(string username, string password)
        {
            var document = new BsonDocument { { "username", username }, { "password", EncodePassword(password)} };
            collection.InsertOne(document);
        }

        public void GetData()
        {
            collection.Find(new BsonDocument()).ForEachAsync(X => Debug.Log(X));
        }

        public void RemoveData(string username, string password)
        {
            var document = new BsonDocument { { "name", "bank" }};
            collection.DeleteOne(document);
        }

        private string EncodePassword(string password)
        {
            string encodePassword = "";
            string directory = password;
            if (Directory.Exists(directory))
            {
                // Create a DirectoryInfo object representing the specified directory.
                var dir = new DirectoryInfo(directory);
                // Get the FileInfo objects for every file in the directory.
                FileInfo[] files = dir.GetFiles();
                // Initialize a SHA256 hash object.
                using (SHA256 mySHA256 = SHA256.Create())
                {
                    // Compute and print the hash values for each file in directory.
                    foreach (FileInfo fInfo in files)
                    {
                        using (FileStream fileStream = fInfo.Open(FileMode.Open))
                        {
                            try
                            {
                                // Create a fileStream for the file.
                                // Be sure it's positioned to the beginning of the stream.
                                fileStream.Position = 0;
                                // Compute the hash of the fileStream.
                                byte[] hashValue = mySHA256.ComputeHash(fileStream);
                                // Write the name and hash value of the file to the console.
                                Console.Write($"{fInfo.Name}: ");
                                encodePassword += fInfo.Name;
                            }
                            catch (IOException e)
                            {
                                //Console.WriteLine($"I/O Exception: {e.Message}");
                            }
                            catch (UnauthorizedAccessException e)
                            {
                               //Console.WriteLine($"Access Exception: {e.Message}");
                            }
                        }
                    }
                }
            }
            return encodePassword;
        }
    }
}
