using System.Collections.Generic;
using System.Linq;

namespace ExtensionsLibrary
{
    public static class ListExtensions
    {
        public static void AddNewRow<T>(this List<T> source) where T : new()
        {
            source.Add(new T());
        }

        /// <summary>
        ///中身が完全に一致するデータを削除する 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="entity"></param>
        public static void RemoveRow<T>(this List<T> source,T entity) where T : new()
        {
            var deleteEntity = source.Where(e => e.Json() == entity.Json()).First();
            source.Remove(deleteEntity);
        }
    }
}
