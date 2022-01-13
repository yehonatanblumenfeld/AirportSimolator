using System;

namespace Airport.Simulator
{
    class Program
    {
        
        static void Main(string[] args)
        {
           
            Logic logic = new Logic();
            while (true)
            {
                logic.WriteMenuCommands();
                var command = Console.ReadLine();
                if(!logic.checkCommand(command)) break;                
            }
            Console.WriteLine("thank you bye !");
        }
    }
}
