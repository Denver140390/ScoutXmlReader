using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;
using ScoutXmlReader.Models;

//TODO Inform user about failed parsings AND add time, when failed, to these reports
namespace ScoutXmlReader.Services
{
    public static class XmlToScoutDataParser
    {
        private static readonly String DATA_DATETIME_FORMAT = "dd.MM.yyyy - HH:mm:ss";
        private static readonly String TERMINAL_DATETIME_FORMAT = "dd.MM.yyyy.HH.mm.ss";

        public static readonly List<String> Reports = new List<String>();

        public static ScoutData Parse(String xmlFilePath)
        {
            if (xmlFilePath == null)
                return null;
            
            XDocument xml;
            Boolean isSuccess = TryLoadXml(xmlFilePath, out xml);
            if (!isSuccess)
            {
                return null;
            }

            var data = new ScoutData();

            data.BeginDate = ParseDateTime(xml.Root.Attribute("begindate")?.Value, DATA_DATETIME_FORMAT);
            data.EndDate = ParseDateTime(xml.Root.Attribute("enddate")?.Value, DATA_DATETIME_FORMAT);

            data.Servers = xml
                .Elements("data").Elements("server")
                .Select(serverElement => new ScoutServer
                {
                    State = ParseBoolean(serverElement.Attribute("state")?.Value),
                    UpTime = ParseTimeSpan(serverElement.Attribute("UpTime")?.Value)
                })
                .ToList();

            data.Terminals = xml
                .Elements("data").Elements("terminals").Elements("terminal")
                .Select(terminalElement => new ScoutTerminal
                {
                    Protocol = terminalElement.Attribute("protocol")?.Value,
                    SerialId = terminalElement.Attribute("serialId")?.Value,
                    SimNumber = terminalElement.Attribute("simNumber")?.Value,
                    ConnectionTime = ParseDateTime(terminalElement.Attribute("connectionTime")?.Value, TERMINAL_DATETIME_FORMAT)
                })
                .ToList();

            Int32 terminalIndex = 0;
            foreach (var terminalElement in xml.Elements("data").Elements("terminals").Elements("terminal"))
            {
                data.Terminals[terminalIndex].Sensors = terminalElement
                    .Elements("sensor")
                    .Select(sensorElement => new ScoutSensor
                    {
                        Type = sensorElement.Attribute("type")?.Value,
                        Value = sensorElement.Attribute("value")?.Value
                    })
                    .ToList();

                terminalIndex++;
            }

            return data;
        }

        private static Boolean TryLoadXml(string xmlFilePath, out XDocument xml)
        {
            try
            {
                xml = XDocument.Load(xmlFilePath);
                return true;
            }
            catch (Exception e)
            {
                xml = null;
                Reports.Add($"Failed to load Xml file. {e.Message}");
                return false;
            }
        }

        private static DateTime? ParseDateTime(String dateTimeString, String dateTimeFormat)
        {
            if (dateTimeString == null || dateTimeFormat == null)
                return null;

            DateTime dateTime;
            Boolean isSuccess = DateTime.TryParseExact(dateTimeString, dateTimeFormat, null, DateTimeStyles.None, out dateTime);

            if (isSuccess)
            {
                return dateTime;
            }
            else
            {
                Reports.Add($"Could not parse date. Value: {dateTimeString}");
                return null;
            }
        }

        private static Boolean? ParseBoolean(String state)
        {
            if (state == null)
                return null;

            if (state.Equals("On"))
                return true;
            if (state.Equals("Off"))
                return false;

            Reports.Add($"Could not parse State. Value: {state}");
            return null;
        }

        private static TimeSpan? ParseTimeSpan(String timeSpanString)
        {
            if (timeSpanString == null)
                return null;

            TimeSpan timeSpan;
            Boolean isSuccess = TimeSpan.TryParse(timeSpanString, out timeSpan);

            if (isSuccess)
            {
                //TryParse() parses successfully, but parses a bit incorrectly. Here is a little fix for that.
                return new TimeSpan(0, timeSpan.Days, timeSpan.Hours, timeSpan.Minutes);
            }
            else
            {
                Reports.Add($"Could not parse UpTime. Value: {timeSpanString}");
                return null;
            }
        }
    }
}
