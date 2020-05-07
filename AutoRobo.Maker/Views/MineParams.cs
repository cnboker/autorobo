using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Xml.Serialization;

namespace AutoRobo.Makers.Views
{
    public class MineParams
    {
        private ArrayList categoryList;
        [XmlIgnore]
        public string ConfigName = "";
        private ArrayList fieldList;
        private int insertIndex;
        private bool insertMode;
        private ArrayList keywordList;
        private int pageMaxCount = -1;
        private URLDATA startURL;

        public bool AddDataField(DataType type, string fieldName, string xpath, bool isPattern, string heading, string regex = "")
        {
            bool flag = true;
            bool flag2 = false;
            if (this.fieldList == null)
            {
                this.fieldList = new ArrayList();
            }
            if (((((type == DataType.Text) || (type == DataType.Text_Near_Heading)) || ((type == DataType.Image) || (type == DataType.HTML))) || ((type == DataType.File) || (type == DataType.Url))) && (fieldName != null))
            {
                foreach (DATAFIELD datafield in this.fieldList)
                {
                    if (datafield.name == fieldName)
                    {
                        flag = false;
                        break;
                    }
                }
                if (!flag)
                {
                    return flag;
                }
            }
            if (type == DataType.Link_NextPage)
            {
                for (int i = 0; i < this.fieldList.Count; i++)
                {
                    DATAFIELD datafield2 = (DATAFIELD)this.fieldList[i];
                    if (datafield2.type == DataType.Link_NextPage)
                    {
                        datafield2.xpath = xpath;
                        datafield2.name = fieldName;
                        this.fieldList[i] = datafield2;
                        flag2 = true;
                    }
                }
            }
            if (type == DataType.Link_Follow)
            {
                bool flag3 = false;
                for (int j = 0; j < this.fieldList.Count; j++)
                {
                    DATAFIELD datafield3 = (DATAFIELD)this.fieldList[j];
                    if ((datafield3.type == DataType.Link_Follow) && (datafield3.xpath == xpath))
                    {
                        flag3 = true;
                    }
                    if (flag3 && (datafield3.type == DataType.Link_Back))
                    {
                        this.insertMode = true;
                        this.insertIndex = j;
                        flag2 = true;
                        break;
                    }
                }
            }
            if (this.insertMode && (type == DataType.Link_Back))
            {
                this.insertMode = false;
                flag2 = true;
            }
            if (flag && !flag2)
            {
                DATAFIELD datafield4;
                datafield4.type = type;
                datafield4.name = fieldName;
                datafield4.xpath = xpath;
                datafield4.pattern = isPattern;
                datafield4.heading = heading;
                datafield4.regex = regex;
                if (this.insertMode)
                {
                    this.fieldList.Insert(this.insertIndex, datafield4);
                    return flag;
                }
                this.fieldList.Add(datafield4);
            }
            return flag;
        }

        public void DeleteDataField(string fieldName)
        {
            for (int i = 0; i < this.fieldList.Count; i++)
            {
                DATAFIELD datafield = (DATAFIELD)this.fieldList[i];
                if (datafield.name == fieldName)
                {
                    this.fieldList.Remove(datafield);
                    if (this.insertMode && (this.insertIndex >= i))
                    {
                        this.insertIndex--;
                        return;
                    }
                    break;
                }
            }
        }

        private bool IsSinglePage()
        {
            if (this.fieldList != null)
            {
                foreach (DATAFIELD datafield in this.fieldList)
                {
                    if (datafield.type == DataType.Link_NextPage)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public void Optimize()
        {
            int num = 0;
            int num2 = 0;
            if (this.fieldList != null)
            {
                DATAFIELD datafield;
                for (num = 0; num < this.fieldList.Count; num++)
                {
                    datafield = (DATAFIELD)this.fieldList[num];
                    if (datafield.type == DataType.Link_Follow)
                    {
                        num2++;
                    }
                    if (datafield.type == DataType.Link_Back)
                    {
                        num2--;
                    }
                }
                for (num = 0; num < num2; num++)
                {
                    this.AddDataField(DataType.Link_Back, null, null, false, null, "");
                }
                for (num = 0; num < this.fieldList.Count; num++)
                {
                    datafield = (DATAFIELD)this.fieldList[num];
                    if ((datafield.type == DataType.Link_NextPage) && (num != (this.fieldList.Count - 1)))
                    {
                        this.fieldList.Add(datafield);
                        this.fieldList.Remove(datafield);
                        break;
                    }
                }
                num = 0;
                while (num < this.fieldList.Count)
                {
                    datafield = (DATAFIELD)this.fieldList[num];
                    if ((datafield.type == DataType.Link_Follow) && ((num + 1) < this.fieldList.Count))
                    {
                        DATAFIELD datafield2 = (DATAFIELD)this.fieldList[num + 1];
                        if (datafield2.type == DataType.Link_Back)
                        {
                            this.fieldList.Remove(datafield);
                            this.fieldList.Remove(datafield2);
                            continue;
                        }
                    }
                    num++;
                }
            }
        }

        [XmlArrayItem(typeof(URLDATA))]
        public ArrayList CategoryList
        {
            get
            {
                return this.categoryList;
            }
            set
            {
                this.categoryList = value;
            }
        }

        [XmlArrayItem(typeof(DATAFIELD))]
        public ArrayList FieldList
        {
            get
            {
                return this.fieldList;
            }
            set
            {
                this.fieldList = value;
            }
        }

        [XmlArrayItem(typeof(string))]
        public ArrayList KeywordList
        {
            get
            {
                return this.keywordList;
            }
            set
            {
                this.keywordList = value;
            }
        }

        [XmlIgnore]
        public int PageMaxCount
        {
            get
            {
                return this.pageMaxCount;
            }
            set
            {
                this.pageMaxCount = value;
            }
        }

        public bool SinglePage
        {
            get
            {
                return this.IsSinglePage();
            }
        }

        public URLDATA StartURL
        {
            get
            {
                return this.startURL;
            }
            set
            {
                this.startURL = value;
            }
        }
    }
}
