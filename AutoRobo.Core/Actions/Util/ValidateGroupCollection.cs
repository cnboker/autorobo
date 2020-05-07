using System;
using System.Collections.Generic;
using System.Text;

namespace AutoRobo.Core.Actions
{
    public class ValidateGroup
    {
        public ActionValidateImage ValidateImage { get; set; }
        public ActionValidateCode ValidateCode { get; set; }
        public int ImageIndex { get; set; }
        public int CodeIndex { get; set; }
        public string GroupName { get; set; }
    }

    /// <summary>
    /// 处理验证码图片和验证码规则
    /// </summary>
    public class ValidateGroupCollection : List<ValidateGroup>
    {
        private ActionList list;

        public ValidateGroupCollection(ActionList list) {
            this.list = list;
        }

        private ValidateGroup GetGroup(string groupName)
        {
            foreach (var o in this)
            {
                if (o.GroupName == groupName)
                {
                    return o;
                }
            }
            return null;
        }

        private void AddToValidateGroup(IValidateGroup v, int index)
        {
            if (string.IsNullOrEmpty(v.GroupName)) {
                v.GroupName = "group1";
            }
            ValidateGroup g = GetGroup(v.GroupName);
            if (g == null) {
                g = new ValidateGroup();
                g.GroupName = v.GroupName;
                this.Add(g);
            }
            if (v is ActionValidateCode) {
                g.ValidateCode = v as ActionValidateCode;
                g.CodeIndex = index;
            }
            else if (v is ActionValidateImage) {
                g.ValidateImage = v as ActionValidateImage;
                g.ImageIndex = index;
            }
            
        }


        public void PerpareRunTest(bool isPartScript) {
            int index = 0;
            foreach (ActionBase action in list)
            {
                if (action is ActionValidateImage || action is ActionValidateCode)
                {
                    var a = action as IValidateGroup;
                    AddToValidateGroup((IValidateGroup)action, index);
                }            
                index++;
            }

            foreach (var o in this) {
                if (o.ImageIndex != -1 && o.CodeIndex != -1)
                {
                    if (o.ValidateCode != null)
                    {
                        o.ValidateCode.ValidateImage = o.ValidateImage;
                    }
                    else {
                        continue;
                    }
                    if (o.ValidateImage != null)
                    {
                        o.ValidateImage.ActionValidateCode = o.ValidateCode;
                    }
                    else {
                        continue;
                    }
                    if (o.CodeIndex < o.ImageIndex)
                    {
                        list.MoveTo(o.ImageIndex, o.CodeIndex);
                        if (!isPartScript)
                        {
                            //scott
                            //list.RefreshGrid();
                        }
                    }
                }
            }
        }
    }
}
