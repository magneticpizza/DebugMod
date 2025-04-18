namespace DebugMod
{
    public static partial class BindableFunctions
    {
        [BindableMethod(name = "Toggle All UI", category = BindableCategory.ModUI)]
        public static void ToggleAllPanels()
        {
            bool active = !(
                DebugMod.settings.HelpPanelVisible ||
                DebugMod.settings.InfoPanelVisible ||
                DebugMod.settings.EnemiesPanelVisible ||
                DebugMod.settings.TopMenuVisible ||
                DebugMod.settings.ConsoleVisible ||
                DebugMod.settings.MinInfoPanelVisible
                );

            if (MinimalInfoPanel.minInfo)
            {
                DebugMod.settings.InfoPanelVisible = false;
                DebugMod.settings.MinInfoPanelVisible = active;
            }
            else
            {
                DebugMod.settings.InfoPanelVisible = active;
                DebugMod.settings.MinInfoPanelVisible = false;
            }
            DebugMod.settings.TopMenuVisible = active;
            DebugMod.settings.EnemiesPanelVisible = active;
            DebugMod.settings.ConsoleVisible = active;
            DebugMod.settings.HelpPanelVisible = active;

            if (DebugMod.settings.EnemiesPanelVisible)
            {
                EnemiesPanel.RefreshEnemyList();
            }
        }
        [BindableMethod(name = "Toggle showing room IDs", category = BindableCategory.ModUI)]
        public static void ToggleShowRoomIDs()
        {
            DebugMod.settings.ShowRoomIDs = !DebugMod.settings.ShowRoomIDs;
        }

        [BindableMethod(name = "Toggle Binds", category = BindableCategory.ModUI)]
        public static void ToggleHelpPanel()
        {
            DebugMod.settings.HelpPanelVisible = !DebugMod.settings.HelpPanelVisible;
        }

        [BindableMethod(name = "Toggle Info", category = BindableCategory.ModUI)]
        public static void ToggleInfoPanel()
        {
            if (MinimalInfoPanel.minInfo)
            {
                DebugMod.settings.InfoPanelVisible = false;
                DebugMod.settings.MinInfoPanelVisible = !DebugMod.settings.MinInfoPanelVisible;
            }
            else
            {
                DebugMod.settings.InfoPanelVisible = !DebugMod.settings.InfoPanelVisible;
                DebugMod.settings.MinInfoPanelVisible = false;
            }
        }

        [BindableMethod(name = "Toggle Top Menu", category = BindableCategory.ModUI)]
        public static void ToggleTopRightPanel()
        {
            DebugMod.settings.TopMenuVisible = !DebugMod.settings.TopMenuVisible;
        }

        [BindableMethod(name = "Toggle Console", category = BindableCategory.ModUI)]
        public static void ToggleConsole()
        {
            DebugMod.settings.ConsoleVisible = !DebugMod.settings.ConsoleVisible;
        }

        [BindableMethod(name = "Toggle Enemy Panel", category = BindableCategory.ModUI)]
        public static void ToggleEnemyPanel()
        {
            DebugMod.settings.EnemiesPanelVisible = !DebugMod.settings.EnemiesPanelVisible;
            if (DebugMod.settings.EnemiesPanelVisible)
            {
                EnemiesPanel.RefreshEnemyList();
            }
        }

        [BindableMethod(name = "Toggle SaveState Panel", category = BindableCategory.ModUI)]
        public static void ToggleSaveStatesPanel()
        {
            DebugMod.settings.SaveStatePanelVisible = !DebugMod.settings.SaveStatePanelVisible;
        }

        // A variant of info panel. View handled in the two InfoPanel classes
        //  TODO: stop not knowing how to use xor in c#
        [BindableMethod(name = "Alt. Info Switch", category = BindableCategory.ModUI)]
        public static void ToggleFullInfo()
        {
            MinimalInfoPanel.minInfo = !MinimalInfoPanel.minInfo;

            if (MinimalInfoPanel.minInfo)
            {
                if (DebugMod.settings.InfoPanelVisible)
                {
                    DebugMod.settings.InfoPanelVisible = false;
                    DebugMod.settings.MinInfoPanelVisible = true;
                }
            }
            else
            {
                if (DebugMod.settings.MinInfoPanelVisible)
                {
                    DebugMod.settings.MinInfoPanelVisible = false;
                    DebugMod.settings.InfoPanelVisible = true;
                }
            }

        }
    }
}
