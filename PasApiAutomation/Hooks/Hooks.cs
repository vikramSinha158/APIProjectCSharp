using TechTalk.SpecFlow;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using Microsoft.Extensions.Configuration;
using PasApiAutomation.Base;
using R1.Automation.API.Core.Utilities;
using R1.Automation.Reporting.Core;
using System;

namespace PasApiAutomation.Hooks
{




    [Binding]
    class Hooks
    {
        //Global Variable for Extend report

        [ThreadStatic]
        private static ExtentTest featureName;
        [ThreadStatic]
        private static ExtentTest scenario;
        private static AventStack.ExtentReports.ExtentReports extent;
        private readonly ScenarioContext _scenarioContext;

        private Settings _Settings;
        private static string Token;
        private static IConfigurationRoot Config= Utility.ConfigUrl;
        private static string ExtentReportReq;
        private static string ArchiveReport;
        private static string DeleteArchiveReport;
        public Hooks(Settings Settings, ScenarioContext scenarioContext)
        {
            _Settings = Settings;
            _scenarioContext = scenarioContext;
        }

        [BeforeScenario]
        public void TestSetup()
        {

            if (!string.IsNullOrEmpty(ExtentReportReq) && ExtentReportReq.Equals("Yes"))
            {
                scenario = featureName.CreateNode<Scenario>(_scenarioContext.ScenarioInfo.Title);
            }


        }

        [BeforeScenario("LOCMicroservice")]
        public void CreateDBConnection()
        {
            _Settings.DbConnection = _Settings.DAccess.ConnectToDB(_Settings.config["AppSettings:DBConnectionString"]);
        }

        [AfterScenario("LOCMicroservice")]
        public void CloseDBConnection()
        {
            _Settings.DAccess.CloseDBConnection();
        }


        [BeforeTestRun]
        public static void InitializeReport()
        {
            ExtentReportReq = Config["AppSettings:ExtentReport"];
            ArchiveReport = Config["AppSettings:ArchiveExtentReport"];
            DeleteArchiveReport = Config["AppSettings:DeleteArchiveReport"];
            if (!string.IsNullOrEmpty(DeleteArchiveReport) && DeleteArchiveReport.Equals("Yes"))
            {
                ExtentReport.DeleteArchiveFolder(Config["AppSettings:ExtentReportArchiveFolderName"], Config["AppSettings:NumOfDaysToKeepArchiveReport"]);
            }

            if (!string.IsNullOrEmpty(ArchiveReport) && ArchiveReport.Equals("Yes"))
            {
                ExtentReport.ArchiveOldFolders(Config["AppSettings:ExtentReportFolderName"], Config["AppSettings:ExtentReportArchiveFolderName"], Config["AppSettings:NumOfDaysToKeepExtentReport"], Config["AppSettings:DeleteBeforeArchive"], Config["AppSettings:FileSizeToKeepExtentReport"]);
            }

            if (!string.IsNullOrEmpty(ExtentReportReq) && ExtentReportReq.Equals("Yes"))
            {
                extent = ExtentReport.InitReport(Config["AppSettings:ExtentReportFolderName"]);
            }
        }

        [AfterTestRun]
        public static void TearDownReport()
        {
            if (!string.IsNullOrEmpty(ExtentReportReq) && ExtentReportReq.Equals("Yes"))
            {
                //Flush report once test completes
                extent.Flush();
            }

        }

        [BeforeFeature]
        public static void BeforeFeature(FeatureContext featureContext)
        {
            if (!string.IsNullOrEmpty(ExtentReportReq) && ExtentReportReq.Equals("Yes"))
            {
                //Create dynamic feature name
                featureName = extent.CreateTest<Feature>(featureContext.FeatureInfo.Title);
            }

        }

        [AfterStep]
        public void InsertReportingSteps()
        {
            if (!string.IsNullOrEmpty(ExtentReportReq) && ExtentReportReq.Equals("Yes"))
            {
                object TestResult = _Settings.Rep.ConfigSteps(_scenarioContext);
                _Settings.Rep.InsertStepsInReport(_scenarioContext, TestResult, scenario);
            }
        }


    }
}
