namespace DebugMod
{
    public static partial class BindableFunctions
    {
        [BindableMethod(name = "Kill All", category = BindableCategory.Cheats)]
        public static void KillAll()
        {
            PlayMakerFSM.BroadcastEvent("INSTA KILL");
            Console.AddLine("INSTA KILL broadcasted!");
        }

        [BindableMethod(name = "Infinite Jump", category = BindableCategory.Cheats)]
        public static void ToggleInfiniteJump()
        {
            PlayerData.instance.infiniteAirJump = !PlayerData.instance.infiniteAirJump;
            Console.AddLine("Infinite Jump set to " + PlayerData.instance.infiniteAirJump.ToString().ToUpper());
        }

        [BindableMethod(name = "Infinite Soul", category = BindableCategory.Cheats)]
        public static void ToggleInfiniteSoul()
        {
            DebugMod.infiniteSoul = !DebugMod.infiniteSoul;
            Console.AddLine("Infinite SOUL set to " + DebugMod.infiniteSoul.ToString().ToUpper());
        }

        [BindableMethod(name = "Infinite HP", category = BindableCategory.Cheats)]
        public static void ToggleInfiniteHP()
        {
            DebugMod.infiniteHP = !DebugMod.infiniteHP;
            Console.AddLine("Infinite HP set to " + DebugMod.infiniteHP.ToString().ToUpper());
        }

        [BindableMethod(name = "Invincibility", category = BindableCategory.Cheats)]
        public static void ToggleInvincibility()
        {
            PlayerData.instance.isInvincible = !PlayerData.instance.isInvincible;
            Console.AddLine("Invincibility set to " + PlayerData.instance.isInvincible.ToString().ToUpper());

            DebugMod.playerInvincible = PlayerData.instance.isInvincible;
        }

        [BindableMethod(name = "Noclip", category = BindableCategory.Cheats)]
        public static void ToggleNoclip()
        {
            DebugMod.noclip = !DebugMod.noclip;

            if (DebugMod.noclip)
            {
                if (!DebugMod.playerInvincible)
                    ToggleInvincibility();
                Console.AddLine("Enabled noclip");
                DebugMod.noclipPos = DebugMod.RefKnight.transform.position;
            }
            else
            {
                if (DebugMod.playerInvincible)
                    ToggleInvincibility();
                Console.AddLine("Disabled noclip");
            }
        }

        [BindableMethod(name = "Kill Self", category = BindableCategory.Cheats)]
        public static void KillSelf()
        {
            if (DebugMod.GM.isPaused) UIManager.instance.TogglePauseGame();
            HeroController.instance.TakeHealth(9999);

            HeroController.instance.heroDeathPrefab.SetActive(true);
            DebugMod.GM.ReadyForRespawn();
            GameCameras.instance.hudCanvas.gameObject.SetActive(false);
            GameCameras.instance.hudCanvas.gameObject.SetActive(true);
        }

        [BindableMethod(name = "Toggle Hero Collider", category = BindableCategory.Cheats)]
        public static void ToggleHeroCollider()
        {
            if (!DebugMod.RefHeroCollider.enabled)
            {
                DebugMod.RefHeroCollider.enabled = true;
                DebugMod.RefHeroBox.enabled = true;
                Console.AddLine("Enabled hero collider" + (DebugMod.noclip ? " and disabled noclip" : ""));
                DebugMod.noclip = false;
            }
            else
            {
                DebugMod.RefHeroCollider.enabled = false;
                DebugMod.RefHeroBox.enabled = false;
                Console.AddLine("Disabled hero collider" + (DebugMod.noclip ? "" : " and enabled noclip"));
                DebugMod.noclip = true;
                DebugMod.noclipPos = DebugMod.RefKnight.transform.position;
            }
        }
    }
}
