using Microsoft.VisualStudio.TestTools.UnitTesting;
using NI_torpedo;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Text;

namespace NI_torpedo.Tests
{
    [TestClass()]
    public class GameWindowTests
    {
        [TestMethod()]
        public void al_tippTest()
        {
            //Arrange
            GameWindow gameWindow = new GameWindow(false, "Valaki");
            Vector vektor = new Vector(2, 5);
            Vector actual;
            List<Vector> expected = new List<Vector>();

            expected.Add(new Vector(2, 6));
            expected.Add(new Vector(2, 4));
            expected.Add(new Vector(3, 5));
            expected.Add(new Vector(1, 5));

            //Act
            gameWindow.al_tipp(true, vektor, out actual);

            //Assert
            
            Assert.IsTrue(expected.Contains(actual), " hIBA!");
            Console.WriteLine(actual);
        }
    }
}

       