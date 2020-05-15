using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PDAI
{
    class Screen
    {
        private Form _form;

        public bool full_Screen { set { if (value) { _form.WindowState = FormWindowState.Maximized; } else { _form.WindowState = FormWindowState.Normal; } } get { return full_Screen; } }

        public Screen(Form form)
        {
            _form = form;
        }

    }
}
