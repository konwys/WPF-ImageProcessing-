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
using System.Windows.Shapes;
using System.Drawing;
using System.Drawing.Imaging;
using WPF.ViewModels;
using Microsoft.Win32;
using System.IO;
using biblioteka;


namespace WPF.Views
{
    public partial class ImgView : Window
    {
        ImgViewModel image = new ImgViewModel();
        
        OpenFileDialog openFile = new OpenFileDialog();

        public ImgView()
        {
            InitializeComponent();
        }
        
        private void browse_Click(object sender, RoutedEventArgs e)
        {
            openFile.Filter = "Choose Image(*.jpg;*.png;*.gif)|*.jpg;*.png;*.gif";
            if (openFile.ShowDialog() == true)
            {
                Uri fileUri = new Uri(openFile.FileName);
                imgview.Source = new BitmapImage(fileUri);
    
            }
        }
        
        private void transform_Click(object sender, RoutedEventArgs e)
        {
            Bitmap img = new Bitmap(System.Drawing.Image.FromFile(openFile.FileName));
            image.ToMainColors(img);
            imgview.Source = image.Convert(img);

        }
    }
}
