using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace r6_marketplace.Extensions
{
    public static class HttpExtensions
    {
        public static async Task<string> ReadAsStringAsyncSafe(this HttpContent content)
        {
            return await content.ReadAsStringAsync().ConfigureAwait(false);
        }
    }
}
