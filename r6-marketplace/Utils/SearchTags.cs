#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace r6_marketplace.Utils
{
    public static class SearchTags
    {
        internal static (List<string>, List<string>) PrepareTags(IEnumerable<Enum>? filters)
        {
            List<string> types = new();
            List<string> tags = new();
            if (filters == null || !filters.Any())
                return (types, tags);
            if (filters != null && filters.Any())
            {
                foreach (var element in filters)
                {
                    if (element is Type)
                    {
                        types.Add(element.ToString());
                    }
                    else if (allowedEnumTypes.Contains(element.GetType()))
                    {
                        tags.Add(GetAPIName(element.ToString()));
                    }
                }
            }
            return (types, tags);
        }
        private static readonly List<System.Type> allowedEnumTypes = new List<System.Type>
            {
                typeof(Rarity),
                typeof(Weapon),
                typeof(Operator),
                typeof(Season),
                typeof(EsportsTeam),
                typeof(Event)
            };
        public enum Rarity
        {
            rarity_uncommon,
            rarity_rare,
            /// <summary>
            /// This is epic I believe
            /// </summary>
            rarity_superrare,
            rarity_legendary,
        }
        public enum Weapon
        {
            AUG__A2,
            SG_CQB,
            GSH_18,
            MK17__CQB,
            M590A1,
            LFP586,
            V308,
            LMG_E,
            _9mm__C1,
            SDP__9mm,
            TYPE_89,
            M1014,
            OTs_03,
            M12,
            _417,
            K1A,
            M45__MEUSOC,
            Bailiff__410,
            _556XI,
            USP40,
            TCSG12,
            ARX200,
            G8A1,
            SUPERNOVA,
            COMMANDO__9,
            CAMRS,
            AR_1550,
            P9,
            AK_12,
            SUPER__90,
            P_10C,
            PARA_308,
            MP5K,
            M870,
            SPEAR__308,
            _57__USG,
            MP5,
            UMP45,
            FO_12,
            SMG_11,
            ITA12L,
            P12,
            C7E,
            AR33,
            AK_74M,
            _1911__TACOPS,
            T_5__SMG,
            SASG_12,
            SPSMG9,
            DP27,
            SUPER__SHORTY,
            VECTOR__45__ACP,
            ACS12,
            SIX12__SD,
            F2,
            L85A2,
            ALDA__556,
            Mx4__Storm,
            M249__SAW,
            _552__COMMANDO,
            SPAS_12,
            POF9,
            PCX_33,
            FMG_9,
            _6P41,
            SCORPION__EVO__3__A1,
            BOSG122,
            M762,
            T_95__LSW,
            P90,
            KERATOS__357,
            Mk__14__EBR,
            SR_25,
            UZK50GI,
            SPAS_15,
            Q_929,
            F90,
            MP5SD,
            P229,
            GONNE_6,
            BEARING__9,
            M249,
            PDW9,
            PMM,
            MPX,
            SC3000K,
            SIX12,
            _9x19VSN,
            D_50,
            R4_C,
            _416_C__CARBINE,
            G36C,
            MK1__9mm,
            MP7,
            SMG_12,
            M4,
            C75__Auto,
            ITA12S,
            P226__MK__25,
            AUG__A3,
            C8_SFW,
            P10__RONI,
            PRB92,
            CSRX300,
            RG15,
            _44__Mag__Semi_Auto
        }
        public enum Operator
        {
            THUNDERBIRD,
            LESION,
            RAUORA,
            WARDEN,
            STRIKER,
            FINKA,
            RAM,
            NOMAD,
            BANDIT,
            ACE,
            NOKK,
            MAESTRO,
            MIRA,
            CASTLE,
            ZOFIA,
            FROST,
            KALI,
            THATCHER,
            DOC,
            IANA,
            KAPKAN,
            MELUSI,
            SKOPOS,
            VIGIL,
            ROOK,
            FENRIR,
            WAMAI,
            BRAVA,
            ELA,
            JACKAL,
            MONTAGNE,
            GRIM,
            FLORES,
            SMOKE,
            THERMITE,
            BUCK,
            HIBANA,
            GLAZ,
            PULSE,
            TWITCH,
            LION,
            TUBARAO,
            JAGER,
            GRIDLOCK,
            AMARU,
            ALIBI,
            ORYX,
            CAVEIRA,
            GOYO,
            SENS,
            YING,
            BLITZ,
            SOLIS,
            ECHO,
            IQ,
            ZERO,
            SENTRY,
            ARUNI,
            THORN,
            DEIMOS,
            CAPITAO,
            MAVERICK,
            MOZZIE,
            MUTE,
            ASH,
            CLASH,
            OSA,
            VALKYRIE,
            TACHANKA,
            SLEDGE,
            KAID,
            AZAMI,
            DOKKAEBI,
            BLACKBEARD,
            FUZE
        }
        public enum Season
        {
            Y1S1,
            Y1S2,
            Y1S3,
            Y1S4,
            Y1MC,
            Y2S1,
            Y2S2,
            Y2S3,
            Y2S4,
            Y3S1,
            Y3S2,
            Y3S3,
            Y3S4,
            Y4S1,
            Y4S2,
            Y4S3,
            Y4S4,
            Y5S1,
            Y5S2,
            Y5S3,
            Y5S4,
            Y6S1,
            Y6S2,
            Y6S3,
            Y6S4,
            Y7S1,
            Y7S2,
            Y7S3,
            Y7S4,
            Y8S1,
            Y8S2,
            Y8S3,
            Y8S4,
            Y9S1,
            Y9S2,
            Y9S3,
            Y9S4
        }
        public enum Type
        {
            GadgetSkin,
            DroneSkin,
            Charm,
            CharacterUniform,
            WeaponSkin,
            OperatorCardPortrait,
            WeaponAttachmentSkinSet,
            CharacterHeadgear,
            OperatorCardBackground
        }
        public enum EsportsTeam
        {
            Natus_Vincere,
            FAV_gaming,
            Santos_E_Sports,
            MNM_Gaming,
            Mirage,
            Astralis,
            Team_Empire,
            QConfirm,
            GUTS_Gaming,
            Heroic,
            Chiefs_Esports_Club,
            KOI,
            Team_Vitality,
            _00_NATION,
            Parabellum_Esports,
            Giants_Gaming,
            Cloud9,
            ORDER,
            XSET
        }
        public enum Event
        {
            telly,
            doktorcurse,
            showdown,
            snowball,
            quarantine,
            frosty,
            toky,
            rimv2,
            beret,
            mafia,
            bot
        }

        /// <summary>
        /// Returns the original name of a tag, replacing underscores with dashes, inserting spaces and removing leading underscores if present
        /// </summary>
        public static string GetOriginalName(this Enum Name) => Name.ToString().GetOriginalName();
        /// <summary>
        /// Returns the original name of a tag, replacing underscores with dashes, inserting spaces and removing leading underscores if present
        /// </summary>
        public static string GetOriginalName(this string Name)
        {
            if (Name.Length > 1 && Name[0] == '_' && char.IsDigit(Name[1]))
            {
                Name = Name.Substring(1);
            }
            Name = Name.Replace("__", " ").Replace("_", "-");
            return Name;
        }
        /// <summary>
        /// Returns the name of a tag, used by the Ubisoft API
        /// </summary>
        public static string GetAPIName(string name)
        {
            if (name.Length > 1 && name[0] == '_' && char.IsDigit(name[1]))
            {
                name = name.Substring(1);
            }
            name = name.Replace("__", " ").Replace("_", "-").Replace(" ", "_"); // fuck this
            return name;
        }
    }
}
