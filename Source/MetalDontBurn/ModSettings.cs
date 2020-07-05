using RimWorld;
using RimWorld.Planet;
using System;
using UnityEngine;
using Verse;

namespace MetalDontBurn
{
  public class Settings : ModSettings
  {
    public static float multiplySilverYield = 1f;

    public override void ExposeData()
    {
      base.ExposeData();
      Scribe_Values.Look(ref Settings.multiplySilverYield, "OYAmountToMultiplySilver", 0.0f, true);

    }
  }

  public class Mod : Verse.Mod
  {
    public bool resetDefaults;
    public Vector2 scrollPosition;
    private Settings settings;

    public Mod(ModContentPack con) : base(con)
    {
      settings = GetSettings<Settings>();
    }

    public override void DoSettingsWindowContents(Rect canvas)
    {
      Listing_Standard lister = new Listing_Standard();

      // lister.ColumnWidth = canvas.width - 80f;
      float height = canvas.y + 800f; // set height here
      Rect viewRect = new Rect(0f, 0f, canvas.width - 260f, height);


      lister.Begin(canvas);
      lister.BeginScrollView(new Rect(120f, 0f, canvas.width - 240f, canvas.height), ref scrollPosition, ref viewRect);
      lister.Settings_Header("YieldsHeader".Translate(), Color.clear, GameFont.Medium, TextAnchor.MiddleLeft);
      lister.GapLine();
      lister.Gap(12f);
      lister.Settings_SliderLabeled(AddFlooredResultToLabel("OYMultiplyAmountLabelSilver".Translate(), Settings.multiplySilverYield, Settings.yieldSilver), "%", ref Settings.multiplySilverYield, 0.0f, 10f, 1f, 1);


      lister.End();
      lister.EndScrollView(ref viewRect);

      base.DoSettingsWindowContents(canvas);
    }

    public override void WriteSettings()
    {
      if (resetDefaults)
      {
        Settings.multiplySilverYield = 1f;

        resetDefaults = false;
      }
      Mod.UpdateAllChanges();
      base.WriteSettings();
    }

    public override string SettingsCategory()
    {
      return "ModTitle".Translate();
    }

    public string AddFlooredResultToLabel(string label, float multiplier, float mass)
    {
      return label + ": " + Math.Floor(mass * multiplier).ToString();
    }

    public string AddResultToLabel(string label, float multiplier, float mass)
    {
      return label + ": " + (mass * multiplier).ToString();
    }

    public static void UpdateAllChanges()
    {
      DefDatabase<ThingDef>.GetNamed("MineableSilver").building.mineableYield = (int)Math.Floor(Settings.yieldSilver * Settings.multiplySilverYield);
    }
  }
}
