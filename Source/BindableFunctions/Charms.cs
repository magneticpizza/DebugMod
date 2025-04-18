namespace DebugMod
{
    public static partial class BindableFunctions
    {
        [BindableMethod(name = "Give All Charms", category = BindableCategory.Charms)]
        public static void GiveAllCharms()
        {
            for (int i = 1; i <= 40; i++)
            {
                PlayerData.instance.SetBoolInternal("gotCharm_" + i, true);

                if (i == 36 && !DebugMod.GrimmTroupe())
                {
                    break;
                }
            }

            PlayerData.instance.charmSlots = 10;
            PlayerData.instance.hasCharm = true;
            PlayerData.instance.charmsOwned = 40;
            PlayerData.instance.royalCharmState = 4;
            PlayerData.instance.gotShadeCharm = true;
            PlayerData.instance.gotKingFragment = true;
            PlayerData.instance.gotQueenFragment = true;
            PlayerData.instance.notchShroomOgres = true;
            PlayerData.instance.notchFogCanyon = true;
            PlayerData.instance.colosseumBronzeOpened = true;
            PlayerData.instance.colosseumBronzeCompleted = true;
            PlayerData.instance.salubraNotch1 = true;
            PlayerData.instance.salubraNotch2 = true;
            PlayerData.instance.salubraNotch3 = true;
            PlayerData.instance.salubraNotch4 = true;

            if (DebugMod.GrimmTroupe())
            {
                PlayerData.instance.SetBoolInternal("fragileGreed_unbreakable", true);
                PlayerData.instance.SetBoolInternal("fragileHealth_unbreakable", true);
                PlayerData.instance.SetBoolInternal("fragileStrength_unbreakable", true);
                PlayerData.instance.SetIntInternal("grimmChildLevel", 5);
                PlayerData.instance.gotGrimmNotch = true;
                PlayerData.instance.charmSlots = 11;
            }

            Console.AddLine("Added all charms to inventory");
        }

        [BindableMethod(name = "Increment Kingsoul", category = BindableCategory.Charms)]
        public static void IncreaseKingsoulLevel()
        {
            if (!PlayerData.instance.gotCharm_36)
            {
                PlayerData.instance.gotCharm_36 = true;
            }

            PlayerData.instance.royalCharmState++;
            
            PlayerData.instance.gotShadeCharm = PlayerData.instance.royalCharmState == 4;

                if (PlayerData.instance.royalCharmState >= 5)
            {
                PlayerData.instance.royalCharmState = 0;
            }
        }

        [BindableMethod(name = "Fix Fragile Heart", category = BindableCategory.Charms)]
        public static void FixFragileHeart()
        {
            if (PlayerData.instance.brokenCharm_23)
            {
                PlayerData.instance.brokenCharm_23 = false;
                Console.AddLine("Fixed fragile heart");
            }
        }

        [BindableMethod(name = "Fix Fragile Greed", category = BindableCategory.Charms)]
        public static void FixFragileGreed()
        {
            if (PlayerData.instance.brokenCharm_24)
            {
                PlayerData.instance.brokenCharm_24 = false;
                Console.AddLine("Fixed fragile greed");
            }
        }

        [BindableMethod(name = "Fix Fragile Strength", category = BindableCategory.Charms)]
        public static void FixFragileStrength()
        {
            if (PlayerData.instance.brokenCharm_25)
            {
                PlayerData.instance.brokenCharm_25 = false;
                Console.AddLine("Fixed fragile strength");
            }
        }

        [BindableMethod(name = "Can Overcharm", category = BindableCategory.Charms)]
        public static void ToggleOvercharm()
        {
            PlayerData.instance.canOvercharm = true;
            PlayerData.instance.overcharmed = !PlayerData.instance.overcharmed;

            Console.AddLine("Set overcharmed: " + PlayerData.instance.overcharmed);
        }

        [BindableMethod(name = "Increment Grimmchild", category = BindableCategory.Charms)]
        public static void IncreaseGrimmchildLevel()
        {
            if (!DebugMod.GrimmTroupe())
            {
                Console.AddLine("Grimmchild does not exist on this patch");
                return;
            }

            if (!PlayerData.instance.GetBoolInternal("gotCharm_40"))
            {
                PlayerData.instance.SetBoolInternal("gotCharm_40", true);
            }

            PlayerData.instance.SetIntInternal("grimmChildLevel", PlayerData.instance.GetIntInternal("grimmChildLevel") + 1);

            if (PlayerData.instance.GetIntInternal("grimmChildLevel") >= 6)
            {
                PlayerData.instance.SetIntInternal("grimmChildLevel", 0);
            }
        }
    }
}
