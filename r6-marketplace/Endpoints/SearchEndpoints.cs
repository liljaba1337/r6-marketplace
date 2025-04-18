using System;
using System.Collections.Generic;
using System.Linq;
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
        /// <summary>
        /// Get all the search tags you can use in INSERTHERE.
        /// </summary>
        /// <returns>An instance of <see cref="Classes.Tags.Tags"/>.</returns>
        public async Task<Classes.Tags.Tags> GetSearchTags()
        {
            web.EnsureAuthenticated();
            var response = await web.Post(Data.dataUri, RequestBodies.GetSearchTagsData);

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

        public async Task<object> SearchItem(string name, List<string> tags)
        {
            web.EnsureAuthenticated();


            return null;
        }
    }
}
