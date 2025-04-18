using System.Linq;
using GlobalEnums;
using UnityEngine;

namespace DebugMod
{
    public static partial class BindableFunctions
    {
        [BindableMethod(name = "Toggle Hitboxes (enemy panel)", category = BindableCategory.EnemyPanel)]
        public static void ToggleEnemyCollision()
        {
            EnemiesPanel.hitboxes = !EnemiesPanel.hitboxes;

            if (EnemiesPanel.hitboxes)
            {
                Console.AddLine("Enabled hitboxes");
            }
            else
            {
                Console.AddLine("Disabled hitboxes");
            }
        }

        [BindableMethod(name = "Toggle HP Bars", category = BindableCategory.EnemyPanel)]
        public static void ToggleEnemyHPBars()
        {
            EnemiesPanel.hpBars = !EnemiesPanel.hpBars;

            if (EnemiesPanel.hpBars)
            {
                Console.AddLine("Enabled HP bars");
            }
            else
            {
                Console.AddLine("Disabled HP bars");
            }
        }

        [BindableMethod(name = "Toggle Enemy Scan", category = BindableCategory.EnemyPanel)]
        public static void ToggleEnemyAutoScan()
        {
            EnemiesPanel.autoUpdate = !EnemiesPanel.autoUpdate;

            if (EnemiesPanel.autoUpdate)
            {
                Console.AddLine("Enabled auto-scan (May impact performance)");
            }
            else
            {
                Console.AddLine("Disabled auto-scan");
            }
        }

        [BindableMethod(name = "Enemy Scan", category = BindableCategory.EnemyPanel)]
        public static void EnemyScan()
        {
            EnemiesPanel.EnemyUpdate(200f);
            Console.AddLine("Refreshing collider data...");
        }

        [BindableMethod(name = "Self Damage", category = BindableCategory.EnemyPanel)]
        public static void SelfDamage()
        {
            if (PlayerData.instance.health <= 0 || HeroController.instance.cState.dead || !GameManager.instance.IsGameplayScene() || GameManager.instance.IsGamePaused() || HeroController.instance.cState.recoiling || HeroController.instance.cState.invulnerable)
            {
                Console.AddLine("Unacceptable conditions for selfDamage(" + PlayerData.instance.health.ToString() + "," + DebugMod.HC.cState.dead.ToString() + "," + DebugMod.GM.IsGameplayScene().ToString() + "," + DebugMod.HC.cState.recoiling.ToString() + "," + DebugMod.GM.IsGamePaused().ToString() + "," + DebugMod.HC.cState.invulnerable.ToString() + ")." + " Pressed too many times at once?");
                return;
            }
            if (!DebugMod.settings.EnemiesPanelVisible)
            {
                Console.AddLine("Enable EnemyPanel for self-damage");
                return;
            }
            if (EnemiesPanel.enemyPool.Count < 1)
            {
                Console.AddLine("Unable to locate a single enemy in the scene.");
                return;
            }

            GameObject enemyObj = EnemiesPanel.enemyPool.ElementAt(new System.Random().Next(0, EnemiesPanel.enemyPool.Count)).gameObject;
            CollisionSide side = HeroController.instance.cState.facingRight ? CollisionSide.right : CollisionSide.left;
            int damageAmount = 1;
            int hazardType = (int)HazardType.NON_HAZARD;

            PlayMakerFSM fsm = FSMUtility.LocateFSM(enemyObj, "damages_hero");
            if (fsm != null)
            {
                damageAmount = FSMUtility.GetInt(fsm, "damageDealt");
                hazardType = FSMUtility.GetInt(fsm, "hazardType");
            }

            object[] paramArray;

            if (EnemiesPanel.parameters.Length == 2)
            {
                paramArray = new object[] { enemyObj, side };
            }
            else if (EnemiesPanel.parameters.Length == 4)
            {
                paramArray = new object[] { enemyObj, side, damageAmount, hazardType };
            }
            else
            {
                Console.AddLine("Unexpected parameter count on HeroController.TakeDamage");
                return;
            }

            Console.AddLine("Attempting self damage");
            EnemiesPanel.takeDamage.Invoke(HeroController.instance, paramArray);
        }
    }
}
