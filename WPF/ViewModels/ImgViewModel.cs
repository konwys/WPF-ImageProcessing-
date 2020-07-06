using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;   
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Drawing;
using System.Drawing.Imaging;
using Microsoft.Win32;
using System.Windows.Input;
using System.ComponentModel;

namespace WPF.ViewModels
{
    public class ImgViewModel : INotifyPropertyChanged 
    {
        OpenFileDialog openFile = new OpenFileDialog();
        public void ToMainColors(Bitmap MyImage)
        {
            for (int i = 0; i < MyImage.Width; i++)
            {
                for (int k = 0; k < MyImage.Height; k++)
                {
                    Color pixelColor = MyImage.GetPixel(i, k);

                    if (pixelColor.R > pixelColor.G && pixelColor.R > pixelColor.B) // najwiekszy R
                    {
                        Color newColor = Color.FromArgb(pixelColor.R, 255, 0, 0);
                        MyImage.SetPixel(i, k, newColor);
                    }
                    else if (pixelColor.G > pixelColor.R && pixelColor.G > pixelColor.B) // najwiekszy G
                    {
                        Color newColor = Color.FromArgb(pixelColor.R, 0, 255, 0);
                        MyImage.SetPixel(i, k, newColor);
                    }
                    else if (pixelColor.B > pixelColor.G && pixelColor.B > pixelColor.R) // najwiekszy B
                    {
                        Color newColor = Color.FromArgb(pixelColor.R, 0, 0, 255);
                        MyImage.SetPixel(i, k, newColor);
                    }
                }
            }

            Graphics g = Graphics.FromImage(MyImage);
            g.DrawImage(MyImage, 0, 0);
            
        }
        
        public BitmapImage Convert(Bitmap src)
        {
            MemoryStream ms = new MemoryStream();
            ((System.Drawing.Bitmap)src).Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            ms.Seek(0, SeekOrigin.Begin);
            image.StreamSource = ms;
            image.EndInit();
            return image;
        }
       
        private string imagePath;
        public string ImagePath
        {
            get
            { return imagePath; }
            set
            {
                imagePath = value;
                SetPropertyChanged("ImagePath");            
            }
        }

        ICommand _loadImageCommand;

        public ICommand LoadImageCommand
        {
            get
            {
                if (_loadImageCommand == null)
                {
                    _loadImageCommand = new RelayCommand(param => LoadImage());
                }
                return _loadImageCommand;
            }
        }

        private void LoadImage()
        {
            OpenFileDialog open = new OpenFileDialog();
            open.DefaultExt = (".png");
            open.Filter = "Pictures (*.jpg;*.gif;*.png)|*.jpg;*.gif;*.png";

            if (open.ShowDialog() == true)
                ImagePath = open.FileName;
                             
        }
        
        #region Property Changed Event Handler
        protected void SetPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion Property Changed Event Handler


    }
  
    // IMPLEMENTACJA RELAYCOMMAND
    public class RelayCommand : ICommand
    {
        #region Fields

        readonly Action<object> _execute;
        readonly Predicate<object> _canExecute;

        #endregion // Fields

        #region Constructors

        public RelayCommand(Action<object> execute)
            : this(execute, null)
        {
        }

      
        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");

            _execute = execute;
            _canExecute = canExecute;
        }
        #endregion // Constructors

        #region ICommand Members

   
        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }

        #endregion // ICommand Members
    }


}
