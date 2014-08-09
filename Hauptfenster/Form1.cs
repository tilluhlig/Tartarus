// ***********************************************************************
// Assembly         : Hauptfenster
// Author           : Till
// Created          : 07-20-2013
//
// Last Modified By : Till
// Last Modified On : 06-30-2013
// ***********************************************************************
// <copyright file="Form1.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Color = System.Drawing.Color;

namespace Hauptfenster
{
    /// <summary>
    ///     Class Form1
    /// </summary>
    public partial class Form1 : Form
    {
        #region Fields

        /// <summary>
        ///     The pp
        /// </summary>
        public Game pp;

        #endregion Fields

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Form1" /> class.
        /// </summary>
        public Form1()
        {
            Tausch.Path = Application.StartupPath;
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            //pictureBox1.Anchor = AnchorStyles.None;
            Size = new Size(909, 611);
            //  Size = new System.Drawing.Size(800, 600);
            Width = Size.Width;
            Height = Size.Height;
            //  pictureBox1.Left = 0;
            //pictureBox1.Top = 0;
            //    pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;

            /* pictureBox1.Width = Size.Width;
             pictureBox1.Height = Size.Height;
             pictureBox1.Anchor = AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;*/

            tabControl1.TabPages.Remove(tabPage3);
            tabControl1.TabPages.Remove(tabPage2);
            tabControl1.TabPages.Remove(tabPage5);
            comboBox1.SelectedIndex = 4;
            //    comboBox1.Text = "40";
            comboBox3.SelectedIndex = 0;
            // FormBorderStyle = FormBorderStyle.None;

            Refresh();
            // Tausch.screenwidth = pictureBox1.Width;
            //  Tausch.screenheight = pictureBox1.Height;
            // StartPosition = FormStartPosition.Manual;
            label17.Text = "";
            label22.Text = "";
            label27.Text = "";
            Left = 0;
            Top = 0;

            progressBar1.Left = Width / 2 - progressBar1.Width / 2;
            progressBar1.Top = Height / 2 - progressBar1.Height / 2;
            label31.ForeColor = Color.Green;
            label31.AutoSize = true;

            Tausch.Mod = "C.conf";
            comboBox3.SelectedIndex = 2;
            comboBox3.Text = "C.conf";
            //  timer3.Enabled = true;
        }

        #endregion Constructors

        #region Destructors

        /// <summary>
        ///     Finalizes an instance of the <see cref="Form1" /> class.
        /// </summary>
        ~Form1()
        {
        }

        #endregion Destructors

        #region Methods

        /// <summary>
        ///     Gets the draw surface.
        /// </summary>
        /// <returns>IntPtr.</returns>
        public IntPtr getDrawSurface()
        {
            return pictureBox1.Handle; //pictureBox1 muss natürlich der Name eurer PictureBox sein!
        }

        /// <summary>
        ///     Hides all.
        /// </summary>
        public void HideAll()
        {
            if (tabControl1.TabPages.IndexOf(tabPage1) >= 0) tabControl1.TabPages.Remove(tabPage1);
            if (tabControl1.TabPages.IndexOf(tabPage2) >= 0) tabControl1.TabPages.Remove(tabPage2);
            if (tabControl1.TabPages.IndexOf(tabPage3) >= 0) tabControl1.TabPages.Remove(tabPage3);
            if (tabControl1.TabPages.IndexOf(tabPage4) >= 0) tabControl1.TabPages.Remove(tabPage4);
            if (tabControl1.TabPages.IndexOf(tabPage5) >= 0) tabControl1.TabPages.Remove(tabPage5);
            Cursor.Hide();
        }

        /// <summary>
        ///     Inserts the specified A.
        /// </summary>
        /// <param name="A">The A.</param>
        /// <param name="Text">The text.</param>
        /// <param name="Farbe">The farbe.</param>
        public void Insert(RichTextBox A, String Text, Color Farbe)
        {
            Text = Text + "\n";
            A.AppendText(Text);
            A.Select(A.TextLength - Text.Length, Text.Length);
            A.SelectionColor = Farbe;
        }

