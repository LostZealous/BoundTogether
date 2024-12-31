using Terraria.ModLoader.Config;

namespace BoundTogether
{
    public class BoundTogetherConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;

        [LabelKey("$Mods.BoundTogether.Configs.BoundTogetherConfig.MaxTetherRange.Label")]
        [TooltipKey("$Mods.BoundTogether.Configs.BoundTogetherConfig.MaxTetherRange.Tooltip")]
        public int MaxTetherRange { get; set; } = 10;

        [LabelKey("$Mods.BoundTogether.Configs.BoundTogetherConfig.EnablePenalties.Label")]
        [TooltipKey("$Mods.BoundTogether.Configs.BoundTogetherConfig.EnablePenalties.Tooltip")]
        public bool EnablePenalties { get; set; } = true;
    }
}
