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
        IconList IconLib = new IconList();

        public IconLibrary()
        {
            string fileContents = "";

            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("weatherjson.iconlogic.json"))
            {
                TextReader tr = new StreamReader(stream);
                fileContents = tr.ReadToEnd();
            }

            if (fileContents != "")
            {
                IconLib = JsonConvert.DeserializeObject<IconList>(fileContents);
            }
            else
            {
                throw new Exception("Contents of the icon library can't be loaded.");
            }

        }

        public IconList.Icon GetIcon(string forecastText)
        {

            IconList.Icon icon = new IconList.Icon();

            foreach(var item in IconLib.Icons)
            {
                if (item.Phrases.ToString().ToLower().Contains(forecastText.ToLower()))
                {
                    icon = item;
                    break;
                }

            }

            return icon;
        }
    }
}
