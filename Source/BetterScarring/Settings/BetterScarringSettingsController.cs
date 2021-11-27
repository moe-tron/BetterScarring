using UnityEngine;
using Verse;

namespace BetterScarring.Settings
{
    public class BetterScarringSettingsController : Mod
    {
        public BetterScarringSettingsController(ModContentPack contentPack) : base(contentPack) {
            GetSettings<BetterScarringSettings>();
        }

        public override string SettingsCategory()
        {
            return "Better Scarring";
        }

        public override void DoSettingsWindowContents(Rect inRect)
        {
            BetterScarringSettings.DoSettingsWindowContents(inRect);
        }

    }
}
