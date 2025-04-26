using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using r6_marketplace.Extensions;
using r6_marketplace.RequestBodies.Shared;

namespace r6_marketplace.RequestBodies.SearchItems
{
    internal class Root : RequestRoot<Variables>
    {
        public Root(string text, int limit, int offset, List<string> types, List<string> tags,
            Endpoints.SearchEndpoints.SortBy sortBy, Endpoints.SearchEndpoints.SortDirection sortDirection) :
            base(new Variables(text, limit, offset, types, tags, sortBy, sortDirection))
        { }
        public override string operationName => "GetMarketableItems";
        public override string query => RequestBodies.RequestQueries.SearchData;
    }

    internal class Variables : BaseVariables
    {
        public Variables(string text, int limit, int offset, List<string> types, List<string> tags,
            Endpoints.SearchEndpoints.SortBy sortBy, Endpoints.SearchEndpoints.SortDirection sortDirection)
        {
            this.limit = limit;
            this.offset = offset;
            this.limit = limit;
            this.offset = offset;
            filterBy = new(types, tags, text);
            this.sortBy = new(sortBy.Format(), sortDirection.ToString());
        }

        int limit { get; }
        int offset { get; }
        public bool withOwnership { get; } = true;
        public SortBy sortBy { get; }
        public FilterBy filterBy { get; }
    }

    internal class SortBy
    {
        public SortBy(string field, string direction = "ASC")
        {
            this.field = field;
            this.direction = direction;
        }

        public string field { get; }
        public string direction { get; }
        public string paymentItemId { get; } = "9ef71262-515b-46e8-b9a8-b6b6ad456c67";
    }
    internal class FilterBy
    {
        public FilterBy(List<string> types, List<string> tags, string text)
        {
            this.types = types.Select(x => new List<string> { x }).ToList();
            this.tags = tags.Select(x => new List<string> { x }).ToList();
            this.text = text;
        }
        public List<List<string>> types { get; }
        public List<List<string>> tags { get; }
        public string text { get; }
    }
}
