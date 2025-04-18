namespace DebugMod
{
    public static partial class BindableFunctions
    {
        [BindableMethod(name = "Dump Console", category = BindableCategory.Console)]
        public static void DumpConsoleLog()
        {
            Console.AddLine("Saving console log...");
            Console.SaveHistory();
        }
    }
}
