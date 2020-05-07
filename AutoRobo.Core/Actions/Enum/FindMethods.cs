namespace AutoRobo.Core.Actions
{
    public enum FindMethods
    {
        Id,
        Name,
        Class,
        Value,
        Href, 
        Alt,        
        For,          
        Index,          
        Src, 
        //Style, 
        Text, 
        Title, 
        Url,                
        Custom,
        XPath,
        /// <summary>
        /// 通过旁边标签获取控件
        /// </summary>
        ProximityText,
        /// <summary>
        /// 通过jquery selector
        /// </summary>
        CssSelector
    }
}
