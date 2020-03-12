using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Generador_de_automatas_ER
{
    
    public class MyTabForm:TabPage
    {
        private Form frm;
        
        public MyTabForm(MyFormPage frn_contenido) {
            this.frm = frn_contenido;
            this.Controls.Add(frn_contenido.pnl);
            this.Text = frn_contenido.Text;
        }
        protected override void Dispose(bool disposing)
        {

            if (disposing)
            {
                frm.Dispose();
            }
            base.Dispose(disposing);
        }
    }
    public class MyFormPage : Form {
        public Panel pnl;
    }
}
