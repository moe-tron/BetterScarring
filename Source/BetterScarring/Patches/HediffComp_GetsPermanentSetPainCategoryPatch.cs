using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using RimWorld;
using Verse;

namespace BetterScarring.Patches
{
    [HarmonyPatch(typeof(HediffComp_GetsPermanent), "SetPainCategory")]
    class HediffComp_GetsPermanentSetPainCategoryPatch
    {
        [HarmonyPrefix]
        private static bool SetPainCategory(HediffComp_GetsPermanent __instance, ref PainCategory category) {
            try
            {
                // Note: I don't think this will work 100% on scars created during pawn generation
                // Some pre-checks making sure we can actually do the checks below without throwing an exception.
                if (__instance == null || __instance.parent == null || __instance.parent.Part == null || __instance.Pawn == null || __instance.Pawn.health == null)
                {
                    return true;
                }

                // delicate parts or parts that are not skin covered will retain their original pain category.
                if (__instance.parent.Part.def.delicate || !__instance.parent.Part.def.IsSkinCovered(__instance.parent.Part, __instance.Pawn.health.hediffSet))
                {
                    return true;
                }
                if (category == PainCategory.HighPain || category == PainCategory.MediumPain)
                {
                    category = Rand.Chance(0.5f) ? PainCategory.Painless : PainCategory.LowPain;
                }
            }
            catch (Exception e) {
                Log.Error($"Exception during SetPainCategory prefix from BetterScarring: {e.Message}");
            }
            return true;
        
        }
    }
}
