using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Repo.Model.Models.Common;

namespace Repo.UI.Extensions
{
    public static class EntityExtension
    {
        public static void Print(this Entity entity)
        {
            var collectionTypes = new Type[]
            {
                typeof(ICollection<>), typeof(IList<>), typeof(IEnumerable<>)
            };

            var properties = entity.GetType().GetProperties().OrderBy(p => p.Name).ToList();

            foreach (var property in properties)
            {
                var propertyType = property.PropertyType;
                if (propertyType.IsGenericType && collectionTypes.Contains(propertyType.GetGenericTypeDefinition()))
                {
                    var collection = (IEnumerable<object>)property.GetValue(entity);
                    var serializedCollection = JsonConvert.SerializeObject(collection);

                    Console.WriteLine($"{property.Name}: {serializedCollection}");
                }
                else
                {
                    Console.WriteLine($"{property.Name}: {property.GetValue(entity)}");
                }
            }
        }
    }
}
