using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Sistema.Api.dll.Repositorio.Util
{
    public static class ListSorter
    {
        public enum SortingOrder
        {
            Ascending,
            Descending,
        };

        public static List<T> Sort<T>(IEnumerable<T> toSort, Dictionary<string, SortingOrder> sortOptions)
        {
            IOrderedEnumerable<T> orderedList = null;

            foreach (KeyValuePair<string, SortingOrder> entry in sortOptions)
            {
                if (orderedList != null)
                {
                    if (entry.Value == SortingOrder.Ascending)
                    {
                        orderedList = orderedList.ApplyOrder<T>(entry.Key, "ThenBy");
                    }
                    else
                    {
                        orderedList = orderedList.ApplyOrder<T>(entry.Key, "ThenByDescending");
                    }
                }
                else
                {
                    if (entry.Value == SortingOrder.Ascending)
                    {
                        orderedList = toSort.ApplyOrder<T>(entry.Key, "OrderBy");
                    }
                    else
                    {
                        orderedList = toSort.ApplyOrder<T>(entry.Key, "OrderByDescending");
                    }
                }
            }

            return orderedList.ToList();
        }

        private static IOrderedEnumerable<T> ApplyOrder<T>(this IEnumerable<T> source, string property, string methodName)
        {
            ParameterExpression param = Expression.Parameter(typeof(T), "x");
            Expression expr = param;
            foreach (string prop in property.Split('.'))
            {

                expr = Expression.PropertyOrField(expr, prop);
            }
            Type delegateType = typeof(Func<,>).MakeGenericType(typeof(T), expr.Type);
            LambdaExpression lambda = Expression.Lambda(delegateType, expr, param);

            MethodInfo mi = typeof(Enumerable).GetMethods().Single(
                    method => method.Name == methodName
                            && method.IsGenericMethodDefinition
                            && method.GetGenericArguments().Length == 2
                            && method.GetParameters().Length == 2)
                    .MakeGenericMethod(typeof(T), expr.Type);
            return (IOrderedEnumerable<T>)mi.Invoke(null, new object[] { source, lambda.Compile() });
        }
    }
}
