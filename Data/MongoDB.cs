using System;
using Api.Data.Collections;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace Api
{

    public class MongoDB
    {
        public IMongoDatabase DB { get; }
        public MongoDB(IConfiguration configuration)
        {
            try
            {
                var settings = MongoClientSettings.FromUrl(new MongoUrl(configuration["ConnectionString"]));
                var client = new MongoClient(settings);
                DB = client.GetDatabase(configuration["NomeBanco"]);
                Mapclasses();

            }
            catch (System.Exception ex)
            {

                throw new MongoException("It was not possible to connect to MongoDB", ex);
            }
        }
        private void Mapclasses()
        {
            var conventionPack = new ConventionPack { new CamelCaseElementNameConvention() };
            ConventionRegistry.Register("CamelCase", conventionPack, t => true);

            if (!BsonClassMap.IsClassMapRegistered(typeof(infectado)))
            {
                BsonClassMap.RegisterClassMap<infectado>(i =>
                {
                    i.AutoMap();
                    i.SetIgnoreExtraElements(true);
                });
            }
        }

    }

}