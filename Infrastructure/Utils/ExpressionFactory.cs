using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace BookMania.Infrastructure.Utils
{
    public static class ExpressionFactory
    {
        //public static IQueryable<TSource> Or<TSource, TKey>(IQueryable<TSource> source, Expression<Func<TSource, TKey>> queryProperty, params TKey[] criterias)
        public static Func<TSource, bool> Or<TSource, TKey>(IQueryable<TSource> source, Expression<Func<TSource, TKey>> queryProperty, params TKey[] criterias)
        {
            ParameterExpression paramExpr = Expression.Parameter(typeof(TSource), "x");
            IEnumerable<Expression> listOfOrStatements = criterias.Select(c =>
            {
                // (x.propertyName == aConst)
                var propertyName = (queryProperty.Body as MemberExpression)?.Member.Name;
                Expression left = Expression.Property(paramExpr, propertyName);
                Expression right = Expression.Constant(c);
                return (Expression)Expression.Equal(left, right);
            });

            // Creating a (x => (x.propertyName == aConst) || (x.propertyName == bConst) || (x.propertyName == cConst) ...)
            var predicateBody = listOfOrStatements.Aggregate((acc, curr) => Expression.OrElse(acc, curr));
            //var whereExpr = Expression.Call(
            //    typeof(Queryable),
            //    "Where",
            //    new Type[] {source.ElementType },
            //    source.Expression,
            //    Expression.Lambda<Func<TSource, TKey>>(predicateBody, new ParameterExpression[] { paramExpr })
            //);
            //return source.Provider.CreateQuery<TSource>(whereExpr);
            //return source.Provider.CreateQuery<TSource>(predicateBody);
            var lambda = Expression.Lambda< Func<TSource, bool>>(predicateBody, new ParameterExpression[] { paramExpr });
            return lambda.Compile();
        }
    }
}
