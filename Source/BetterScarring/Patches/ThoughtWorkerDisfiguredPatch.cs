using HarmonyLib;
using Verse;
using RimWorld;

namespace BetterScarring.Patches
{
    [HarmonyPatch(typeof(ThoughtWorker_Disfigured), "CurrentSocialStateInternal")]
    internal static class ThoughtWorkerDisfiguredPatch
    {
        [HarmonyPostfix]
        private static void CurrentSocialStateInternal(ref Pawn pawn, ref Pawn other, ref ThoughtState __result)
        {
            if (!__result.Active)
            {
                return;
            }
            // If result is true we can already assume other is disfigured, no need to re-run the check or other ones performed before.
            if (IdeoUtil.hasScarIgnorningMeme(pawn) || IdeoUtil.hasScarLikingPrecept(pawn) || TraitUtil.hasScarIgnoringTraits(pawn) || TraitUtil.hasScarLikingTraits(pawn))
            {
                __result = false;
            }
        }
    }
}
