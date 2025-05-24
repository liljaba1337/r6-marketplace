using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

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
                        tags.Add(GetOriginalName(element.ToString()));
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
            AUG_A2,
            SG_CQB,
            GSH_18,
            MK17_CQB,
            M590A1,
            LFP586,
            V308,
            LMG_E,
            _9mm_C1,
            SDP_9mm,
            TYPE_89,
            M1014,
            OTs_03,
            M12,
            _417,
            K1A,
            M45_MEUSOC,
            Bailiff_410,
            _556XI,
            USP40,
            TCSG12,
            ARX200,
            G8A1,
            SUPERNOVA,
            COMMANDO_9,
            CAMRS,
            AR_1550,
            P9,
            AK_12,
            SUPER_90,
            P_10C,
            PARA_308,
            MP5K,
            M870,
            SPEAR_308,
            _57_USG,
            MP5,
            UMP45,
            FO_12,
            SMG_11,
            ITA12L,
            P12,
            C7E,
            AR33,
            AK_74M,
            _1911_TACOPS,
            T_5_SMG,
            SASG_12,
            SPSMG9,
            DP27,
            SUPER_SHORTY,
            VECTOR_45_ACP,
            ACS12,
            SIX12_SD,
            F2,
            L85A2,
            ALDA_556,
            Mx4_Storm,
            M249_SAW,
            _552_COMMANDO,
            SPAS_12,
            POF9,
            PCX_33,
            FMG_9,
            _6P41,
            SCORPION_EVO_3_A1,
            BOSG122,
            M762,
            T_95_LSW,
            P90,
            KERATOS_357,
            Mk_14_EBR,
            SR_25,
            UZK50GI,
            SPAS_15,
            Q_929,
            F90,
            MP5SD,
            P229,
            GONNE_6,
            BEARING_9,
            M249,
            PDW9,
            PMM,
            MPX,
            SC3000K,
            SIX12,
            _9x19VSN,
            D_50,
            R4_C,
            _416_C_CARBINE,
            G36C,
            MK1_9mm,
            MP7,
            SMG_12,
            M4,
            C75_Auto,
            ITA12S,
            P226_MK_25,
            AUG_A3,
            C8_SFW,
            P10_RONI,
            PRB92,
            CSRX300,
            RG15,
            _44_Mag_Semi_Auto
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
        internal static string GetOriginalName(string name)
        {
            if (name.Length > 1 && name[0] == '_' && char.IsDigit(name[1]))
            {
                name = name.Substring(1);
            }
            name = name.Replace("_", "-");
            return name;
        }
    }
}
