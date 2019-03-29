using System;
using System.Runtime.InteropServices;

namespace CoreRtOffsetOf
{
    [StructLayout(LayoutKind.Sequential)]
    public struct StructA
    {
        public int x;
        public int y;
    }

    public static class Program
    {
        [DllImport("CoreRtHint")]
        public static extern void Hint(StructA a);

        public static void Main(string[] args)
        {
            if (args.Length == Int32.MaxValue)
            {
                Hint(default);
            }

            try
            { Console.WriteLine(Marshal.OffsetOf<StructA>(nameof(StructA.y))); }
            catch (Exception ex)
            { Console.Error.WriteLine(ex); }

            try
            { Console.WriteLine(Marshal.OffsetOf(typeof(StructA), nameof(StructA.y))); }
            catch (Exception ex)
            { Console.Error.WriteLine(ex); }

            Console.WriteLine("Done.");
            Console.ReadLine();
        }
    }
}
