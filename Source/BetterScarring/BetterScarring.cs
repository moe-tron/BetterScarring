using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using RimWorld;
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
