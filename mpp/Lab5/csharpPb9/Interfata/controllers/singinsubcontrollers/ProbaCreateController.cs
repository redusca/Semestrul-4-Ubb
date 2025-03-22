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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Interfata.controllers.singinsubcontrollers
{
    public partial class ProbaCreateController : UserControl
    {
        private ServiceArbitru service;
        private SignInController sign;
        private static readonly ILog log = LogManager.GetLogger("");
        public ProbaCreateController(ServiceArbitru service, SignInController sign)
        {
            log.Info("ProbaCreateController created");
            InitializeComponent();
            this.service = service;
            this.sign = sign;
        }

        private void ProbaCreateController_Load(object sender, EventArgs e)
        {

        }

        public DataSetProba getProbaSet()
        {
            DataSetProba proba = new DataSetProba();
            proba.Nume = numeText.Text;
            string id;
            if(alergatRadio.Checked)
            {
                proba.Categorie = Categorie.alergat;
                id = "s";
            }
            else if (ciclismRadio.Checked)
            {
                proba.Categorie = Categorie.ciclism;
                id = "c";
            }
            else
            {
                proba.Categorie = Categorie.inot;
                id = "i";
            }

            id += service.getAllProbe().Where(p => p.Id.Contains(id)).Count().ToString();
            proba.id = id;
            return proba;
        }

        private void SelectButton_Click(object sender, EventArgs e)
        {
            sign.SwitchController();
        }
    }
}
