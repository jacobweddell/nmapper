using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace NMapper
{
    public static class Mapper
    {
        public static T Map<T>(object sourceObject)
        {
            T targetObject = GetInstance<T>();

            CopyProperties(sourceObject, targetObject);

            return targetObject;
        }

        public static T Map<T>(object sourceObject, Dictionary<string, string> mappingToUse)
        {
            T targetObject = GetInstance<T>();

            CopyProperties(sourceObject, targetObject, mappingToUse);

            return targetObject;
        }

        private static List<PropertyInfo> GetPropertyList(object objectToUse)
        {
            var type = objectToUse.GetType();
            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            return new List<PropertyInfo>(properties);
        }

        private static T GetInstance<T>()
        {
           return Activator.CreateInstance<T>();
        }

        private static void CopyProperties(object sourceObject, object targetObject)
        {
            var targetProperties = GetPropertyList(targetObject);
            var sourceProperties = GetPropertyList(sourceObject);

            foreach (var targetProperty in targetProperties)
            {
                var sourceProperty = GetPropertyByName(sourceProperties, targetProperty.Name);

                if (sourceProperty.Equals(null))
                    continue;

                SetTargetProperty(sourceProperty, targetProperty, sourceObject, targetObject);
            }
        }

        private static void CopyProperties(object sourceObject, object targetObject, Dictionary<string, string> mapping)
        {
            var targetProperties = GetPropertyList(targetObject);
            var sourceProperties = GetPropertyList(sourceObject);

            foreach (var targetProperty in targetProperties)
            {
                var targetPropertyEntry = mapping.FirstOrDefault(m => m.Value.Equals(targetProperty.Name));

                if (targetPropertyEntry.Equals(null))
                    continue;

                var sourceProperty = GetPropertyByName(sourceProperties, targetPropertyEntry.Key);

                if (sourceProperty.Equals(null))
                    continue;

                SetTargetProperty(sourceProperty, targetProperty, sourceObject, targetObject);
            }
        }

        private static void SetTargetProperty(PropertyInfo sourceProperty, PropertyInfo targetProperty, object sourceObject, object targetObject)
        {
            targetProperty.SetValue(targetObject, sourceProperty.GetValue(sourceObject));
        }

        private static PropertyInfo GetPropertyByName(IEnumerable<PropertyInfo> properties, string name)
        {
            return properties.FirstOrDefault(sp => sp.Name.Equals(name) || (sp.GetCustomAttribute<Mapping>()?.Name?.Equals(name) ?? false));
        }
    }
}
