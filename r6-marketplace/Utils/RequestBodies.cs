using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using r6_marketplace.Utils.RequestBody.Shared;

namespace r6_marketplace.Utils.RequestBody
{
    file class RequestQueries
    {
        internal const string GetItemData =
            "query GetItemDetails($spaceId: String!, $itemId: String!, $tradeId: String!, $fetchTrade: Boolean!) {  game(spaceId: $spaceId) {    id    marketableItem(itemId: $itemId) { id item {   ...SecondaryStoreItemFragment   ...SecondaryStoreItemOwnershipFragment   __typename } marketData {   ...MarketDataFragment   __typename } paymentLimitations {   id   paymentItemId   minPrice   maxPrice   __typename } __typename    }    viewer { meta {   id   trades(filterBy: {states: [Created], itemIds: [$itemId]}) {     nodes {  ...TradeFragment  __typename     }     __typename   }   trade(tradeId: $tradeId) @include(if: $fetchTrade) {     ...TradeFragment     __typename   }   __typename } __typename    }    __typename  }}fragment SecondaryStoreItemFragment on SecondaryStoreItem {  id  assetUrl  itemId  name  tags  type  __typename}fragment SecondaryStoreItemOwnershipFragment on SecondaryStoreItem {  id  viewer {    meta { id isOwned quantity __typename    }    __typename  }  __typename}fragment MarketDataFragment on MarketableItemMarketData {  id  sellStats {    id    paymentItemId    lowestPrice    highestPrice    activeCount    __typename  }  buyStats {    id    paymentItemId    lowestPrice    highestPrice    activeCount    __typename  }  lastSoldAt {    id    paymentItemId    price    performedAt    __typename  }  __typename}fragment TradeFragment on Trade {  id  tradeId  state  category  createdAt  expiresAt  lastModifiedAt  failures  tradeItems {    id    item { ...SecondaryStoreItemFragment ...SecondaryStoreItemOwnershipFragment __typename    }    __typename  }  payment {    id    item { ...SecondaryStoreItemQuantityFragment __typename    }    price    transactionFee    __typename  }  paymentOptions {    id    item { ...SecondaryStoreItemQuantityFragment __typename    }    price    transactionFee    __typename  }  paymentProposal {    id    item { ...SecondaryStoreItemQuantityFragment __typename    }    price    __typename  }  viewer {    meta { id tradesLimitations {   ...TradesLimitationsFragment   __typename } __typename    }    __typename  }  __typename}fragment SecondaryStoreItemQuantityFragment on SecondaryStoreItem {  id  viewer {    meta { id quantity __typename    }    __typename  }  __typename}fragment TradesLimitationsFragment on UserGameTradesLimitations {  id  buy {    id    resolvedTransactionCount    resolvedTransactionPeriodInMinutes    activeTransactionCount    __typename  }  sell {    id    resolvedTransactionCount    resolvedTransactionPeriodInMinutes    activeTransactionCount    resaleLocks { id itemId expiresAt __typename    }    __typename  }  __typename}";
        internal const string GetItemPriceHistoryData =
            "query GetItemPriceHistory($spaceId: String!, $itemId: String!, $paymentItemId: String!) {\n  game(spaceId: $spaceId) {\n    id\n    marketableItem(itemId: $itemId) {\n      id\n      priceHistory(paymentItemId: $paymentItemId) {\n        id\n        date\n        lowestPrice\n        averagePrice\n        highestPrice\n        itemsCount\n        __typename\n      }\n      __typename\n    }\n    __typename\n  }\n}";
        internal const string GetSearchTagsData =
            "[\r\n    {\r\n        \"operationName\": \"GetMarketplaceGameConfig\",\r\n        \"variables\": {\r\n            \"spaceId\": \"0d2ae42d-4c27-4cb7-af6c-2099062302bb\"\r\n        },\r\n        \"query\": \"query GetMarketplaceGameConfig($spaceId: String!) {\\n  game(spaceId: $spaceId) {\\n    id\\n    marketplace {\\n      assets {\\n        backgroundImage\\n        homeAssets {\\n          buyTab\\n          sellTab\\n          transactionsTab\\n          __typename\\n        }\\n        __typename\\n      }\\n      privilege {\\n        tradePrivilegeId\\n        __typename\\n      }\\n      currency {\\n        currencyId\\n        currencyUrl\\n        __typename\\n      }\\n      contact {\\n        bugUrl\\n        bugLabel\\n        __typename\\n      }\\n      tags {\\n        color\\n        value\\n        iconUrl\\n        displayName\\n        __typename\\n      }\\n      tagGroups {\\n        type\\n        values\\n        displayName\\n        __typename\\n      }\\n      types {\\n        value\\n        iconUrl\\n        displayName\\n        __typename\\n      }\\n      homeTypes {\\n        value\\n        displayName\\n        iconUrl\\n        __typename\\n      }\\n      filters {\\n        categoryId\\n        type\\n        displayName\\n        sortType\\n        sortDirection\\n        values {\\n          value\\n          displayName\\n          __typename\\n        }\\n        __typename\\n      }\\n      recommendations {\\n        userItemRecommendations {\\n          projectId\\n          categoryId\\n          __typename\\n        }\\n        __typename\\n      }\\n      tabConfig {\\n        buy {\\n          filterSelection {\\n            categoryId\\n            values\\n            __typename\\n          }\\n          sortSelection {\\n            field\\n            direction\\n            orderType\\n            __typename\\n          }\\n          __typename\\n        }\\n        sell {\\n          filterSelection {\\n            categoryId\\n            values\\n            __typename\\n          }\\n          sortSelection {\\n            field\\n            direction\\n            orderType\\n            __typename\\n          }\\n          __typename\\n        }\\n        __typename\\n      }\\n      userResearch {\\n        link\\n        linkId\\n        homeBanner {\\n          iconUrl\\n          title\\n          description\\n          buttonLabel\\n          __typename\\n        }\\n        footerLink {\\n          iconUrl\\n          buttonLabel\\n          __typename\\n        }\\n        __typename\\n      }\\n      __typename\\n    }\\n    __typename\\n  }\\n}\"\r\n    }\r\n]";
        internal const string SearchData =
            "[{\"operationName\":\"GetMarketableItems\",\"variables\":{\"withOwnership\":true,\"spaceId\":\"0d2ae42d-4c27-4cb7-af6c-2099062302bb\",\"limit\":{LIMIT},\"offset\":{OFFSET},\"filterBy\":{\"types\":{TYPES},\"tags\":[[{TAGS}]],\"text\": \"{TEXT}\"}\"sortBy\":{\"field\":\"{SORTBY}\",{ORDERTYPE}\"direction\":\"{DIRECTION}\",\"paymentItemId\":\"9ef71262-515b-46e8-b9a8-b6b6ad456c67\"}},\"query\":\"query GetMarketableItems($spaceId: String!, $limit: Int!, $offset: Int, $filterBy: MarketableItemFilter, $withOwnership: Boolean = true, $sortBy: MarketableItemSort) {\\n  game(spaceId: $spaceId) {\\n    id\\n    marketableItems(\\n      limit: $limit\\n      offset: $offset\\n      filterBy: $filterBy\\n      sortBy: $sortBy\\n      withMarketData: true\\n    ) {\\n      nodes {\\n        ...MarketableItemFragment\\n        __typename\\n      }\\n      totalCount\\n      __typename\\n    }\\n    __typename\\n  }\\n}\\n\\nfragment MarketableItemFragment on MarketableItem {\\n  id\\n  item {\\n    ...SecondaryStoreItemFragment\\n    ...SecondaryStoreItemOwnershipFragment @include(if: $withOwnership)\\n    __typename\\n  }\\n  marketData {\\n    ...MarketDataFragment\\n    __typename\\n  }\\n  viewer {\\n    meta {\\n      id\\n      activeTrade {\\n        ...TradeFragment\\n        __typename\\n      }\\n      __typename\\n    }\\n    __typename\\n  }\\n  __typename\\n}\\n\\nfragment SecondaryStoreItemFragment on SecondaryStoreItem {\\n  id\\n  assetUrl\\n  itemId\\n  name\\n  tags\\n  type\\n  __typename\\n}\\n\\nfragment SecondaryStoreItemOwnershipFragment on SecondaryStoreItem {\\n  id\\n  viewer {\\n    meta {\\n      id\\n      isOwned\\n      quantity\\n      __typename\\n    }\\n    __typename\\n  }\\n  __typename\\n}\\n\\nfragment MarketDataFragment on MarketableItemMarketData {\\n  id\\n  sellStats {\\n    id\\n    paymentItemId\\n    lowestPrice\\n    highestPrice\\n    activeCount\\n    __typename\\n  }\\n  buyStats {\\n    id\\n    paymentItemId\\n    lowestPrice\\n    highestPrice\\n    activeCount\\n    __typename\\n  }\\n  lastSoldAt {\\n    id\\n    paymentItemId\\n    price\\n    performedAt\\n    __typename\\n  }\\n  __typename\\n}\\n\\nfragment TradeFragment on Trade {\\n  id\\n  tradeId\\n  state\\n  category\\n  createdAt\\n  expiresAt\\n  lastModifiedAt\\n  failures\\n  tradeItems {\\n    id\\n    item {\\n      ...SecondaryStoreItemFragment\\n      ...SecondaryStoreItemOwnershipFragment\\n      __typename\\n    }\\n    __typename\\n  }\\n  payment {\\n    id\\n    item {\\n      ...SecondaryStoreItemQuantityFragment\\n      __typename\\n    }\\n    price\\n    transactionFee\\n    __typename\\n  }\\n  paymentOptions {\\n    id\\n    item {\\n      ...SecondaryStoreItemQuantityFragment\\n      __typename\\n    }\\n    price\\n    transactionFee\\n    __typename\\n  }\\n  paymentProposal {\\n    id\\n    item {\\n      ...SecondaryStoreItemQuantityFragment\\n      __typename\\n    }\\n    price\\n    __typename\\n  }\\n  viewer {\\n    meta {\\n      id\\n      tradesLimitations {\\n        ...TradesLimitationsFragment\\n        __typename\\n      }\\n      __typename\\n    }\\n    __typename\\n  }\\n  __typename\\n}\\n\\nfragment SecondaryStoreItemQuantityFragment on SecondaryStoreItem {\\n  id\\n  viewer {\\n    meta {\\n      id\\n      quantity\\n      __typename\\n    }\\n    __typename\\n  }\\n  __typename\\n}\\n\\nfragment TradesLimitationsFragment on UserGameTradesLimitations {\\n  id\\n  buy {\\n    id\\n    resolvedTransactionCount\\n    resolvedTransactionPeriodInMinutes\\n    activeTransactionCount\\n    __typename\\n  }\\n  sell {\\n    id\\n    resolvedTransactionCount\\n    resolvedTransactionPeriodInMinutes\\n    activeTransactionCount\\n    resaleLocks {\\n      id\\n      itemId\\n      expiresAt\\n      __typename\\n    }\\n    __typename\\n  }\\n  __typename\\n}\"}]";
    }
}

