using InMemoryDb.Models;
using Microsoft.Azure.CognitiveServices.Language.LUIS.Authoring;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using SmartHomeBot.Common;
using SmartHomeBot.Common.Enums;
using SmartHomeBot.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using MoreLinq;
using System.Net;
using System.Text;
using Intent = SmartHomeBot.Common.Enums.Intent;

namespace InMemoryDb
{
    public static class StaticDb
    {
        static StaticDb()
        {
            DbConnection = new SqliteConnection("Filename=:memory:");
            DbConnection.Open();
            LoadStartData();
        }

        private static DbConnection DbConnection { get; set; }

        public static ModelContext CreateContext()
        {
            var options = new DbContextOptionsBuilder()
                .UseSqlite(DbConnection)
                .Options;

            var context = new ModelContext(options);
            return context;
        }

        public static string ListDevices()
        {
            var dbContext = CreateContext();
            var devices = dbContext.Device
                .Select(x =>
                    $"{x.PowerState.Name,-20}\t{x.Location.Name,-20}\t{x.DeviceType.Name,-20}\t{x.Name,-20}");

            return $"{"Status",-20}\t{"Location",-20}\t{"Type",-20}\t{"Name",-20}\n" + string.Join("\n", devices);
        }

        private static async void LoadStartData()
        {
            var credentials = new ApiKeyServiceClientCredentials(Config.LuisSubscriptionKey);
            var client = new LUISAuthoringClient(credentials) { Endpoint = Config.LuisEndPoint };
            var chierarchicalentities = await client.Model.ListHierarchicalEntitiesAsync(Config.LuisAppGuid, "0.1");
            var entities = await client.Model.ListEntitiesAsync(Config.LuisAppGuid, "0.1");
            var compentities = await client.Model.ListCompositeEntitiesAsync(Config.LuisAppGuid, "0.1");

            ModelContext dbContext = CreateContext();
            dbContext.Database.EnsureCreated();

            dbContext.Intent.AddRange(
                new Models.Intent { Name = Intent.cancel.ToString() },
                new Models.Intent { Name = Intent.get_list.ToString() },
                new Models.Intent { Name = Intent.None.ToString() },
                new Models.Intent { Name = Intent.set_power_state.ToString() }); //TODO MW Table Intent Useless?


            foreach (var entity in entities)
            {
                if (!Enum.TryParse(entity.Name, out FirstLevelEntity firstLevelEntityEnum))
                {
                    continue;
                }

                var firstLevelEntity = new LuisEntity { Name = firstLevelEntityEnum.ToString() };
                dbContext.LuisEntity.Add(firstLevelEntity);

                var hEnt = await client.Model.GetHierarchicalEntityAsync(Config.LuisAppGuid, "0.1", entity.Id);
                foreach (var child in hEnt.Children)
                {
                    dbContext.LuisEntity.Add(new LuisEntity { Name = child.Name.Split("\\").Last(), ParentEntity = firstLevelEntity });
                }
            }

            dbContext.SaveChanges();

            var locations = dbContext.LuisEntity.Where(x => x.ParentEntity.Name == FirstLevelEntity.Location.ToString());
            var deviceTypes = dbContext.LuisEntity.Where(x => x.ParentEntity.Name == FirstLevelEntity.DeviceType.ToString());
            var powerStates = dbContext.LuisEntity.Where(x => x.ParentEntity.Name == FirstLevelEntity.PowerState.ToString()).ToArray();

            var locationTypePairs = locations.Cartesian(deviceTypes, (l, d) => (l, d));
            var rnd = new Random();
            foreach (var (location, deviceType) in locationTypePairs)
            {
                if (rnd.NextDouble() < 0.3333)
                    continue;

                int r = rnd.Next(powerStates.Count());
                dbContext.Device.Add(new Device
                {
                    Name = deviceType.Name + "-" + location.Name,
                    Location = location,
                    DeviceType = deviceType,
                    PowerState = powerStates[r],
                });
            }

            dbContext.SaveChanges();
        }
    }
}
