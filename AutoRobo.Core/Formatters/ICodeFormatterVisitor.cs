using AutoRobo.Core.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoRobo.Core.Formatters
{
    public interface ICodeFormatterVisitor
    {
        string CodeOutput { get; }
        void Visit(ActionVariable var);
        void Visit(ActionTypeText action);
        void Visit(ActionWait action);
        void Visit(ModuleCall action);
        void Visit(ActionSleep action);
        void Visit(ActionSelectList action);
        void Visit(ActionRefresh action);
        void Visit(ActionRadio action);
        void Visit(ActionBack action);
        void Visit(ActionBrowser action);
        void Visit(ActionCall action);
        void Visit(ActionCheckbox action);
        void Visit(ActionClick action);
        void Visit(ActionCloseWindow action);
        void Visit(ActionDirectionKey action);
        void Visit(ActionDoubleClick action);
        void Visit(ActionFileDialog action);
        void Visit(ActionFireEvent action);
        void Visit(ActionForward action);
        void Visit(ActionKey action);
        void Visit(ActionMouse action);
        void Visit(ActionNavigate action);
        void Visit(ActionOpenWindow action);


        void Visit(ActionAddListToTable action);

        void Visit(ActionDatabaseWriter action);

        void Visit(ActionFileWriter actionFileWriter);

        void Visit(ActionFileReader actionFileReader);

        void Visit(ActionFetchText actionAddDataToList);
        void Assert(string expect, string variableName);
    }
}
