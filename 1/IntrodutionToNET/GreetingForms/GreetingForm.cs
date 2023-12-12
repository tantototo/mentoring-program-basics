using System;
using System.Windows.Forms;
using Multitargeting;

namespace GreetingForms
{
    public partial class GreetingForm : Form
    {
        public GreetingForm()
        {
            InitializeComponent();
        }

        private void StartOk_Click(object sender, EventArgs e)
        {
            MessageBox.Show(String.IsNullOrEmpty(name.Text)
                ? "You didn't input a name!"
                : GreetingConstructor.SayTimeAndHello(name.Text));
        }
    }
}