        /// <summary>
        ///     Lades the text.
        /// </summary>
        /// <param name="Text">The text.</param>
        public void LadeText(String Text)
        {
            label31.BringToFront();
            label31.Text = Text;
            label31.Top = progressBar1.Top - label31.Height; // + progressBar1.Height
            label31.Left = Width / 2 - label31.Width / 2;
            if (!label31.Visible) label31.Show();
        }

        /// <summary>
        ///     Shows the specified pages.
        /// </summary>
        /// <param name="pages">The pages.</param>
        /// <param name="select">The select.</param>
        public void Show(TabPage[] pages, TabPage select)
        {
            for (int i = 0; i < pages.Count(); i++)
            {
                if (tabControl1.TabPages.IndexOf(pages[i]) < 0) tabControl1.TabPages.Insert(0, pages[i]);
            }

            tabControl1.SelectTab(select.Name);
            Cursor.Show();
        }

        /// <summary>
        ///     Handles the Click event of the button1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void button1_Click(object sender, EventArgs e)
        {
        }

        /// <summary>
        ///     Handles the 1 event of the button1_Click control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void button1_Click_1(object sender, EventArgs e)
        {
            tabControl1.TabPages.Remove(tabPage2);
            tabControl1.TabPages.Insert(1, tabPage3);
            tabControl1.SelectedTab = tabPage3;
        }

        /// <summary>
        ///     Handles the Click event of the button10 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void button10_Click(object sender, EventArgs e)
        {
            HTTP.HTTP.sgames.Clear();
            if (checkBox2.Checked)
            {
                if (File.Exists("Content\\sgames.txt"))
                {
                    var datei = new StreamReader("Content\\sgames.txt");
                    for (int i = 0; !datei.EndOfStream; i++) HTTP.HTTP.sgames.Add(datei.ReadLine());
                    datei.Close();
                }
            }
            else
            {
                HTTP.HTTP.sgames = HTTP.HTTP.get_sgames();
                if (HTTP.HTTP.IsFailure(HTTP.HTTP.sgames)) return;
                HTTP.HTTP.sgames.RemoveAt(0);
                var datei = new StreamWriter("Content\\sgames.txt");
                for (int i = 0; i < HTTP.HTTP.sgames.Count; i++) datei.WriteLine(HTTP.HTTP.sgames[i]);
                datei.Close();
            }

            String sel = listBox1.Text;
            listBox1.Items.Clear();
            for (int i = 0; i < HTTP.HTTP.sgames.Count; i++)
                listBox1.Items.Add(HTTP.HTTP.sgames[i]);

            bool found = false;
            for (int i = 0; i < listBox1.Items.Count; i++)
                if (listBox1.Items[i].ToString() == sel)
                {
                    found = true;
                    listBox1.SelectedIndex = i;
                    break;
                }
        }

        /// <summary>
        ///     Handles the Click event of the button11 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void button11_Click(object sender, EventArgs e)
        {
            HTTP.HTTP.ogames.Clear();
            if (checkBox2.Checked)
            {
                if (File.Exists("Content\\ogames.txt"))
                {
                    var datei = new StreamReader("Content\\ogames.txt");
                    for (int i = 0; !datei.EndOfStream; i++) HTTP.HTTP.ogames.Add(datei.ReadLine());
                    datei.Close();
                }
            }
            else
            {
                HTTP.HTTP.ogames = HTTP.HTTP.get_ogames();
                if (HTTP.HTTP.IsFailure(HTTP.HTTP.ogames)) return;
                HTTP.HTTP.ogames.RemoveAt(0);
                var datei = new StreamWriter("Content\\ogames.txt");
                for (int i = 0; i < HTTP.HTTP.ogames.Count; i++) datei.WriteLine(HTTP.HTTP.ogames[i]);
                datei.Close();
            }

            String sel = listBox2.Text;
            listBox2.Items.Clear();
            for (int i = 0; i + 2 < HTTP.HTTP.ogames.Count; i += 3)
            {
                listBox2.Items.Add(HTTP.HTTP.ogames[i] + " (" + HTTP.HTTP.ogames[i + 1] + "," + HTTP.HTTP.ogames[i + 2] +
                                   ")");
            }

            bool found = false;
            for (int i = 0; i < listBox2.Items.Count; i++)
                if (listBox2.Items[i].ToString() == sel)
                {
                    found = true;
                    listBox2.SelectedIndex = i;
                    break;
                }
        }

