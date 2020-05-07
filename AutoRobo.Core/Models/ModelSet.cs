using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using AutoRobo.Core.ActionBuilder;
using AutoRobo.Core.Actions;
using AutoRobo.Core.IO;
using AutoRobo.DataMapping;
using AutoRobo.WebHelper;
using Util;

namespace AutoRobo.Core.Models
{
    public class ModelSet
    {
        private log4net.ILog logger = log4net.LogManager.GetLogger("ModelSet");
        private ActionList activeActionList = null;
        private string startUrl;

        //public bool ShouldInsertActions { get; set; }
        /// <summary>
        /// 主活动数据
        /// </summary>
        public MainActionModel MainActionModel { get; set; }
        //.bit文件名称
        public string ProjectName { get; set; }
        //public event EventHandler DataLoaded;
        /// <summary>
        /// 脚本起始页面
        /// </summary>
        public string StartUrl {
            get {
                return startUrl;
            }
            set {
                if (value != startUrl) {
                    startUrl = value;
                    if (AppContext.Current.Browser != null)
                    {
                        AppContext.Current.Browser.Navigate(value);
                    }
                }
            }
        }
        /// <summary>
        /// 脚本来源， Custom(用户自定义), System(系统内置脚本)
        /// </summary>
        public string Target
        {
            get;
            set;
        }
        //是收集数据还是发布数据
        public DataMethod DataMethod { get; set; }
        /// <summary>
        /// 变量活动数据
        /// </summary>
        public VariableActionModel<ActionVariable> VariableActionModel { get; set; }
        /// <summary>
        /// 过程活动数据
        /// </summary>
        public SubActionModel SubActionModel { get; set; }

        public int InsertPosition { get; set; }

        static public event Action<ActionList> DataSourceChanged;
   
        public IActionNameRepository Repository
        {
            get;
            set;
        }
      
        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="context"></param>
        /// <param name="writer"></param>
        public ModelSet(IActionNameRepository repository)
        {                    
            this.Repository = repository;
            this.MainActionModel = new MainActionModel();
            this.VariableActionModel = new VariableActionModel<ActionVariable>();
            this.SubActionModel = new SubActionModel();
            AppContext.Current.ActionModel = this;
            Load();
        }


        public void RemoveActiveAction(int index)
        {
            activeActionList.RemoveAt(index);
        }
    
        /// <summary>
        /// 激活活动项
        /// </summary>
        public ActionList ActiveActionModel
        {
            get
            {
                if (activeActionList == null)
                {
                    activeActionList = MainActionModel;
                }
                return activeActionList;
            }
            set
            {
                if (value != activeActionList)
                {
                    activeActionList = value;
                    if (DataSourceChanged != null) {
                        DataSourceChanged(value);
                    }
                }
            }
        }

        /// <summary>
        /// 切换活动视图
        /// </summary>
        /// <param name="selectedIndex"></param>
        public void Switch(StatementType statementType)
        {
            if (statementType == StatementType.Main)
            {
                activeActionList = MainActionModel;
            }
            else if (statementType == StatementType.Variable)
            {
                activeActionList = VariableActionModel;
            }
            else if (statementType == StatementType.Sub)
            {
                activeActionList = SubActionModel;
            }
            if (DataSourceChanged != null) {
                DataSourceChanged(activeActionList);
            }
        }

        public ActionBase CreateAction(IAppContext context, string actionEnum, ActionParameter parameter)
        {
            ActionBase actionBase = ActionFactory.Create(actionEnum.ToString());
            actionBase.AppContext = context;
            bool result = false;
            try
            {
                result = actionBase.Parse(parameter);
            }
            catch (NotPairElementException ex)
            {
                MessageBox.Show(ex.Message);
            }
            if (result)
            {
                return actionBase;
            }
            return null;
        }

        public void AddAction(IAppContext context, string actionEnum, ActionParameter parameter)
        {
            ActionBase actionBase = CreateAction(context, actionEnum, parameter);
            if (actionBase != null)
            {
                AddAction(actionBase);
            }
        }

