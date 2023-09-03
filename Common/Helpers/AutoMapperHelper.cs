using System.Reflection;

namespace Common.Helpers
{
    public static class AutoMapperHelper
    {
        /// <summary>
        /// 将一个对象转换为指定类型的实体对象。
        /// </summary>
        /// <typeparam name="T">目标实体类型</typeparam>
        /// <param name="obj">要转换的对象</param>
        /// <returns>转换后的实体对象</returns>
        public static T ConvertToEntity<T>(object obj) where T : new()
        {
            Type objectType = obj.GetType();  // 获取对象的类型信息
            Type entityType = typeof(T);  // 获取目标实体类型的类型信息
            T entity = new T();  // 创建目标实体类型的实例

            PropertyInfo[] objectProperties = objectType.GetProperties();  // 获取对象的属性数组
            PropertyInfo[] entityProperties = entityType.GetProperties();  // 获取实体类型的属性数组

            foreach (PropertyInfo objectProperty in objectProperties)
            {
                // 在实体属性中查找与对象属性名称和类型匹配的属性
                PropertyInfo? entityProperty = Array.Find(entityProperties, p =>
                    p.Name == objectProperty.Name && p.PropertyType == objectProperty.PropertyType);

                if (entityProperty != null && entityProperty.CanWrite)
                {
                    // 获取对象属性值并设置到实体属性中
                    object? value = objectProperty.GetValue(obj);
                    entityProperty.SetValue(entity, value);
                }
            }

            return entity;  // 返回转换后的实体对象
        }

        /// <summary>
        ///  将一个对象集合转换为指定类型的实体对象集合
        /// </summary>
        /// <typeparam name="T">目标实体类型</typeparam>
        /// <param name="collection">对象集合</param>
        /// <returns></returns>
        public static List<T> ConvertCollectionToEntities<T>(IEnumerable<object> collection) where T : new()
        {
            List<T> entities = new List<T>();
            foreach (var obj in collection)
            {
                T entity = ConvertToEntity<T>(obj);
                entities.Add(entity);
            }
            return entities;
        }
    }
}
