//logan Ellis
//June 17th
//QR code reader


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
using QRCodeDecoderLibrary;//added
using System.Drawing;//added (needs reference added)
//Use Project Add Reference to add the two above
//Project - Properties - change the Target Framework to 4.6.1
//use a Try-Catch to avoid issues with invalid qr codes

namespace Culminating_2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       
        
        QRDecoder Decoder;
        
        public MainWindow()
        {
            InitializeComponent();
            try
            {

                 Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();
                 ofd.ShowDialog();

                 System.Drawing.Bitmap bi = new System.Drawing.Bitmap(ofd.FileName);
                //System.Drawing.Bitmap bi =  Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();
                Decoder = new QRDecoder();

                // call image decoder methos with <code>Bitmap</code> image of QRCode barcode
                byte[][] DataByteArray = Decoder.ImageDecoder(bi);
                // convert binary result to text string

                MessageBox.Show(DataByteArray.GetLength(0).ToString());// + "\n" + DataByteArray.GetLength(1).ToString());
                string Result = ByteArrayToStr(DataByteArray[0]);

                // string Result = ByteArrayToStr(DataByteArray[Index]);


                MessageBox.Show(Result);
                Clipboard.SetText(Result);
                QRoutput.Content = (Result);
                webbrowser.Navigate(Result);
}
            catch (Exception ex)
            {
                MessageBox.Show("This QR code is incorrect or incomplete\n" + ex.Message);
            }


        }

        // The QRDecoder converts byte array to text string the class using this conversion
        public static string ByteArrayToStr(byte[] DataArray)
        {
            System.Text.Decoder TextDecoder = Encoding.UTF8.GetDecoder();
            int CharCount = TextDecoder.GetCharCount(DataArray, 0, DataArray.Length);
            char[] CharArray = new char[CharCount];
            TextDecoder.GetChars(DataArray, 0, DataArray.Length, CharArray, 0);
            return new string(CharArray);

        }

    }
}