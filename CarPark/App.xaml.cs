using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace CarPark
{
 
    public partial class App : Application
    {
        public App()
        {
            Batteries_V2.Init();
        }
    }
}
