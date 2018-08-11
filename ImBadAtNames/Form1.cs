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
        Color botonIluminado = Color.FromArgb(66, 66, 76);
        Color botonApagado = Color.FromArgb(55, 55, 64);

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

            try
            {
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
            catch (Exception)
            {
                return "Ups :c";
            }
      
        }

        private void btnAddDir_Click(object sender, EventArgs e)
        {
            currentDirectory = SelectFolder();
            SetDirectory();
            tbxDir.Text = currentDirectory;
        }

        private void GetImagesFromDir()
        {
            foreach (var image in Directory.GetFiles(currentDirectory))
            {
                if (IsImage(image.ToString()))
                {
                    listImagesO.Add(image);
                    listImagesF.Add(image);
                }
            }
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
                pbFrame.ImageLocation = listImagesF[FramePos];
            }
            catch (Exception)
            {

            }

        }

        private string GetFilename()
        {
            string name;
            name = listImagesO[FramePos];
            name = name.Remove(0, name.LastIndexOf(@"\"));
            return name;
        }

        private void Updates(int num)
        {
            try
            {
                string nombre = albums[tab, num].Substring(albums[tab, num].LastIndexOf("\\") + num, (albums[tab, num].Length) - (albums[tab, num].LastIndexOf("\\") + num));

                switch (num)
                {
                    case 0:
                        lblNum0.Text = nombre;
                        break;

                    case 1:
                        lblNum1.Text = nombre;
                        break;

                    case 2:
                        lblNum2.Text = nombre;
                        break;

                    case 3:
                        lblNum3.Text = nombre;
                        break;

                    case 4:
                        lblNum4.Text = nombre;
                        break;

                    case 5:
                        lblNum5.Text = nombre;
                        break;

                    case 6:
                        lblNum6.Text = nombre;
                        break;

                    case 7:
                        lblNum7.Text = nombre;
                        break;

                    case 8:
                        lblNum8.Text = nombre;
                        break;

                    case 9:
                        lblNum9.Text = nombre;
                        break;

                    case 10:
                        lblNumDot.Text = nombre;
                        break;

                    default:
                        break;
                }
            }
            catch (Exception)
            {

            }
            
      
        }

        private void MoveImage(int album)
        {
            try
            {
                string ruta = albums[tab, album] + GetFilename();

                listImagesF[FramePos] = ruta;
                File.Move(listImagesO[FramePos], ruta);
                FramePos = FramePos + 1;
            }
            catch (Exception)
            {

            }
            LoadFrame();

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e) //Teclas
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

                    if (FramePos > 0) //Accíón
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
                            File.Move(listImagesF[FramePos], listImagesO[FramePos]);
                            listImagesF[FramePos] = listImagesO[FramePos];                            
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

        private void AddAblumEvent(object sender, EventArgs e)
        {
            try
            {
                Button btnPressed = (Button)sender;

                switch (btnPressed.Name)
                {
                    case "btnNum1":
                        CreateAlbum(1);
                        Updates(1);
                        break;

                    case "btnNum2":
                        CreateAlbum(2);
                        Updates(2);
                        break;

                    case "btnNum3":
                        CreateAlbum(3);
                        Updates(3);
                        break;

                    case "btnNum4":
                        CreateAlbum(4);
                        Updates(4);
                        break;

                    case "btnNum5":
                        CreateAlbum(5);
                        Updates(5);
                        break;

                    case "btnNum6":
                        CreateAlbum(6);
                        Updates(6);
                        break;

                    case "btnNum7":
                        CreateAlbum(7);
                        Updates(7);
                        break;

                    case "btnNum8":
                        CreateAlbum(8);
                        Updates(8);
                        break;

                    case "btnNum9":
                        CreateAlbum(9);
                        Updates(9);
                        break;

                    default:
                        break;
                }
            }
            catch (Exception)
            {
                Label btnPressed = (Label)sender;

                switch (btnPressed.Name)
                {
                    case "lblNum1":
                        CreateAlbum(1);
                        Updates(1);
                        break;

                    case "lblNum2":
                        CreateAlbum(2);
                        Updates(2);
                        break;

                    case "lblNum3":
                        CreateAlbum(3);
                        Updates(3);
                        break;

                    case "lblNum4":
                        CreateAlbum(4);
                        Updates(4);
                        break;

                    case "lblNum5":
                        CreateAlbum(5);
                        Updates(5);
                        break;

                    case "lblNum6":
                        CreateAlbum(6);
                        Updates(6);
                        break;

                    case "lblNum7":
                        CreateAlbum(7);
                        Updates(7);
                        break;

                    case "lblNum8":
                        CreateAlbum(8);
                        Updates(8);
                        break;

                    case "lblNum9":
                        CreateAlbum(9);
                        Updates(9);
                        break;

                    default:
                        break;
                }
            }                  // Metodo para crear album
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
                        if (x < 255)
                        {
                            boton.ForeColor = Color.FromArgb(x, 35, 35);
                            x = x+2;
                            Thread.Sleep(1);
                        }
                        else if (x >= 255)
                        {
                            statesArray[state] = 1;
                        }
                        break;



                    case 1:
                        x = 255;
                        statesArray[state] = 2;
                        break;

                    case 2:
                        if (x > 35)
                        {
                            boton.ForeColor = Color.FromArgb(x, 35, 35);
                            x = x - 1;
                            Thread.Sleep(7);
                        }
                        else if (x <= 35)
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

       

        private void LabelEnter(object sender, EventArgs e)
        {
            Label objeto = (Label)sender;

            objeto.BackColor = Color.FromArgb(66, 66, 76);

            switch (objeto.Name)
            {
                case "lblNum0":
                    btnNum0.BackColor = botonIluminado;
                    lblTag0.BackColor = botonIluminado;
                    break;

                case "lblTag0":
                    btnNum0.BackColor = botonIluminado;
                    lblNum0.BackColor = botonIluminado;
                    break;

                case "lblNum1":
                    btnNum1.BackColor = botonIluminado;
                    lblTag1.BackColor = botonIluminado;
                    break;

                case "lblTag1":
                    btnNum1.BackColor = botonIluminado;
                    lblNum1.BackColor = botonIluminado;
                    break;

                case "lblNum2":
                    btnNum2.BackColor = botonIluminado;
                    lblTag2.BackColor = botonIluminado;
                    break;

                case "lblTag2":
                    btnNum2.BackColor = botonIluminado;
                    lblNum2.BackColor = botonIluminado;
                    break;

                case "lblNum3":
                    btnNum3.BackColor = botonIluminado;
                    lblTag3.BackColor = botonIluminado;
                    break;

                case "lblTag3":
                    btnNum3.BackColor = botonIluminado;
                    lblNum3.BackColor = botonIluminado;
                    break;

                case "lblNum4":
                    btnNum4.BackColor = botonIluminado;
                    lblTag4.BackColor = botonIluminado;
                    break;

                case "lblTag4":
                    btnNum4.BackColor = botonIluminado;
                    lblNum4.BackColor = botonIluminado;
                    break;

                case "lblNum5":
                    btnNum5.BackColor = botonIluminado;
                    lblTag5.BackColor = botonIluminado;
                    break;

                case "lblTag5":
                    btnNum5.BackColor = botonIluminado;
                    lblNum5.BackColor = botonIluminado;
                    break;

                case "lblNum6":
                    btnNum6.BackColor = botonIluminado;
                    lblTag6.BackColor = botonIluminado;
                    break;

                case "lblTag6":
                    btnNum6.BackColor = botonIluminado;
                    lblNum6.BackColor = botonIluminado;

                    break;

                case "lblNum7":
                    btnNum7.BackColor = botonIluminado;
                    lblTag7.BackColor = botonIluminado;
                    break;

                case "lblTag7":
                    btnNum7.BackColor = botonIluminado;
                    lblNum7.BackColor = botonIluminado;
                    break;

                case "lblNum8":
                    btnNum8.BackColor = botonIluminado;
                    lblTag8.BackColor = botonIluminado;
                    break;

                case "lblTag8":
                    btnNum8.BackColor = botonIluminado;
                    lblNum8.BackColor = botonIluminado;
                    break;

                case "lblNum9":
                    btnNum9.BackColor = botonIluminado;
                    lblTag9.BackColor = botonIluminado;
                    break;

                case "lblTag9":
                    btnNum9.BackColor = botonIluminado;
                    lblNum9.BackColor = botonIluminado;
                    break;

                case "lblNumDot":
                    btnNumDot.BackColor = botonIluminado;
                    lblTagDot.BackColor = botonIluminado;
                    break;

                case "lblTagDot":
                    btnNumDot.BackColor = botonIluminado;
                    lblNumDot.BackColor = botonIluminado;
                    break;

                case "lblUndo":
                    btnUndo.BackColor = botonIluminado;
                    lblTagUndo.BackColor = botonIluminado;
                    break;

                case "lblTagUndo":
                    btnUndo.BackColor = botonIluminado;
                    lblUndo.BackColor = botonIluminado;
                    break;

                default:
                    break;
            }
        }

        private void LabelLeave(object sender, EventArgs e)
        {
            Label objeto = (Label)sender;

            objeto.BackColor = Color.FromArgb(55, 55, 64);

            switch (objeto.Name)
            {
                case "lblNum0":
                    btnNum0.BackColor = botonApagado;
                    lblTag0.BackColor = botonApagado;
                    break;

                case "lblTag0":
                    btnNum0.BackColor = botonApagado;
                    lblNum0.BackColor = botonApagado;
                    break;

                case "lblNum1":
                    btnNum1.BackColor = botonApagado;
                    lblTag1.BackColor = botonApagado;
                    break;

                case "lblTag1":
                    btnNum1.BackColor = botonApagado;
                    lblNum1.BackColor = botonApagado;
                    break;

                case "lblNum2":
                    btnNum2.BackColor = botonApagado;
                    lblTag2.BackColor = botonApagado;
                    break;

                case "lblTag2":
                    btnNum2.BackColor = botonApagado;
                    lblNum2.BackColor = botonApagado;
                    break;

                case "lblNum3":
                    btnNum3.BackColor = botonApagado;
                    lblTag3.BackColor = botonApagado;
                    break;

                case "lblTag3":
                    btnNum3.BackColor = botonApagado;
                    lblNum3.BackColor = botonApagado;
                    break;

                case "lblNum4":
                    btnNum4.BackColor = botonApagado;
                    lblTag4.BackColor = botonApagado;
                    break;

                case "lblTag4":
                    btnNum4.BackColor = botonApagado;
                    lblNum4.BackColor = botonApagado;
                    break;

                case "lblNum5":
                    btnNum5.BackColor = botonApagado;
                    lblTag5.BackColor = botonApagado;
                    break;

                case "lblTag5":
                    btnNum5.BackColor = botonApagado;
                    lblNum5.BackColor = botonApagado;
                    break;

                case "lblNum6":
                    btnNum6.BackColor = botonApagado;
                    lblTag6.BackColor = botonApagado;
                    break;

                case "lblTag6":
                    btnNum6.BackColor = botonApagado;
                    lblNum6.BackColor = botonApagado;

                    break;

                case "lblNum7":
                    btnNum7.BackColor = botonApagado;
                    lblTag7.BackColor = botonApagado;
                    break;

                case "lblTag7":
                    btnNum7.BackColor = botonApagado;
                    lblNum7.BackColor = botonApagado;
                    break;

                case "lblNum8":
                    btnNum8.BackColor = botonApagado;
                    lblTag8.BackColor = botonApagado;
                    break;

                case "lblTag8":
                    btnNum8.BackColor = botonApagado;
                    lblNum8.BackColor = botonApagado;
                    break;

                case "lblNum9":
                    btnNum9.BackColor = botonApagado;
                    lblTag9.BackColor = botonApagado;
                    break;

                case "lblTag9":
                    btnNum9.BackColor = botonApagado;
                    lblNum9.BackColor = botonApagado;
                    break;

                case "lblNumDot":
                    btnNumDot.BackColor = botonApagado;
                    lblTagDot.BackColor = botonApagado;
                    break;

                case "lblTagDot":
                    btnNumDot.BackColor = botonApagado;
                    lblNumDot.BackColor = botonApagado;
                    break;

                case "lblUndo":
                    btnUndo.BackColor = botonApagado;
                    lblTagUndo.BackColor = botonApagado;
                    break;

                case "lblTagUndo":
                    btnUndo.BackColor = botonApagado;
                    lblUndo.BackColor = botonApagado;
                    break;

                default:
                    break;
            }
        }

        private void ButtonLeave(object sender, EventArgs e)
        {
            Button boton = (Button)sender;

            switch (boton.Name)
            {
                case "btnNum0":
                    lblNum0.BackColor = botonApagado;
                    lblTag0.BackColor = botonApagado;
                    break;

                case "btnNum1":
                    lblNum1.BackColor = botonApagado;
                    lblTag1.BackColor = botonApagado;
                    break;

                case "btnNum2":
                    lblNum2.BackColor = botonApagado;
                    lblTag2.BackColor = botonApagado;
                    break;

                case "btnNum3":
                    lblNum3.BackColor = botonApagado;
                    lblTag3.BackColor = botonApagado;
                    break;

                case "btnNum4":
                    lblNum4.BackColor = botonApagado;
                    lblTag4.BackColor = botonApagado;
                    break;

                case "btnNum5":
                    lblNum5.BackColor = botonApagado;
                    lblTag5.BackColor = botonApagado;
                    break;

                case "btnNum6":
                    lblNum6.BackColor = botonApagado;
                    lblTag6.BackColor = botonApagado;
                    break;

                case "btnNum7":
                    lblNum7.BackColor = botonApagado;
                    lblTag7.BackColor = botonApagado;
                    break;

                case "btnNum8":
                    lblNum8.BackColor = botonApagado;
                    lblTag8.BackColor = botonApagado;
                    break;

                case "btnNum9":
                    lblNum9.BackColor = botonApagado;
                    lblTag9.BackColor = botonApagado;
                    break;

                case "btnNumDot":
                    lblNumDot.BackColor = botonApagado;
                    lblTagDot.BackColor = botonApagado;
                    break;

                case "btnUndo":
                    lblUndo.BackColor = botonApagado;
                    lblTagUndo.BackColor = botonApagado;
                    break;

                default:
                    break;
            }
        }

        private void ButtonEnter(object sender, EventArgs e)
        {
            Button boton = (Button)sender;

            switch (boton.Name)
            {
                case "btnNum0":
                    lblNum0.BackColor = botonIluminado;
                    lblTag0.BackColor = botonIluminado;
                    break;

                case "btnNum1":
                    lblNum1.BackColor = botonIluminado;
                    lblTag1.BackColor = botonIluminado;
                    break;

                case "btnNum2":
                    lblNum2.BackColor = botonIluminado;
                    lblTag2.BackColor = botonIluminado;
                    break;

                case "btnNum3":
                    lblNum3.BackColor = botonIluminado;
                    lblTag3.BackColor = botonIluminado;
                    break;

                case "btnNum4":
                    lblNum4.BackColor = botonIluminado;
                    lblTag4.BackColor = botonIluminado;
                    break;

                case "btnNum5":
                    lblNum5.BackColor = botonIluminado;
                    lblTag5.BackColor = botonIluminado;
                    break;

                case "btnNum6":
                    lblNum6.BackColor = botonIluminado;
                    lblTag6.BackColor = botonIluminado;
                    break;

                case "btnNum7":
                    lblNum7.BackColor = botonIluminado;
                    lblTag7.BackColor = botonIluminado;
                    break;

                case "btnNum8":
                    lblNum8.BackColor = botonIluminado;
                    lblTag8.BackColor = botonIluminado;
                    break;

                case "btnNum9":
                    lblNum9.BackColor = botonIluminado;
                    lblTag9.BackColor = botonIluminado;
                    break;

                case "btnNumDot":
                    lblNumDot.BackColor = botonIluminado;
                    lblTagDot.BackColor = botonIluminado;
                    break;

                case "btnUndo":
                    lblUndo.BackColor = botonIluminado;
                    lblTagUndo.BackColor = botonIluminado;
                    break;

                default:
                    break;
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }


        //private void FrameAnimation(object sender)
        //{
        //    CheckForIllegalCrossThreadCalls = false;


        //    Point location = pb.Location;
        //    pb.Location = pbFrame.Location;
        //    int y = location.Y;
        //    int z = 600;


        //    for (int x = pb.Location.X; x < location.X; x = x+20)
        //    {
        //        z = z - 20;
        //        y = y + 5;
        //        pb.Location = new Point(x, y);
        //        pb.Size = new Size(z, z);
        //        Thread.Sleep(1);
        //    }
        //    pb.Visible = false;
        //}

        //private void button1_Click_1(object sender, EventArgs e)
        //{
        //    Button boton = (Button)sender;

        //    pb.Visible = false;
        //    pb.Location = boton.Location;
        //    pb.Size = new Size(600, 600);
        //    pb.Image = Image.FromFile(listImagesF[FramePos - 1]);
        //    pb.SizeMode = PictureBoxSizeMode.Zoom;

        //    pb.BringToFront();

        //    Thread pbAnimado = new Thread(FrameAnimation);
        //    pbAnimado.Start(pb);
        //}
    }
}
