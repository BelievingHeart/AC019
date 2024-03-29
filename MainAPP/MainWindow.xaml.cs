﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using Lib_CognexPanels;
using Lib_MeasurementUtilities;

namespace MainAPP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly string VppDir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName + "/VPP";
        private readonly List<string> LoggerTitle = new List<string>() {"时间", "W1", "W2", "W3", "L"};
        private readonly string LogBaseDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log");
        private WindowTweak _windowTweak1;
        private WindowTweak _windowTweak2;


        public MainWindow()
        {
            InitializeComponent();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            LeiSai.Disconnet();
            Application.Current.Shutdown();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if(!LeiSai.Init()) MessageBox.Show("通信板卡连接失败");

            _windowTweak1 = new WindowTweak(new LeiSai(3, 3, 4, 100),
                new DataLogger(LoggerTitle,
                    Path.Combine(LogBaseDir, "左")),
                ((DisplayPanel)Host1.Child).Display,
                Path.Combine(VppDir, "BlockLeft.vpp"),
                BtnRun1);

            _windowTweak2 = new WindowTweak(new LeiSai(3, 3, 4, 100),
                new DataLogger(LoggerTitle,
                    Path.Combine(LogBaseDir, "右")),
                ((DisplayPanel)Host2.Child).Display,
                Path.Combine(VppDir, "BlockRight.vpp"),
                BtnRun2);

            
        }


    }
}