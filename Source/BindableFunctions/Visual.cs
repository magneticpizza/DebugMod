using System;
using DebugMod.Hitbox;
using UnityEngine;

namespace DebugMod
{
    public static partial class BindableFunctions
    {
        [BindableMethod(name = "Show Hitboxes", category = BindableCategory.Visual)]
        public static void ShowHitboxes()
        {
            if (++DebugMod.settings.ShowHitBoxes > 2) DebugMod.settings.ShowHitBoxes = 0;
            Console.AddLine("Toggled show hitboxes: " + DebugMod.settings.ShowHitBoxes);
        }

        [BindableMethod(name = "Toggle Vignette", category = BindableCategory.Visual)]
        public static void ToggleVignette()
        {
            HeroController.instance.vignette.enabled = !HeroController.instance.vignette.enabled;
        }

        [BindableMethod(name = "Deactivate Visual Masks", category = BindableCategory.Visual)]
        public static void DeactivateVisualMasks()
        {
            int ctr = 0;

            void disableMask(GameObject go)
            {
                foreach (Renderer r in go.GetComponentsInChildren<Renderer>())
                {
                    if (r.enabled)
                    {
                        ctr++;
                        r.enabled = false;
                    }
                }
            }

            float knightZ = HeroController.instance.transform.position.z;
            foreach (GameObject go in GameObject.FindObjectsOfType<GameObject>())
            {
                if (go.transform.position.z > knightZ) continue;

                // A collection of ways to identify masks. It's possible some slip through the cracks I guess
                if (go.name.StartsWith("msk_"))
                    disableMask(go);
                else if (go.name.StartsWith("Tut_msk"))
                    disableMask(go);
                else if (go.name.StartsWith("black_solid"))
                    disableMask(go);
                else if (go.name.ToLower().Contains("vignette"))
                    disableMask(go);
                else if (go.LocateMyFSM("unmasker") is PlayMakerFSM)
                    disableMask(go);
                else if (go.LocateMyFSM("remasker_inverse") is PlayMakerFSM)
                    disableMask(go);
                else if (go.LocateMyFSM("remasker") is PlayMakerFSM)
                    disableMask(go);
            }

            Console.AddLine($"Deactivated {ctr} masks");
        }

        [BindableMethod(name = "Toggle Hero Light", category = BindableCategory.Visual)]
        public static void ToggleHeroLight()
        {
            GameObject gameObject = DebugMod.RefKnight.transform.Find("HeroLight").gameObject;
            Color color = gameObject.GetComponent<SpriteRenderer>().color;
            if (Math.Abs(color.a) > 0f)
            {
                color.a = 0f;
                gameObject.GetComponent<SpriteRenderer>().color = color;
                Console.AddLine("Rendering HeroLight invisible...");
            }
            else
            {
                color.a = 0.7f;
                gameObject.GetComponent<SpriteRenderer>().color = color;
                Console.AddLine("Rendering HeroLight visible...");
            }
        }

        [BindableMethod(name = "Toggle HUD", category = BindableCategory.Visual)]
        public static void ToggleHUD()
        {
            if (GameCameras.instance.hudCanvas.gameObject.activeInHierarchy)
            {
                GameCameras.instance.hudCanvas.gameObject.SetActive(false);
                Console.AddLine("Disabling HUD...");
            }
            else
            {
                GameCameras.instance.hudCanvas.gameObject.SetActive(true);
                Console.AddLine("Enabling HUD...");
            }
        }

        [BindableMethod(name = "Toggle Camera Shake", category = BindableCategory.Visual)]
        public static void ToggleCameraShake()
        {
            bool newValue = !GameCameras.instance.cameraShakeFSM.enabled;
            GameCameras.instance.cameraShakeFSM.enabled = newValue;
            Console.AddLine($"{(newValue ? "Enabling" : "Disabling")} Camera Shake...");
        }

        [BindableMethod(name = "Reset Camera Zoom", category = BindableCategory.Visual)]
        public static void ResetZoom()
        {
            GameCameras.instance.tk2dCam.ZoomFactor = 1f;
            Console.AddLine("Zoom factor was reset");
        }

        [BindableMethod(name = "Zoom In", category = BindableCategory.Visual)]
        public static void ZoomIn()
        {
            float zoomFactor = GameCameras.instance.tk2dCam.ZoomFactor;
            GameCameras.instance.tk2dCam.ZoomFactor = zoomFactor + zoomFactor * 0.05f;
            Console.AddLine("Zoom level increased to: " + GameCameras.instance.tk2dCam.ZoomFactor);
        }

        [BindableMethod(name = "Zoom Out", category = BindableCategory.Visual)]
        public static void ZoomOut()
        {
            float zoomFactor2 = GameCameras.instance.tk2dCam.ZoomFactor;
            GameCameras.instance.tk2dCam.ZoomFactor = zoomFactor2 - zoomFactor2 * 0.05f;
            Console.AddLine("Zoom level decreased to: " + GameCameras.instance.tk2dCam.ZoomFactor);
        }

        [BindableMethod(name = "Hide Hero", category = BindableCategory.Visual)]
        public static void HideHero()
        {
            tk2dSprite component = DebugMod.RefKnight.GetComponent<tk2dSprite>();
            Color color = component.color;
            if (Math.Abs(color.a) > 0f)
            {
                color.a = 0f;
                component.color = color;
                Console.AddLine("Rendering Hero sprite invisible...");
            }
            else
            {
                color.a = 1f;
                component.color = color;
                Console.AddLine("Rendering Hero sprite visible...");
            }
        }

        [BindableMethod(name = "Shade Spawn Points", category = BindableCategory.Visual)]
        public static void ShadeSpawnPoint()
        {
            if (DebugMod.HC == null)
            {
                Console.AddLine("Player isn't in scene. How did you reach here?");
                return;
            }

            var component = HeroController.instance.gameObject.GetComponent<ShadeSpawnLocation>();
            if (component == null) HeroController.instance.gameObject.AddComponent<ShadeSpawnLocation>();
            //not gonna delete component if disabled to not break something if someone spams

            ShadeSpawnLocation.EnabledCompass = !ShadeSpawnLocation.EnabledCompass;
            Console.AddLine("Shade spawn point toggled " + (ShadeSpawnLocation.EnabledCompass ? "On" : "Off"));
        }

        [BindableMethod(name = "Shade Retreat Border", category = BindableCategory.Visual)]
        public static void ShowShadeRetreatBorder()
        {
            if (DebugMod.HC == null)
            {
                Console.AddLine("Player isn't in scene. How did you reach here?");
                return;
            }

            var component = HeroController.instance.gameObject.GetComponent<ShadeSpawnLocation>();
            if (component == null) HeroController.instance.gameObject.AddComponent<ShadeSpawnLocation>();
            //not gonna delete component if disabled to not break something if someone spams

            if (!ShadeSpawnLocation.EnabledCompass)
            {
                ShadeSpawnLocation.EnabledCompass = true;
            }

            if (++ShadeSpawnLocation.ShowShadeRetreatBorder > 2) ShadeSpawnLocation.ShowShadeRetreatBorder = 0;

            string displaytext = ShadeSpawnLocation.ShowShadeRetreatBorder switch
            {
                1 => "Closest",
                2 => "All",
                _ => "None"
            };

            Console.AddLine($"Shade Reach Showing {displaytext}");
        }
    }
}
