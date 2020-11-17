using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Windows;

namespace NI_torpedo.ViewModel.Tests
{
    [TestClass()]
    public class GameWindowTests
    {
        [TestMethod()]
        public void AlTippTest()
        {
            //Arrange
            GameWindow_Al_viewmodel viewmodel = new GameWindow_Al_viewmodel();
            viewmodel.TestMetod(new Vector(5, 5), new List<Vector>() { new Vector(5, 6), new Vector(5, 4), new Vector(4, 5) });
            Vector expected = new Vector(6, 5);

            //Act
            List<int> result = viewmodel.Al_Tipp();

            //Assert
            Assert.AreEqual(expected, new Vector(result[0], result[1]));
        }
    }
}
