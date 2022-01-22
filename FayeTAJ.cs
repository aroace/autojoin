using Terraria;
using Terraria.ID;
using Terraria.Enums;
using Terraria.ModLoader;
using System.ComponentModel;
using Terraria.ModLoader.Config;
using System.Collections.Generic;

namespace FayeTAJ
{
    public class FayeTAJ : Mod
    {

        public static Configuration Config;
        public static Dictionary<string, Team> TeamLiteral;

        public override void Load()
        {

            TeamLiteral = new Dictionary<string, Team>();

            TeamLiteral.Add("red", Team.Red);
            TeamLiteral.Add("green", Team.Green);
            TeamLiteral.Add("blue", Team.Blue);
            TeamLiteral.Add("yellow", Team.Yellow);
            TeamLiteral.Add("pink", Team.Pink);
            TeamLiteral.Add("none", Team.None);

            Logger.InfoFormat("[{0}] Loaded.", DisplayName);
        }

        public override void Unload()
        {
            TeamLiteral = null;
            Config = null;
            Logger.InfoFormat("[{0}] Unloaded; cleared static references correctly.", DisplayName);
        }
    }

    public class AJPlayer : ModPlayer
    {
        public override void OnEnterWorld(Player player)
        {
            if (Main.netMode == NetmodeID.SinglePlayer || player != Main.LocalPlayer) return;

            string team = FayeTAJ.Config.Team.ToLower();
            if (!FayeTAJ.TeamLiteral.ContainsKey(team))
            {
                Main.NewText("[FayeTAJ] Invalid team specified in configuration. Defaulting to 'none'.", Microsoft.Xna.Framework.Color.Red);
                return;
            }

            Main.LocalPlayer.team = ((int)FayeTAJ.TeamLiteral[team]);
            Main.NewText("[FayeTAJ] Team joined.", Microsoft.Xna.Framework.Color.LightGreen);
        }
    }

    public class Configuration : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;

        public override void OnLoaded()
        {
            FayeTAJ.Config = this;
        }

        [DefaultValue("pink")]
        [Label("Team to join")]
        [Tooltip("The team that you want to join automatically.")]
        public string Team { get; set; }
    }
}