using DebugMod.JankColoStuff;

namespace DebugMod
{
    public static partial class BindableFunctions
    {
        [BindableMethod(name = "(0) Reset Bronze Waves", category = BindableCategory.Colo1)]
        public static void Colo1Preset0()
        {
            ColoBronzeWaveChanger.SetWavePreset(0);
        }

        [BindableMethod(name = "(1) Aspids", category = BindableCategory.Colo1)]
        public static void Colo1Preset1()
        {
            ColoBronzeWaveChanger.SetWavePreset(1);
        }

        [BindableMethod(name = "(2) Baldurs 2", category = BindableCategory.Colo1)]
        public static void Colo1Preset2()
        {
            ColoBronzeWaveChanger.SetWavePreset(2);
        }

        [BindableMethod(name = "(3) Gruzzers", category = BindableCategory.Colo1)]
        public static void Colo1Preset3()
        {
            ColoBronzeWaveChanger.SetWavePreset(3);
        }

        [BindableMethod(name = "(4) Zote", category = BindableCategory.Colo1)]
        public static void Colo1Preset4()
        {
            ColoBronzeWaveChanger.SetWavePreset(4);
        }

        //Colo 2
        [BindableMethod(name = "(0) Reset Silver Waves", category = BindableCategory.Colo2)]
        public static void Colo2Preset0()
        {
            ColoSilverWaveChanger.SetWavePreset(0);
        }

        [BindableMethod(name = "(1) Hoppers", category = BindableCategory.Colo2)]
        public static void Colo2Preset1()
        {
            ColoSilverWaveChanger.SetWavePreset(1);
        }

        [BindableMethod(name = "(2) Grub Mimic", category = BindableCategory.Colo2)]
        public static void Colo2Preset2()
        {
            ColoSilverWaveChanger.SetWavePreset(2);
        }

        [BindableMethod(name = "(3) Obbles", category = BindableCategory.Colo2)]
        public static void Colo2Preset3()
        {
            ColoSilverWaveChanger.SetWavePreset(3);
        }

        [BindableMethod(name = "(4) Oblobbles", category = BindableCategory.Colo2)]
        public static void Colo2Preset4()
        {
            ColoSilverWaveChanger.SetWavePreset(4);
        }

        //Colo 3 Wave Presets, see ColoGoldWavechanger.cs

        [BindableMethod(name = "(0) Reset Gold Waves", category = BindableCategory.Colo3)]
        public static void Colo3Preset0()
        {
            ColoGoldWaveChanger.SetWavePreset(0);
        }

        [BindableMethod(name = "(1) Frogs", category = BindableCategory.Colo3)]
        public static void Colo3Preset1()
        {
            ColoGoldWaveChanger.SetWavePreset(1);
        }

        [BindableMethod(name = "(2) Sanctum Waves", category = BindableCategory.Colo3)]
        public static void Colo3Preset2()
        {
            ColoGoldWaveChanger.SetWavePreset(2);
        }

        [BindableMethod(name = "(3) Mawlurks", category = BindableCategory.Colo3)]
        public static void Colo3Preset3()
        {
            ColoGoldWaveChanger.SetWavePreset(3);
        }

        [BindableMethod(name = "(4) Floorless", category = BindableCategory.Colo3)]
        public static void Colo3Preset4()
        {
            ColoGoldWaveChanger.SetWavePreset(4);
        }

        [BindableMethod(name = "(5) Final Waves", category = BindableCategory.Colo3)]
        public static void Colo3Preset5()
        {
            ColoGoldWaveChanger.SetWavePreset(5);
        }

        [BindableMethod(name = "(6) GodTamer", category = BindableCategory.Colo3)]
        public static void Colo3Preset6()
        {
            ColoGoldWaveChanger.SetWavePreset(6);
        }
    }
}
