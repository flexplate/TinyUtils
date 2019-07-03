using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Bitness
{
    class Program
    {

        /// <summary>
        /// Source: https://docs.microsoft.com/en-gb/windows/desktop/debug/pe-format
        /// </summary>
        private static Dictionary<Int32, string> MachineType = new Dictionary<Int32, string>()
        {
            {0x0000, "The contents of this field are assumed to be applicable to any machine type"},
            {0x01d3, "Matsushita AM33"},
            {0x8664, "x64"},
            {0x01c0, "ARM little endian"},
            {0xaa64, "ARM64 little endian"},
            {0x01c4, "ARM Thumb-2 little endian"},
            {0x0ebc, "EFI byte code"},
            {0x014c, "Intel 386 or later processors and compatible processors"},
            {0x0200, "Intel Itanium processor family"},
            {0x9041, "Mitsubishi M32R little endian"},
            {0x0266, "MIPS16"},
            {0x0366, "MIPS with FPU"},
            {0x0466, "MIPS16 with FPU"},
            {0x01f0, "Power PC little endian"},
            {0x01f1, "Power PC with floating point support"},
            {0x0166, "MIPS little endian"},
            {0x5032, "RISC-V 32-bit address space"},
            {0x5064, "RISC-V 64-bit address space"},
            {0x5128, "RISC-V 128-bit address space"},
            {0x01a2, "Hitachi SH3"},
            {0x01a3, "Hitachi SH3 DSP"},
            {0x01a6, "Hitachi SH4"},
            {0x01a8, "Hitachi SH5"},
            {0x01c2, "Thumb"},
            {0x0169, "MIPS little-endian WCE v2"}
        };

        static void Main(string[] args)
        {
            // Initial checks.
            if (args.Length != 1) { Environment.Exit(0); }      // Wrong args (probably none). Just get out of here.
            if (!File.Exists(args[0])) { Environment.Exit(2); } // File not found

            // File exists. Get PE offset from position 0x3c. FileShare.ReadWrite means we can read the file even if it's in use.
            var Stream = new FileStream(args[0], FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            byte[] PeOffset = new byte[2];
            Stream.Seek(0x3c, SeekOrigin.Begin);
            Stream.Read(PeOffset, 0, 2);

            // Jump to offset specified in MS-DOS stub and read the PE signature.
            Stream.Seek(PeOffset[0] | PeOffset[1] << 8, SeekOrigin.Begin);
            byte[] Values = new byte[4];
            Stream.Read(Values, 0, 4);

            // For sanity's sake, make sure we've got an actual PE signature - "P E  NULL NULL"?
            if (Values.SequenceEqual(new byte[] { 0x50, 0x45, 0x00, 0x00 }))
            {
                // Machine type is the next 2 bytes after the signature.
                Stream.Read(Values, 0, 2);
                Int32 TypeId = Values[0] | Values[1] << 8;
                string TypeName;
                if (MachineType.TryGetValue(TypeId, out TypeName))
                {
                    Console.WriteLine("This assembly is compiled for {0}.", TypeName);
                }
                else
                {
                    Console.WriteLine("Machine type could not be determined for this assembly.");
                }
            }
            else
            {
                Console.WriteLine("Machine type could not be determined for this assembly.");
            }
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
