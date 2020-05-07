using System.Windows.Forms;
using AutoRobo.ClientLib.Ants;
using AutoRobo.Makers.Models.Repositories;
using AutoRobo.Makers.Views;
using System.Drawing;
using AutoRobo.Core.Models;
using System;
using AutoRobo.Core;
using System.IO;

namespace AutoRobo.Makers
{
    public class MakerApp
    {
       
        public static void Start() {            
            FileActionRepository repository = new FileActionRepository();                   
            ////主UI容器
            MakerView root = new MakerView(repository);           
            Application.Run(root);            
        }   
      
    }
}
