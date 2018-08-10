using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.Threading;

namespace ImBadAtNames
{
    public partial class Form1 : Form
    {
        CommonOpenFileDialog fileDialog = new CommonOpenFileDialog();

        List<string> listImagesO = new List<string>();
        List<string> listImagesF = new List<string>();
        List<string> listImagesRecycle = new List<string>();
        string currentDirectory;
        string[,] albums = new string[10, 10];
        int tab = 0;
        int FramePos = 0;

        int[] statesArray = new int[20]; //0: Glow | 1: Peak | 2: Fading | 3: Restarting | 4: Stop

    
        
       
    
        Thread btn1ActivatingAnimation;
        Thread btn2ActivatingAnimation;
        Thread btn3ActivatingAnimation;
        Thread btn4ActivatingAnimation;
        Thread btn5ActivatingAnimation;
        Thread btn6ActivatingAnimation;
        Thread btn7ActivatingAnimation;
        Thread btn8ActivatingAnimation;
        Thread btn9ActivatingAnimation;
        Thread btn0ActivatingAnimation;
        Thread btnNumDotActivatingAnimation;
        Thread btnUndoActivatingAnimation;


        public Form1()
        {
            InitializeComponent();

        }

        private string SelectFolder()
        {
            fileDialog.IsFolderPicker = true;
            fileDialog.ShowDialog();
            string folder;

            folder = fileDialog.FileName;
            if (Directory.Exists(folder))
            {
                return folder;
            } 
            else
            {
                return "Ups :c";
            }         
        }

        private void btnAddDir_Click(object sender, EventArgs e)
        {
            currentDirectory = SelectFolder();
            SetDirectory();
            Updates();
        }

        private void GetImagesFromDir()
        {
            foreach (var image in Directory.GetFiles(currentDirectory))
            {
                if (IsImage(image.ToString()))
                {
                    listImagesO.Add(image);
                }
            }

            CopyListAtoB();
        }

        private void CopyListAtoB()
        {
            listImagesF = listImagesO;
        }