namespace r6_marketplace.Utils.RequestBody.Shared
{
    internal abstract class RequestRoot<TVariables>
    {
        public abstract string operationName { get; }
        public TVariables variables { get; }
        public abstract string query { get; }

        protected RequestRoot(TVariables variables)
        {
            this.variables = variables;
        }

        internal List<RequestRoot<TVariables>> AsList() => new() { this };
        internal string AsJson() => System.Text.Json.JsonSerializer.Serialize(AsList());
    }

    internal abstract class BaseVariables
    {
        public string spaceId { get; } = "0d2ae42d-4c27-4cb7-af6c-2099062302bb";
    }
}

namespace r6_marketplace.Utils.RequestBody.GetItemPriceHistory
{
    internal class Root : RequestRoot<Variables>
    {
        public Root(string itemId) : base(new Variables(itemId)) { }

        public override string operationName => "GetItemPriceHistory";
        public override string query => RequestQueries.GetItemPriceHistoryData;
    }

    internal class Variables : BaseVariables
    {
        public Variables(string itemId)
        {
            this.itemId = itemId;
        }

        public string itemId { get; }
        public string paymentItemId { get; } = "9ef71262-515b-46e8-b9a8-b6b6ad456c67";
    }
}

namespace r6_marketplace.Utils.RequestBody.GetItemDetails
{
    internal class Root : RequestRoot<Variables>
    {
        public Root(string itemId) : base(new Variables(itemId)) { }

        public override string operationName => "GetItemDetails";
        public override string query => RequestQueries.GetItemData;
    }

    internal class Variables : BaseVariables
    {
        public Variables(string itemId)
        {
            this.itemId = itemId;
        }

        public string itemId { get; }
        public string tradeId { get; } = "";
        public bool fetchTrade { get; } = false;
    }
}