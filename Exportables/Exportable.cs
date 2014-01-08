namespace Magana
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    
    /// <summary>
    /// Exportable is a static class that allows you to
    /// cache the valid properties of a Type which can be
    /// later used to gain access to only the valid fields
    /// without having to re-walk the reflection tree.
    /// </summary>
    public static class Exportable
    {
        /// <summary>
        /// propertiesCache. is a private field which caches the results
        /// of the ValidProperties method
        /// </summary>
        private static Dictionary<Type, IList<PropertyInfo>> propertiesCache = new Dictionary<Type, IList<PropertyInfo>>();

        /// <summary>
        /// ValidProperties is the core method of this class.
        /// Given a type and a string array of valid properties, this method
        /// will grab all the PropertyInfo references for the valid properties
        /// cache them and return a list of PropertyInfo elements ordered by the string array
        /// </summary>
        /// <param name='objectType'>
        /// The type for the object in question.
        /// </param>
        /// <param name='validProperties'>
        /// An array of valid property names.
        /// </param>
        /// <returns>A list of propertyinfo elements</returns>
        public static IList<PropertyInfo> ValidProperties(Type objectType, params string[] validProperties)
        {
            if (propertiesCache.ContainsKey(objectType)) return propertiesCache[objectType];
            if (validProperties.Length == 0) return null;

            lock (propertiesCache) 
            {
                // Avoid race conditions
                if (propertiesCache.ContainsKey(objectType)) return propertiesCache[objectType];

                var tmpList = (from prop in objectType.GetProperties()
                               where validProperties.Contains(prop.Name)
                               select prop).ToList();
            
                propertiesCache.Add(objectType, new List<PropertyInfo>());

                var tmp = propertiesCache[objectType];

                validProperties.ToList()
                    .ForEach(prop => 
                    {
                        if (tmpList.Any(c => c.Name == prop) && !tmp.Any(c => c.Name == prop))
                        {
                            tmp.Add(tmpList.First(p => p.Name == prop));
                        }
                    });
            }

            return propertiesCache[objectType];
        }
    }
}
