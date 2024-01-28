using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;

namespace EthDiagnosticTool.Global.Helper
{
    public static class IniHelper
    {

        public struct IniFile
        {
            /// <summary>
            /// 配置文件的绝对路径
            /// </summary>
            public readonly string filepath;

            /// <summary>
            /// 配置文件的文件夹的绝对路径
            /// </summary>
            public string DirectoryPath
            {
                get
                {
                    var d = Path.GetDirectoryName(filepath);
                    return d == null ? throw new Exception() : d;
                }
            }

            /// <summary>
            /// 配置文件内容
            /// </summary>
            public Dictionary<string, Dictionary<string, string>> content;

            public IniFile(string filepath)
            {
                if (Path.IsPathRooted(filepath))
                {
                    this.filepath = filepath;
                }
                else
                {
                    throw new ArgumentException();
                }

                var content = ReadContent(filepath);
                this.content = content;
            }

            /// <summary>
            /// 读取配置文件内容
            /// </summary>
            /// <param name="filepath"></param>
            /// <returns></returns>
            public static Dictionary<string, Dictionary<string, string>> ReadContent(string filepath)
            {
                var lines = FileHelper.ReadLinesFromFile(filepath);
                var content = new Dictionary<string, Dictionary<string, string>>();
                string lastSection = "";
                foreach (var line in lines)
                {
                    var m = Regex.Match(line, @"\[(\w+)\]");
                    if (m.Success)
                    {
                        lastSection = m.Groups[1].Value;
                        continue;
                    }

                    if (!content.ContainsKey(lastSection))
                    {
                        content[lastSection] = new Dictionary<string, string>();
                    }

                    m = Regex.Match(line, @"([^=]+)=([^=]+)");
                    if (m.Success)
                    {
                        var key = m.Groups[1].Value.Trim();
                        var value = m.Groups[2].Value.Trim();
                        content[lastSection][key] = value;
                    }
                }

                return content;
            }

            /// <summary>
            /// 把配置文件内容写入到文件
            /// </summary>
            /// <param name="filepath"></param>
            /// <param name="content"></param>
            public static void WriteContent(string filepath, Dictionary<string, Dictionary<string, string>> content)
            {
                List<string> lines = new List<string>();
                foreach (var section in content)
                {
                    lines.Add($"[{section.Key}]");
                    foreach (var item in section.Value)
                    {
                        lines.Add($"{item.Key} = {item.Value}");
                    }
                }
                FileHelper.WriteLinesToFile(filepath, lines.ToArray());
            }

            public void WriteContent()
            {
                WriteContent(filepath, content);
            }
        }
    }
}
