using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PDAI
{
    class Font_Class
    {


        public void Size(object obj)
        {
            string font = "Arial";
            int fontSize = 10;

            if (obj.GetType() == typeof(Button)) { (obj as Button).Font = new System.Drawing.Font(font, fontSize, System.Drawing.FontStyle.Regular); }
            if (obj.GetType() == typeof(Label)) { (obj as Label).Font = new System.Drawing.Font(font, fontSize, System.Drawing.FontStyle.Regular); }
            if (obj.GetType() == typeof(TextBox)) { (obj as TextBox).Font = new System.Drawing.Font(font, fontSize, System.Drawing.FontStyle.Regular); }
            if (obj.GetType() == typeof(ComboBox)) { (obj as ComboBox).Font = new System.Drawing.Font(font, fontSize, System.Drawing.FontStyle.Regular); }
            if (obj.GetType() == typeof(DateTimePicker)) { (obj as DateTimePicker).Font = new System.Drawing.Font(font, fontSize, System.Drawing.FontStyle.Regular); }
        }


        public void Size(object obj, int size)
        {
            string font = "Arial";
            int fontSize = size;

            if (obj.GetType() == typeof(Button)) { (obj as Button).Font = new System.Drawing.Font(font, fontSize, System.Drawing.FontStyle.Regular); }
            if (obj.GetType() == typeof(Label)) { (obj as Label).Font = new System.Drawing.Font(font, fontSize, System.Drawing.FontStyle.Regular); }
            if (obj.GetType() == typeof(TextBox)) { (obj as TextBox).Font = new System.Drawing.Font(font, fontSize, System.Drawing.FontStyle.Regular); }
            if (obj.GetType() == typeof(ComboBox)) { (obj as ComboBox).Font = new System.Drawing.Font(font, fontSize, System.Drawing.FontStyle.Regular); }
            if (obj.GetType() == typeof(RichTextBox)) { (obj as RichTextBox).Font = new System.Drawing.Font(font, fontSize, System.Drawing.FontStyle.Regular); }
            if (obj.GetType() == typeof(DateTimePicker)) { (obj as DateTimePicker).Font = new System.Drawing.Font(font, fontSize, System.Drawing.FontStyle.Regular); }
        }


        public void Size(ListView obj)
        {
            string font = "Arial";
            int fontSize = 10;

            obj.Font = new System.Drawing.Font(font, fontSize, System.Drawing.FontStyle.Regular);
        }

        public void Size(ListView obj, int size)
        {
            string font = "Arial";
            int fontSize = size;

            obj.Font = new System.Drawing.Font(font, fontSize, System.Drawing.FontStyle.Regular);
        }


        public void Size(ListView obj, string font, int fontSize)
        {
            obj.Font = new System.Drawing.Font(font, fontSize, System.Drawing.FontStyle.Regular);
        }


    }
}
