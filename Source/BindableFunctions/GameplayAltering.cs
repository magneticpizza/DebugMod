using UnityEngine;

namespace DebugMod
{
    public static partial class BindableFunctions
    {
        [BindableMethod(name = "Nail Damage +4", category = BindableCategory.GameplayAltering)]
        public static void IncreaseNailDamage()
        {
            int num = 4;
            if (PlayerData.instance.nailDamage == 0)
            {
                num = 5;
            }
            PlayerData.instance.nailDamage += num;
            PlayMakerFSM.BroadcastEvent("UPDATE NAIL DAMAGE");
            Console.AddLine("Increased base nailDamage by " + num.ToString());
        }

        [BindableMethod(name = "Nail Damage -4", category = BindableCategory.GameplayAltering)]
        public static void DecreaseNailDamage()
        {
            int num = PlayerData.instance.nailDamage - 4;
            if (num >= 0)
            {
                PlayerData.instance.nailDamage = num;
                PlayMakerFSM.BroadcastEvent("UPDATE NAIL DAMAGE");
                Console.AddLine("Decreased base nailDamage by 4");
            }
            else
            {
                Console.AddLine("Cannot set base nailDamage less than 0 therefore forcing 0 value");
                PlayerData.instance.nailDamage = 0;
                PlayMakerFSM.BroadcastEvent("UPDATE NAIL DAMAGE");
            }
        }

        [BindableMethod(name = "Decrease Timescale", category = BindableCategory.GameplayAltering)]
        public static void TimescaleDown()
        {
            float timeScale = Time.timeScale;
            float num3 = timeScale - 0.1f;
            if (num3 > 0f)
            {
                Time.timeScale = num3;
                Console.AddLine("New TimeScale value: " + num3 + " Old value: " + timeScale);
            }
            else
            {
                Console.AddLine("Cannot set TimeScale equal or lower than 0");
            }
        }

        [BindableMethod(name = "Increase Timescale", category = BindableCategory.GameplayAltering)]
        public static void TimescaleUp()
        {
            float timeScale2 = Time.timeScale;
            float num4 = timeScale2 + 0.1f;
            if (num4 < 2f)
            {
                Time.timeScale = num4;
                Console.AddLine("New TimeScale value: " + num4 + " Old value: " + timeScale2);
            }
            else
            {
                Console.AddLine("Cannot set TimeScale greater than 2.0");
            }
        }

        [BindableMethod(name = "Reset Debug States", category = BindableCategory.GameplayAltering)]
        public static void Reset()
        {
            var pd = PlayerData.instance;
            var HC = HeroController.instance;
            var GC = GameCameras.instance;

            //nail damage
            pd.nailDamage = 5 + pd.nailSmithUpgrades * 4;
            PlayMakerFSM.BroadcastEvent("UPDATE NAIL DAMAGE");

            //Hero Light
            GameObject gameObject = DebugMod.RefKnight.transform.Find("HeroLight").gameObject;
            Color color = gameObject.GetComponent<SpriteRenderer>().color;
            color.a = 0.7f;
            gameObject.GetComponent<SpriteRenderer>().color = color;

            //HUD
            if (!GC.hudCanvas.gameObject.activeInHierarchy)
                GC.hudCanvas.gameObject.SetActive(true);

            //Hide Hero
            tk2dSprite component = DebugMod.RefKnight.GetComponent<tk2dSprite>();
            color = component.color; color.a = 1f;
            component.color = color;

            //rest all is self explanatory
            Time.timeScale = 1f;
            GC.tk2dCam.ZoomFactor = 1f;
            HC.vignette.enabled = false;
            EnemiesPanel.hitboxes = false;
            EnemiesPanel.hpBars = false;
            EnemiesPanel.autoUpdate = false;
            pd.infiniteAirJump = false;
            DebugMod.infiniteSoul = false;
            DebugMod.infiniteHP = false;
            pd.isInvincible = false;
            DebugMod.noclip = false;
        }
    }
}