        /// <summary>
        ///     Handles the Click event of the button12 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void button12_Click(object sender, EventArgs e)
        {
            richTextBox2.Clear();
            if (checkBox2.Checked)
            {
                Insert(richTextBox2, "Sie sind Offline!", Color.Red);
                return;
            }

            if (listBox2.SelectedIndex < 0) return;
            HTTP.HTTP.gameid = HTTP.HTTP.ogames[listBox2.SelectedIndex * 3].Trim(' ');
            List<String> list = HTTP.HTTP.join_game();
            if (!HTTP.HTTP.IsFailure(list))
            {
                // Spiel beigetreten
                HTTP.HTTP.gameid = list[1].TrimStart(' ');
                timer2_Tick(null, null);
                Insert(richTextBox2, HTTP.HTTP.Get_Meldung(list), Color.Green);
            }
            else
                Insert(richTextBox2, HTTP.HTTP.Get_Meldung(list), Color.Red);
        }

        /// <summary>
        ///     Handles the Click event of the button13 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void button13_Click(object sender, EventArgs e)
        {
            if (textBox4.Text != textBox5.Text)
            {
                label17.Text = "Falsches Passwort";

                return;
            }

            HTTP.HTTP.name = textBox3.Text;
            HTTP.HTTP.passwd = textBox4.Text;
            label17.Text = HTTP.HTTP.Get_Meldung(HTTP.HTTP.create_player(textBox6.Text));
        }

        /// <summary>
        ///     Handles the Click event of the button14 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void button14_Click(object sender, EventArgs e)
        {
            richTextBox2.Clear();
            if (checkBox2.Checked)
            {
                Insert(richTextBox2, "Sie sind Offline!", Color.Red);
                return;
            }

            List<String> list = HTTP.HTTP.create_game("v1.0");
            if (!HTTP.HTTP.IsFailure(list))
            {
                // neues Spiel erstellen
                HTTP.HTTP.gameid = list[1].TrimStart(' ');
                Tausch.CreateNewGame = true;
                timer2_Tick(null, null);
                Insert(richTextBox2, "Spiel erstellt", Color.Red);
            }
            else
                Insert(richTextBox2, HTTP.HTTP.Get_Meldung(list), Color.Red);
        }

        /// <summary>
        ///     Handles the Click event of the button15 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void button15_Click(object sender, EventArgs e)
        {
            HTTP.HTTP.name = textBox8.Text;
            HTTP.HTTP.passwd = textBox7.Text;
            // label22.Text = HTTP.HTTP.IsFailure(HTTP.HTTP.get_sgames()) ? "Login Fehlerhaft" : "Eingeloggt";
            List<String> list = null;
            if (!checkBox2.Checked) list = HTTP.HTTP.login();
            if (checkBox2.Checked)
            {
                label22.Text = "Eingeloggt";
            }
            else
                label22.Text = HTTP.HTTP.Get_Meldung(list);
            if (HTTP.HTTP.IsFailure(list) && list != null)
            {
                HTTP.HTTP.name = "";
                HTTP.HTTP.passwd = "";
            }
            else
            {
                if (tabControl1.TabPages.IndexOf(tabPage2) < 0) tabControl1.TabPages.Insert(1, tabPage2);
                if (tabControl1.TabPages.IndexOf(tabPage3) >= 0) tabControl1.TabPages.Remove(tabPage3);
                if (tabControl1.TabPages.IndexOf(tabPage5) >= 0) tabControl1.TabPages.Remove(tabPage5);
            }
        }

