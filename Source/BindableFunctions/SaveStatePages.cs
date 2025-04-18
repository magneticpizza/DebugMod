using UnityEngine;

namespace DebugMod
{
    public static partial class BindableFunctions
    {
        [BindableMethod(name = "Quickslot save to file", category = BindableCategory.SaveStatePages)]
        public static void CurrentSaveStateToFile()
        {
            DebugMod.saveStateManager.SaveNewState(SaveStateType.File);
        }

        [BindableMethod(name = "Load file to quickslot", category = BindableCategory.SaveStatePages)]
        public static void CurrentSlotToSaveMemory()
        {
            DebugMod.saveStateManager.LoadNewState(SaveStateType.File);
        }

        [BindableMethod(name = "Save new state to file", category = BindableCategory.SaveStatePages)]
        public static void NewSaveStateToFile()
        {
            DebugMod.saveStateManager.SaveNewState(SaveStateType.SkipOne);

        }

        [BindableMethod(name = "Load new state from file", category = BindableCategory.SaveStatePages)]
        public static void LoadFromFile()
        {
            DebugMod.saveStateManager.LoadNewState(SaveStateType.SkipOne);
        }

        [BindableMethod(name = "Next Save Page", category = BindableCategory.SaveStatePages)]
        public static void NextStatePage()
        {
            if (SaveStateManager.inSelectSlotState)
            {
                SaveStateManager.currentStateFolder++;
                if (SaveStateManager.currentStateFolder == SaveStateManager.savePages) { SaveStateManager.currentStateFolder = 0; } //rollback to 0 if 10, keep folder between 0 and 9
                SaveStateManager.path = (
                    Application.persistentDataPath +
                    "/Savestates-1221/" +
                    SaveStateManager.currentStateFolder.ToString() +
                    "/"); //change path
                DebugMod.saveStateManager.RefreshStateMenu(); // update menu
            }
        }

        [BindableMethod(name = "Prev Save Page", category = BindableCategory.SaveStatePages)]
        public static void PrevStatePage()
        {
            if (SaveStateManager.inSelectSlotState)
            {
                SaveStateManager.currentStateFolder--;
                if (SaveStateManager.currentStateFolder == -1) { SaveStateManager.currentStateFolder = SaveStateManager.savePages - 1; } //rollback to max if past limit, keep folder between 0 and 9
                SaveStateManager.path = (
                    Application.persistentDataPath +
                    "/Savestates-1221/" +
                    SaveStateManager.currentStateFolder.ToString() +
                    "/"); //change path
                DebugMod.saveStateManager.RefreshStateMenu(); // update menu
            }
        }
    }
}
