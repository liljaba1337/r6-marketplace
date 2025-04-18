using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace r6_marketplace.Utils
{
    public class Data
    {
        /// <summary>
        /// Langugage codes for the API. Uncommon ones like jp or ko are not yet supported (by this package, not API).
        /// </summary>
        public enum Local
        {
            de,
            en,
            es,
            fr,
            it,
            pl,
            ru,
            tr
        }
        public enum Weapon
        {

        }
        public static readonly Uri dataUri = new Uri("https://public-ubiservices.ubi.com/v1/profiles/me/uplay/graphql");
        public static readonly Uri authUri = new Uri("https://public-ubiservices.ubi.com/v3/profiles/sessions");
        public static string FormatLanguage(Local lang)
        {
            return lang switch
            {
                Local.de => "de-DE",
                Local.en => "en-US",
                Local.es => "es-ES",
                Local.fr => "fr-FR",
                Local.it => "it-IT",
                Local.pl => "pl-PL",
                Local.ru => "ru-RU",
                Local.tr => "tr-TR",
                _ => throw new ArgumentOutOfRangeException(nameof(lang), lang, null)
            };
        }
    }
}
