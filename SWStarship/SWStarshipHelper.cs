using System.Collections.Generic;

namespace SWStarship
{
    /// <summary>
    /// Star Wars Helper
    /// </summary>
    public class SWStarshipHelper
    {
        const int UNIT_TYPE_NUMBER = 0;
        const int UNIT_TYPE_NAME = 1;

        /// <summary>
        /// This will calculate how many stops to cover a certain distance.
        /// Expected Output
        ///     Y-wing: 74
        ///     Millennium Falcon: 9
        ///     Rebel Transport: 11
        ///     
        ///     1,000,000 / (1 * 7 (in Week) * 24 * 80) = 74.40
        ///     1,000,000 / (2 * 30 (in Month) * 24 * 75) = 9.25
        ///     1,000,000 / (6 * 30 (in Month) * 24 * 20) = 11.57
        /// </summary>
        /// <param name="megaLightsTotal">e.g. 1000000</param>
        /// <param name="consumables">e.g. 1 week</param>
        /// <param name="megaLights">e.g. 80</param>
        /// <returns>Total Amount of Stops e.g. 74</returns>
        public int CalculateStops(int megaLightsTotal, string consumables, string megaLights)
        {
            string[] consumablesSplit = consumables.Split(' ');
            //Try to convert MegaLights to Integer
            int.TryParse(megaLights, out int megaLightsNumber); ;

            //Check if Consumable Format is correct
            if (consumablesSplit.Length == 2 && megaLightsNumber != 0)
            {
                //Try to convert Consumables' Unit Number to Integer
                int.TryParse(consumablesSplit[UNIT_TYPE_NUMBER], out int number);
                int numberOfDays = GetDays(number, consumablesSplit[UNIT_TYPE_NAME]);

                //Prevent Division by Zero
                if (numberOfDays > 0)
                {
                    return megaLightsTotal / (GetDays(number, consumablesSplit[1]) * 24 * megaLightsNumber);
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// This data can be query in the database to make it more flexible.
        /// e.g. Quarterly, Semi-Annual, Century etc...
        /// </summary>
        /// <param name="unitTypeNumber"></param>
        /// <param name="unitTypeName"></param>
        /// <returns></returns>
        int GetDays(int unitTypeNumber, string unitTypeName)
        {
            Dictionary<string, int> numberOfDays = new Dictionary<string, int>();

            //Data will be coming from Database
            numberOfDays.Add("DAYS", 1);
            numberOfDays.Add("DAY", 1);

            numberOfDays.Add("MONTHS", 30);
            numberOfDays.Add("MONTH", 30);

            numberOfDays.Add("YEARS", 365);
            numberOfDays.Add("YEAR", 365);

            numberOfDays.Add("WEEKS", 7);
            numberOfDays.Add("WEEK", 7);


            if (numberOfDays.ContainsKey(unitTypeName.ToUpper()))
            {
                return unitTypeNumber * numberOfDays[unitTypeName.ToUpper()];
            }
            else
            {
                return 0;
            }
        }
    }
}
