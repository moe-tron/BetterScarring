using System.Reflection;
using Verse;
using HarmonyLib;

namespace BetterScarring
{
    [StaticConstructorOnStartup]
    public static class BetterScarring
    {
        static BetterScarring() {
            Log.Message("Better Scarring initialized!");
            Harmony harmony = new Harmony("BetterScarring.Patches");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }

    }
}
