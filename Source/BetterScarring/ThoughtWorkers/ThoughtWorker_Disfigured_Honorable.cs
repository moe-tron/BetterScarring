using System;
using Verse;
using RimWorld;


namespace BetterScarring
{
    class ThoughtWorker_Disfigured_Honorable : ThoughtWorker
    {
		protected override ThoughtState CurrentSocialStateInternal(Pawn pawn, Pawn other)
		{
			if (!other.RaceProps.Humanlike || other.Dead)
			{
				return false;
			}
			if (!RelationsUtility.PawnsKnowEachOther(pawn, other))
			{
				return false;
			}
			if (!RelationsUtility.IsDisfigured(other, pawn, false))
			{
				return false;
			}
			if (PawnUtility.IsBiologicallyBlind(pawn))
			{
				return false;
			}
			if ((IdeoUtil.hasScarLikingPrecept(pawn)) || TraitUtil.hasScarLikingTraits(pawn))
			{
				return true;
			}
			return false;
		}
	}
}