        /// <summary>
        /// 增加活动
        /// </summary>
        /// <param name="action"></param>
        public void AddAction(ActionBase action)
        {
            if (action == null) return;
            ActionList source = ActiveActionModel;
            ////采集数据不检查活动重复
            //if (DataMethod == Models.DataMethod.Collect) {
            //    action.CheckDuplication = false;
            //}
            //input control actionclick return           
            //if (ActiveActionModel.Contain(action) && action.CheckDuplication)
            //{
            //    int index = ActiveActionModel.GetActiveIndex(action);
            //    if (index != -1)
            //    {
            //        ActiveActionModel.RemoveAt(index);
            //        ActiveActionModel.Insert(index, action);
            //    }
            //    if (DataSourceUpdate != null)
            //    {
            //        DataSourceUpdate(source);
            //    }
            //    return;
            //}
            //如果新增活动是变量类型则增加活动到变量列表
            if (action is ActionVariable)
            {
                source = VariableActionModel;
            }
            else if (action is ActionScriptPart) {
                source = SubActionModel;
            }
            else
            {
                //非变量活动和脚本活动，数据源定向到主数据源
                if (source == VariableActionModel)
                {
                    source = MainActionModel;
                }
            }
            //if (ShouldInsertActions)
            //{
            //    if (InsertPosition < 0) InsertPosition = 0;
            //    if (InsertPosition > source.Count)
            //    {
            //        InsertPosition = 0;
            //    }
            //    source.Insert(InsertPosition, action);
            //    InsertPosition++;
            //}
            //else
            //{
                source.Add(action);
            //}
            if (DataSourceChanged != null)
            {
                DataSourceChanged(source);
            }
            action.ParentTest = source;
        }


        /// <summary>
        /// 清空数据
        /// </summary>
        public void Empty()
        {
            MainActionModel.Clear();
            VariableActionModel.Clear();
            SubActionModel.Clear();
            InsertPosition = -1;
            //ShouldInsertActions = true;
            if (DataSourceChanged != null)
            {
                DataSourceChanged(MainActionModel);
            }
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        public void Save()
        {
            IActionWriteRepository rep = Repository as IActionWriteRepository;
            if (rep == null) {
                throw new ApplicationException("Repository必须实现IActionWriteRepository接口");
            }
            rep.Write(Export());
        }

        public string Export() {
            ModelSerilizer serializer = new ModelSerilizer();
            return serializer.Serilize(this);
        }

        /// <summary>
        /// 创建新活动
        /// </summary>
        /// <param name="startUrl"></param>
        /// <param name="method"></param>
        public void New(string projectName, string startUrl, DataMethod method) {
            this.ProjectName = projectName;
            //设置项目路径
            AppSettings.Instance.CurrentExecutePath = Path.Combine(AppSettings.Instance.LibraryPath, projectName);
            this.StartUrl = startUrl;
            this.DataMethod = method;
            IActionRepository rep = Repository as IActionRepository;
            rep.New(projectName);
            if (!string.IsNullOrEmpty(StartUrl))
            {
                App.Invoke(() =>
                {
                    AppContext.Current.Browser.Navigate(StartUrl);
                }, true);
            }
            AddAction(new ActionNavigate() { URL = startUrl, AppContext = AppContext.Current });            
            if (DataSourceChanged != null)
            {
                DataSourceChanged(MainActionModel);
            }
        }
     
        /// <summary>
        /// 注入账户信息变量
        /// </summary>
        private void InjectIdentity() {
            string identityName = "账户信息";
            VariableActionModel.Remove(identityName);
            VariableActionModel.Add(new ActionAttributeObject(SecurityDataMap.Instance, VariableDirection.Input));           
        }
        /// <summary>
        /// 读取数据
        /// </summary>
        private void Load()
        {            
            ActionVariable var = null;
            IActionReadRepository rep = Repository as IActionReadRepository;
            if (rep == null)
            {
                throw new ApplicationException("Repository必须实现IActionReadRepository接口");
            }
            ActionXmlSet xmlset = rep.Read();
            if (xmlset == null) return;
            if (!string.IsNullOrEmpty(xmlset.XmlAction)) {
                ModelSerilizer serializer = new ModelSerilizer();
                serializer.Deserialize(xmlset.XmlAction);
            }
            if (xmlset.IsLoadIdentiy) {
                InjectIdentity();
            }
          
            AppContext.Current.ActionModel.ProjectName = xmlset.Name;
            //构造数据模型            
            if (!string.IsNullOrEmpty(xmlset.SchemaSet.Schema))
            {
                VariableActionModel.Remove(xmlset.SchemaSet.Name);
                var map = new DataMap(xmlset.SchemaSet.Name);
                map.Init(xmlset.SchemaSet.Schema);
                map.UpdateValues(xmlset.SchemaSet.SchemaValue);
                //将数据模型转换为输入表变量增加到ModelSet
                var = new ActionAttributeObject(map, VariableDirection.Input);
                VariableActionModel.Add(var);
            }
            Switch(StatementType.Main);
            
        }

        public ActionBase FindAction(StatementType statementType, string Name)
        {
            if (statementType == StatementType.Main)
            {
                return MainActionModel.FirstOrDefault(c => c.ElementName == Name);
            }
            else if (statementType == StatementType.Sub)
            {
                return SubActionModel.FirstOrDefault(c => c.ElementName == Name);
            }
            else if (statementType == StatementType.Variable)
            {
                return VariableActionModel.FirstOrDefault(c => c.ElementName == Name);
            }
            return null;
        }

      
       
    }
}