        /// <summary>
        ///     Handles the Click event of the button16 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void button16_Click(object sender, EventArgs e)
        {
            if (textBox11.Text != textBox12.Text)
            {
                label27.Text = "Falsches neues Passwort";

                return;
            }
            HTTP.HTTP.name = textBox10.Text;
            HTTP.HTTP.passwd = textBox9.Text;
            label27.Text = HTTP.HTTP.Get_Meldung(HTTP.HTTP.set_passwd(textBox11.Text));
        }

        /// <summary>
        ///     Handles the Click event of the button17 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void button17_Click(object sender, EventArgs e)
        {
            richTextBox2.Clear();
            if (checkBox2.Checked)
            {
                Insert(richTextBox2, "Sie sind Offline!", Color.Red);
                return;
            }
            /* for (int p = 0; p < listBox1.Items.Count; p++)
             {
                 String Pfad = "";
                 String ID = HTTP.HTTP.sgames[p].ToString();
                 HTTP.HTTP.gameid = ID;

                 if (HTTP.HTTP.gameid != "")
                 {
                     Pfad = "Content\\Games\\" + HTTP.HTTP.gameid + "\\";
                 }
                 else
                     Pfad = "Content\\Games\\temp\\";

                 HTTP.HTTP.Dir(Pfad);
                 File.Delete(Pfad + "GameInfo.dat");
             }*/

            for (int p = 0; p < listBox1.Items.Count; p++)
            {
                String Pfad = "";
                String ID = HTTP.HTTP.sgames[p];
                HTTP.HTTP.gameid = ID;

                if (HTTP.HTTP.gameid != "")
                {
                    Pfad = "Content\\Games\\" + HTTP.HTTP.gameid + "\\";
                }
                else
                    Pfad = "Content\\Games\\temp\\";

                HTTP.HTTP.Dir(Pfad);

                if (!File.Exists(Pfad + "OldMap.dat") && !File.Exists(Pfad + "Map.dat"))
                {
                    List<String> list = HTTP.HTTP.get_map();
                    if (!HTTP.HTTP.IsFailure(list))
                    {
                        var datei = new StreamWriter(Pfad + "OldMap.dat");
                        for (int i = 1; i < list.Count; i++) datei.WriteLine(list[i]);
                        datei.Close();
                    }
                }

                if (!File.Exists(Pfad + "OldData.dat") && !File.Exists(Pfad + "Data.dat"))
                {
                    List<String> list = HTTP.HTTP.get_data();
                    if (!HTTP.HTTP.IsFailure(list))
                    {
                        var datei = new StreamWriter(Pfad + "OldData.dat");
                        for (int i = 1; i < list.Count; i++) datei.WriteLine(list[i]);
                        datei.Close();
                    }
                }

                if (!File.Exists(Pfad + "GameInfo.dat"))
                {
                    File.Delete(Pfad + "GameInfo.dat");
                    List<String> list2 = HTTP.HTTP.get_gameinfo();
                    if (!HTTP.HTTP.IsFailure(list2))
                    {
                        var datei = new StreamWriter(Pfad + "GameInfo.dat");
                        for (int i = 1; i < list2.Count; i++) datei.WriteLine(list2[i]);
                        datei.Close();
                    }
                }

                if ((File.Exists(Pfad + "OldData.dat") && File.Exists(Pfad + "OldMap.dat")) ||
                    (File.Exists(Pfad + "Data.dat") && File.Exists(Pfad + "Map.dat")))
                {
                    // Spiel starten
                    Insert(richTextBox2, ID + " OK", Color.Green);
                }
                else
                    Insert(richTextBox2, ID + " FAIL", Color.Red);
            }
        }

        /// <summary>
        ///     Handles the Click event of the button18 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void button18_Click(object sender, EventArgs e)
        {
            // alle hochladen
        }

