namespace DebugMod
{
    public static partial class BindableFunctions
    {
        [BindableMethod(name = "Give Pale Ore", category = BindableCategory.Consumables)]
        public static void GivePaleOre()
        {
            PlayerData.instance.ore = 6;
            Console.AddLine("Set player pale ore to 6");
        }

        [BindableMethod(name = "Give Simple Keys", category = BindableCategory.Consumables)]
        public static void GiveSimpleKey()
        {
            PlayerData.instance.simpleKeys = 3;
            Console.AddLine("Set player simple keys to 3");
        }

        [BindableMethod(name = "Give Rancid Eggs", category = BindableCategory.Consumables)]
        public static void GiveRancidEgg()
        {
            PlayerData.instance.rancidEggs += 10;
            Console.AddLine("Giving player 10 rancid eggs");
        }

        [BindableMethod(name = "Give Geo", category = BindableCategory.Consumables)]
        public static void GiveGeo()
        {
            HeroController.instance.AddGeo(1000);
            Console.AddLine("Giving player 1000 geo");
        }

        [BindableMethod(name = "Give Essence", category = BindableCategory.Consumables)]
        public static void GiveEssence()
        {
            PlayerData.instance.dreamOrbs += 100;
            Console.AddLine("Giving player 100 essence");
        }
    }
}
