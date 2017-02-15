using HomeCollector.Factories;
using HomeCollector.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HomeCollector
{
    public partial class MainForm : Form
    {
        private ICollectionBase _currentCollection;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            InitializeControls();
        }

        private void InitializeControls()
        {
            LoadCollectionTypes(cboCollectionType);
        }

        internal void LoadCollectionTypes(ComboBox cboCollectionType)
        {
            //cboCollectionType.DataSource = CollectableBaseFactory.CollectableTypes;
            //cboCollectionType.DisplayMember = "Name";
            cboCollectionType.Items.Clear();
            foreach (Type collectionType in CollectableBaseFactory.CollectableTypes)
            {
                string name = CollectableBaseFactory.GetFriendlyNameFromType(collectionType);
                cboCollectionType.Items.Add(name);
            }
        }

        private void btnLoadCollection_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();

        }
    }
}
