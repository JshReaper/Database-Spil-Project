﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Spillet
{
    public partial class Form1 : Form
    {
        private Graphics dc;
        private GameWorld gw;
        public Form1()
        {
            InitializeComponent();
            DataManager.GenerateDataBase();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dc = CreateGraphics();
            gw = new GameWorld(dc,this.DisplayRectangle,this);
        }

        private void GameLoop_Tick(object sender, EventArgs e)
        {
            gw.GameLoop();
        }
    }
}
