using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;

namespace Symmetriskcrypt
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Controller controller = new Controller();
        byte[] keybyte;
        byte[] ivbyte;
        public MainWindow()
        {
            InitializeComponent();
            LoadTools();
        }
        private string selectedtool = "";
        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void KeyTextBox(object sender, TextChangedEventArgs e)
        {

        }

        private void IVTextBox(object sender, TextChangedEventArgs e)
        {

        }

        private void ASCIITextBox(object sender, TextChangedEventArgs e)
        {

        }

        private void HexChiperText(object sender, TextChangedEventArgs e)
        {

        }

        private void HexPlainText(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void GenerateKeyAndIV(object sender, RoutedEventArgs e)//Getsa the right information about what key and iv it should make
        {
            int keyL = 0;
            int iv = 0;
            if (selectedtool == "")
            {
                MessageBox.Show("You need to select a tool first");
            }
            switch (selectedtool)
            {
                case "Aes":
                    keyL = 32;
                    iv = 16;
                    break;
                case "DES":
                    keyL = 8;
                    iv = 8;
                    break;
                case "TripleDES":
                    keyL = 16;
                    iv = 8;
                    break;
                default:
                    break;
            }
            keybyte = controller.GenerateRandom(keyL);
            ivbyte = controller.GenerateRandom(iv);
            Key.Text = Convert.ToBase64String(keybyte);
            IV.Text = Convert.ToBase64String(ivbyte);
        }

        private void EncryptText(object sender, RoutedEventArgs e)//sends information to the encrypter
        {
            Encrypt crypt = new Encrypt();
            if (selectedtool == "")
            {
                MessageBox.Show("You need to select a tool first");
            }
            else if (Key.Text == "" || IV.Text == "")
            {
                MessageBox.Show("You need to generate key and IV");
            }
            byte[] tekst = Encoding.UTF8.GetBytes(ASCIIText.Text);
            byte[] encrypt;
            Hexplain.Text = BitConverter.ToString(tekst);
            Stopwatch sw = new Stopwatch();
            sw.Start();
            switch (selectedtool)
            {
                case "Aes":
                    encrypt = crypt.EncryptAes(tekst, keybyte, ivbyte);
                    ASCII.Text = Convert.ToBase64String(encrypt);
                    HexChiper.Text = BitConverter.ToString(encrypt);
                    break;
                case "DES":
                    encrypt = crypt.EncryptDES(tekst, keybyte, ivbyte);
                    ASCII.Text = Convert.ToBase64String(encrypt);
                    HexChiper.Text = BitConverter.ToString(encrypt);
                    break;
                case "TripleDES":
                    encrypt = crypt.EncryptripleDES(tekst, keybyte, ivbyte);
                    ASCII.Text = Convert.ToBase64String(encrypt);
                    HexChiper.Text = BitConverter.ToString(encrypt);
                    break;
                default:
                    break;

            }
            sw.Stop();
            Encrypttime.Content = sw.ElapsedMilliseconds;
        }

        private void DecryptText(object sender, RoutedEventArgs e)//Sends information to the Decrypter
        {
            Decrypt decrypt = new Decrypt();
            byte[] text = Convert.FromBase64String(ASCII.Text);
            byte[] decrypttext;
            Stopwatch sw = new Stopwatch();
            sw.Start();
            switch (selectedtool)
            {
                case "Aes":
                    decrypttext = decrypt.DecryptAes(text, keybyte, ivbyte);
                    ASCIIText.Text = Encoding.UTF8.GetString(decrypttext);
                    Hexplain.Text = BitConverter.ToString(decrypttext);
                    break;
                case "DES":
                    decrypttext = decrypt.DecryptDES(text, keybyte, ivbyte);
                    ASCIIText.Text = Encoding.UTF8.GetString(decrypttext);
                    Hexplain.Text = BitConverter.ToString(decrypttext);
                    break;
                case "TripleDES":
                    decrypttext = decrypt.DecryptTripleDESC(text, keybyte, ivbyte);
                    ASCIIText.Text = Encoding.UTF8.GetString(decrypttext);
                    Hexplain.Text = BitConverter.ToString(decrypttext);
                    break;
                default:
                    break;
            }
            sw.Stop();
            Decrypttime.Content = sw.ElapsedMilliseconds;
        }

        public void LoadTools()
        {
            string[] tool = { "Aes", "DES", "TripleDES" };
            Toollist.ItemsSource = tool;
        }

        private void ToolList(object sender, SelectionChangedEventArgs e)
        {
            selectedtool = Toollist.SelectedItem.ToString();
        }
    }
}
