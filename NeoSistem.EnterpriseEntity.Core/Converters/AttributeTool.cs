// <copyright file="AttributeTool.cs" company="EnterpriseEntity">
//     Copyright (c) EnterpriseEntity. Her hakkı saklıdır.
// </copyright>
namespace NeoSistem.EnterpriseEntity.Core
{
    using System;
    using System.Reflection;

    /// <summary>
    /// Attribute Tool Class
    /// </summary>
    /// <typeparam name="T">Attribute class</typeparam>
    public class AttributeTool<T> where T : Attribute
    {
        /// <summary>
        /// Object Attribute bilgisi
        /// </summary>
        /// <param name="t">Object class</param>
        /// <returns>T tipi Attribute sonuçlar</returns>
        public static T GetAttribute(object t)
        {
            return (T)Attribute.GetCustomAttribute(t.GetType(), typeof(T));
        }

        /// <summary>
        /// Member Attribute bilgisi
        /// </summary>
        /// <param name="t">MemberInfo class</param>
        /// <returns>T tipi Attribute sonuçlar</returns>
        public static T GetAttribute(MemberInfo t)
        {
            return (T)Attribute.GetCustomAttribute(t, typeof(T));
        }

        /// <summary>
        /// Assembly Attribute bilgisi
        /// </summary>
        /// <param name="t">Assembly class</param>
        /// <returns>T tipi Attribute sonuçlar</returns>
        public static T GetAttribute(Assembly t)
        {
            return (T)Attribute.GetCustomAttribute(t, typeof(T));
        }

        /// <summary>
        /// Module Attribute bilgisi
        /// </summary>
        /// <param name="t">Module class</param>
        /// <returns>T tipi Attribute sonuçlar</returns>
        public static T GetAttribute(Module t)
        {
            return (T)Attribute.GetCustomAttribute(t, typeof(T));
        }

        /// <summary>
        /// Parameter Attribute bilgisi
        /// </summary>
        /// <param name="t">ParameterInfo class</param>
        /// <returns>T tipi Attribute sonuçlar</returns>
        public static T GetAttribute(ParameterInfo t)
        {
            return (T)Attribute.GetCustomAttribute(t, typeof(T));
        }
    }
}
