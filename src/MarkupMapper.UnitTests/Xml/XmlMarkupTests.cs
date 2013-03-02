using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Should;
using System.Xml.Linq;
using MarkupMapper.Xml;
using MarkupMapper.Xml.CustomExceptions;

namespace MarkupMapper.UnitTests.Xml
{
    [TestFixture]
    public class XmlMarkupTests
    {
        const string Model1Xml = "<Model1 />";
        readonly XElement xModel1 = XElement.Parse(Model1Xml);

        class Model1
        {
            
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Map_Model1_FromNull_ThrowsArgumentNullException()
        {
            XmlMapper.Map<Model1>(null);
        }

        [Test]
        [ExpectedException(typeof(XmlMappingException))]
        public void Map_Model1_FromWrongRootElementName_ThrowsMappingException()
        {
            MarkupMapper.Xml.XmlMapper.Map<Model1>(new XElement("x"));
        }




        [Test]
        [Ignore]
        public void Map_Model1FromXml_GetsCreated()
        {
            Model1 Model1 = XmlMapper.Map<Model1>(xModel1);
        }
    }
}
