using System;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DebugMod
{
    public static partial class BindableFunctions
    {
        private static readonly FieldInfo TimeSlowed = typeof(GameManager).GetField("timeSlowed", BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Static);
        private static readonly FieldInfo IgnoreUnpause = typeof(UIManager).GetField("ignoreUnpause", BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Static);
        internal static readonly FieldInfo cameraGameplayScene = typeof(CameraController).GetField("isGameplayScene", BindingFlags.Instance | BindingFlags.NonPublic);
        private static bool corniferYeeteded = false;

        [BindableMethod(name = "Clear White Screen", category = BindableCategory.Misc)]
        public static void ClearWhiteScreen()
        {
            //fix white screen 
            string wakeControl = "Dream Return";
            GameObject knight = GameObject.Find("Knight");
            PlayMakerFSM wakeFSM = knight.LocateMyFSM(wakeControl);
            wakeFSM.SetState("GET UP");
            wakeFSM.SendEvent("FINISHED");
            GameObject.Find("Blanker White").LocateMyFSM("Blanker Control").SendEvent("FADE OUT");
            HeroController.instance.EnableRenderer();
        }

        [BindableMethod(name = "Reset Encounters", category = BindableCategory.Misc)]
        public static void ResetProxyFSMEncounters()
        {
            try
            {
                //literally couldnt figure out how to get this to not be awful to look at
                //this resets the fsm responsible for a couple weird persistent values with the knight
                GameObject knight = GameObject.Find("Knight");
                PlayMakerFSM proxyFSM = knight.LocateMyFSM("ProxyFSM");
                proxyFSM.FsmVariables.FindFsmBool("Faced Radiance").Value = false;
                proxyFSM.FsmVariables.FindFsmBool("Faced Nightmare").Value = false;
                proxyFSM.FsmVariables.FindFsmBool("Faced Zote").Value = false;


            }
            catch (Exception e)
            {
                Console.AddLine("Error while attempting to reset Proxy variables");
                DebugMod.instance.Log("Error while attempting to reset Knight-ProxyFSM variables: \n" + e);
            }
        }

        [BindableMethod(name = "Nail Damage +4", category = BindableCategory.Misc)]
        public static void IncreaseNailDamage()
        {
            int num = 4;
            if (PlayerData.instance.nailDamage == 0)
            {
                num = 5;
            }
            PlayerData.instance.nailDamage = PlayerData.instance.nailDamage + num;
            PlayMakerFSM.BroadcastEvent("UPDATE NAIL DAMAGE");
            Console.AddLine("Increased base nailDamage by " + num.ToString());
        }

        [BindableMethod(name = "Nail Damage -4", category = BindableCategory.Misc)]
        public static void DecreaseNailDamage()
        {
            int num2 = PlayerData.instance.nailDamage - 4;
            if (num2 >= 0)
            {
                PlayerData.instance.nailDamage = num2;
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

        [BindableMethod(name = "Force Pause", category = BindableCategory.Misc)]
        public static void ForcePause()
        {
            try
            {
                if ((PlayerData.instance.disablePause || (bool)TimeSlowed.GetValue(GameManager.instance) || (bool)IgnoreUnpause.GetValue(UIManager.instance)) && DebugMod.GetSceneName() != "Menu_Title" && DebugMod.GM.IsGameplayScene())
                {
                    TimeSlowed.SetValue(GameManager.instance, false);
                    IgnoreUnpause.SetValue(UIManager.instance, false);
                    PlayerData.instance.disablePause = false;
                    UIManager.instance.TogglePauseGame();
                    Console.AddLine("Forcing Pause Menu because pause is disabled");
                }
                else
                {
                    Console.AddLine("Game does not report that Pause is disabled, requesting it normally.");
                    UIManager.instance.TogglePauseGame();
                }
            }
            catch (Exception e)
            {
                Console.AddLine("Error while attempting to pause, check ModLog.txt");
                DebugMod.instance.Log("Error while attempting force pause:\n" + e);
            }
        }

        [BindableMethod(name = "Hazard Respawn", category = BindableCategory.Misc)]
        public static void Respawn()
        {
            if (GameManager.instance.IsGameplayScene() && !HeroController.instance.cState.dead && PlayerData.instance.health > 0)
            {
                if (UIManager.instance.uiState.ToString() == "PAUSED")
                {
                    UIManager.instance.TogglePauseGame();
                    GameManager.instance.HazardRespawn();
                    Console.AddLine("Closing Pause Menu and respawning...");
                    return;
                }
                if (UIManager.instance.uiState.ToString() == "PLAYING")
                {
                    HeroController.instance.RelinquishControl();
                    GameManager.instance.HazardRespawn();
                    HeroController.instance.RegainControl();
                    Console.AddLine("Respawn signal sent");
                    return;
                }
                Console.AddLine("Respawn requested in some weird conditions, abort, ABORT");
            }
        }

        [BindableMethod(name = "Set Respawn", category = BindableCategory.Misc)]
        public static void SetHazardRespawn()
        {
            Vector3 manualRespawn = DebugMod.RefKnight.transform.position;
            HeroController.instance.SetHazardRespawn(manualRespawn, false);
            Console.AddLine("Manual respawn point on this map set to" + manualRespawn.ToString());
        }

        [BindableMethod(name = "Force Camera Follow", category = BindableCategory.Misc)]
        public static void ForceCameraFollow()
        {
            if (!DebugMod.cameraFollow)
            {
                Console.AddLine("Forcing camera follow");
                DebugMod.cameraFollow = true;
            }
            else
            {
                DebugMod.cameraFollow = false;
                BindableFunctions.cameraGameplayScene.SetValue(DebugMod.RefCamera, true);
                Console.AddLine("Returning camera to normal settings");
            }
        }

        [BindableMethod(name = "Decrease Timescale", category = BindableCategory.Misc)]
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

        [BindableMethod(name = "Increase Timescale", category = BindableCategory.Misc)]
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

        private static void CorniferYeeted(Scene current, Scene next) => CorniferYeeted();

        private static void CorniferYeeted()
        {
            (from x in UnityEngine.Object.FindObjectsOfType<GameObject>()
             where x.name.Contains("Cornifer")
             select x).ToList<GameObject>().ForEach(delegate (GameObject x)
             {
                 UnityEngine.Object.Destroy(x);
             });
        }


        [BindableMethod(name = "Reset Debug States", category = BindableCategory.Misc)]
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

        [BindableMethod(name = "Yeet Cornifer-Toggle", category = BindableCategory.Misc)]
        public static void CorniferYeet()
        {
            corniferYeeteded = !corniferYeeteded;

            if (corniferYeeteded)
            {
                CorniferYeeted();
                UnityEngine.SceneManagement.SceneManager.activeSceneChanged += CorniferYeeted;
                Console.AddLine("Cornifer yeeted on next loads lol");
            }
            else
            {
                UnityEngine.SceneManagement.SceneManager.activeSceneChanged -= CorniferYeeted;
                Console.AddLine("Cornifer unyeeted from next loads on???");
            }
        }
    }
}
