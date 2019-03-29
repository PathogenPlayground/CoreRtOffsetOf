// The P/Invoke hints do not seem to have an impact either way on OffsetOf for either struct.
//#define USE_PINVOKE_HINTS
using System;
using System.Runtime.InteropServices;

namespace CoreRtOffsetOf
{
    [StructLayout(LayoutKind.Sequential)]
    public struct BlittableStruct
    {
        public int x;
        public int y;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MarshaledStruct
    {
        public int x;
        public int y;
        public string s;
    }

    public static class Program
    {
#if USE_PINVOKE_HINTS
        [DllImport("CoreRtHint")]
        public static extern void HintBlittableStruct(BlittableStruct a);

        [DllImport("CoreRtHint")]
        public static extern void HintMarshaledStruct(MarshaledStruct a);
#endif

        private static void Test(string description, Func<object> test)
        {
            Console.Write($"{description}: ");
            try
            { Console.WriteLine(test()); }
            catch (Exception ex)
            {
                ConsoleColor oldColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Error.WriteLine("FAILED");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Error.WriteLine(ex);
                Console.ForegroundColor = oldColor;
            }
        }

        public static unsafe void Main(string[] args)
        {
#if USE_PINVOKE_HINTS
            if (args.Length == Int32.MaxValue)
            {
                HintBlittableStruct(default);
                HintMarshaledStruct(default);
            }
#endif

            Test("SizeOf<BlittableStruct>", () => Marshal.SizeOf<BlittableStruct>());
            Test("SizeOf(BlittableStruct)", () => Marshal.SizeOf(typeof(BlittableStruct)));
            Test("sizeof(BlittableStruct)", () => sizeof(BlittableStruct));

            Test("SizeOf<MarshaledStruct>", () => Marshal.SizeOf<MarshaledStruct>());
            Test("SizeOf(MarshaledStruct)", () => Marshal.SizeOf(typeof(MarshaledStruct)));

            Test("OffsetOf<BlittableStruct>", () => Marshal.OffsetOf<BlittableStruct>(nameof(BlittableStruct.y)));
            Test("OffsetOf(BlittableStruct)", () => Marshal.OffsetOf(typeof(BlittableStruct), nameof(BlittableStruct.y)));

            Test("OffsetOf<MarshaledStruct>", () => Marshal.OffsetOf<MarshaledStruct>(nameof(MarshaledStruct.y)));
            Test("OffsetOf(MarshaledStruct)", () => Marshal.OffsetOf(typeof(MarshaledStruct), nameof(MarshaledStruct.y)));

            Console.WriteLine("Done.");
            Console.ReadLine();
        }
    }
}
