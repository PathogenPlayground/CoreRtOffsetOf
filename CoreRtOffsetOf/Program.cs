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

    [StructLayout(LayoutKind.Sequential)]
    public struct StructB
    {
        public int x;
        public int y;
    }

    public static class Program
    {
        [DllImport("CoreRtHint")]
        public static extern void TestA(StructA a);

        [DllImport("CoreRtHint")]
        public static extern void TestB(StructB b);

        public static void Main(string[] args)
        {
            if (args.Length == Int32.MaxValue)
            {
                TestA(default);
                TestB(default);
            }

            try
            { Console.WriteLine(Marshal.OffsetOf<StructA>(nameof(StructA.y))); }
            catch (Exception ex)
            { Console.Error.WriteLine(ex); }

            try
            { Console.WriteLine(Marshal.OffsetOf(typeof(StructA), nameof(StructA.y))); }
            catch (Exception ex)
            { Console.Error.WriteLine(ex); }

            try
            { Console.WriteLine(Marshal.OffsetOf(typeof(StructB), nameof(StructB.y))); }
            catch (Exception ex)
            { Console.Error.WriteLine(ex); }

            Console.WriteLine("Done.");
            Console.ReadLine();
        }
    }
}
