﻿using Microsoft.EntityFrameworkCore.Query;
using RealEstate.Base;
using RealEstate.Base.Enums;
using RealEstate.Services.BaseLog;
using RealEstate.Services.Database.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RealEstate.Services.Extensions
{
    public static class QueryFilterExtensions
    {
        public static IOrderedQueryable<TSource> OrderDescendingByCreationDateTime<TSource>(this IQueryable<TSource> entities) where TSource : BaseEntity
        {
            var source = entities.OrderByDescending(x => x.Audits.FirstOrDefault(v => v.Type == LogTypeEnum.Create).DateTime);
            return source;
        }

        public static IOrderedQueryable<TSource> OrderByCreationDateTime<TSource>(this IQueryable<TSource> entities) where TSource : BaseEntity
        {
            var source = entities.OrderBy(x => x.Audits.FirstOrDefault(v => v.Type == LogTypeEnum.Create).DateTime);
            return source;
        }

        //public static int IsJson<TSource>(this string entity, Func<TSource, string> predicate)
        //{
        //    var source = entity.Where(s => Convert.ToInt32(IsJson(predicate.Invoke(s))) > 1);
        //    return entity;
        //}

        public static string JsonValue(string expression, [NotParameterized] string path) => throw new NotSupportedException();

        public static IOrderedQueryable<TSource> OrderDescendingByCreationDateTime<TSource>(this IQueryable<TSource> entities, Func<LogJsonEntity, bool> predicate) where TSource : BaseEntity
        {
            var source = entities.OrderByDescending(x => x.Audits.FirstOrDefault(predicate).DateTime);
            return source;
        }

        public static IOrderedQueryable<TSource> OrderByCreationDateTime<TSource>(this IQueryable<TSource> entities, Func<LogJsonEntity, bool> predicate) where TSource : BaseEntity
        {
            var source = entities.OrderBy(x => x.Audits.FirstOrDefault(predicate).DateTime);
            return source;
        }

        public static T First<T>(this List<T> list) where T : BaseLogViewModel
        {
            var result = list?.OrderByCreationDateTime().FirstOrDefault();
            return result;
        }

        public static IOrderedEnumerable<TSource> OrderDescendingByCreationDateTime<TSource>(this ICollection<TSource> sources) where TSource : BaseEntity
        {
            var source = sources
                .OrderByDescending(x => x.Audits.FirstOrDefault(v => v.Type == LogTypeEnum.Create).DateTime);

            return source;
        }

        public static IOrderedEnumerable<TSource> OrderDescendingByCreationDateTime<TSource>(this ICollection<TSource> sources, Func<LogJsonEntity, bool> predicate) where TSource : BaseEntity
        {
            var source = sources
                .OrderByDescending(x => x.Audits.FirstOrDefault(predicate).DateTime);

            return source;
        }

        public static IOrderedEnumerable<TSource> OrderByCreationDateTime<TSource>(this ICollection<TSource> sources) where TSource : BaseEntity
        {
            var source = sources
                .OrderBy(x => x.Audits.FirstOrDefault(v => v.Type == LogTypeEnum.Create).DateTime);

            return source;
        }

        public static IOrderedEnumerable<TSource> OrderByCreationDateTime<TSource>(this ICollection<TSource> sources, Func<LogJsonEntity, bool> predicate) where TSource : BaseEntity
        {
            var source = sources.OrderBy(x => x.Audits.FirstOrDefault(predicate).DateTime);
            return source;
        }

        public static IOrderedEnumerable<TModel> OrderByCreationDateTime<TModel>(this List<TModel> sources) where TModel : BaseLogViewModel
        {
            var source = sources.OrderBy(x => x.Logs.Create.DateTime);
            return source;
        }

        public static IOrderedEnumerable<TModel> OrderByCreationDateTime<TModel>(this List<TModel> sources, Func<TModel, bool> predicate) where TModel : BaseLogViewModel
        {
            var source = sources.OrderBy(predicate);
            return source;
        }

        public static IOrderedEnumerable<TModel> OrderDescendingByCreationDateTime<TModel>(this List<TModel> sources) where TModel : BaseLogViewModel
        {
            var source = sources.OrderByDescending(x => x.Logs.Create.DateTime);
            return source;
        }

        public static IOrderedEnumerable<TModel> OrderDescendingByCreationDateTime<TModel>(this List<TModel> sources, Func<TModel, bool> predicate) where TModel : BaseLogViewModel
        {
            var source = sources.OrderByDescending(predicate);
            return source;
        }

        public static List<TModel> Map<TEntity, TModel>(this List<TEntity> model) where TEntity : BaseEntity where TModel : BaseLogViewModel
        {
            if (model?.Any() != true)
                return default;

            var result = model
                .Select(entity => entity.Map<TModel>())
                .Where(x => x != null)
                .ToHasNotNullList();
            return result;
        }

        public static IList<TEntity> WhereNotDeleted<TEntity>(this ICollection<TEntity> entities) where TEntity : BaseEntity
        {
            if (entities?.Any() != true)
                return default;

            var result = entities.Where(entity => string.IsNullOrEmpty(entity.Audit)
                                                  || entity.Audits.OrderByDescending(x => x.DateTime).FirstOrDefault().Type != LogTypeEnum.Delete);
            return result.ToList();
        }

        public static IQueryable<TEntity> WhereNotDeleted<TEntity>(this IQueryable<TEntity> entities) where TEntity : BaseEntity
        {
            var result = entities.Where(entity => string.IsNullOrEmpty(entity.Audit)
                                                  || entity.Audits.OrderByDescending(x => x.DateTime).FirstOrDefault().Type != LogTypeEnum.Delete);
            return result;
        }
    }
}