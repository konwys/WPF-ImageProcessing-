using Microsoft.VisualStudio.TestTools.UnitTesting;
using WPF.ViewModels;
using System.Drawing;


namespace TestyJednostkowe
{
    [TestClass]
    public class ImgViewModelTests
    {
        [TestMethod]
        public void ToMainColorsTests()
        {
            // Arrange
            var img = new ImgViewModel();
            Bitmap image = new Bitmap(@"zielone.jpg");

            //Act and assert
                Assert.ThrowsException< System.ArgumentException> (()=>img.ToMainColors(image));
        }



    }
}
