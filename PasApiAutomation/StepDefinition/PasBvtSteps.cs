using Newtonsoft.Json.Linq;
using PasApiAutomation.Base;
using RestSharp;
using TechTalk.SpecFlow;
using Xunit;

namespace PasApiAutomation.StepDefinition
{
    [Binding]
    public class PasBvtSteps
    {
        private Settings _settings;


        public PasBvtSteps(Settings settings)
        {
            _settings = settings;
        }

        [Given(@"Initialize Get Request For common microservice with URL ""(.*)""")]
        public void GivenInitializeGetRequestForCommonMicroserviceWithURL(string url)
        {
            _settings.Request = _settings.lib.GetRequest(_settings.config["AppSettings:" + url + ""], Method.GET);
        }

        [When(@"Add Headers for common microservice")]
        public void WhenAddHeadersForCommonMicroservice()
        {
            if (_settings.ParameterList.Count != 0)
            {
                _settings.ParameterList.Clear();
            }
            _settings.ParameterList.Add("Content-Type", "application/json");
            _settings.lib.AddHeadersForGetPost(_settings.Request, _settings.ParameterList);
        }

        [When(@"Execute Get Request for common microservice Get All")]
        public void WhenExecuteGetRequestForCommonMicroserviceGetAll()
        {
            _settings.Response = _settings.RestClient.Execute(_settings.Request);
        }

        [Then(@"Status code should be for common microservice Get All ""(.*)""")]
        public void ThenStatusCodeShouldBeForCommonMicroserviceGetAll(string statusCode)
        {
            _settings.StatusCode = _settings.Response.StatusCode;
            int numericStatusCode = (int)_settings.StatusCode;

            Assert.True(int.Parse(statusCode) == numericStatusCode, "Expected Status Code is:- " + int.Parse(statusCode) + " But Found:- " + numericStatusCode);
        }

        [Then(@"Verify Server for common microservice Get All")]
        public void ThenVerifyServerForCommonMicroserviceGetAll()
        {
            dynamic res = _settings.Response.Content.ToString();
            string serName=_settings.CommonMethod.VerifyServerName(res);
            if (serName==null)
            {
                Assert.True(serName!=null, "No server name being returned in response");
            }
            else
            {
                Assert.True(serName.Contains(_settings.Util.GetTestData("CommonServerName")), "In Response:- "+ res+" ,Server Name does not contains " + _settings.Util.GetTestData("CommonServerName"));
            }
        }

        [Given(@"Initialize Get Request For gateway microservice with URL ""(.*)""")]
        public void GivenInitializeGetRequestForGatewayMicroserviceWithURL(string url)
        {
            _settings.Request = _settings.lib.GetRequest(_settings.config["AppSettings:" + url + ""], Method.GET);
        }

        [When(@"Add Headers for gateway microservice")]
        public void WhenAddHeadersForGatewayMicroservice()
        {
            if (_settings.ParameterList.Count != 0)
            {
                _settings.ParameterList.Clear();
            }
            _settings.ParameterList.Add("Content-Type", "application/json");
            _settings.lib.AddHeadersForGetPost(_settings.Request, _settings.ParameterList);
        }

        [When(@"Execute Get Request for gateway microservice Get All")]
        public void WhenExecuteGetRequestForGatewayMicroserviceGetAll()
        {
            _settings.Response = _settings.RestClient.Execute(_settings.Request);
        }
        [Then(@"Status code should be for gateway microservice Get All ""(.*)""")]
        public void ThenStatusCodeShouldBeForGatewayMicroserviceGetAll(string statusCode)
        {
            _settings.StatusCode = _settings.Response.StatusCode;
            int numericStatusCode = (int)_settings.StatusCode;

            Assert.True(int.Parse(statusCode) == numericStatusCode, "Expected Status Code is:- " + int.Parse(statusCode) + " But Found:- " + numericStatusCode);
        }

        [Then(@"Verify Server for gateway microservice Get All")]
        public void ThenVerifyServerForGatewayMicroserviceGetAll()
        {
            dynamic res = _settings.Response.Content.ToString();
            string serName = _settings.CommonMethod.VerifyServerName(res);
            if (serName==null)
            {
                Assert.True(serName!=null, "No server name being returned in response");
            }
            else
            {
                Assert.True(serName.Contains(_settings.Util.GetTestData("GatewayServerName")), "In Response:- "+ res+" ,Server Name does not contains " + _settings.Util.GetTestData("GatewayServerName"));
            }
        }

        [Given(@"User connects to facility database and runs the ""(.*)"" query to fetch userid from users table")]
        public void GivenUserConnectsToFacilityDatabaseAndRunsTheQueryToFetchUseridFromUsersTable(string query)
        {
            string dbQuery = _settings.Util.GetQueryData(query) +"'"+ _settings.Util.GetTestData("DBUserName") + "'";
            _settings.PasUserName = _settings.DAccess.ExecuteQuerySingleValue(_settings.DbConnection, dbQuery);
        }

