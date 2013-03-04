using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using MarkupMapper.Xml;
using MarkupMapper.Xml.CustomExceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MarkupMapper.UnitTests.Xml
{
    [TestClass]
    public class XmlMarkupTests
    {
        #region Xml
        const string Model1Xml = "<Model1 />";
        readonly XElement xModel1 = XElement.Parse(Model1Xml);
        readonly XElement xModel2 = new XElement("Model2", new XAttribute("Id", 1));
        #endregion

        #region Classes
        class Model1
        {

        }

        class Model2
        {
            public int Id { get; set; }
        }

        class ModelWithoutParameterlessConstuctor
        {
            public ModelWithoutParameterlessConstuctor(int i)
            {
            }
        }
        #endregion

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Map_Model1_FromNull_ThrowsArgumentNullException()
        {
            XmlMapper.Map<Model1>(null);
        }

        [TestMethod]
        [ExpectedException(typeof(XmlMappingException))]
        public void Map_Model1_FromWrongRootElementName_ThrowsXmlMappingException()
        {
            MarkupMapper.Xml.XmlMapper.Map<Model1>(new XElement("x"));
        }

        [TestMethod]
        public void Map_Model1FromModel1Xml_GetsCreated()
        {
            Model1 Model1 = XmlMapper.Map<Model1>(xModel1);
            Assert.IsNotNull(Model1);
        }

        [TestMethod]
        public void Map_ClassWithOneProperty_Fromth_XmlWithSameAttribute_GetsMapped()
        {
            Model2 model2 = (Model2)XmlMapper.Map(xModel2, typeof(Model2));

            string idFromXml = xModel2.Attribute("Id").Value;
            Assert.AreEqual(1, model2.Id);

        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Map_ClassWithoutParameterlessConstructor_ThrowsInvalidOperationException()
        {
            XmlMapper.Map<ModelWithoutParameterlessConstuctor>(xModel1);
        }
    }
}
