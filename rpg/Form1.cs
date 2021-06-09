using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace rpg
{

    public partial class Form1 : Form
    {
        public Actions a = new Actions(new OneToOne());
        int strategy = 1; // 1 - 1x1, 2 - 3x3, 1 - AxA

        public ActionsInvoker invoker = new ActionsInvoker();

        BeepObserver observer = new BeepObserver();

        public Form1()
        {
            InitializeComponent();
        }

        private void createArmies_Click(object sender, EventArgs e)
        {
            try
            {
                if ((Convert.ToUInt32(textBoxCash1.Text) < LightInfantry.cost) || (Convert.ToUInt32(textBoxCash2.Text) < LightInfantry.cost))
                {
                    lblInfoExceptions.Text = "Введены некорректные значения денег!";
                }
                else
                {
                    a.CreateArmies(1, Convert.ToUInt32(textBoxCash1.Text));
                    a.CreateArmies(2, Convert.ToUInt32(textBoxCash2.Text));
                    if (checkBoxBeep.Checked == true)
                    {
                        a.AttachUnits(observer);
                    }
                    lblInfo.Text = "Армии созданы!";
                    lblInfoExceptions.Text = "";
                    if (strategy == 1) showArmies1();
                    if (strategy == 2) showArmies2();
                }
            }
            catch
            {
                lblInfoExceptions.Text = "Введены некорректные значения денег!";
            }
        }

        private void showArmies1()
        {
            var army_1 = a.GetArmy(1);
            var army_2 = a.GetArmy(2);
            lblArmy1.Text = "Армия 1:";
            lblArmy2.Text = "Армия 2:";
            lblArmy12.Text = "Армия 1:";
            lblArmy13.Text = "Армия 1:";
            lblArmy22.Text = "Армия 2:";
            lblArmy23.Text = "Армия 2:";


            for (int i = army_1.Count - 1; i >= 0; i--)
            {
                string str = Convert.ToString(army_1[i].GetType());
                string hp = Convert.ToString(army_1[i].Health());


                switch (str)
                {
                    case "rpg.ProxyHI":
                        {
                            lblArmy1.Text += " H";
                            if ((army_1[i] as ProxyHI).horse == true) lblArmy1.Text += "(5)";
                            if ((army_1[i] as ProxyHI).spear == true) lblArmy1.Text += "(|)";
                            if ((army_1[i] as ProxyHI).helmet == true) lblArmy1.Text += "(^)";
                            if ((army_1[i] as ProxyHI).shield == true) lblArmy1.Text += "([])";
                            break;
                        }
                    case "rpg.LightInfantry":
                        lblArmy1.Text += " L";
                        break;
                    case "rpg.Bowman":
                        lblArmy1.Text += " B";
                        break;
                    case "rpg.Doctor":
                        lblArmy1.Text += " D";
                        break;
                    case "rpg.Wizard":
                        lblArmy1.Text += " W";
                        break;
                    default:
                        lblArmy1.Text += " E";
                        break;
                }
                lblArmy1.Text += hp;
            }

            for (int i = army_2.Count - 1; i >= 0; i--)
            {
                string str = Convert.ToString(army_2[i].GetType());
                string hp = Convert.ToString(army_2[i].Health());
                switch (str)
                {
                    case "rpg.ProxyHI":
                        {
                            lblArmy2.Text += " H";
                            if ((army_2[i] as ProxyHI).horse == true) lblArmy2.Text += "(5)";
                            if ((army_2[i] as ProxyHI).spear == true) lblArmy2.Text += "(|)";
                            if ((army_2[i] as ProxyHI).helmet == true) lblArmy2.Text += "(^)";
                            if ((army_2[i] as ProxyHI).shield == true) lblArmy2.Text += "([])";
                            break;
                        }
                    case "rpg.LightInfantry":
                        lblArmy2.Text += " L";
                        break;
                    case "rpg.Bowman":
                        lblArmy2.Text += " B";
                        break;
                    case "rpg.Doctor":
                        lblArmy2.Text += " D";
                        break;
                    case "rpg.Wizard":
                        lblArmy2.Text += " W";
                        break;
                    default:
                        lblArmy2.Text += " E";
                        break;
                }
                lblArmy2.Text += hp;
            }
        }

        private void showArmies2()
        {
            var army_1 = a.GetArmy(1);
            var army_2 = a.GetArmy(2);
            lblArmy1.Text = "Армия 1:";
            lblArmy2.Text = "Армия 2:";
            lblArmy12.Text = "Армия 1:";
            lblArmy13.Text = "Армия 1:";
            lblArmy22.Text = "Армия 2:";
            lblArmy23.Text = "Армия 2:";

            int j = 2;
            for (int i = army_1.Count - 1; i >= 0; i--)
            {
                string str = Convert.ToString(army_1[i].GetType());
                string hp = Convert.ToString(army_1[i].Health());

                if (j == 2) j = 0;
                else j++;

                if (j == 0)
                {
                    switch (str)
                    {
                        case "rpg.ProxyHI":
                            {
                                lblArmy1.Text += " H";
                                if ((army_1[i] as ProxyHI).horse == true) lblArmy1.Text += "(5)";
                                if ((army_1[i] as ProxyHI).spear == true) lblArmy1.Text += "(|)";
                                if ((army_1[i] as ProxyHI).helmet == true) lblArmy1.Text += "(^)";
                                if ((army_1[i] as ProxyHI).shield == true) lblArmy1.Text += "([])";
                                break;
                            }
                        case "rpg.LightInfantry":
                            lblArmy1.Text += " L";
                            break;
                        case "rpg.Bowman":
                            lblArmy1.Text += " B";
                            break;
                        case "rpg.Doctor":
                            lblArmy1.Text += " D";
                            break;
                        case "rpg.Wizard":
                            lblArmy1.Text += " W";
                            break;
                        default:
                            lblArmy1.Text += " E";
                            break;
                    }
                    lblArmy1.Text += hp;
                }

                if (j == 1)
                {
                    switch (str)
                    {
                        case "rpg.ProxyHI":
                            {
                                lblArmy12.Text += " H";
                                if ((army_1[i] as ProxyHI).horse == true) lblArmy12.Text += "(5)";
                                if ((army_1[i] as ProxyHI).spear == true) lblArmy12.Text += "(|)";
                                if ((army_1[i] as ProxyHI).helmet == true) lblArmy12.Text += "(^)";
                                if ((army_1[i] as ProxyHI).shield == true) lblArmy12.Text += "([])";
                                break;
                            }
                        case "rpg.LightInfantry":
                            lblArmy12.Text += " L";
                            break;
                        case "rpg.Bowman":
                            lblArmy12.Text += " B";
                            break;
                        case "rpg.Doctor":
                            lblArmy12.Text += " D";
                            break;
                        case "rpg.Wizard":
                            lblArmy12.Text += " W";
                            break;
                        default:
                            lblArmy12.Text += " E";
                            break;
                    }
                    lblArmy12.Text += hp;
                }

                if (j == 2)
                {
                    switch (str)
                    {
                        case "rpg.ProxyHI":
                            {
                                lblArmy13.Text += " H";
                                if ((army_1[i] as ProxyHI).horse == true) lblArmy13.Text += "(5)";
                                if ((army_1[i] as ProxyHI).spear == true) lblArmy13.Text += "(|)";
                                if ((army_1[i] as ProxyHI).helmet == true) lblArmy13.Text += "(^)";
                                if ((army_1[i] as ProxyHI).shield == true) lblArmy13.Text += "([])";
                                break;
                            }
                        case "rpg.LightInfantry":
                            lblArmy13.Text += " L";
                            break;
                        case "rpg.Bowman":
                            lblArmy13.Text += " B";
                            break;
                        case "rpg.Doctor":
                            lblArmy13.Text += " D";
                            break;
                        case "rpg.Wizard":
                            lblArmy13.Text += " W";
                            break;
                        default:
                            lblArmy13.Text += " E";
                            break;
                    }
                    lblArmy13.Text += hp;
                }
            }


            j = 2;
            for (int i = army_2.Count - 1; i >= 0; i--)
            {
                string str = Convert.ToString(army_2[i].GetType());
                string hp = Convert.ToString(army_2[i].Health());

                if (j == 2) j = 0;
                else j++;

                if (j == 0)
                {
                    switch (str)
                    {
                        case "rpg.ProxyHI":
                            {
                                lblArmy2.Text += " H";
                                if ((army_2[i] as ProxyHI).horse == true) lblArmy2.Text += "(5)";
                                if ((army_2[i] as ProxyHI).spear == true) lblArmy2.Text += "(|)";
                                if ((army_2[i] as ProxyHI).helmet == true) lblArmy2.Text += "(^)";
                                if ((army_2[i] as ProxyHI).shield == true) lblArmy2.Text += "([])";
                                break;
                            }
                        case "rpg.LightInfantry":
                            lblArmy2.Text += " L";
                            break;
                        case "rpg.Bowman":
                            lblArmy2.Text += " B";
                            break;
                        case "rpg.Doctor":
                            lblArmy2.Text += " D";
                            break;
                        case "rpg.Wizard":
                            lblArmy2.Text += " W";
                            break;
                        default:
                            lblArmy2.Text += " E";
                            break;
                    }
                    lblArmy2.Text += hp;
                }

                if (j == 1)
                {
                    switch (str)
                    {
                        case "rpg.ProxyHI":
                            {
                                lblArmy22.Text += " H";
                                if ((army_2[i] as ProxyHI).horse == true) lblArmy22.Text += "(5)";
                                if ((army_2[i] as ProxyHI).spear == true) lblArmy22.Text += "(|)";
                                if ((army_2[i] as ProxyHI).helmet == true) lblArmy22.Text += "(^)";
                                if ((army_2[i] as ProxyHI).shield == true) lblArmy22.Text += "([])";
                                break;
                            }
                        case "rpg.LightInfantry":
                            lblArmy22.Text += " L";
                            break;
                        case "rpg.Bowman":
                            lblArmy22.Text += " B";
                            break;
                        case "rpg.Doctor":
                            lblArmy22.Text += " D";
                            break;
                        case "rpg.Wizard":
                            lblArmy22.Text += " W";
                            break;
                        default:
                            lblArmy22.Text += " E";
                            break;
                    }
                    lblArmy22.Text += hp;
                }

                if (j == 2)
                {
                    switch (str)
                    {
                        case "rpg.ProxyHI":
                            {
                                lblArmy23.Text += " H";
                                if ((army_2[i] as ProxyHI).horse == true) lblArmy23.Text += "(5)";
                                if ((army_2[i] as ProxyHI).spear == true) lblArmy23.Text += "(|)";
                                if ((army_2[i] as ProxyHI).helmet == true) lblArmy23.Text += "(^)";
                                if ((army_2[i] as ProxyHI).shield == true) lblArmy23.Text += "([])";
                                break;
                            }
                        case "rpg.LightInfantry":
                            lblArmy23.Text += " L";
                            break;
                        case "rpg.Bowman":
                            lblArmy23.Text += " B";
                            break;
                        case "rpg.Doctor":
                            lblArmy23.Text += " D";
                            break;
                        case "rpg.Wizard":
                            lblArmy23.Text += " W";
                            break;
                        default:
                            lblArmy23.Text += " E";
                            break;
                    }
                    lblArmy23.Text += hp;
                }
            }
        }

        private void DoDuel()
        {
            invoker.SetCommand(new DoDuelCommand(a));
            invoker.DoDuel();
            if (strategy == 1) showArmies1();
            if (strategy == 2) showArmies2();
            if (a.CheckWinArmy() != 0)
            {
                lblInfoExceptions.Text = "Вы не можете сделать ход, так как нет войн в данный момент.\n Для начала новой войны нажмите \"Создать армии\"";
            }
            else
            {
                lblInfo.Text = "Бой прошел успешно.";
            }
        }

        private void MakeMove_Click(object sender, EventArgs e)
        {
            DoDuel();
        }

        private void ToEnd_Click(object sender, EventArgs e)
        {
            while (a.CheckWinArmy() == 0) DoDuel();
        }

        private void checkBoxBeep_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxBeep.Checked == true)
            {
                a.AttachUnits(observer);
            }
            else
            {
                a.DetachUnits(observer);
            }
        }

        public class BeepObserver : IObserver
        {
            public void Update(IUnit ob)
            {
                if (ob.Health() <= 0)
                {
                    playSimpleSound();
                }
            }

            private void playSimpleSound()
            {
                SoundPlayer simpleSound = new SoundPlayer(@"C:\Users\Maksim\Documents\REAPER Media\beepdie.wav");
                simpleSound.Play();
            }
        }

        private void button1x1_Click(object sender, EventArgs e)
        {
            a.SetStrategy(new OneToOne());
            strategy = 1;
            showArmies1();
        }

        private void button3x3_Click(object sender, EventArgs e)
        {
            a.SetStrategy(new ThreeToThree());
            strategy = 2;
            showArmies2();
        }

        private void buttonAxA_Click(object sender, EventArgs e)
        {
            a.SetStrategy(new AllToAll());
            showArmies1();
            strategy = 1;
        }

        private void buttonUndo_Click(object sender, EventArgs e)
        {
            invoker.Undo();
            lblInfo.Text = "Отмена хода.";
            if (strategy == 1) showArmies1();
            if (strategy == 2) showArmies2();
        }

        private void buttonRedo_Click(object sender, EventArgs e)
        {
            invoker.Redo();
            lblInfo.Text = "Отмена отмены :^)";
            if (strategy == 1) showArmies1();
            if (strategy == 2) showArmies2();
        }
    }
}
