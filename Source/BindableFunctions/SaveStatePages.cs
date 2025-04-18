using System;
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

        [BindableMethod(name = "Open savestate in text editor", category = BindableCategory.SaveStatePages)]
        public static void OpenSavestateFile()
        {
            DebugMod.saveStateManager.OpenFileOfState();
        }

        [BindableMethod(name = "Next Save Page", category = BindableCategory.SaveStatePages)]
        public static void NextStatePage()
        {
            SaveStateManager.SetPage(SaveStateManager.currentStateFolder + 1);
        }

        [BindableMethod(name = "Prev Save Page", category = BindableCategory.SaveStatePages)]
        public static void PrevStatePage()
        {
            SaveStateManager.SetPage(SaveStateManager.currentStateFolder - 1);
        }
    }
}
