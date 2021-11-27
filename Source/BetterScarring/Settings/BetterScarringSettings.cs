using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace BetterScarring.Settings
{
    internal class BetterScarringSettings : ModSettings
    {

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look<float>(ref BetterScarringSettings.permanentChanceFactor, "permanentChanceFactor", 0.1f, true);
			Scribe_Values.Look<float>(ref BetterScarringSettings.injurySeverityToPermanentRatio, "injurySeverityToPermanentRatio", 4.0f, true);
		}

		public static void DoSettingsWindowContents(Rect inRect)
		{
			Listing_Standard listing_Standard = new Listing_Standard();
			listing_Standard.ColumnWidth = Math.Min(400f, inRect.width / 2f);
			listing_Standard.Begin(inRect);
			listing_Standard.Label("Modifier for Injuries to become permanent: " + (int)(permanentChanceFactor*100) + "%", -1f, "This only applies to injuries that are on skin covered non-delicate body parts. Vanilla is 0.02, default for this mod is 0.1");
			permanentChanceFactor = listing_Standard.Slider(permanentChanceFactor, 0f, 1f);
			listing_Standard.Gap(4f);
			listing_Standard.Label("Injury severity to permanent severity ratio: 1/" + (int)injurySeverityToPermanentRatio, -1f, "ex: 1/4 means that the max severity a permanent injury can have is 1/4 the initial severity of the injury. Minumum Severity for a permanent injury will always be 0.25, the severity of a permanent injury will be a random value from 0.25 to 1/X the original severity. This also only applies to non-delicate parts.");
			injurySeverityToPermanentRatio = listing_Standard.Slider(injurySeverityToPermanentRatio, 1f, 16f);
			listing_Standard.End();
		}



		public static float permanentChanceFactor = 0.1f;

		public static float injurySeverityToPermanentRatio = 4f;
    }

}
