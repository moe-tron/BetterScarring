using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using RimWorld;
using Verse;
using BetterScarring.Settings;

namespace BetterScarring.Patches
{
    [HarmonyPatch(typeof(HediffComp_GetsPermanent), "PreFinalizeInjury")]
    internal static class HediffComp_GetsPermanent_PreFinalizeInjuryPatch
    {
		// Didn't want to write this as a prefix that skips the original method, but I don't think I can write this as a postfix without having to unset everything the original method already did.
		[HarmonyPrefix]
		private static bool PreFinalizeInjury(HediffComp_GetsPermanent __instance)
		{
			// delicate and non-skin covered parts will bypass this prefix.
			bool skipPrefix = __instance.parent.Part.def.delicate 
				|| !__instance.parent.Part.def.IsSkinCovered(__instance.parent.Part, __instance.Pawn.health.hediffSet)
				|| __instance.Pawn.health.hediffSet.PartOrAnyAncestorHasDirectlyAddedParts(__instance.parent.Part);
			if (skipPrefix)
			{
				return true;
			}
			float num = BetterScarringSettings.permanentChanceFactor * __instance.parent.Part.def.permanentInjuryChanceFactor * __instance.Props.becomePermanentChanceFactor;
			if (!__instance.parent.Part.def.delicate)
			{
				float partMaxHealth = __instance.parent.Part.def.GetMaxHealth(__instance.Pawn);
				num *= HediffComp_GetsPermanent_PreFinalizeInjuryPatch.PermanentInjurySeverityCurve.Evaluate(__instance.parent.Severity / partMaxHealth);
			}
			if (Rand.Chance(num))
			{
				if (__instance.parent.Part.def.delicate)
				{
					__instance.IsPermanent = true;
					return false;
				}
				// Permanent injuries can range from 0.25 to 1/4 of the initial injury's severity
				__instance.permanentDamageThreshold = Rand.Range(0.25f, __instance.parent.Severity / BetterScarringSettings.injurySeverityToPermanentRatio);
			}
			return false;
		}

		public static readonly SimpleCurve PermanentInjurySeverityCurve = new SimpleCurve
		{
			{
				new CurvePoint(0.1f, 0.25f),
				true
			},
			{
				new CurvePoint(1f, 1.5f),
				true
			}
		};
	}
}
