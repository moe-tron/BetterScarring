using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RimWorld;
using Verse;

namespace BetterScarring
{
    public static class TraitUtil
    {
        public static bool hasTraits(Pawn pawn)
        {
            return pawn?.story?.traits != null;
        }

        // Would probably be cleaner to just combine the traits into a list and check vs the list of traits but I'm lazy.
        public static bool hasScarLikingTraits(Pawn pawn) {
            return hasTraits(pawn)
                && (pawn.story.traits.HasTrait(TraitDefOf.Masochist)
                || pawn.story.traits.HasTrait(TraitDefOf.Brawler)
                || pawn.story.traits.HasTrait(TraitDefOf.Bloodlust));
        }

         public static bool hasScarIgnoringTraits(Pawn pawn)
        {
            return hasTraits(pawn)
                && (pawn.story.traits.HasTrait(TraitDefOf.Kind)
                || pawn.story.traits.HasTrait(TraitDefOf.Ascetic));
        }
    }
}
