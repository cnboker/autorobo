using System;
using System.Collections.Generic;
using System.Text;

namespace AutoRobo.DataMapping
{
    public class SecurityDataMap : DataMap
    {
        private static SecurityDataMap map = null;
        public static SecurityDataMap Instance
        {
            get {
                if (map == null) {
                    map = new SecurityDataMap();
                }
                return map;
            }
        }

   
        public void UpdateValues(string UserName, string Password, string Email,string Tel, string Mobile,
            string CompanyName, string Description)
        {            
            map.Set("用户名", UserName);
            map.Set("密码", Password);
            map.Set("邮箱", Email);           
            map.Set("手机号码", Mobile);
            map.Set("固话", Tel);
            map.Set("公司名称", CompanyName);
            map.Set("公司介绍", Description);
        }

        public SecurityDataMap()
        {
            Name = "账户信息";
            DisplayName = "注册表格";
            Fields = new List<DataMapField>() { 
                new DataMapField(){
                     DisplayName = "用户名"
                },
                new DataMapField(){
                     DisplayName = "密码"
                },  
                new DataMapField(){
                     DisplayName = "邮箱"
                },
                new DataMapField(){
                     DisplayName = "手机号码"
                },
                 new DataMapField(){
                     DisplayName = "固话"
                },
                 new DataMapField(){
                     DisplayName = "公司名称"
                },
                 new DataMapField(){
                     DisplayName = "公司介绍"
                },
            };
        }
    }
}
