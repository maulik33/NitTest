using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NursingLibrary.Common;
using NursingLibrary.Interfaces;

/// <summary>
/// Summary description for KTPSort
/// </summary>
public class KTPSort
{
    public static IEnumerable<T> Sort<T>(IEnumerable<T> data, SortInfo sortMetaData)
    {
        string sortExpression = sortMetaData.SortExpression;

        IQueryable<T> source = data.AsQueryable<T>();

        Type type = typeof(T);
        string methodName = (sortMetaData.Direction == SortOrder.Ascending) ? "OrderBy" : "OrderByDescending";

        IList<Type> types;
        var parameter = Expression.Parameter(type, "p");
        var propertyAccess = GetMemberExpression(parameter, type, sortExpression, out types);
        var orderByExp = Expression.Lambda(propertyAccess, parameter);

        MethodCallExpression resultExp = Expression.Call(typeof(Queryable), methodName,
            types.ToArray(),
            source.Expression, Expression.Quote(orderByExp));

        return source.Provider.CreateQuery<T>(resultExp).ToList();
    }

    public static Expression GetMemberExpression(Expression parameter, Type type, string propertyName, out IList<Type> types)
    {
        Expression expression = parameter;
        string[] propertyParts = propertyName.Split('.');
        Type currentType = type;
        types = new List<Type>();
        types.Add(currentType);
        for (int i = 0; i < propertyParts.Length; i++)
        {
            expression = GetMemberExpression(ref currentType, expression, propertyParts[i]);
        }

        types.Add(currentType);
        return expression;
    }

    public static Expression GetMemberExpression(ref Type type, Expression parameter, string propertyName)
    {
        var property = type.GetProperty(propertyName);
        if (property == null)
        {
            throw new InvalidOperationException(string.Format("Cannot find Property {0} in {1}", propertyName, type.FullName));
        }

        type = property.PropertyType;
        return Expression.MakeMemberAccess(parameter, property);
    }
}
