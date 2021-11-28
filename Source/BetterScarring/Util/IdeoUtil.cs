using RimWorld;
using Verse;

namespace BetterScarring
{
    public static class IdeoUtil
    {
        [MayRequireIdeology]
        public static readonly PreceptDef Pain_Idealized = DefDatabase<PreceptDef>.GetNamed("Pain_Idealized", true);

        [MayRequireIdeology]
        public static readonly MemeDef Raider_Meme = DefDatabase<MemeDef>.GetNamed("Raider", true);

        [MayRequireIdeology]
        public static readonly MemeDef Cannibal_Meme = DefDatabase<MemeDef>.GetNamed("Cannibal", true);

        public static bool hasScarIgnorningMeme(Pawn pawn) {
            return ModsConfig.IdeologyActive && pawn.Ideo != null && pawn.Ideo.HasMeme(Raider_Meme) || pawn.Ideo.HasMeme(Cannibal_Meme);
        }

        public static bool hasScarLikingPrecept(Pawn pawn)
        {
            return ModsConfig.IdeologyActive && pawn.Ideo != null && pawn.Ideo.HasPrecept(Pain_Idealized);
        }
    }
}
