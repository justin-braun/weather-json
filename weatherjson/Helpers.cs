using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace weatherjson
{
    public static class Helpers
    {
        public static string FormatCountyNames(AlertSourceList.CountyDetails[] CountyList, bool OnlyIncludeMN)
        {
            try
            {
                //List<string> counties = alertCountyString.Split(';').ToList();
                //List<string> geocodes = geoCodes.ToList();

                //var combinedList = counties.Zip(geocodes, (c, s) => new { CountyName = c.Trim(), StateAbbrev = s.Substring(0, 2) }).OrderBy(x => x.StateAbbrev).ToList();
                //var combinedList = counties.Zip(geocodes, (countyName, stateAbbrev) => $"{countyName}{FormatStateCode(stateAbbrev)}").ToList();

                // Linq query to take combined list and group counties by state name
                //var query = from c in combinedList
                //            group c by c.StateAbbrev into countiesByState
                //            let countyNames = from c2 in countiesByState
                //                              select c2.CountyName

                //            select new
                //            {
                //                StateName = countiesByState.Key,
                //                CountyNames = countyNames
                //            };


                // Check for needed filtering
                weatherjson.AlertSourceList.CountyDetails[] filteredCountyList;

                if (OnlyIncludeMN)
                    filteredCountyList = CountyList.Where(c => c.StateAbbrev.ToLower() == "mn").ToArray();
                else
                    filteredCountyList = CountyList.ToArray();

                //Linq query to take combined list and group counties by state name
                var query = from c in filteredCountyList
                            group c by c.StateAbbrev into countiesByState
                            let countyNames = from c2 in countiesByState
                                              select c2.CountyName

                            select new
                            {
                                StateName = countiesByState.Key,
                                CountyNames = countyNames
                            };

                StringBuilder sb = new StringBuilder();

                string lastState = "";
                int index = 0;

                // Start formatting string
                foreach (var state in query.ToList())
                {

                    if (state.CountyNames.Count() == 1)
                    {
                        sb.Append($"{state.CountyNames.ToList()[0]} County in {state.StateName}");
                    }
                    else if (state.CountyNames.Count() == 2)
                    {
                        sb.Append($"{state.CountyNames.ToList()[0]} and {state.CountyNames.ToList()[1]} Counties in {state.StateName}");
                    }
                    else if (state.CountyNames.Count() > 2)
                    {
                        string temp = string.Join(", ", state.CountyNames.ToList());
                        int lastCommaPos = temp.LastIndexOf(", ");
                        sb.Append($"{temp.Remove(lastCommaPos, 1).Insert(lastCommaPos, ", and")} Counties in {state.StateName}");
                    }
                    else
                    {
                        sb.Append($"{state.CountyNames.ToList()[0]} County in {state.StateName}");
                    }

                    if (state.StateName != lastState && index != query.ToList().Count - 1)
                    {
                        sb.Append("; ");
                    }

                    index++;
                    lastState = state.StateName;
                }


                //foreach (var state in query.ToList())
                //{

                //    if (state.CountyNames.Count() == 1)
                //    {
                //        sb.Append($"{state.CountyNames.ToList()[0]} County in {state.StateName}");
                //    }
                //    else if (state.CountyNames.Count() == 2)
                //    {
                //        sb.Append($"{state.CountyNames.ToList()[0]} and {state.CountyNames.ToList()[1]} Counties in {state.StateName}");
                //    }
                //    else if (state.CountyNames.Count() > 2)
                //    {
                //        string temp = string.Join(", ", state.CountyNames.ToList());
                //        int lastCommaPos = temp.LastIndexOf(", ");
                //        sb.Append($"{temp.Remove(lastCommaPos, 1).Insert(lastCommaPos, ", and")} Counties in {state.StateName}");
                //    }
                //    else
                //    {
                //        sb.Append($"{state.CountyNames.ToList()[0]} County in {state.StateName}");
                //    }

                //    if (state.StateName != lastState && index != query.ToList().Count - 1)
                //    {
                //        sb.Append("; ");
                //    }

                //    index++;
                //    lastState = state.StateName;
                //}

                return sb.ToString();
            }
            catch
            {
                return string.Empty;
            }
        }

        public static string FormatCountyNamesOld(string alertCountyString, string[] geoCodes)
        {
            try
            {
                List<string> counties = alertCountyString.Split(';').ToList();
                List<string> geocodes = geoCodes.ToList();

                //var combinedList = counties.Join(geocodes, s => counties.IndexOf(s), i => counties.IndexOf(i), (s, i) => new { sv = s, iv = i }).ToList();
                //var combinedList = counties.Zip(geocodes, (c, s) => new { CountyName = c, StateAbbrev = s.Substring(0, 2) }).ToList();
                var combinedList = counties.Zip(geocodes, (countyName, stateAbbrev) => $"{countyName}{FormatStateCode(stateAbbrev)}").ToList();

                if (combinedList.Count == 1)
                {
                    //return $"{combinedList[0].countyName} ({combinedList[0].StateAbbrev}) County";
                    return $"{combinedList[0]} County";
                }
                else if (combinedList.Count == 2)
                {
                    // return $"{alertCountyString.Replace(';', ',').Replace(",", " and")} Counties";
                    return $"{combinedList[0]} and {combinedList[1]} Counties";
                }
                else if (combinedList.Count > 2)
                {
                    string temp = string.Join(",", combinedList);
                    int lastCommaPos = temp.LastIndexOf(',');

                    return $"{temp.Remove(lastCommaPos, 1).Insert(lastCommaPos, ", and")} Counties";
                }
                else
                {
                    return $"{combinedList[0]} County";
                }
            }
            catch
            {
                return alertCountyString;
            }
        }

        


        private static string FormatStateCode(string stateAbbrev)
        {
            if (stateAbbrev.Substring(0, 2) != "MN")
                return $" ({stateAbbrev.Substring(0, 2)})";
            else
                return string.Empty;
        }
    }
}
