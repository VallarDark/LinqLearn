namespace LinqLearn.Services
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Helpers;
    using Models;
    public class GameFilterProvider : IGameFilterProvider
    {
        public Func<Game, bool> GetFilterFunction(GameSearchSettings searchSettingsCollection)
        {
            Expression<Func<Game, bool>> filterExpression = game => true;

            if (!string.IsNullOrEmpty(searchSettingsCollection.Name))
            {
                filterExpression = filterExpression.And(g => g.Name == searchSettingsCollection.Name);
            }

            if (searchSettingsCollection is { MinPrice: >= 0, MaxPrice: { } }
                && searchSettingsCollection.MaxPrice >= searchSettingsCollection.MinPrice)
            {
                filterExpression = filterExpression.And(g => g.Price >= searchSettingsCollection.MinPrice
                                                             && g.Price <= searchSettingsCollection.MaxPrice);
            }

            if (searchSettingsCollection.Genres is { Count: > 0 })
            {
                filterExpression = filterExpression.And(g => g.Genres.Any(genre => searchSettingsCollection.Genres
                    .Any(searchGenre => genre == searchGenre)));
            }


            return filterExpression.Compile();
        }
    }
}