using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF.ViewModels;
using System.Drawing;
using System.Windows;
using System.Windows.Media.Imaging;
using Xunit;

namespace Xunit.Tests
{
    public class ImgViewModelTests
    {
        [Fact]
        public void ToMainColorsTests()
        {
            // Arrange
            var img = new ImgViewModel();
            Bitmap image = new Bitmap(@"zielone.jpg");
            const string WrongFormatMessage = "Niepoprawny format";

            // Act
            try
            {
                img.ToMainColors(image);
            }
            catch(BadImageFormatException e)
            {   // Assert
                Assert.Contains(e.Message, WrongFormatMessage);
            }
            
        }

        [Fact]
        public void ConvertTests()
        {
            // Arrange
            var img = new ImgViewModel();
            Bitmap image = new Bitmap(@"zielone.jpg");
            var expected = typeof(BitmapImage);

            // Act
            var actual = img.Convert(image);

            //Assert
            Assert.IsType(expected, actual);
        }
           
    }
}
