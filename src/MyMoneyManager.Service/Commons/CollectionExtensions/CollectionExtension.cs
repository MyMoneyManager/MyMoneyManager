using Newtonsoft.Json;
using MyMoneyManager.Shared.Helpers;
using MyMoneyManager.Domain.Commons;
using MyMoneyManager.Service.Exceptions;
using MyMoneyManager.Domain.Configurations;


namespace MyMoneyManager.Service.Commons.CollectionExtensions;

public static class CollectionExtension
{
    public static IQueryable<TEntity> ToPagedList<TEntity>(this IQueryable<TEntity> entities, PaginationParams @params)
            where TEntity : Auditable
    {
        var metaData = new PaginationMetaData(entities.Count(), @params);

        var json = JsonConvert.SerializeObject(metaData);

        if (HttpContextHelper.ResponseHeaders != null)
        {
            if (HttpContextHelper.ResponseHeaders.ContainsKey("X-Pagination"))
                HttpContextHelper.ResponseHeaders.Remove("X-Pagination");

            HttpContextHelper.ResponseHeaders.Add("X-Pagination", json);
        }

        return @params.PageIndex > 0 && @params.PageSize > 0 ?
            entities.OrderBy(e => e.Id)
                .Skip((@params.PageIndex - 1) * @params.PageSize).Take(@params.PageSize) :
                    throw new CustomException(400, "Please, enter valid numbers");
    }
}
