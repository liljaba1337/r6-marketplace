﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using r6_marketplace.Extensions;
using r6_marketplace.Utils;
using r6_marketplace.Utils.Exceptions;

namespace r6_marketplace.Endpoints
{
    public class SearchEndpoints
    {
        private Web web;
        internal SearchEndpoints(Web web)
        {
            this.web = web;
        }

        public enum SortBy
        {
            PurchaseAvailaible,
            SaleAvailaible,
            LastSalePrice,
            ItemName
        }
        
        public enum SortDirection
        {
            /// <summary>
            /// Ascending / A-Z
            /// </summary>
            ASC,
            /// <summary>
            /// Descending / Z-A
            /// </summary>
            DESC
        }

        /// <summary>
        /// Gets all the search tags you can use in <see cref="SearchItem"/>.
        /// </summary>
        /// <returns>An instance of <see cref="Classes.Tags.Tags"/>.</returns>
        public async Task<Classes.Tags.Tags> GetSearchTags()
        {
            web.EnsureAuthenticated();
            var response = await web.Post(Data.dataUri);

            var json = await response.DeserializeAsyncSafe<List<Classes.Tags.RawData.Root>>(false);
            var tagGroups = json?[0].data.game.marketplace.tagGroups;
            var tagGroupDict = tagGroups?.ToDictionary(x => x.displayName, x => x.values);
            var tags = new Classes.Tags.Tags()
            {
                Rarity = tagGroupDict?.GetValueOrDefault("Rarity"),
                Esports_Team = tagGroupDict?.GetValueOrDefault("Esports Team"),
                Season = tagGroupDict?.GetValueOrDefault("Season"),
                Operator = tagGroupDict?.GetValueOrDefault("Operator"),
                Weapon = tagGroupDict?.GetValueOrDefault("Weapon"),
                Event = tagGroupDict?.GetValueOrDefault("Event"),
                Type = json?[0].data.game.marketplace.types.Select(x => x.value).ToList()
            };

            return tags;
        }

        public async Task<object> SearchItem(string? name = default, List<string>? tags = default,
            SortBy sortBy = SortBy.PurchaseAvailaible, SortDirection sortDirection = SortDirection.DESC,
            int limit = 40, int offset = 0)
        {
            web.EnsureAuthenticated();
            //string body = RequestBodies.SearchData
            //    .Replace("{NAME}", name + '*' ?? "")
            //    .Replace("{TAGS}", tags != null ? '"' + string.Join("\",\"", tags) + '"' : "")
            //    .Replace("{SORTBY}", sortBy.Format())
            //    .Replace("{ORDERTYPE}", sortBy)
            //    .Replace("{DIRECTION}", sortDirection.ToString())
            //    .Replace("{LIMIT}", limit.ToString())
            //    .Replace("{OFFSET}", offset.ToString());

            return null;
        }
    }
}