        private bool IsImage(string imagen)
        {
            if (imagen.EndsWith("jpg") ||
                imagen.EndsWith("jpeg") ||
                imagen.EndsWith("png"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void SetDirectory()
        {
            listImagesO.Clear();
            listImagesF.Clear();
            listImagesRecycle.Clear();

            try
            {
                if (Directory.Exists(currentDirectory))
                {
                    GetImagesFromDir();
                    LoadFrame();
                    pbFrame.Focus();
                }
            }
            catch (Exception)
            {

            }
        }

        private void tbxDir_TextChanged(object sender, EventArgs e)
        {

        }

        private void LoadFrame()
        {
            try
            {
                pbFrame.ImageLocation = listImagesO[FramePos];
            }
            catch (Exception)
            {

            }

        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private string GetFilename()
        {
            string name;
            name = listImagesO[FramePos];
            name = name.Remove(0, name.LastIndexOf(@"\"));
            return name;
        }

        private void Updates()
        {

            btnNum1.Text = albums[tab, 1].Substring(albums[tab, 1].LastIndexOf("\\") + 1, (albums[tab, 1].Length) - (albums[tab, 1].LastIndexOf("\\") + 1));


        }

        private void MoveImage(int album)
        {
            try
            {
                listImagesF[FramePos] = listImagesO[FramePos];
                File.Move(listImagesO[FramePos], albums[tab, album] + GetFilename());
                FramePos = FramePos + 1;
            }
            catch (Exception)
            {

            }
            LoadFrame();

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Back:

                    if (btnUndoActivatingAnimation.IsAlive)//Animación de Boton
                    {
                        statesArray[11] = 1;
                    }
                    else
                    {
                        btnUndoActivatingAnimation = new Thread(FadingForeThread);
                        statesArray[11] = 0;
                        btnUndoActivatingAnimation.Start(btnUndo);
                    }

                    if (FramePos > 0)
                    {
                        FramePos = FramePos - 1;
                        if (listImagesRecycle.Count > 0)
                        {
                            if (listImagesRecycle[listImagesRecycle.Count - 1] == listImagesO[FramePos])
                            {
                                listImagesRecycle.RemoveAt(listImagesRecycle.Count - 1);
                            }
                            else
                            {

                                listImagesO[FramePos] = listImagesF[FramePos];

                            }
                        }
                        else
                        {
                            listImagesO[FramePos] = listImagesF[FramePos];
                        }

                    }
                    LoadFrame();
                    break;

                case Keys.NumPad0:

                    if (btn0ActivatingAnimation.IsAlive)//Animación de Boton
                    {
                        statesArray[0] = 1;
                    }
                    else
                    {
                        btn0ActivatingAnimation = new Thread(FadingForeThread);
                        statesArray[0] = 0;
                        btn0ActivatingAnimation.Start(btnNum0);
                    }

                    //Borrar
                    break;

                case Keys.NumPad1:
                    if (btn1ActivatingAnimation.IsAlive)//Animación de Boton
                    {
                        statesArray[1] = 1;
                    }
                    else
                    {
                        btn1ActivatingAnimation = new Thread(FadingForeThread);
                        statesArray[1] = 0;
                        btn1ActivatingAnimation.Start(btnNum1);
                    }                 
                    MoveImage(1);
                    break;

                case Keys.NumPad2:
                    if (btn2ActivatingAnimation.IsAlive)//Animación de Boton
                    {
                        statesArray[2] = 1;
                    }
                    else
                    {
                        btn2ActivatingAnimation = new Thread(FadingForeThread);
                        statesArray[2] = 0;
                        btn2ActivatingAnimation.Start(btnNum2);
                    }
                    MoveImage(2);
                    break;

                case Keys.NumPad3:
                    if (btn3ActivatingAnimation.IsAlive)//Animación de Boton
                    {
                        statesArray[3] = 1;
                    }
                    else
                    {
                        btn3ActivatingAnimation = new Thread(FadingForeThread);
                        statesArray[3] = 0;
                        btn3ActivatingAnimation.Start(btnNum3);
                    }
                    MoveImage(3);
                    break;

                case Keys.NumPad4:
                    if (btn4ActivatingAnimation.IsAlive)//Animación de Boton
                    {
                        statesArray[4] = 1;
                    }
                    else
                    {
                        btn4ActivatingAnimation = new Thread(FadingForeThread);
                        statesArray[4] = 0;
                        btn4ActivatingAnimation.Start(btnNum4);
                    }
                    MoveImage(4);
                    break;

                case Keys.NumPad5:
                    if (btn5ActivatingAnimation.IsAlive)//Animación de Boton
                    {
                        statesArray[5] = 1;
                    }
                    else
                    {
                        btn5ActivatingAnimation = new Thread(FadingForeThread);
                        statesArray[5] = 0;
                        btn5ActivatingAnimation.Start(btnNum5);
                    }
                    MoveImage(5);
                    break;

                case Keys.NumPad6:
                    if (btn6ActivatingAnimation.IsAlive)//Animación de Boton
                    {
                        statesArray[6] = 1;
                    }
                    else
                    {
                        btn6ActivatingAnimation = new Thread(FadingForeThread);
                        statesArray[6] = 0;
                        btn6ActivatingAnimation.Start(btnNum6);
                    }
                    MoveImage(6);
                    break;

                case Keys.NumPad7:
                    if (btn7ActivatingAnimation.IsAlive) //Animación de Boton 
                    {
                        statesArray[7] = 1;
                    }
                    else
                    {
                        btn7ActivatingAnimation = new Thread(FadingForeThread);
                        statesArray[7] = 0;
                        btn7ActivatingAnimation.Start(btnNum7);
                    }
                    MoveImage(7);
                    break;

                case Keys.NumPad8:
                    if (btn8ActivatingAnimation.IsAlive) //Animación de Boton
                    {
                        statesArray[8] = 1;
                    }
                    else
                    {
                        btn8ActivatingAnimation = new Thread(FadingForeThread);
                        statesArray[8] = 0;
                        btn8ActivatingAnimation.Start(btnNum8);
                    }
                    MoveImage(8);
                    break;

                case Keys.NumPad9:
                    if (btn9ActivatingAnimation.IsAlive) //Animación de Boton
                    {
                        statesArray[9] = 1;
                    }
                    else
                    {
                        btn9ActivatingAnimation = new Thread(FadingForeThread);
                        statesArray[9] = 0;
                        btn9ActivatingAnimation.Start(btnNum9);
                    }
                    MoveImage(9);
                    break;

                case Keys.Decimal:

                    if (btnNumDotActivatingAnimation.IsAlive) //Animación de Boton
                    {
                        statesArray[10] = 1;
                    }
                    else
                    {
                        btnNumDotActivatingAnimation = new Thread(FadingForeThread);
                        statesArray[10] = 0;
                        btnNumDotActivatingAnimation.Start(btnNumDot);
                    }

                    if (FramePos < listImagesO.Count-1)
                    {
                        listImagesF[FramePos] = listImagesO[FramePos];
                        if (FramePos < listImagesO.Count - 1)
                        {
                            FramePos = FramePos + 1;
                        }
                    }
                    LoadFrame();
                    break;

                default:
                    break;
            }
        }

        private void CreateAlbum(int boton)
        {
            string album = SelectFolder();

            if (album != "Ups :c")
            {
                albums[tab, boton] = album;
            }
        }

        private void btnNum1_Click(object sender, EventArgs e)
        {
            Button btnPressed = (Button)sender;

            switch (btnPressed.Name)
            {
                case "btnNum1":
                    CreateAlbum(1);
                    Updates();
                    break;

                case "btnNum2":
                    CreateAlbum(2);
                    break;

                case "btnNum3":
                    CreateAlbum(3);
                    break;

                case "btnNum4":
                    CreateAlbum(4);
                    break;

                case "btnNum5":
                    CreateAlbum(5);
                    break;
                
                case "btnNum6":
                    CreateAlbum(6);
                    break;

                case "btnNum7":
                    CreateAlbum(7);
                    break;

                case "btnNum8":
                    CreateAlbum(8);
                    break;

                case "btnNum9":
                    CreateAlbum(9);
                    break;

                default:
                    break;
            }
        }

        private void LoadSettings()
        {
            //Aca va a cargarse el archivo
        }

        private void CreateSettings()
        {
            string appdatafolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            appdatafolder = appdatafolder + "\\PurpleSmart";
            Directory.CreateDirectory(appdatafolder);

            if (!File.Exists(appdatafolder + "\\AppSettingsTL.ini"))
            {
                File.Create(appdatafolder + "\\AppSettingsTL.ini");
            }
        }

        private bool SettingsExist()
        {
            string appdatafolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            appdatafolder = appdatafolder + "\\PurpleSmart";
            if (Directory.Exists(appdatafolder))
            {
                if (File.Exists(appdatafolder + "\\AppSettingsTL.ini"))
                {
                    return true;
                }

            }
            return false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            btn1ActivatingAnimation = new Thread(FadingForeThread);
            btn2ActivatingAnimation = new Thread(FadingForeThread);
            btn3ActivatingAnimation = new Thread(FadingForeThread);
            btn4ActivatingAnimation = new Thread(FadingForeThread);
            btn5ActivatingAnimation = new Thread(FadingForeThread);
            btn6ActivatingAnimation = new Thread(FadingForeThread);
            btn7ActivatingAnimation = new Thread(FadingForeThread);
            btn8ActivatingAnimation = new Thread(FadingForeThread);
            btn9ActivatingAnimation = new Thread(FadingForeThread);
            btnUndoActivatingAnimation = new Thread(FadingForeThread);
            btnNumDotActivatingAnimation = new Thread(FadingForeThread);
            btn0ActivatingAnimation = new Thread(FadingForeThread);



            if (SettingsExist())
            {
                LoadSettings();
            }
            else
            {
                CreateSettings();
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void btnTab3_Click(object sender, EventArgs e)
        {

        }

        private void FadingForeThread(object sender)
        {
            int x = 35;
            int state = 0;
            bool flag = true;
            Button boton = (Button)sender;

            switch (boton.Name)
            {
                case "btnNum1":
                    state = 1;
                    break;

                case "btnNum2":
                    state = 2;
                    break;

                case "btnNum3":
                    state = 3;
                    break;

                case "btnNum4":
                    state = 4;
                    break;

                case "btnNum5":
                    state = 5;
                    break;

                case "btnNum6":
                    state = 6;
                    break;

                case "btnNum7":
                    state = 7;
                    break;

                case "btnNum8":
                    state = 8;
                    break;

                case "btnNum9":
                    state = 9;
                    break;

                case "btnNumDot":
                    state = 10;
                    break;

                case "btnUndo":
                    state = 11;
                    break;

                case "btnNum0":
                    state = 0;
                    break;

                default:
                    flag = false;
                    break;
            }

            while (flag)
            {
                switch (statesArray[state])
                {
                    case 0:
                        if (x < 240)
                        {
                            boton.ForeColor = Color.FromArgb(x, 35, 35);
                            x++;
                            Thread.Sleep(1);
                        }
                        else if (x >= 240)
                        {
                            statesArray[state] = 1;
                        }
                        break;



                    case 1:
                        x = 240;
                        statesArray[state] = 2;
                        break;

                    case 2:
                        if (x > 35)
                        {
                            boton.ForeColor = Color.FromArgb(x, 35, 35);
                            x--;
                            Thread.Sleep(1);
                        }
                        else if (x == 35)
                        {
                            flag = false;
                            statesArray[state] = 0;
                        }
                        break;

                    case 3:
                        break;

                    default:
                        break;
                }
            }
        }
    }
}
