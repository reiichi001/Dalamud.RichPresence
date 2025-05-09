using System.Numerics;
using Dalamud.Interface.Colors;
using ImGuiNET;

using Dalamud.RichPresence.Configuration;
using Dalamud.RichPresence.Models;
using Dalamud.Interface.Utility;
using Dalamud.Utility;

namespace Dalamud.RichPresence.Interface
{
    internal class RichPresenceConfigWindow
    {
        private bool IsOpen = false;
        private RichPresenceConfig RichPresenceConfig;

        public RichPresenceConfigWindow()
        {
            RichPresenceConfig = RichPresencePlugin.DalamudPluginInterface.GetPluginConfig() as RichPresenceConfig ?? new RichPresenceConfig();
        }

        public void DrawRichPresenceConfigWindow()
        {
            if (!this.IsOpen)
            {
                return;
            }

            ImGui.SetNextWindowSize(ImGuiHelpers.ScaledVector2(750, 520));
            var imGuiReady = ImGui.Begin(
                RichPresencePlugin.LocalizationManager.Localize("DalamudRichPresenceConfiguration", LocalizationLanguage.Plugin),
                ref IsOpen,
                ImGuiWindowFlags.NoCollapse | ImGuiWindowFlags.AlwaysAutoResize | ImGuiWindowFlags.NoScrollbar
            );

            if (imGuiReady)
            {
                ImGui.Text(RichPresencePlugin.LocalizationManager.Localize("DalamudRichPresencePreface1", LocalizationLanguage.Plugin));
                ImGui.Text(RichPresencePlugin.LocalizationManager.Localize("DalamudRichPresencePreface2", LocalizationLanguage.Plugin));
                ImGui.Separator();

                ImGui.BeginChild("scrolling", ImGuiHelpers.ScaledVector2(0, 400), true, ImGuiWindowFlags.HorizontalScrollbar);

                ImGui.PushStyleVar(ImGuiStyleVar.ItemSpacing, ImGuiHelpers.ScaledVector2(1, 3));

                ImGui.Checkbox(RichPresencePlugin.LocalizationManager.Localize("DalamudRichPresenceShowName", LocalizationLanguage.Plugin), ref RichPresenceConfig.ShowName);
                ImGui.Checkbox(RichPresencePlugin.LocalizationManager.Localize("DalamudRichPresenceShowFreeCompany", LocalizationLanguage.Plugin), ref RichPresenceConfig.ShowFreeCompany);
                ImGui.Checkbox(RichPresencePlugin.LocalizationManager.Localize("DalamudRichPresenceShowWorld", LocalizationLanguage.Plugin), ref RichPresenceConfig.ShowWorld);
                ImGui.Separator();
                ImGui.Checkbox(RichPresencePlugin.LocalizationManager.Localize("DalamudRichPresenceShowStartTime", LocalizationLanguage.Plugin), ref RichPresenceConfig.ShowStartTime);
                ImGui.Checkbox(RichPresencePlugin.LocalizationManager.Localize("DalamudRichPresenceResetTimeWhenChangingZones", LocalizationLanguage.Plugin), ref RichPresenceConfig.ResetTimeWhenChangingZones);
                ImGui.Separator();
                ImGui.Checkbox(RichPresencePlugin.LocalizationManager.Localize("DalamudRichPresenceShowLoginQueuePosition", LocalizationLanguage.Plugin), ref RichPresenceConfig.ShowLoginQueuePosition);
                ImGui.TextColored(ImGuiColors.DalamudGrey, RichPresencePlugin.LocalizationManager.Localize("DalamudRichPresenceShowLoginQueuePositionDetail", LocalizationLanguage.Plugin));
                ImGui.Separator();
                ImGui.Checkbox(RichPresencePlugin.LocalizationManager.Localize("DalamudRichPresenceShowJob", LocalizationLanguage.Plugin), ref RichPresenceConfig.ShowJob);
                ImGui.Checkbox(RichPresencePlugin.LocalizationManager.Localize("DalamudRichPresenceAbbreviateJob", LocalizationLanguage.Plugin), ref RichPresenceConfig.AbbreviateJob);
                ImGui.Checkbox(RichPresencePlugin.LocalizationManager.Localize("DalamudRichPresenceShowLevel", LocalizationLanguage.Plugin), ref RichPresenceConfig.ShowLevel);
                ImGui.Separator();
                ImGui.Checkbox(RichPresencePlugin.LocalizationManager.Localize("DalamudRichPresenceShowParty", LocalizationLanguage.Plugin), ref RichPresenceConfig.ShowParty);
                ImGui.Checkbox(RichPresencePlugin.LocalizationManager.Localize("DalamudRichPresenceShowAFK", LocalizationLanguage.Plugin), ref RichPresenceConfig.ShowAfk);
                ImGui.Checkbox(RichPresencePlugin.LocalizationManager.Localize("DalamudRichPresenceHideAFKEntirely", LocalizationLanguage.Plugin), ref RichPresenceConfig.HideEntirelyWhenAfk);

                if (Util.IsWine())
                {
                    ImGui.Separator();
                    ImGui.Checkbox(RichPresencePlugin.LocalizationManager.Localize("DalamudRichPresenceRPCBridgeEnabled", LocalizationLanguage.Plugin), ref RichPresenceConfig.RPCBridgeEnabled);
                    ImGui.TextColored(ImGuiColors.DalamudGrey, RichPresencePlugin.LocalizationManager.Localize("DalamudRichPresenceRPCBridgeEnabledDetail", LocalizationLanguage.Plugin));
                }

                ImGui.PopStyleVar();

                ImGui.EndChild();

                ImGui.Separator();

                if (ImGui.Button(RichPresencePlugin.LocalizationManager.Localize("DalamudRichPresenceSaveAndClose", LocalizationLanguage.Plugin)))
                {
                    RichPresencePlugin.DalamudPluginInterface.SavePluginConfig(this.RichPresenceConfig);
                    RichPresencePlugin.RichPresenceConfig = this.RichPresenceConfig;
                    RichPresencePlugin.PluginLog.Information("Settings saved.");
                    this.Close();
                }

                ImGui.End();
            }
        }

        public void Open()
        {
            this.IsOpen = true;
        }

        public void Close()
        {
            this.IsOpen = false;
        }

        public void Toggle()
        {
            this.IsOpen = !this.IsOpen;
        }
    }
}