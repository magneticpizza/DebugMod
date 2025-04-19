namespace DebugMod
{
    public static partial class BindableFunctions
    {
        [BindableMethod(name = "Give Mask", category = BindableCategory.MasksAndVessels)]
        public static void GiveMask()
        {
            if (PlayerData.instance.maxHealthBase < 9)
            {
                HeroController.instance.MaxHealth();
                HeroController.instance.AddToMaxHealth(1);
                PlayMakerFSM.BroadcastEvent("MAX HP UP");
                Console.AddLine("Added Mask");
            }
            else
            {
                Console.AddLine("You have the maximum number of masks");
            }
        }

        [BindableMethod(name = "Give Vessel", category = BindableCategory.MasksAndVessels)]
        public static void GiveVessel()
        {
            if (PlayerData.instance.MPReserveMax < 99)
            {
                HeroController.instance.AddToMaxMPReserve(33);
                PlayMakerFSM.BroadcastEvent("NEW SOUL ORB");
                Console.AddLine("Added Vessel");
            }
            else
            {
                Console.AddLine("You have the maximum number of vessels");
            }
        }

        [BindableMethod(name = "Take Away Mask", category = BindableCategory.MasksAndVessels)]
        public static void TakeAwayMask()
        {
            if (PlayerData.instance.maxHealthBase > 1)
            {
                PlayerData.instance.maxHealth -= 1;
                PlayerData.instance.maxHealthBase -= 1;
                if (!GameCameras.instance.hudCanvas.gameObject.activeInHierarchy)
                    GameCameras.instance.hudCanvas.gameObject.SetActive(true);
                else
                {
                    GameCameras.instance.hudCanvas.gameObject.SetActive(false);
                    GameCameras.instance.hudCanvas.gameObject.SetActive(true);
                }
                Console.AddLine("Took Away Mask");
            }
            else
            {
                Console.AddLine("You have the minimum number of masks");
            }
        }

        [BindableMethod(name = "Take Away Vessel", category = BindableCategory.MasksAndVessels)]
        public static void TakeAwayVessel()
        {
            if (PlayerData.instance.MPReserveMax > 0)
            {
                PlayerData.instance.MPReserveMax -= 33;
                if (!GameCameras.instance.hudCanvas.gameObject.activeInHierarchy)
                    GameCameras.instance.hudCanvas.gameObject.SetActive(true);
                else
                {
                    GameCameras.instance.hudCanvas.gameObject.SetActive(false);
                    GameCameras.instance.hudCanvas.gameObject.SetActive(true);
                }
                Console.AddLine("Removed Vessel");
            }
            else
            {
                Console.AddLine("You have the minimum number of vessels");
            }
        }

        [BindableMethod(name = "Add Health", category = BindableCategory.MasksAndVessels)]
        public static void AddHealth()
        {
            if (PlayerData.instance.health <= 0 || HeroController.instance.cState.dead || !GameManager.instance.IsGameplayScene())
            {
                Console.AddLine("Unacceptable conditions for adding health" + PlayerData.instance.health + "," + DebugMod.HC.cState.dead + "," + DebugMod.GM.IsGameplayScene() + "," + DebugMod.HC.cState.recoiling + "," + DebugMod.GM.IsGamePaused() + "," + DebugMod.HC.cState.invulnerable + ")." + " Pressed too many times at once?");
                return;
            }
            HeroController.instance.AddHealth(1);

            Console.AddLine("Added Health");
        }
        [BindableMethod(name = "Take Health", category = BindableCategory.MasksAndVessels)]
        public static void TakeHealth()
        {
            if (PlayerData.instance.health <= 0 || HeroController.instance.cState.dead || !GameManager.instance.IsGameplayScene())
            {
                Console.AddLine("Unacceptable conditions for taking health" + PlayerData.instance.health + "," + DebugMod.HC.cState.dead + "," + DebugMod.GM.IsGameplayScene() + "," + DebugMod.HC.cState.recoiling + "," + DebugMod.GM.IsGamePaused() + "," + DebugMod.HC.cState.invulnerable + ")." + " Pressed too many times at once?");
                return;
            }
            HeroController.instance.TakeHealth(1);

            Console.AddLine("Attempting to take health");
        }

        [BindableMethod(name = "Add Soul", category = BindableCategory.MasksAndVessels)]
        public static void AddSoul()
        {
            HeroController.instance.AddMPCharge(33);

            Console.AddLine("Added Soul");
        }
        [BindableMethod(name = "Take Soul", category = BindableCategory.MasksAndVessels)]
        public static void TakeSoul()
        {
            HeroController.instance.TakeMP(33);

            Console.AddLine("Attempting to take soul");
        }
        [BindableMethod(name = "Add Lifeblood", category = BindableCategory.MasksAndVessels)]
        public static void Lifeblood()
        {
            PlayMakerFSM.BroadcastEvent("ADD BLUE HEALTH");

            Console.AddLine("Attempting to add lifeblood");
        }
    }
}