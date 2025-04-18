namespace DebugMod
{
    public static partial class BindableFunctions
    {
        [BindableMethod(name = "Update DG Data", category = BindableCategory.Dreamgate)]
        public static void ReadDGData()
        {
            DreamGate.delMenu = false;
            if (!DreamGate.dataBusy)
            {
                Console.AddLine("Updating DGdata from the file...");
                DreamGate.ReadData(true);
            }
        }

        [BindableMethod(name = "Save DG Data", category = BindableCategory.Dreamgate)]
        public static void SaveDGData()
        {
            DreamGate.delMenu = false;
            if (!DreamGate.dataBusy)
            {
                Console.AddLine("Writing DGdata to the file...");
                DreamGate.WriteData();
            }
        }

        [BindableMethod(name = "Add DG Position", category = BindableCategory.Dreamgate)]
        public static void AddDGPosition()
        {
            DreamGate.delMenu = false;

            string entryName = DebugMod.GM.GetSceneNameString();
            int i = 1;

            if (entryName.Length > 5) entryName = entryName.Substring(0, 5);

            while (DreamGate.dgData.ContainsKey(entryName))
            {
                entryName = DebugMod.GM.GetSceneNameString() + i;
                i++;
            }

            DreamGate.AddEntry(entryName);
        }
    }
}
