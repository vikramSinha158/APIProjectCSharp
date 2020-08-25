using Microsoft.Extensions.Configuration;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using PasApiAutomation.CommonMethod;
using R1.Automation.Database.core.Base;
using System.Data;
using R1.Automation.API.Core.Base;
using R1.Automation.API.Core.Utilities;
using R1.Automation.Reporting.Core;

namespace PasApiAutomation.Base
{
    /// <summary>
    /// In this class user can add variables which can be used commonly like BaseUrl and 
    /// User can also create object for the class like CommonMethod. 
    /// </summary>
    public class Settings
    {
        public Uri BaseUrl { get; set; }
        public IRestResponse Response { get; set; }
        public IRestRequest Request { get; set; }
        public RestClient RestClient { get; set; } = new RestClient();
        public Libraries lib { get; set; } = new Libraries();
        public Utility Util { get; set; } = new Utility();
        public CommonMethods CommonMethod { get; set; } = new CommonMethods();
        public ExtentReport Rep { get; set; } = new ExtentReport();
        public DataAccess DAccess { get; set; } = new DataAccess();
        public Dictionary<string, string> ParameterList { get; set; } = new Dictionary<string, string>();
        public IConfigurationRoot config= new ConfigurationBuilder()
                 .AddJsonFile("appsettings.json")
                 .Build();
        public HttpStatusCode StatusCode { get; set; }
        public Dictionary<string, string> ReplaceList { get; set; } = new Dictionary<string, string>();

        public string PasUserName { get; set; }
        public string PasUserPassword { get; set; }
        public string LocToken { get; set; }
        public IDbConnection DbConnection { get; set; }
    }
}
