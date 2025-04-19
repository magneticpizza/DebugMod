using System;
using System.ComponentModel;
using System.Reflection;
using TMPro;

namespace DebugMod
{
    // Better solution for categories than making sure all the names are the same per function.
    // To add a new category, add it in the order you'd like it to appear in the panel pages.
    // If your category has a specific name you'd like it to use (usually if it uses a space), add a Description tag with the appropriate name.
    public enum BindableCategory
    {
        Misc,
        [Description("Gameplay Altering")]
        GameplayAltering,
        SaveStates,
        [Description("SaveState Pages")]
        SaveStatePages,
        Visual,
        [Description("Mod UI")]
        ModUI,
        [Description("Enemy Panel")]
        EnemyPanel,
        Cheats,
        Charms,
        Skills,
        Spells,
        Bosses,
        Items,
        [Description("Masks & Vessels")]
        MasksAndVessels,
        Consumables,
        Dreamgate,
        [Description("Export Data")]
        ExportData,
        Loggers,
        [Description("Player Movement")]
        PlayerMovement,
        [Description("Colosseum 1")]
        Colo1,
        [Description("Colosseum 2")]
        Colo2,
        [Description("Colosseum 3")]
        Colo3
    }
    public static class EnumExtensions
    {
        /// <summary>
        /// Use this instead of ToString() if members of your enum have Description attributes.
        /// </summary>
        public static string GetDescription(this Enum enumeration)
        {
            Type type = enumeration.GetType();
            MemberInfo[] memInfo = type.GetMember(enumeration.ToString());

            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attrs != null && attrs.Length > 0)
                    return ((DescriptionAttribute)attrs[0]).Description;
            }

            return enumeration.ToString();
        }
    }
}
