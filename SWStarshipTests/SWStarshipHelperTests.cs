using Microsoft.VisualStudio.TestTools.UnitTesting;
using StarWarsApiCSharp;
using System.Linq;

namespace SWStarship.Tests
{
    /// <summary>
    /// Test Class for SWStarshipHelper.
    /// </summary>
    [TestClass()]
    public class SWStarshipHelperTests
    {
        private const int PAGE_NUMBER = 1;
        private const int PAGE_SIZE = 100;

        [DataTestMethod()]
        [DataRow(1000000, "Y-wing", 74)]
        [DataRow(1000000, "Millennium Falcon", 9)]
        [DataRow(1000000, "Rebel Transport", 11)]
        public void CalculateStops_ValidDistance_ReturnsTotalNoOfStops(int inputMegaLightsTotal, string starshipName, int expected)
        {
            //Arrange
            IRepository<Starship> starshipRepo = new Repository<Starship>();
            SWStarshipHelper objSWStarshipHelper = new SWStarshipHelper();
            var starship = starshipRepo.GetEntities(PAGE_NUMBER, PAGE_SIZE)
                .Where(x => x.Name.ToUpper() == starshipName.ToUpper()).FirstOrDefault();

            //Act
            var actual = 0;

            if (starship != null)
            {
                actual = objSWStarshipHelper.CalculateStops(
                    inputMegaLightsTotal
                    , starship.Consumables
                    , starship.MegaLights);
            }

            //Assert
            Assert.AreEqual(expected, actual);
        }
    }
}