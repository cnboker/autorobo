using System.Collections.Generic;

namespace AutoRobo.Makers.AutoUpdate.Framework
{
    public interface IUpdateFeedSource
    {
        List<Update> Read(string url);
    }
}