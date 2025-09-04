using PetHealthcare.API.Abstractions;

namespace PetHealthcare.API.Helpers
{
    public class CustomerNumberGenerator : ICustomerNumberGenerator
    {
        public int GenerateCustomerNumber()
        {
            int counter = 9;
            string stringResult = string.Empty;

            while (counter > 0)
            {
                stringResult += new Random().Next(0, 9).ToString();
                counter--;
            }
            if (Int32.TryParse(stringResult, out int number)) return number;
            else return 999999999;
        }
    }
}
