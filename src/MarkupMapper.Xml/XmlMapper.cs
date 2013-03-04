using MarkupMapper.Xml.CustomExceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkupMapper.Xml
{
    public static class XmlMapper
    {
        public static T1 Map<T1>(System.Xml.Linq.XElement xElementToMapFrom)
        {
            var mapObj = Map(xElementToMapFrom, typeof(T1));
            if (mapObj == null) return default(T1);
            return (T1)mapObj;
        }

        public static object Map(System.Xml.Linq.XElement xSource, Type sourceType)
        {
            if (xSource == null) throw new ArgumentNullException("xSource");

            ValidateSourceTypeHasParameterlessConstructor(sourceType);

            var xmlRootName = xSource.Name.LocalName;

            if (!sourceType.IsGenericType)
            {
                string typeName = sourceType.Name;

                if (typeName != xmlRootName)
                {
                    throw new XmlMappingException(string.Format("xml root name='{0}' does not match Generic Type Name='{1}'", xmlRootName, typeName));
                }
                var instance = Activator.CreateInstance(sourceType);

                // Metod MapProperties()
                foreach (var xProperty in xSource.Attributes())
                {
                    var clrProperty = sourceType.GetProperty(xProperty.Name.ToString());
                    if (clrProperty != null && clrProperty.CanWrite)
                    {
                        var propertyType = clrProperty.PropertyType;
                        TypeConverter converter = TypeDescriptor.GetConverter(propertyType);
                        clrProperty.SetValue(instance, converter.ConvertFromString(xProperty.Value));
                    }

                }

                return instance;

            }
            return null;
        }

        private static void ValidateSourceTypeHasParameterlessConstructor(Type sourceType)
        {
            if (sourceType.GetConstructor(Type.EmptyTypes) == null)
            {
                throw new InvalidOperationException("sourceType must have parameterless constructor");
            }
        }
    }
}
