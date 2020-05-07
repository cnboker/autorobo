using System;
using System.Collections.Generic;
using System.Text;
using AutoRobo.Core.Models;
using AutoRobo.DataMapping;
using System.Data;

namespace AutoRobo.Core.Actions
{
    public class ActionArrayVariable : ActionVariable
    {
        public override string GetTypeName()
        {
            return "数组";
        }

        private List<string> array { get; set; }
     
        public ActionArrayVariable() {
            array = new List<string>();
            Direction = VariableDirection.Output;
        }

        public override void Reset()
        {
            array.Clear();
        }

        public void Reverse() {
            array.Reverse();
        }

        public int Count {
            get { return array.Count; }
        }

        public void RemoveAt(int index) {
            array.RemoveAt(index);
        }
        /// <summary>
        /// 数据表行转换为IMapAttribute
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="rowIndex"></param>
        /// <returns></returns>
        public IMapAttribute GetMapAttribute(int rowIndex)
        {
            DataMap dataMap = new DataMap();
            dataMap.Fields.Add(new DataMapField() { DisplayName = Name + ".DataItem", Value = array[rowIndex] });
            return dataMap;
        }
        /// <summary>
        /// 增加单项到数组
        /// </summary>
        /// <param name="value"></param>
        internal void AddItemToArray(string value)
        {
            array.Add(value);
            OnDataUpdate(array);
        }

        internal void AddItemsToList(List<string> values)
        {
            array.AddRange(values);
            OnDataUpdate(array);
        }

        public override string GetDescription()
        {
            return "数组变量";
        }

        public override object Data
        {
            get { return array; }
            set { array = (List<string>)value; }
        }
        public override string ToString()
        {
            return string.Join(",",array.ToArray());
        }

        /// <summary>
        /// jint外部调用方法
        /// </summary>
        /// <param name="list"></param>
        public override void add(string[] list)
        {
            array.AddRange(list);
        }

    }
}
