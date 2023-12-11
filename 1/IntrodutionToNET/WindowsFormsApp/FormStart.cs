using System;
using System.Windows.Forms;
using Multitargeting;

namespace WindowsFormsApp
{
    public partial class FormStart : Form
    {
        public FormStart()
        {
            InitializeComponent();
        }

        private void startOk_Click(object sender, EventArgs e)
        {
            if (name.Text == "") return;
            MessageBox.Show(SaySmth.SayTimeAndHello(name.Text));
        }
        
    }
}