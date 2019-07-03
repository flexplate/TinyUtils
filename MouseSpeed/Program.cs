using System;

namespace MouseSpeed
{
    partial class Program
    {
        static unsafe void Main(string[] args)
        {
            int NewSpeed;

            // A single cmd line argument? Set speed and exit if it's an integer.
            if (args.Length == 1 && int.TryParse(args[0], out NewSpeed))
            {
                if (MouseOptions.SetMouseSpeed(NewSpeed))
                {
                    Console.WriteLine("Mouse speed changed to {0}", NewSpeed);
                    Environment.Exit(0);
                }                
            }

            // Otherwise, tell the user the existing speed and prompt for a new one.
            Console.WriteLine("Mouse speed is currently set to {0}.", MouseOptions.GetMouseSpeed());
            Console.WriteLine("Enter a new speed [1..20] to set the speed, or any other value to exit.");
            string Entry = Console.ReadLine();
            if (int.TryParse(Entry, out NewSpeed))
            {
                if (NewSpeed > 0 && NewSpeed < 21)
                {
                    bool Success = MouseOptions.SetMouseSpeed(NewSpeed);
                    if (Success)
                    {
                        Console.WriteLine("Speed changed to {0}", NewSpeed);
                    }
                    else
                    {
                        Console.WriteLine("Unable to set speed.");
                    }
                    Console.WriteLine("Press any key to exit.");
                    Console.ReadKey();
                }
            }
        }
    }
}
