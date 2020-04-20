using Microsoft.VisualStudio.TestTools.UnitTesting;
using Vectors;

namespace VectorsTest
{
    [TestClass]
    public class VectorsTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            var vector1 = new Vector(new double[] { 1, 1, 1, 1 });
            var vector2 = new Vector(new double[] { 10, 10 });

            vector1.AddVector(vector2);

            Assert.AreEqual(new Vector(new double[] { 11, 11, 1, 1 }), vector1);
        }

        [TestMethod]
        public void TestMethod2()
        {
            var vector1 = new Vector(new double[] { 1, 1, 1, 1 });
            var vector2 = new Vector(new double[] { 10, 10 });

            vector1.AddVector(vector2);

            Assert.AreEqual(new Vector(new double[] { 11, 11 }), vector1);
        }

        [TestMethod]
        public void TestMethod3()
        {
            var vector1 = new Vector(new double[] { 1, 1, 1, 1 });
            var vector2 = new Vector(new double[] { 10, 10 });

            Assert.AreEqual(2, Vector.MultiplyVectors(vector1, vector2));
        }

        [TestMethod]
        public void TestMethod4()
        {
            var vector1 = new Vector(new double[] { 1, 1, 1, 1 });
            var vector2 = new Vector(new double[] { 5, 5, 5, 5 });

            Assert.AreEqual(20, Vector.MultiplyVectors(vector1, vector2));
        }
    }
}