        /// <summary>
        ///     Handles the Click event of the button19 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void button19_Click(object sender, EventArgs e)
        {
            ActiveForm.Text = ActiveForm.Width + "x" + ActiveForm.Height;
        }

        /// <summary>
        ///     Handles the Click event of the button2 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void button2_Click(object sender, EventArgs e)
        {
            Tausch.StartServer = true;
            button6.Show();
            panel3.Show();
        }

        /// <summary>
        ///     Handles the Click event of the button3 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void button3_Click(object sender, EventArgs e)
        {
            Tausch.ZielIP = textBox1.Text;
            Tausch.StartClient = true;
            panel3.Show();
        }

        /// <summary>
        ///     Handles the Click event of the button4 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void button4_Click(object sender, EventArgs e)
        {
            BackColor = Color.Black;
            FormState.Maximize(this, checkBox3.Checked);
            if (!checkBox3.Checked) FormBorderStyle = FormBorderStyle.Sizable;

            //  Tausch.screenwidth = 1024;
            //  Tausch.screenheight = 768;
            Size = new Size(Tausch.screenwidth, Tausch.screenheight);
            //Size = new System.Drawing.Size(800, 600);
            Width = Size.Width;
            Height = Size.Height;

            if (pictureBox1.Width < Tausch.screenwidth) Width += Tausch.screenwidth - pictureBox1.Width;
            if (pictureBox1.Height < Tausch.screenheight) Height += Tausch.screenheight - pictureBox1.Height;

            HTTP.HTTP.gameid = "";
            Tausch.Kartengroesse = Convert.ToInt32(comboBox1.Text);
            Tausch.Mod = comboBox3.Text;
            Tausch.SpielAktiv = true;
            Tausch.StarteSpiel = true;
            HideAll();
            tabControl1.Hide();
            pictureBox1.Show();
        }

        /// <summary>
        ///     Handles the Click event of the button5 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void button5_Click(object sender, EventArgs e)
        {
            tabControl1.TabPages.Remove(tabPage2);
            tabControl1.TabPages.Insert(1, tabPage5);
            tabControl1.SelectedTab = tabPage5;
            timer2_Tick(null, null);
            timer2.Enabled = true;
        }

        /// <summary>
        ///     Handles the Click event of the button6 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void button6_Click(object sender, EventArgs e)
        {
            button6.Hide();
            button4.Hide();
            button3.Hide();
            button2.Hide();
            button1.Hide();
            textBox2.Hide();
            textBox1.Hide();
            richTextBox1.Hide();
            pictureBox1.Width = Width;
            pictureBox1.Height = Height;
            Width = Tausch.screenwidth;
            Height = Tausch.screenheight;
            pictureBox1.Left = 0;
            pictureBox1.Top = 0;

            Tausch.SpielAktiv = true;
            Tausch.StarteSpiel = true;
            tabControl1.Hide();
            pictureBox1.Show();
            Tausch.Output.Add("<SPIELSTART>");
        }

        /// <summary>
        ///     Handles the Click event of the button7 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void button7_Click(object sender, EventArgs e)
        {
            timer2.Enabled = false;
            tabControl1.TabPages.Remove(tabPage5);
            if (tabControl1.TabPages.IndexOf(tabPage2) < 0) tabControl1.TabPages.Insert(1, tabPage2);
            tabControl1.SelectedTab = tabPage2;
        }

        /// <summary>
        ///     Handles the Click event of the button8 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void button8_Click(object sender, EventArgs e)
        {
            tabControl1.TabPages.Remove(tabPage3);
            if (tabControl1.TabPages.IndexOf(tabPage2) < 0) tabControl1.TabPages.Insert(1, tabPage2);
            tabControl1.SelectedTab = tabPage2;
        }

