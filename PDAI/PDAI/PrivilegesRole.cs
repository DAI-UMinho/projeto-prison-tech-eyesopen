using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace PDAI
{
    class PrivilegesRole
    {

        public Panel container { get; }
        public int width { set { container.Size = new Size(value, container.Height); } get { return container.Width; } }
        public int height { set { container.Size = new Size(container.Width, value); } get { return container.Height; } }

        Database database;
        Dictionary<CheckBox, PrivilegeItem> GetPrivilegeItem;
        Dictionary<CheckBox, CheckBox> GetTitleCheckBox;
        Font_Class font;
        Button add;
        TextBox tPrivilegesRole;
        ListView_Class lv;
        ObjectList privilegesList;
        Dictionary<string, List<string>> rolePrivileges;

        int fontSize = 16;
        string currentRole;
        CheckBox currentTitleCheckBox;
        bool autoCheck;
        List<string> allPrivileges;


        public PrivilegesRole()
        {
            container = new Panel();
            container.Size = new Size(200, 400);
            container.Location = new Point(0, 0);
            container.BackColor = Color.White;
            
            font = new Font_Class();
            database = new Database();
            privilegesList = new ObjectList();
            rolePrivileges = new Dictionary<string, List<string>>();

            GetPrivilegeItem = new Dictionary<CheckBox, PrivilegeItem>();
            GetTitleCheckBox = new Dictionary<CheckBox, CheckBox>();
            autoCheck = false;
            allPrivileges = new List<string>();
            foreach (string privilege in Rule.GetPrivileges())
            {
                Array arrayTest = privilege.Split('-');
                if (arrayTest.Length > 1)
                {
                    allPrivileges.Add(privilege);
                }
            }
        }


        public void Open()
        {

            Label title = new Label();
            title.Text = "Adicionar/Editar/Remover  Papel";
            title.ForeColor = Color.FromArgb(127, 127, 127);
            title.Size = new Size(container.Width, 30);
            title.Location = new Point(0, 0);
            container.Controls.Add(title);
            font.Size(title, fontSize);

            Panel line = new Panel();
            line.BackColor = Color.FromArgb(127, 127, 127);
            line.Size = new Size(container.Width, 1);
            line.BackColor = Color.Black;
            line.Location = new Point(0, title.Location.Y + title.Height);
            container.Controls.Add(line);


            Label lRole = new Label();
            lRole.Text = "Papel";
            lRole.Size = new Size(100, 23);
            lRole.Location = new Point(0, line.Location.Y + 80);
            container.Controls.Add(lRole);
            font.Size(lRole, fontSize - 3);

            tPrivilegesRole = new TextBox();
            tPrivilegesRole.Size = new Size(250, 30);
            tPrivilegesRole.Location = new Point(lRole.Location.X, lRole.Location.Y + lRole.Height + 3);
            tPrivilegesRole.TextChanged += new EventHandler(Text_Changed);
            container.Controls.Add(tPrivilegesRole);
            font.Size(tPrivilegesRole, fontSize - 3);


            add = new Button();
            add.Text = "Adicionar";
            add.Size = new Size(100, 50);
            add.Location = new Point(0, tPrivilegesRole.Location.Y + tPrivilegesRole.Height + 50);
            add.Click += new EventHandler(Button_Click);
            container.Controls.Add(add);
            font.Size(add, fontSize - 3);



            lv = new ListView_Class();
            lv.Location = new Point(tPrivilegesRole.Location.X + tPrivilegesRole.Width + container.Width * 1/6, lRole.Location.Y);
            lv.Width = tPrivilegesRole.Width;
            lv.Height = container.Height * 8 / 10 - lv.Location.Y;
            lv.add_Column("Papel", lv.Width - 5);
            lv.add_Column("id", 0);
            lv.DoubleClick += new EventHandler(lv_Click);
            lv.Click += new EventHandler(lv_Click);
            font.Size(lv, fontSize - 3);
            container.Controls.Add(lv);

            database.select.GetPrivilegesRole(lv);

            //for (int i = 0; i < lv.Items.Count; i++)
            //{
            //    rolePrivileges = database.select.GetPrivileges(Convert.ToUInt32(lv.Items[i].SubItems[1].Text),rolePrivileges);
            //}
            rolePrivileges = database.select.GetPrivileges(Convert.ToUInt32(lv.Items[0].SubItems[1].Text), rolePrivileges);
            try { createPrivilegeList(0, Convert.ToUInt32(lv.Items[0].SubItems[1].Text)); } catch (Exception) { }

        }



        private void Text_Changed(object sender, EventArgs e)
        {

            if (rolePrivileges.ContainsKey(((TextBox)sender).Text)) add.Text = "Remover";
            else add.Text = "Adicionar";

        }

        private void lv_Click(object sender, EventArgs e)
        {
            rolePrivileges = database.select.GetPrivileges(Convert.ToUInt32(lv.Items[lv.getIndexSelectedItem()].SubItems[1].Text), rolePrivileges);
            createPrivilegeList(lv.getIndexSelectedItem(), Convert.ToUInt32(lv.Items[lv.getIndexSelectedItem()].SubItems[1].Text));
            tPrivilegesRole.Text = ((ListView_Class)sender).Items[((ListView_Class)sender).getIndexSelectedItem()].Text;
          
        }



        private void Button_Click(object sender, EventArgs e)
        {

            if (add.Text == "Alterar")
            {
                bool sucess = false;
                foreach (string privilege in allPrivileges)
                {
                    try
                    {

                        database.update.Privileges(Convert.ToUInt32(lv.Items[lv.getIndexSelectedItem()].SubItems[1].Text), privilege, rolePrivileges[lv.Items[lv.getIndexSelectedItem()].Text].Contains(privilege));
                        sucess = true;
                        
                    }
                    catch (Exception ee){ MessageBox.Show("" + ee); }
                }

                if(sucess) MessageBox.Show("Alterado com sucesso!");
                else MessageBox.Show("Não foi possível alterar.");
                add.Text = "Adicionar";
            }
            else
            {
                if (add.Text == "Adicionar") { database.insert.SetPrivileges(database.insert.PrivilegesRole(tPrivilegesRole.Text), allPrivileges); }
                else
                {
                    DialogResult dialogResult = MessageBox.Show("Pretende eliminar o papel selecionado?", "Eliminar", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        database.delete.PrivilegesRole(tPrivilegesRole.Text);
                    }

                }

                database.select.GetPrivilegesRole(lv);
                rolePrivileges = new Dictionary<string, List<string>>();
                for (int i = 0; i < lv.Items.Count; i++)
                {
                    rolePrivileges = database.select.GetPrivileges(Convert.ToUInt32(lv.Items[i].SubItems[1].Text), rolePrivileges);
                }
                tPrivilegesRole.Text = "";
                try { createPrivilegeList(lv.Items.Count - 1, Convert.ToUInt32(lv.Items[lv.Items.Count - 1].SubItems[1].Text)); } catch (Exception) { if (container.Contains(privilegesList.container)) { container.Controls.Remove(privilegesList.container); } }
            }
 
        }


        private void createPrivilegeList(int index, uint id)
        {
            if (lv.Items.Count != 0)
            {
                lv.set_ItemAsSelected(index);
                currentRole = lv.Items[index].SubItems[0].Text;

                if (container.Contains(privilegesList.container)) container.Controls.Remove(privilegesList.container);
                privilegesList = new ObjectList();
                privilegesList.locationX = lv.Location.X + lv.Width + container.Width * 1 / 10;
                privilegesList.locationY = lv.Location.Y;
                privilegesList.width = (container.Width - privilegesList.locationX) * 9 / 10;
                privilegesList.height = container.Height * 8 / 10 - lv.Location.Y;
                container.Controls.Add(privilegesList.container);
                privilegesList.container.Visible = false;
                string val = "";
                PrivilegeItem privileges = new PrivilegeItem(CheckBoxContentChanged);
                foreach (string privilege in Rule.GetPrivileges())
                {
                    string privilegeTitle = privilege.Split('-').GetValue(0).ToString();
                    
                    if (val == privilegeTitle)
                    {
                        string thePrivilege = privilege.Split('-').GetValue(1).ToString();
                        CheckBox checkBox = new CheckBox();
                        privileges.AddPrivilege(id, thePrivilege, privilege, 10, false, rolePrivileges[lv.Items[index].SubItems[0].Text].Contains(privilege), checkBox);
                        GetTitleCheckBox[checkBox] = currentTitleCheckBox;
                        if (rolePrivileges[lv.Items[index].SubItems[0].Text].Contains(privilege)) setPrivilegeTitleAsChecked(checkBox);
                        
                    }
                    else
                    {
                        privileges = new PrivilegeItem(CheckBoxContentChanged);
                        privileges.locationX = 0;
                        privileges.locationY = 0;
                        privileges.width = privilegesList.width;
                        privileges.height = 0;
                        privilegesList.AddItem(privileges.container);
                        CheckBox checkBox = new CheckBox();
                        privileges.AddPrivilege(id,privilegeTitle, privilege, 12, true, false, checkBox);
                        //privileges.AddPrivilege(id, thePrivilege, privilege, 10, false, privilegesRole[lv.Items[index].SubItems[0].Text].Contains(privilege));
                        val = privilegeTitle;
                        GetPrivilegeItem[checkBox] = privileges;
                        currentTitleCheckBox = checkBox;

                    }
                }

                privilegesList.Update();
                privileges.Update();
                privilegesList.container.Visible = true;
            }
        }


        private void CheckBoxContentChanged(object sender, EventArgs e)
        {
            if(((CheckBox)sender).Name.Split('-').GetValue(0).ToString() == "title")
            {
                if(!autoCheck)
                foreach (CheckBox ckb in GetPrivilegeItem[((CheckBox)sender)].privilegesCheckBoxes[((CheckBox)sender).Name.Split('-').GetValue(1).ToString()])
                {
                    if (((CheckBox)sender).Checked) ckb.Checked = true;
                    else ckb.Checked = false;
                }
            }
            else
            {
                if (((CheckBox)sender).Checked) { rolePrivileges[currentRole].Add(((CheckBox)sender).Name); setPrivilegeTitleAsChecked(((CheckBox)sender)); }
                else
                {
                    rolePrivileges[currentRole].Remove(((CheckBox)sender).Name);
                    int counter = 0;
                    foreach (CheckBox ckb in GetPrivilegeItem[GetTitleCheckBox[((CheckBox)sender)]].privilegesCheckBoxes[GetTitleCheckBox[((CheckBox)sender)].Name.Split('-').GetValue(1).ToString()])
                    {
                        if (ckb.Checked) counter++;
                    }
                    if(counter == 0)
                    {
                        autoCheck = true;
                        GetTitleCheckBox[((CheckBox)sender)].Checked = false;
                    }
                }
            }
            add.Text = "Alterar";
            autoCheck = false;
        }


        private void setPrivilegeTitleAsChecked(CheckBox checkBox)
        {
            if (!GetTitleCheckBox[checkBox].Checked)
            {
                autoCheck = true;
                GetTitleCheckBox[checkBox].Checked = true;
            }
        }



    }
}
