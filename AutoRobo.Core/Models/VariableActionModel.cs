using System;
using System.Collections.Generic;
using System.Text;
using AutoRobo.Core.Actions;
using System.Linq;
using System.Data;
using AutoRobo.DataMapping;
using AutoRobo.Core.IO;
using Util;
using System.IO;
using AutoRobo.DB;

namespace AutoRobo.Core.Models
{

    /// <summary>
    /// 变量活动列表
    /// </summary>
    public class VariableActionModel<T> : ActionList where T : ActionVariable
    {
        public VariableActionModel()
        {
          
        }
        /// <summary>
        /// 通过名称获取变量
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public ActionVariable[] GetVariableObjectsByName(string[] names)
        {
            return this.Select(t => (ActionVariable)t).Where(t => names.Contains(t.Name)).ToArray();
        }

        /// <summary>
        /// 将字符串中包含的变量用变量值替换, 输入字符串只能是包含整数和字符串变量，其他变量报告异常       
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string Parse(string input)
        {
            var variables = StringHelper.GetVariable(input);
            if (variables.Count == 0) return input;
            Dictionary<string, string> dict = new Dictionary<string, string>();
            foreach (var varName in variables)
            {
                string variable = varName.Substring(1);
                ActionVariable actionVar = Find(variable);
                if (actionVar == null) {
                    throw new ApplicationException(string.Format("变量{0}不存在异常", variable));
                }
                if (actionVar is ActionIntegerVariable || actionVar is ActionStringVariable)
                {
                    dict.Add(varName, actionVar.Data.ToString());
                }
            }
            StringBuilder sb = new StringBuilder(input);
            foreach (string key in dict.Keys)
            {
                sb.Replace(key, dict[key]);
            }
            return sb.ToString();
        }

        /// <summary>
        /// 移除变量
        /// </summary>
        /// <param name="name">变量名称</param>
        public void Remove(string name) {
            var action = this.FirstOrDefault(c => c.ElementName == name);
            if (action != null) {
                this.Remove(action);
            }
        }      
        /// <summary>
        /// 将数组数据作为列添加到表格（垂直添加数据)
        /// </summary>
        /// <param name="arrayName"></param>
        /// <param name="tableName"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="startColumnIndex"></param>
        public void AddListToTableColumn(string arrayName, string tableName) {
            List<string> arr = Find(arrayName).Data as List<string>;
            if (arr == null) throw new ApplicationException(string.Format("数组{0}不存在", arrayName));
            if (arr.Count == 0) return;
            Find<ActionTableVariable>(tableName).AddListToTableColumn(arr);  
        }

        /// <summary>
        /// 将数组数据作为行添加到表格（水平添加数据)
        /// </summary>
        /// <param name="arrayName"></param>
        /// <param name="tableName"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="startColumnIndex"></param>
        public void AddListToTableRow(string arrayName, string tableName)
        {
            List<string> arr = Find(arrayName).Data as List<string>;
            if (arr.Count == 0) return;
            if (arr == null) throw new ApplicationException(string.Format("数组{0}不存在", arrayName));
            Find<ActionTableVariable>(tableName).AddListToTableRow(arr); 
        }


        /// <summary>
        /// 初始化变量，变量不存在则创建，如果以前有创建则清空数据
        /// </summary>
        public void Initialize() {
            foreach (ActionVariable var in this) {
                if (var.Direction == VariableDirection.Output)
                {
                    var.Reset();
                }
            }
            //每次运行的时候重新创建数据库
           // SqlceDataBase db = new SqlceDataBase(AppSettings.Instance.TempDbFile);
           // db.ClearDB();            
        }


        public T2 Find<T2>(string variableName) where T2:ActionVariable
        {
            foreach (var action in this) {
                ActionVariable var = action as ActionVariable;
                if (var.Name == variableName) {
                    return (T2)var;
                }
            }
            return null;
        }

        public ActionVariable Find(string variableName) {
            foreach (var action in this)
            {
                ActionVariable var = action as ActionVariable;
                if (var.Name == variableName)
                {
                    return var;
                }
            }
            return null;
        }

     
        /// <summary>
        /// 转换自定义对象到键值对象
        /// </summary>
        /// <returns></returns>
        public IMapAttribute GetInputVariableAttribute()
        {
            List<ActionAttributeObject> list = new List<ActionAttributeObject>();
            foreach (ActionVariable var in this)
            {
                if (var.Direction == VariableDirection.InOut || var.Direction == VariableDirection.Input)
                {
                    if (var is ActionAttributeObject)
                    {
                        list.Add((ActionAttributeObject)var);
                    }
                }
            }            
            DataMap map = new DataMap();
            foreach (ActionAttributeObject o in list)
            {
                foreach (var x in ((IMapAttribute)o.Data).Fields)
                {
                    string name = o.Name + "." + x.DisplayName;
                    string value = x.Value;
                    map.Fields.Add(new DataMapField() { DisplayName = name, Value = value });
                }
            }
            return map;
        }

   
        /// <summary>
        /// 清除输出表格数据
        /// </summary>
        public void ClearOutputDataTableData() {
            var tableVariables = this.OfType<ActionTableVariable>();
            foreach (var v in tableVariables)
            {
                if (v.Direction == Models.VariableDirection.Output)
                {
                    ActionTableVariable t = v as ActionTableVariable;
                    DataTable dt = t.Data as DataTable;
                    if (dt != null)
                    {
                        dt.Clear();
                    }
                }
            }
        }
    }
}