        [Given(@"User connects to facility database and runs the ""(.*)"" query to fetch password from aspnet_Users table")]
        public void GivenUserConnectsToFacilityDatabaseAndRunsTheQueryToFetchPasswordFromAspnet_UsersTable(string query)
        {
            string dbQuery = _settings.Util.GetQueryData(query);
            dbQuery = dbQuery.Replace("XXX", _settings.Util.GetTestData("DBUserName"));
            _settings.PasUserPassword = _settings.DAccess.ExecuteQuerySingleValue(_settings.DbConnection, dbQuery);
        }

        [When(@"Initialize Post Request For LOC Token with URL ""(.*)""")]
        public void WhenInitializePostRequestForLOCTokenWithURL(string url)
        {
            _settings.Request = _settings.lib.GetRequest(_settings.config["AppSettings:" + url + ""], Method.POST);
        }

        [When(@"Add Headers for LOC Token")]
        public void WhenAddHeadersForLOCToken()
        {
            _settings.Request.AddHeader("content-type", "application/x-www-form-urlencoded");
        }
        [When(@"Add Body for LOC Token")]
        public void WhenAddBodyForLOCToken()
        {
            _settings.Request.AddParameter("application/x-www-form-urlencoded", $"userId={_settings.PasUserName}&password={_settings.PasUserPassword}&facilitycode={_settings.Util.GetTestData("FacilityCode")}&username={_settings.Util.GetTestData("DBUserName")}", ParameterType.RequestBody);
        }
        [Then(@"Execute Post Request for LOC Token")]
        public void ThenExecutePostRequestForLOCToken()
        {
            _settings.Response = _settings.RestClient.Execute(_settings.Request);
        }
        [Then(@"Verify Status Code should be ""(.*)"" for LOC Token")]
        public void ThenVerifyStatusCodeShouldBeForLOCToken(string statusCode)
        {
            _settings.StatusCode = _settings.Response.StatusCode;
            int numericStatusCode = (int)_settings.StatusCode;

            Assert.True(int.Parse(statusCode) == numericStatusCode, "Expected Status Code is:- " + int.Parse(statusCode) + " But Found:- " + numericStatusCode);
        }
        [Then(@"Save Token")]
        public void ThenSaveToken()
        {
            dynamic res = JObject.Parse(_settings.Response.Content);
            _settings.LocToken = res.access_token;
            Assert.True(!string.IsNullOrEmpty(_settings.LocToken), " In Responce access_token is not displaying");
        }
        [Then(@"Initialize Get Request For LOC microservice with URL ""(.*)""")]
        public void ThenInitializeGetRequestForLOCMicroserviceWithURL(string url)
        {
            _settings.Request = _settings.lib.GetRequest(_settings.config["AppSettings:" + url + ""], Method.GET);
        }

        [Then(@"Add Headers for LOC microservice")]
        public void ThenAddHeadersForLOCMicroservice()
        {
            if (_settings.ParameterList.Count != 0)
            {
                _settings.ParameterList.Clear();
            }
            _settings.ParameterList.Add("Content-Type", "application/json");
            _settings.ParameterList.Add("userid", _settings.PasUserName);
            _settings.ParameterList.Add("Authorization", "bearer "+_settings.LocToken);
            _settings.lib.AddHeadersForGetPost(_settings.Request, _settings.ParameterList);
        }

        [Then(@"Execute Get Request for LOC microservice Get All")]
        public void ThenExecuteGetRequestForLOCMicroserviceGetAll()
        {
            _settings.Response = _settings.RestClient.Execute(_settings.Request);
        }
        [Then(@"Verify Status Code should be ""(.*)"" for LOC Get All")]
        public void ThenVerifyStatusCodeShouldBeForLOCGetAll(string statusCode)
        {
            _settings.StatusCode = _settings.Response.StatusCode;
            int numericStatusCode = (int)_settings.StatusCode;

            Assert.True(int.Parse(statusCode) == numericStatusCode, "Expected Status Code is:- " + int.Parse(statusCode) + " But Found:- " + numericStatusCode);
        }
        [Then(@"Verify Server for LOC microservice Get All")]
        public void ThenVerifyServerForLOCMicroserviceGetAll()
        {
            dynamic res = _settings.Response.Content.ToString();
            string serName = _settings.CommonMethod.VerifyServerName(res);
            if (serName == null)
            {
                Assert.True(serName != null, "No server name being returned in response");
            }
            else
            {
                Assert.True(serName.Contains(_settings.Util.GetTestData("LOCServerName")), "In Response:- " + res + " ,Server Name does not contains " + _settings.Util.GetTestData("LOCServerName"));
            }
        }


    }
}
