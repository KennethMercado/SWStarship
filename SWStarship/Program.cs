using StarWarsApiCSharp;
using System;

namespace SWStarship
{
    class Program
    {
        private const string USAGE_TEXT = "Enter a distance in mega lights (MGLT) <e.g. 1000000>: ";
        private const string PROCESS_TEXT = "Please wait...";
        private const string ERROR_MESSAGE = "Please key-in the correct mega lights number e.g. 1000000";
        private const string DISPLAY_FORMAT_NAME = "Name:{0} - Total Amount Of Stops:";
        private const string DISPLAY_FORMAT_TOTAL_AMOUNT_OF_STOPS = "{0}";
        private const int PAGE_NUMBER = 1;
        private const int PAGE_SIZE = 100;
        
        static void Main(string[] args)
        {
            IRepository<Starship> starshipRepo = new Repository<Starship>();
            SWStarshipHelper objSWStarshipHelper = new SWStarshipHelper();

            Console.Clear();
            Console.WriteLine(USAGE_TEXT);
            int.TryParse(Console.ReadLine(), out int inputMegaLightsTotal);

            if (inputMegaLightsTotal != 0)
            {
                Console.WriteLine(Environment.NewLine + PROCESS_TEXT);

                foreach (var starship in starshipRepo.GetEntities(PAGE_NUMBER, PAGE_SIZE))
                {
                    var originalColor = Console.ForegroundColor;

                    Console.Write(Environment.NewLine + string.Format(DISPLAY_FORMAT_NAME, starship.Name));
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write(string.Format(DISPLAY_FORMAT_TOTAL_AMOUNT_OF_STOPS
                        , objSWStarshipHelper.CalculateStops(
                            inputMegaLightsTotal
                            , starship.Consumables
                            , starship.MegaLights).ToString()));
                    Console.ForegroundColor = originalColor;
                }
            }
            else
            {
                Console.WriteLine(ERROR_MESSAGE);
            }

            Console.WriteLine(Environment.NewLine + Environment.NewLine + "Press any key to close this window . . .");
            Console.ReadLine();
        }

    }
}
