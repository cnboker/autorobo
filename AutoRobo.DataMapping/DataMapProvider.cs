using System;
using System.Collections.Generic;
using System.Text;

namespace AutoRobo.DataMapping
{
    public class DataMapProvider
    {
        private DataMap activeMap = null;

        public DataMapProvider() { }

     

        public DataMap ActiveMap
        {
            get
            {
                return activeMap;
            }    
        }

      
    
        public virtual void Save()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// 获取某一网站定义表单
        /// </summary>
        /// <param name="websiteId"></param>
        /// <returns></returns>
        public virtual DataMap[] SelectByWebsite(string websiteId)
        {
            return null;
        }    

        /// <summary>
        /// 获取所有数据结构列表
        /// </summary>
        /// <returns></returns>
        public virtual DataMap[] SelectAll()
        {
            return null;
            //return new DataMap[] { new StaticRegisterDataMap(), new StaticThreadDataMap(new Post()) };
        }

        public virtual DataMap Select(string Name)
        {
            return null;
            //if (Name == "register") {
            //    return new StaticRegisterDataMap();
            //}
            //else if (Name == "thread")
            //{
            //    return new StaticThreadDataMap(new Post());
            //}
            //else {
            //    return null;
            //}
        }

        public virtual DataMap New()
        {
            throw new System.NotImplementedException();
        }

        //public virtual void Copy(System.Collections.Generic.List<IfacesEnumsStructsClasses.IHTMLElement> elments)
        //{
        //    throw new System.NotImplementedException();
        //}
        
    }
}
