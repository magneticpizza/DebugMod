using UnityEngine;

namespace DebugMod
{
    public static partial class BindableFunctions
    {
        [BindableMethod(name = "Move 0.1 units to the right", category = BindableCategory.PlayerMovement)]
        public static void MoveRight()
        {
            var HeroPos = HeroController.instance.transform.position;
            HeroController.instance.transform.position = new Vector3(HeroPos.x + DebugMod.AmountToMove, HeroPos.y);
            Console.AddLine("Moved player 0.1 units to the right");
        }
        [BindableMethod(name = "Move 0.1 units to the left", category = BindableCategory.PlayerMovement)]
        public static void MoveL()
        {
            var HeroPos = HeroController.instance.transform.position;
            HeroController.instance.transform.position = new Vector3(HeroPos.x - DebugMod.AmountToMove, HeroPos.y);
            Console.AddLine("Moved player 0.1 units to the left");
        }
        [BindableMethod(name = "Move 0.1 units up", category = BindableCategory.PlayerMovement)]
        public static void MoveUp()
        {
            var HeroPos = HeroController.instance.transform.position;
            HeroController.instance.transform.position = new Vector3(HeroPos.x, HeroPos.y + DebugMod.AmountToMove);
            Console.AddLine("Moved player 0.1 units to the up");
        }
        [BindableMethod(name = "Move 0.1 units down", category = BindableCategory.PlayerMovement)]
        public static void MoveDown()
        {
            var HeroPos = HeroController.instance.transform.position;
            HeroController.instance.transform.position = new Vector3(HeroPos.x, HeroPos.y - DebugMod.AmountToMove);
            Console.AddLine("Moved player 0.1 units to the down");
        }

        [BindableMethod(name = "FaceLeft", category = BindableCategory.PlayerMovement)]
        public static void FaceLeft()
        {
            HeroController.instance.FaceLeft();
            Console.AddLine("Made player face left");
        }

        [BindableMethod(name = "FaceRight", category = BindableCategory.PlayerMovement)]
        public static void FaceRight()
        {
            HeroController.instance.FaceRight();
            Console.AddLine("Made player face right");
        }
    }
}
