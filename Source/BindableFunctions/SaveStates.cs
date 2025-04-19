using GlobalEnums;
using System.Reflection;

namespace DebugMod
{
    public static partial class BindableFunctions
    {
        [BindableMethod(name="Load scene bench", category =BindableCategory.SaveStates)]
        public static void LoadBenchTing()
        {
            FieldInfo fadeFi = typeof(GameManager).GetField("needFirstFadeIn", BindingFlags.Instance | BindingFlags.NonPublic);
            //FieldInfo intervalFi = typeof(GameManager).GetField("intervalStartTime", BindingFlags.Instance | BindingFlags.NonPublic);

            //DebugMod.GM.SetupSceneRefs();
            fadeFi.SetValue(DebugMod.GM, true);
            //intervalFi.SetValue(DebugMod.GM, Time.realtimeSinceStartup);
            DebugMod.GM.ReadyForRespawn();
            DebugMod.GM.SetState(GameState.PLAYING);
            DebugMod.GM.ui.ConfigureMenu();
            //DebugMod.GM.ContinueGame();
        }

        [BindableMethod(name = "Position Save", category = BindableCategory.SaveStates)]
        public static void RoomSaveState()
        {
            SavePositionManager.SaveState();
        }

        [BindableMethod(name = "Position Load", category = BindableCategory.SaveStates)]
        public static void RoomLoadState()
        {
            SavePositionManager.LoadState();
        }

        [BindableMethod(name = "Quickslot (save)", category = BindableCategory.SaveStates)]
        public static void SaveState()
        {
            DebugMod.saveStateManager.SaveNewState(SaveStateType.Memory);
        }

        [BindableMethod(name = "Quickslot (load)", category = BindableCategory.SaveStates)]
        public static void LoadState()
        {
            DebugMod.saveStateManager.LoadNewState(SaveStateType.Memory);
        }

        [BindableMethod(name = "Load Savestate On Death", category = BindableCategory.SaveStates)]
        public static void LoadStateOnDeath()
        {
            DebugMod.stateOnDeath = !DebugMod.stateOnDeath;
            Console.AddLine("Quickslot SaveState will " + (DebugMod.stateOnDeath ? "now" : "no longer") + " load on death");
        }

        [BindableMethod(name = "Toggle Sisyphus Save", category = BindableCategory.SaveStates)]
        public static void ToggleSisyphusSave()
        {
            Sisyphus.Toggle();
        }

        /*
        [BindableMethod(name = "Toggle auto slot", category = BindableCategory.Savestates)]
        public static void ToggleAutoSlot()
        {   
            DebugMod.saveStateManager.ToggleAutoSlot();
        }
        
        [BindableMethod(name = "Refresh state menu", category = BindableCategory.Savestates)]
        public static void RefreshSaveStates()
        {
            DebugMod.saveStateManager.RefreshStateMenu();
        }
        */
    }
}
