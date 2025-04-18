using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace DebugMod
{
    public static partial class BindableFunctions
    {
        /*[BindableMethod(name = "Show chain window (physics)", category = BindableCategory.Loggers)]
        public static void ToggleChainTimerPhysics()
        {
            ChainTimer.LogChainsPhysics = !ChainTimer.LogChainsPhysics;
        }*/
        [BindableMethod(name = "Show chain window (graphics)", category = BindableCategory.Loggers)]
        public static void ToggleChainTimerGraphics()
        {
            ChainTimer.logChains = !ChainTimer.logChains;
            Console.AddLine(ChainTimer.logChains.ToString());
        }
        [BindableMethod(name = "Show Walljump Input", category = BindableCategory.Loggers)]
        public static void ToggleWallJumpTimer()
        {
            ChainTimer.logWallJumps = !ChainTimer.logWallJumps;
            Console.AddLine(ChainTimer.logWallJumps.ToString());
        }
        [BindableMethod(name = "Show Repress Input", category = BindableCategory.Loggers)]
        public static void ToggleRepressTimer()
        {
            ChainTimer.logRepresses = !ChainTimer.logRepresses;
            Console.AddLine(ChainTimer.logRepresses.ToString());
        }
        [BindableMethod(name = "Delete Input Data", category = BindableCategory.Loggers)]
        public static void DeleteInputData()
        {
            ChainTimer.jumpChainTimer.ClearRecord();
            ChainTimer.repressTimer.ClearRecord();
            ChainTimer.wallJumpTimer.ClearRecord();
            ChainTimer.ClearLines();
        }
        [BindableMethod(name = "Toggle TFT logging (graphics)", category = BindableCategory.Loggers)]
        public static void ToggleTFTLoggingGraphics()
        {
            ChainTimer.logTFT = !ChainTimer.logTFT;
            Console.AddLine("logging tft (graphics) now set to " + ChainTimer.logTFT.ToString());
        }
        [BindableMethod(name = "Show Average TFT", category = BindableCategory.Loggers)]
        public static void ShowAverageTFT()
        {
            Console.AddLine("Graphics TFT avg (s): " + ChainTimer.tftTimesGraphics.DefaultIfEmpty(0).Average().ToString());
        }
        [BindableMethod(name = "Clear TFT logger", category = BindableCategory.Loggers)]
        public static void ClearTFTValues()
        {
            ChainTimer.tftTimesGraphics.Clear();
            ChainTimer.tftTimesGraphics.TrimExcess();
        }
        [BindableMethod(name = "Export TFT data to file (graphics)", category = BindableCategory.Loggers)]
        public static void TFTToFileGraphics()
        {
            List<float> orig = ChainTimer.tftTimesGraphics;
            string filename = "TFTGraphicsData";
            string path = string.Concat(new object[] { Application.persistentDataPath, "/" + filename + ".json" });
            string data = "";
            foreach (float item in orig)
            {
                data += item.ToString() + ", ";
            }
            data = "{" + data.Substring(0, data.Length - 2) + "}";
            File.WriteAllText(path, data);
        }
    }
}
