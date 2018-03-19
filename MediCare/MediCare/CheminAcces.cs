using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MediCare
{
    public partial class CheminAcces : Form
    {
        public CheminAcces()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if ( ofd.ShowDialog() == DialogResult.OK )
            {
                MessageBox.Show(ofd.FileName);

            }
        }
    }
}
