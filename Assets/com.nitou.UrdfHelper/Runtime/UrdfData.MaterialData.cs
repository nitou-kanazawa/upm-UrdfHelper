using System.Xml.Linq;
using UnityEngine;

// [REF]
//  ROS Doc: Create your own urdf file https://wiki.ros.org/urdf/Tutorials/Create%20your%20own%20urdf%20file

namespace nitou.UrdfHelper {
    public partial class UrdfData {

        public class MaterialData {

            public string Name;
            public Color Color;
            public string Texture;

            public MaterialData(string name) {
                Name = name;
                Color = Color.white;
                Texture = string.Empty;
            }


            /// ----------------------------------------------------------------------------
            #region Static Method (�f�[�^�ϊ�)

            /// <summary>
            /// ���ʗp�̕�����D
            /// </summary>
            public static readonly string NAME_KEY = "material";

            /// <summary>
            /// �V���A���C�Y�p�N���X����XML�v�f�ɕϊ�����D
            /// </summary>
            public static XElement ToXElement(MaterialData material) {
                var xMaterial = new XElement(NAME_KEY, 
                    new XAttribute("name", material.Name),
                    material.Color.ToXElement(),
                    new XElement("texture", material.Texture)
                );
                return xMaterial;
            }

            /// <summary>
            /// XML�v�f����V���A���C�Y�p�N���X�ɕϊ�����D
            /// </summary>
            public static MaterialData FromXElement(XElement xMaterial) {

                var name = xMaterial.Attribute("name")?.Value ?? "Unnamed";

                var xColor = xMaterial.Element("color");
                var color = xColor.ReadColor(Color.white);

                var texture = xMaterial.Element("texture")?.Value ?? string.Empty;

                return new MaterialData(name) {
                    Color = color,
                    Texture = texture
                };
            }
            #endregion

        }
    }


    public static class XmlUtils {

        /// <summary>
        /// XML�v�f����Color��ǂݎ��.
        /// </summary>
        /// <param name="xElement">�F��������XML�v�f</param>
        /// <param name="defaultColor">�w�肳�ꂽ�������Ȃ��ꍇ�̃f�t�H���g�l</param>
        /// <returns>XML�v�f����ǂݎ����Color</returns>
        public static Color ReadColor(this XElement xElement, Color defaultColor) {
            if (xElement == null)
                return defaultColor;

            float r = float.Parse(xElement.Attribute("r")?.Value ?? defaultColor.r.ToString());
            float g = float.Parse(xElement.Attribute("g")?.Value ?? defaultColor.g.ToString());
            float b = float.Parse(xElement.Attribute("b")?.Value ?? defaultColor.b.ToString());
            float a = float.Parse(xElement.Attribute("a")?.Value ?? defaultColor.a.ToString());

            return new Color(r, g, b, a);
        }

        /// <summary>
        /// Color��XML�v�f�Ƃ��ďo�͂���
        /// </summary>
        /// <param name="color">Color�f�[�^</param>
        /// <returns>Color�����܂�XElement</returns>
        public static XElement ToXElement(this Color color) {
            return new XElement("color",
                new XAttribute("r", color.r),
                new XAttribute("g", color.g),
                new XAttribute("b", color.b),
                new XAttribute("a", color.a)
            );
        }
    }



}
