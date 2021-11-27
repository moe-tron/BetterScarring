using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using Verse;
using RimWorld;

namespace BetterScarring.Patches
{

    [HarmonyPatch(typeof(ThoughtWorker_Pretty), "CurrentSocialStateInternal")]
    internal static class ThoughtWorkerPrettyPatch
    {

        [HarmonyPostfix]
        private static void CurrentSocialStateInternal(ref Pawn pawn, ref Pawn other, ref ThoughtState __result) {
            if (__result.Active || !other.RaceProps.Humanlike || !RelationsUtility.PawnsKnowEachOther(pawn, other) || PawnUtility.IsBiologicallyOrArtificallyBlind(pawn)) {
                return;
            }
            if (RelationsUtility.IsDisfigured(other, pawn, false) && (IdeoUtil.hasScarIgnorningMeme(pawn) || IdeoUtil.hasScarLikingPrecept(pawn) || TraitUtil.hasScarIgnoringTraits(pawn) || TraitUtil.hasScarLikingTraits(pawn)))
            {
                float statValue = other.GetStatValue(StatDefOf.PawnBeauty, true);
                if (statValue >= 2f)
                {
                    __result = ThoughtState.ActiveAtStage(1);
                }
                if (statValue >= 1f)
                {
                    __result = ThoughtState.ActiveAtStage(0);
                }
            }
        }
    }
}
