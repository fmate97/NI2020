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
        public void AlTipp_UpperRightCornerVector_GoodResult()
        {
            //Arrange
            GameWindow_Al_viewmodel viewmodel = new GameWindow_Al_viewmodel();
            viewmodel.TestMetod(new Vector(0, 0), new List<Vector>() { new Vector(0, 1), });
            Vector expected = new Vector(1, 0);

            //Act
            List<int> result = viewmodel.Al_Tipp();

            //Assert
            Assert.AreEqual(expected, new Vector(result[0], result[1]));
        }

        [TestMethod()]
        public void AlTipp_UpperRightCornerVector_WrongResult()
        {
            //Arrange
            GameWindow_Al_viewmodel viewmodel = new GameWindow_Al_viewmodel();
            viewmodel.TestMetod(new Vector(0, 0), new List<Vector>() { new Vector(0, 1), });
            Vector expected = new Vector(-1, 1);

            //Act
            List<int> result = viewmodel.Al_Tipp();

            //Assert
            Assert.AreNotEqual(expected, new Vector(result[0], result[1]));
        }

        [TestMethod()]
        public void AlTipp_UpperLeftCornerVector_GoodResult()
        {
            //Arrange
            GameWindow_Al_viewmodel viewmodel = new GameWindow_Al_viewmodel();
            viewmodel.TestMetod(new Vector(9, 0), new List<Vector>() { new Vector(8, 0), });
            Vector expected = new Vector(9, 1);

            //Act
            List<int> result = viewmodel.Al_Tipp();

            //Assert
            Assert.AreEqual(expected, new Vector(result[0], result[1]));
        }

        [TestMethod()]
        public void AlTipp_UpperLeftCornerVector_WrongResult()
        {
            //Arrange
            GameWindow_Al_viewmodel viewmodel = new GameWindow_Al_viewmodel();
            viewmodel.TestMetod(new Vector(9, 0), new List<Vector>() { new Vector(9, 1), });
            Vector expected = new Vector(10, 1);

            //Act
            List<int> result = viewmodel.Al_Tipp();

            //Assert
            Assert.AreNotEqual(expected, new Vector(result[0], result[1]));
        }

        [TestMethod()]
        public void AlTipp_BottomRightCornerVector_GoodResult()
        {
            //Arrange
            GameWindow_Al_viewmodel viewmodel = new GameWindow_Al_viewmodel();
            viewmodel.TestMetod(new Vector(0, 9), new List<Vector>() { new Vector(0, 8), });
            Vector expected = new Vector(1, 9);

            //Act
            List<int> result = viewmodel.Al_Tipp();

            //Assert
            Assert.AreEqual(expected, new Vector(result[0], result[1]));
        }

        [TestMethod()]
        public void AlTipp_BottomRightCornerVector_WrongResult()
        {
            //Arrange
            GameWindow_Al_viewmodel viewmodel = new GameWindow_Al_viewmodel();
            viewmodel.TestMetod(new Vector(0, 9), new List<Vector>() { new Vector(1, 9), });
            Vector expected = new Vector(1, 9);

            //Act
            List<int> result = viewmodel.Al_Tipp();

            //Assert
            Assert.AreNotEqual(expected, new Vector(result[0], result[1]));
        }

        [TestMethod()]
        public void AlTipp_BottomLeftCornerVector_GoodResult()
        {
            //Arrange
            GameWindow_Al_viewmodel viewmodel = new GameWindow_Al_viewmodel();
            viewmodel.TestMetod(new Vector(9, 9), new List<Vector>() { new Vector(9, 8), });
            Vector expected = new Vector(8, 9);

            //Act
            List<int> result = viewmodel.Al_Tipp();

            //Assert
            Assert.AreEqual(expected, new Vector(result[0], result[1]));
        }

        [TestMethod()]
        public void AlTipp_BottomLeftCornerVector_WrongResult()
        {
            //Arrange
            GameWindow_Al_viewmodel viewmodel = new GameWindow_Al_viewmodel();
            viewmodel.TestMetod(new Vector(9, 9), new List<Vector>() { new Vector(1, 9), });
            Vector expected = new Vector(10, 10);

            //Act
            List<int> result = viewmodel.Al_Tipp();

            //Assert
            Assert.AreNotEqual(expected, new Vector(result[0], result[1]));
        }

        [TestMethod()]
        public void AlTipp_UpperEdgeVector_GoodResult()
        {
            //Arrange
            GameWindow_Al_viewmodel viewmodel = new GameWindow_Al_viewmodel();
            viewmodel.TestMetod(new Vector(5, 0), new List<Vector>() { new Vector(5, 1), new Vector(4, 0) });
            Vector expected = new Vector(6, 0);

            //Act
            List<int> result = viewmodel.Al_Tipp();

            //Assert
            Assert.AreEqual(expected, new Vector(result[0], result[1]));
        }

        [TestMethod()]
        public void AlTipp_BottomEdgeVector_WrongResult()
        {
            //Arrange
            GameWindow_Al_viewmodel viewmodel = new GameWindow_Al_viewmodel();
            viewmodel.TestMetod(new Vector(6, 9), new List<Vector>() { new Vector(7, 9), new Vector(6, 8) });
            Vector expected = new Vector(-1, 1);

            //Act
            List<int> result = viewmodel.Al_Tipp();

            //Assert
            Assert.AreNotEqual(expected, new Vector(result[0], result[1]));
        }

        [TestMethod()]
        public void AlTipp_LeftEdgeVector_GoodResult()
        {
            //Arrange
            GameWindow_Al_viewmodel viewmodel = new GameWindow_Al_viewmodel();
            viewmodel.TestMetod(new Vector(9, 5), new List<Vector>() { new Vector(8, 5), new Vector(9, 6) });
            Vector expected = new Vector(9, 4);

            //Act
            List<int> result = viewmodel.Al_Tipp();

            //Assert
            Assert.AreEqual(expected, new Vector(result[0], result[1]));
        }

        [TestMethod()]
        public void AlTipp_RightEdgeVector_WrongResult()
        {
            //Arrange
            GameWindow_Al_viewmodel viewmodel = new GameWindow_Al_viewmodel();
            viewmodel.TestMetod(new Vector(0, 3), new List<Vector>() { new Vector(1, 3), new Vector(0, 4) });
            Vector expected = new Vector(-1, 1);

            //Act
            List<int> result = viewmodel.Al_Tipp();

            //Assert
            Assert.AreNotEqual(expected, new Vector(result[0], result[1]));
        }


        [TestMethod()]
        public void AlTipp_WithValidVector_NextVector()
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

        [TestMethod()]
        public void AlTipp_WithValidVector_WrongResult()
        {
            //Arrange
            GameWindow_Al_viewmodel viewmodel = new GameWindow_Al_viewmodel();
            viewmodel.TestMetod(new Vector(2, 2), new List<Vector>() { new Vector(5, 6), new Vector(5, 4), new Vector(4, 5) });
            Vector expected = new Vector(6, 5);

            //Act
            List<int> result = viewmodel.Al_Tipp();

            //Assert
            Assert.AreNotEqual(expected, new Vector(result[0], result[1]));
        }

       
    }
}
