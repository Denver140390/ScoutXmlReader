using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScoutXmlReader.Models;
using ScoutXmlReader.Services;

namespace ScoutXmlReaderTest
{
    [TestClass]
    public class XmlToScoutDataParserTest
    {
        [TestMethod]
        public void TestParseMethodRunsWithoutExceptions()
        {
            XmlToScoutDataParser.Parse(null);
            XmlToScoutDataParser.Parse("");
            XmlToScoutDataParser.Parse("ScoutXmlReader.exe");
            XmlToScoutDataParser.Parse("SomeXml.xml");
        }

        [TestMethod]
        public void TestParseMethodReturnsCorrectData()
        {
            ScoutData dataSample = new ScoutData
            {
                BeginDate = new DateTime(2011, 12, 12, 18, 0, 23),
                EndDate = new DateTime(2011, 12, 12, 20, 12, 24),
                ServerState = "Off",
                ServerUpTime = new TimeSpan(0, 221, 17, 12),

                Terminals = new List<ScoutTerminal>
                {
                    new ScoutTerminal
                    {
                        Protocol = "MT-600",
                        SerialId = "12345",
                        SimNumber = "2352523523",
                        ConnectionTime = new DateTime(2011, 12, 10, 23, 59, 59),
                        Sensors = new List<ScoutSensor>
                        {
                            new ScoutSensor {Type = "Speed", Value = "23.15"},
                            new ScoutSensor {Type = "Motor", Value = "Off"},
                            new ScoutSensor {Type = "GPS_Sattelites_Count", Value = "10"}
                        }
                    },
                    new ScoutTerminal
                    {
                        Protocol = "MT-530",
                        SerialId = "12345",
                        SimNumber = "2352523523",
                        ConnectionTime = new DateTime(2011, 12, 10, 23, 59, 59),
                        Sensors = new List<ScoutSensor>
                        {
                            new ScoutSensor {Type = "FuelLevel", Value = "127.12"},
                            new ScoutSensor()
                        }
                    },
                    new ScoutTerminal
                    {
                        Protocol = "MT-500",
                        SerialId = "a-12345",
                        Sensors = new List<ScoutSensor>()
                    },
                    new ScoutTerminal
                    {
                        Sensors = new List<ScoutSensor>()
                    }
                }
            };

            ScoutData data = XmlToScoutDataParser.Parse("ScoutData.xml");

            Assert.IsTrue(data.Equals(dataSample));
        }
    }
}