        /// <summary>
        ///     Handles the Click event of the button9 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void button9_Click(object sender, EventArgs e)
        {
            richTextBox2.Clear();
            if (listBox1.SelectedIndex < 0) return;
            String Pfad = "";
            HTTP.HTTP.gameid = HTTP.HTTP.sgames[listBox1.SelectedIndex].Trim(' ');

            if (HTTP.HTTP.gameid != "")
            {
                Pfad = "Content\\Games\\" + HTTP.HTTP.gameid + "\\";
            }
            else
                Pfad = "Content\\Games\\temp\\";

            bool fail = false;

            HTTP.HTTP.Dir(Pfad);
            if (!File.Exists(Pfad + "OldMap.dat") && !File.Exists(Pfad + "Map.dat") && !fail)
            {
                List<String> list = HTTP.HTTP.get_map();
                if (!HTTP.HTTP.IsFailure(list))
                {
                    var datei = new StreamWriter(Pfad + "OldMap.dat");
                    for (int i = 1; i < list.Count; i++) datei.WriteLine(list[i]);
                    datei.Close();
                }
                else
                {
                    Insert(richTextBox2, HTTP.HTTP.Get_Meldung(list), Color.Red);
                    fail = true;
                }
            }

            if (!File.Exists(Pfad + "OldData.dat") && !File.Exists(Pfad + "Data.dat") && !fail)
            {
                List<String> list = HTTP.HTTP.get_data();
                if (!HTTP.HTTP.IsFailure(list))
                {
                    var datei = new StreamWriter(Pfad + "OldData.dat");
                    for (int i = 1; i < list.Count; i++) datei.WriteLine(list[i]);
                    datei.Close();
                }
                else
                {
                    Insert(richTextBox2, HTTP.HTTP.Get_Meldung(list), Color.Red);
                    fail = true;
                }
            }

            if (!checkBox2.Checked && !fail) //File.Exists(Pfad + "GameInfo.dat")
            {
                File.Delete(Pfad + "GameInfo.dat");
                List<String> list = HTTP.HTTP.get_gameinfo();
                if (!HTTP.HTTP.IsFailure(list))
                {
                    var datei = new StreamWriter(Pfad + "GameInfo.dat");
                    for (int i = 1; i < list.Count; i++) datei.WriteLine(list[i]);
                    datei.Close();
                }
                else
                {
                    Insert(richTextBox2, HTTP.HTTP.Get_Meldung(list), Color.Red);
                    fail = true;
                }
            }

            if (!fail && File.Exists(Pfad + "GameInfo.dat") &&
                (File.Exists(Pfad + "OldData.dat") && File.Exists(Pfad + "OldMap.dat")) ||
                (File.Exists(Pfad + "Data.dat") && File.Exists(Pfad + "Map.dat")))
            {
                // Spiel starten
                var datei = new StreamReader(Pfad + "GameInfo.dat");
                var list = new List<String>();
                for (; !datei.EndOfStream; ) list.Add(datei.ReadLine());
                datei.Close();

                if (!HTTP.HTTP.IsFailure(list))
                {
                    if (list[0].Trim() == HTTP.HTTP.name)
                    {
                        Tausch.CurrentPlayer = list[0].Trim() == list[1].Trim() ? 0 : 1;
                        BackColor = Color.Black;
                        FormState.Maximize(this, checkBox3.Checked);
                        if (!checkBox3.Checked) FormBorderStyle = FormBorderStyle.Sizable;

                        Size = new Size(Tausch.screenwidth, Tausch.screenheight);
                        Width = Size.Width;
                        Height = Size.Height;

                        if (pictureBox1.Width < Tausch.screenwidth) Width += Tausch.screenwidth - pictureBox1.Width;
                        if (pictureBox1.Height < Tausch.screenheight)
                            Height += Tausch.screenheight - pictureBox1.Height;

                        if ((File.Exists(Pfad + "Data.dat") && File.Exists(Pfad + "Map.dat")))
                        {
                            Tausch.Map = Pfad + "Map.dat";
                            Tausch.Data = Pfad + "Data.dat";
                        }
                        else
                        {
                            Tausch.Map = Pfad + "OldMap.dat";
                            Tausch.Data = Pfad + "OldData.dat";
                        }

                        //Tausch.SpielAktiv = true;
                        Tausch.Mod = "A.conf";
                        Tausch.SpielLaden = true;
                        HideAll();
                        tabControl1.Hide();
                        pictureBox1.Show();
                    }
                    else
                    {
                        Insert(richTextBox2, "Spieler ist nicht am Zug", Color.Red);
                        File.Delete(Pfad + "OldMap.dat");
                        File.Delete(Pfad + "OldData.dat");
                        File.Delete(Pfad + "Map.dat");
                        File.Delete(Pfad + "Data.dat");
                        File.Delete(Pfad + "GameInfo.dat");
                    }
                }
                else
                    Insert(richTextBox2, HTTP.HTTP.Get_Meldung(list), Color.Red);
            }
        }

