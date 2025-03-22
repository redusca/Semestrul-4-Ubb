using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Interfata.controllers.singinsubcontrollers
{
    public partial class ProbaChoiceController : UserControl
    {
        private ServiceArbitru service;
        private SignInController sign;
        private static readonly ILog log = LogManager.GetLogger("");
        public ProbaChoiceController(ServiceArbitru service, SignInController sign)
        {
            log.Info("ProbaChoiceController created");
            InitializeComponent();
            this.service = service;
            this.sign = sign;
            var probe = service.getAllProbe().Where(p => p.Id_arbitru == -1).ToList();
            foreach (var el in probe)
            {
                comboBox1.Items.Add(el);
            }
        }


        public Proba getProba()
        {

            if(comboBox1.SelectedItem == null)
            {
                log.Error("Proba not selected");
                throw new ArgumentException("Proba not selected");
            }
            return (Proba)comboBox1.SelectedItem;
        }

        private void ProbaChoiceController_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            sign.SwitchController();
        }
    }
}
