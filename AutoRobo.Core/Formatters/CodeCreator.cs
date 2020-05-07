using AutoRobo.Core.Models;
using Jsbeautifier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoRobo.Core.Formatters
{
    public class CodeCreator
    {
        public ModelSet Model { get; set; }

        public CodeCreator(ModelSet model) {
            this.Model = model;
        }

        public string CodeOutput { get; set; }
       
        public void Accept(ICodeFormatterVisitor visitor) {
            foreach (ICodeFormatterAcceptor acceptor in Model.VariableActionModel) {
                acceptor.Accept(visitor);
            }
            foreach(ICodeFormatterAcceptor acceptor in Model.MainActionModel){
                acceptor.Accept(visitor);
            }
            Beautifier beautifier = new Beautifier();
            beautifier.Opts.IndentSize = 4;
            beautifier.Opts.IndentChar = ' ';
            beautifier.Opts.PreserveNewlines = true;
            beautifier.Opts.JslintHappy = false;
            beautifier.Opts.KeepArrayIndentation = false;
            beautifier.Opts.BraceStyle = BraceStyle.Collapse;
            beautifier.Flags.IndentationLevel = 0;
            beautifier.Opts.BreakChainedMethods = false;
            CodeOutput = beautifier.Beautify(visitor.CodeOutput);
        }
    }
}
