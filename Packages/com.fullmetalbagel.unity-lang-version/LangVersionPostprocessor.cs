#nullable enable

using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using UnityEditor;
using UnityEngine.Device;

namespace UnityLangVersion.Editor
{
    public class LangVersionPostprocessor : AssetPostprocessor
    {
        public static string OnGeneratedCSProject(string path, string content)
        {
            var project = XDocument.Parse(content);
            var xmlns = project.Root!.Attribute("xmlns");
            var @namespace = xmlns == null ? "" : $"{{{xmlns.Value}}}";
            var noneName = $"{@namespace}None";
            var cscFilePath = project.Descendants(noneName)
                .Select(element => element.Attribute("Include")?.Value)
                .SingleOrDefault(file => file != null && file.EndsWith("csc.rsp"))
            ;
            if (cscFilePath == null) return content;

            var csc = File.ReadAllLines(Path.Combine(Application.dataPath!, "..", cscFilePath));
            var isVersion10 = csc.Any(line =>
                line.Trim().Equals("-langVersion:10", StringComparison.OrdinalIgnoreCase));
            var isVersionPreview = csc.Any(line =>
                line.Trim().Equals("-langVersion:preview", StringComparison.OrdinalIgnoreCase));
            if (!isVersion10 && !isVersionPreview) return content;

            var langVersionName = $"{@namespace}LangVersion";
            var langVersionElement = project.Descendants(langVersionName).SingleOrDefault();
            if (langVersionElement == null)
            {
                var propertyGroupName = $"{@namespace}PropertyGroup";
                var propertyGroup = project.Descendants(propertyGroupName).First();
                langVersionElement = new XElement(langVersionName, "9.0");
                propertyGroup.Add(langVersionElement);
            }

            if (isVersion10) langVersionElement.Value = "10.0";
            else if (isVersionPreview) langVersionElement.Value = "11.0";
            return project.Declaration + Environment.NewLine + project;
        }
    }
}