        /// <summary>
        ///     Handles the CheckedChanged event of the checkBox1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                HTTP.HTTP.SetLocal();
            }
            else
                HTTP.HTTP.UnsetLocal();
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
        }

        /// <summary>
        ///     Handles the SelectedIndexChanged event of the comboBox1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Tausch.Kartengroesse = comboBox1.SelectedIndex + 1;
        }

        /// <summary>
        ///     Handles the Activated event of the Form1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void Form1_Activated(object sender, EventArgs e)
        {
        }

        /// <summary>
        ///     Handles the FormClosing event of the Form1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="FormClosingEventArgs" /> instance containing the event data.</param>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            pp.Exit();
        }

        /// <summary>
        ///     Handles the KeyPress event of the Form1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyPressEventArgs" /> instance containing the event data.</param>
        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        /// <summary>
        ///     Handles the Load event of the Form1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            //   button4_Click(null, null);
        }

        /// <summary>
        ///     Handles the Resize event of the Form1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void Form1_Resize(object sender, EventArgs e)
        {
            progressBar1.Left = Width / 2 - progressBar1.Width / 2;
            progressBar1.Top = Height / 2 - progressBar1.Height / 2;
        }

        /// <summary>
        ///     Handles the VisibleChanged event of the Form1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void Form1_VisibleChanged(object sender, EventArgs e)
        {
        }

        /// <summary>
        ///     Handles the Click event of the label12 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void label12_Click(object sender, EventArgs e)
        {
        }

        /// <summary>
        ///     Handles the SelectedIndexChanged event of the listBox1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        /// <summary>
        ///     Handles the Paint event of the panel5 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="PaintEventArgs" /> instance containing the event data.</param>
        private void panel5_Paint(object sender, PaintEventArgs e)
        {
        }

        /// <summary>
        ///     Handles the KeyPress event of the tabControl1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyPressEventArgs" /> instance containing the event data.</param>
        private void tabControl1_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        /// <summary>
        ///     Handles the KeyPress event of the textBox2 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyPressEventArgs" /> instance containing the event data.</param>
        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                Tausch.Output.Add(textBox2.Text);
                String q = textBox2.Text + "\n";
                Insert(richTextBox1, q, Color.Red);
                textBox2.Text = "";
            }
        }

        /// <summary>
        ///     Handles the KeyDown event of the textBox8 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyEventArgs" /> instance containing the event data.</param>
        private void textBox8_KeyDown(object sender, KeyEventArgs e)
        {
        }

        /// <summary>
        ///     Handles the Tick event of the timer1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Tausch.Input.Count > 0)
            {
                for (int i = 0; i < Tausch.Input.Count; i++)
                {
                    if (Tausch.Input[i] == "<SPIELSTART>")
                    {
                        Tausch.SpielAktiv = true;
                        button6.Hide();
                        button4.Hide();
                        button3.Hide();
                        button2.Hide();
                        button1.Hide();
                        textBox2.Hide();
                        textBox1.Hide();
                        richTextBox1.Hide();
                        label10.Show();
                    }
                    else if (Tausch.Input[i] == "<ALL>")
                    {
                        Tausch.SpielAktiv = true;
                        richTextBox1.Hide();
                        button6.Hide();
                        button4.Hide();
                        button3.Hide();
                        button2.Hide();
                        button1.Hide();
                        textBox2.Hide();
                        textBox1.Hide();
                        pictureBox1.Width = Width;
                        pictureBox1.Height = Height;
                        Width = Tausch.screenwidth;
                        Height = Tausch.screenheight;
                        pictureBox1.Left = 0;
                        pictureBox1.Top = 0;
                        tabControl1.Hide();
                        pictureBox1.Show();
                    }
                    else
                    {
                        String q = Tausch.Input[i] + "\n";
                        Insert(richTextBox1, q, Color.Blue);
                    }
                }
                Tausch.Input.Clear();
            }
        }

        /// <summary>
        ///     Handles the Tick event of the timer2 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void timer2_Tick(object sender, EventArgs e)
        {
            //if (!checkBox2.Checked)
            // {
            button10_Click(null, null);
            button11_Click(null, null);
            // }
        }

        /// <summary>
        ///     Handles the Tick event of the timer3 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void timer3_Tick(object sender, EventArgs e)
        {
            timer3.Enabled = false;
            button4_Click(null, null);
        }

        #endregion Methods

        #region Classes

        /// <summary>
        ///     Class Colors
        /// </summary>
        public static class Colors
        {
            #region Methods

            /// <summary>
            ///     Froms the name.
            /// </summary>
            /// <param name="name">The name.</param>
            /// <returns>System.Drawing.Color.</returns>
            public static Color FromName(String name)
            {
                return ColorTranslator.FromHtml(name);
            }

            /// <summary>
            ///     To the name.
            /// </summary>
            /// <param name="Name">The name.</param>
            /// <returns>String.</returns>
            public static String ToName(Color Name)
            {
                return ColorTranslator.ToHtml(Name);
            }

            #endregion Methods
        }

        #endregion Classes

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }

    /// <summary>
    ///     Class Tausch
    /// </summary>
    public static class Tausch
    {
        #region Fields

        /// <summary>
        ///     The create new game
        /// </summary>
        public static bool CreateNewGame = false;

        /// <summary>
        ///     The current player
        /// </summary>
        public static int CurrentPlayer = 0;

        /// <summary>
        ///     The data
        /// </summary>
        public static String Data = "";

        /// <summary>
        ///     The input
        /// </summary>
        public static List<String> Input = new List<String>();

        /// <summary>
        ///     The kartengroesse
        /// </summary>
        public static int Kartengroesse = 5;

        /// <summary>
        ///     The map
        /// </summary>
        public static String Map = "";

        /// <summary>
        ///     The mod
        /// </summary>
        public static String Mod = "A.conf";

        /// <summary>
        ///     The output
        /// </summary>
        public static List<String> Output = new List<String>();

        public static string Path = "";

        /// <summary>
        ///     The screenheight
        /// </summary>
        public static int screenheight = (int)(Screen.PrimaryScreen.Bounds.Height * 1f);

        /// <summary>
        ///     The screenwidth
        /// </summary>
        public static int screenwidth = (int)(Screen.PrimaryScreen.Bounds.Width * 1f);

        /// <summary>
        ///     The spiel aktiv
        /// </summary>
        public static bool SpielAktiv = false;

        /// <summary>
        ///     The spiel laden
        /// </summary>
        public static bool SpielLaden = false;

        /// <summary>
        ///     The start client
        /// </summary>
        public static bool StartClient = false;

        /// <summary>
        ///     The starte spiel
        /// </summary>
        public static bool StarteSpiel = false;

        /// <summary>
        ///     The start server
        /// </summary>
        public static bool StartServer = false;

        /// <summary>
        ///     The ziel IP
        /// </summary>
        public static String ZielIP = "localhost";

        #endregion Fields

        //Hauptfenster.Form1.pictureBox1.width;

        //public static int screenwidth = (int)(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width * 1f);//Hauptfenster.Form1.pictureBox1.width;
    }
}