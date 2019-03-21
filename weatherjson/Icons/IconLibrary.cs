using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace weatherjson
{
    public class IconLibrary
    {
        Icons.IconList IconLib = new Icons.IconList();

        public IconLibrary()
        {
            string fileContents = "";

            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("weawtherjson.Icons.iconlogic.json"))
            {
                TextReader tr = new StreamReader(stream);
                fileContents = tr.ReadToEnd();
            }

            if (fileContents != "")
            {
                IconLib = JsonConvert.DeserializeObject<Icons.IconList>(fileContents);
            }
            else
            {
                throw new Exception("Contents of the icon library can't be loaded.");
            }

            foreach(var icon in IconLib.Icons)
            {
                
            }
        }

        public string GetIcon(string dayDesc, string forecastText)
        {
            return "";
        }
    }
}
