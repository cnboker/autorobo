using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace AutoRobo.DataMapping
{
    public class Option
    {
        public string Value { get; set; }
        public bool BaseLine { get; set; }
       
    }

    public class MultipleDataMapField : DataMapField
    {
       
        public List<Option> Options;

        public MultipleDataMapField()
        {
            Options = new List<Option>();
        }
        
        public override void AddAttribute(string name, string value)
        {
            if (name == "title") {
                DisplayName = value;
            }
            else if (name.IndexOf("values") == 0) {
                var _token = name.Substring(name.IndexOf(".") + 1);
                AddOption(_token, value);
            }

            base.AddAttribute(name, value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key">2.value, 2.baseline格式</param>
        /// <param name="value"></param>
        private void AddOption(string key, string value) {
            var arr = key.Split(".".ToCharArray());
            int index = Convert.ToInt16(arr[0]) - 2;
            Option opt = new Option();
            if (Options.Count > index)
            {
                opt = Options[index];
            }
            else {
                Options.Add(opt);
            }
            if (arr[1] == "value") {
                opt.Value = value;
            }
            else if (arr[1].ToLower() == "baseline") {
                logger.Info("baseline:" + value);
                opt.BaseLine = (value == "checked" ? true : false);
            }
        }
    }
}
