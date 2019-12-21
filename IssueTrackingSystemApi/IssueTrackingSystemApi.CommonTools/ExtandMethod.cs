using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using IssueTrackingSystemApi.Models;
using IssueTrackingSystemApi.Models.Entity;

namespace IssueTrackingSystemApi.CommonTools
{
    public static class ExtandMethod
    {
        /// <summary>
        /// class轉換
        /// </summary>
        /// <typeparam name="T">轉換後的class</typeparam>
        /// <param name="source">資料來源</param>
        /// <param name="action">對轉換後class額外要做的事</param>
        /// <returns></returns>
        public static T ObjectConvert<T>(this object source, Action<T> action = null) where T : class, new()
        {
            if (source == null) return null;

            T target = new T();
            StringBuilder sql = new StringBuilder("");

            PropertyInfo[] targetProperties = typeof(T).GetProperties();
            PropertyInfo[] sourceProperties = source.GetType().GetProperties();

            List<string> columnList = new List<string>();
            List<string> conitionList = new List<string>();

            foreach (PropertyInfo property in targetProperties)
            {
                string propertyName = property.Name;
                var sourcePropertie = sourceProperties.FirstOrDefault(i => i.Name == propertyName);

                if (sourcePropertie == null) continue; // 找不到就下一個

                if (property.PropertyType != sourcePropertie.PropertyType) continue; // type對不起來也下一筆

                property.SetValue(target, sourcePropertie.GetValue(source));
            }

            action?.Invoke(target);

            return target;
        }

    }

}
