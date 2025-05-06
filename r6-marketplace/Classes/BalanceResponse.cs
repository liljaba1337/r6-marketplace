using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace r6_marketplace.Classes.BalanceResponse
{
    internal class Data
    {
        public Game game { get; set; }
    }

    internal class Game
    {
        public string id { get; set; }
        public Viewer viewer { get; set; }
        public string __typename { get; set; }
    }

    internal class Meta
    {
        public string id { get; set; }
        public SecondaryStoreItem secondaryStoreItem { get; set; }
        public string __typename { get; set; }
        public int quantity { get; set; }
    }

    internal class Root
    {
        public Data data { get; set; }
    }

    internal class SecondaryStoreItem
    {
        public Meta meta { get; set; }
        public string __typename { get; set; }
    }

    internal class Viewer
    {
        public Meta meta { get; set; }
        public string __typename { get; set; }
    }


}
