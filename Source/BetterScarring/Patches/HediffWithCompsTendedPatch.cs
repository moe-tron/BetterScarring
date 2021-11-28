using System;
using HarmonyLib;
using Verse;

namespace BetterScarring.Patches
{
    [HarmonyPatch(typeof(HediffWithComps), "Tended")]
    internal static class HediffWithCompsTendedPatch
    {
        // Postfix, depending on the tend quality we'll set the permanentDamageThreshold back to where it won't result in a permanent injury.
        [HarmonyPostfix]
        private static void Tended(HediffWithComps __instance)
        {
            try
            {
                HediffComp_GetsPermanent permComp = __instance.TryGetComp<HediffComp_GetsPermanent>();
                HediffComp_TendDuration hediffComp_TendDuration = __instance.TryGetComp<HediffComp_TendDuration>();
                if (__instance == null || !(__instance is Hediff_Injury) || __instance.IsPermanent() || permComp == null || hediffComp_TendDuration == null)
                {
                    return; // patch is not applicable
                }
                float tendQuality = hediffComp_TendDuration.tendQuality;
                if (Rand.Chance(PermanentDamageThresholdCureCurve.Evaluate(tendQuality)))
                {
                    permComp.permanentDamageThreshold = 9999f; // Makes it to where injury won't become permanent.
                }
            }
            catch (Exception e) {
                Log.Error($"Exception during Tended postfix from BetterScarring: {e.Message}");
            }
        }

        // Curve for tends curing injuries that will become permanent
        // Low quality tends up to 20% will never cure the injury.
        // Max quality herbal tends will have a 10% chance to cure.
        // Max quality normal meds should be ~55% or so
        // Max quality glitterworld tends should always cure the injury.
        private static readonly SimpleCurve PermanentDamageThresholdCureCurve = new SimpleCurve
        {
            {
                new CurvePoint(0.2f, 0.0f),
                true
            },
            {
                new CurvePoint(0.7f, 0.1f),
                true
            },
            {
                new CurvePoint(1.3f, 1.0f),
                true
            }
        };
    }
}
