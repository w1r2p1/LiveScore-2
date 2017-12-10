using LiveScore.Contracts;
using LiveScore.Exceptions;
using LiveScore.Filters;
using LiveScore.Filters.Games;
using LiveScore.Models.Business;
using LiveScore.Models.Request;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace LiveScoreTests.Filters
{
    public class GamesFilterFactoryTests
    {
        private GamesFilterFactory filterFactory;

        public GamesFilterFactoryTests()
        {
            filterFactory = new GamesFilterFactory();
        }

        #region Test Create mehthod for single filter

        [Theory]
        [InlineData(FilterType.StartDate, "2017-09-28T20:45:00", FilterRelation.AND)]
        [InlineData(FilterType.StartDate, "2017-09-28T20:45:00", FilterRelation.OR)]
        public void CreateTheoryStartDateSuccess(FilterType type, string value, FilterRelation relation)
        {
            var filter = new Filter
            {
                Name = type,
                Value = value,
                Relation = relation
            };

            var filterModel = filterFactory.Create(filter);
            Assert.NotNull(filterModel);
            Assert.IsType<GamesStartDateFilter>(filterModel);
        }

        [Theory]
        [InlineData(FilterType.EndDate, "2017-09-28T20:45:00", FilterRelation.AND)]
        [InlineData(FilterType.EndDate, "2017-09-28T20:45:00", FilterRelation.OR)]
        public void CreateTheoryEndDateSuccess(FilterType type, string value, FilterRelation relation)
        {
            var filter = new Filter
            {
                Name = type,
                Value = value,
                Relation = relation
            };

            var filterModel = filterFactory.Create(filter);
            Assert.NotNull(filterModel);
            Assert.IsType<GamesEndDateFilter>(filterModel);
        }

        [Theory]
        [InlineData(FilterType.Team, "1", FilterRelation.AND)]
        [InlineData(FilterType.Team, "1", FilterRelation.OR)]
        public void CreateTheoryTeamSuccess(FilterType type, string value, FilterRelation relation)
        {
            var filter = new Filter
            {
                Name = type,
                Value = value,
                Relation = relation
            };

            var filterModel = filterFactory.Create(filter);
            Assert.NotNull(filterModel);
            Assert.IsType<GamesTeamFilter>(filterModel);
        }

        [Theory]
        [InlineData(FilterType.Group, "1", FilterRelation.AND)]
        [InlineData(FilterType.Group, "1", FilterRelation.OR)]
        public void CreateTheoryGroupSuccess(FilterType type, string value, FilterRelation relation)
        {
            var filter = new Filter
            {
                Name = type,
                Value = value,
                Relation = relation
            };

            var filterModel = filterFactory.Create(filter);
            Assert.NotNull(filterModel);
            Assert.IsType<GamesGroupFilter>(filterModel);
        }

        [Theory]
        [InlineData(FilterType.StartDate, "9999-99-99T99:99:99")]
        [InlineData(FilterType.EndDate, "9999-99-99T99:99:99")]
        [InlineData(FilterType.StartDate, "random text")]
        [InlineData(FilterType.EndDate, "random text")]
        public void CreateTheoryDateFormatFailure(FilterType type, string value)
        {
            var filter = new Filter
            {
                Name = type,
                Value = value,
                Relation = FilterRelation.AND
            };

            Assert.Throws<ClientRequestException>(() => filterFactory.Create(filter));
        }

        [Theory]
        [InlineData(FilterType.Team, "")]
        [InlineData(FilterType.Team, null)]
        [InlineData(FilterType.Team, "random text")]
        [InlineData(FilterType.Team, "Red Star Belgrade")]
        public void CreateTheoryTeamIdFailure(FilterType type, string value)
        {
            var filter = new Filter
            {
                Name = type,
                Value = value,
                Relation = FilterRelation.AND
            };

            Assert.Throws<ClientRequestException>(() => filterFactory.Create(filter));
        }

        [Theory]
        [InlineData(FilterType.Group, "")]
        [InlineData(FilterType.Group, null)]
        [InlineData(FilterType.Group, "random text")]
        [InlineData(FilterType.Group, "A")]
        public void CreateTheoryGroupIdFailure(FilterType type, string value)
        {
            var filter = new Filter
            {
                Name = type,
                Value = value,
                Relation = FilterRelation.AND
            };

            Assert.Throws<ClientRequestException>(() => filterFactory.Create(filter));
        }

        #endregion Test Create mehthod for single filter

        #region Test Create mehthod for multiple filters

        [Theory]
        [InlineData(
            FilterType.StartDate, "2017-09-28T20:45:00", FilterRelation.AND,
            FilterType.EndDate, "2017-09-28T20:45:00", FilterRelation.AND)]
        public void CreateTheoryMultipleSuccess(
            FilterType type1, string value1, FilterRelation relation1,
            FilterType type2, string value2, FilterRelation relation2)
        {
            var filters = new List<Filter>
            {
                new Filter
                {
                    Name = type1,
                    Value = value1,
                    Relation = relation1
                },
                new Filter
                {
                    Name = type2,
                    Value = value2,
                    Relation = relation2
                }
            };

            var filterModels = filterFactory.Create(filters.ToArray());
            Assert.NotNull(filterModels);
            Assert.NotEmpty(filterModels);
            
            foreach(var filterModel in filterModels)
            {
                Assert.NotNull(filterModel);
                Assert.IsAssignableFrom<IFilter<Game>>(filterModel);
            }
        }

        [Theory]
        [InlineData(
            FilterType.StartDate, "2017-09-28T20:45:00", FilterRelation.AND,
            FilterType.EndDate, "9999-99-99T99:99:99", FilterRelation.AND)]
        public void CreateTheoryMultipleFailure(
            FilterType type1, string value1, FilterRelation relation1,
            FilterType type2, string value2, FilterRelation relation2)
        {
            var filters = new List<Filter>
            {
                new Filter
                {
                    Name = type1,
                    Value = value1,
                    Relation = relation1
                },
                new Filter
                {
                    Name = type2,
                    Value = value2,
                    Relation = relation2
                }
            };

            Assert.Throws<ClientRequestException>(() => filterFactory.Create(filters.ToArray()));
        }

        #endregion Test Create mehthod for multiple filters
    }
}
