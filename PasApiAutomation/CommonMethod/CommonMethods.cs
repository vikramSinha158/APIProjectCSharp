

namespace PasApiAutomation.CommonMethod
{
    public class CommonMethods
    {

        /// <summary>This method is used to return string according to result</summary>
        /// <param name="result"></param>
        /// <returns>This method is used to return string according to result</returns>
        public string VerifyServerName(string result)
        {
            if (string.IsNullOrEmpty(result))
            {
                return null;
            }
            else
            {
                string[] serverName = result.Split("-");
                return serverName[0];
            }
        }

    }
}
