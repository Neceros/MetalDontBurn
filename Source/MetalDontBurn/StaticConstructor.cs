using RimWorld;
using RimWorld.Planet;
using System;
using System.Linq;
using UnityEngine;
using Verse;

namespace MetalDontBurn
{
  [StaticConstructorOnStartup]
  public static class Startup
  {
    static Startup()
    {
      Settings.yieldSilver = DefDatabase<ThingDef>.GetNamed("MineableSilver").building.mineableYield;

      DefDatabase<ThingDef>.GetNamed("MineableSilver").building.mineableYield = (int)Math.Floor(Settings.yieldSilver * Settings.multiplySilverYield);
    }
  }
}
