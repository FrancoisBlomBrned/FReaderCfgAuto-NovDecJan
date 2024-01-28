using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EthDiagnosticTool.Global.Helper
{
    using System.IO;
    using System.Threading;

    public static class FileHelper
    {
        /// <summary>
        /// 以文本格式写入文件
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="content"></param>
        /// <param name="encoding"></param>
        public static void WriteToFile(string filepath, string content, Encoding? encoding = null, FileMode? fileMode = null)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            encoding ??= Encoding.UTF8;
            fileMode ??= FileMode.Create;
            var dir = Path.GetDirectoryName(filepath) ?? throw new Exception();
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
            using (var file = new FileStream(filepath, fileMode.Value, FileAccess.Write))
            {
                using (var writer = new StreamWriter(file, encoding))
                {
                    writer.Write(content);
                }
            }
        }

        /// <summary>
        /// 以文本格式读取文件
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string ReadFromFile(string filepath, Encoding? encoding = null, FileMode? fileMode = null)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            encoding ??= Encoding.UTF8;
            fileMode ??= FileMode.OpenOrCreate;
            string content = "";
            var dir = Path.GetDirectoryName(filepath) ?? throw new Exception();
            if (dir != "" && !Directory.Exists(dir)) Directory.CreateDirectory(dir);
            using (var file = new FileStream(filepath, fileMode.Value, FileAccess.Read))
            {
                using (var reader = new StreamReader(file, encoding))
                {
                    content = reader.ReadToEnd();
                }
            }
            return content;
        }

        public static void WriteLinesToFile(string filepath, string[] content, Encoding? encoding = null, FileMode? fileMode = null)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            encoding ??= Encoding.UTF8;
            fileMode ??= FileMode.Create;
            var dir = Path.GetDirectoryName(filepath) ?? throw new Exception();
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
            using (var file = new FileStream(filepath, fileMode.Value, FileAccess.Write))
            {
                using (var writer = new StreamWriter(file, encoding))
                {
                    foreach (var line in content)
                    {
                        writer.WriteLine(line);
                    }
                }
            }
        }

        /// <summary>
        /// 以文本格式按行读取文件
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string[] ReadLinesFromFile(string filepath, Encoding? encoding = null, FileMode? fileMode = null)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            encoding ??= Encoding.UTF8;
            fileMode ??= FileMode.OpenOrCreate;
            List<string> lines = new List<string>();
            var dir = Path.GetDirectoryName(filepath) ?? throw new Exception();
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
            using (var file = new FileStream(filepath, fileMode.Value, FileAccess.Read))
            {
                using (var reader = new StreamReader(file, encoding))
                {
                    while (!reader.EndOfStream)
                    {
                        lines.Add(reader.ReadLine() ?? "");
                    }
                }
            }
            return lines.ToArray();
        }



        /// <summary>
        /// 以 CSV 格式读取文件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filepath"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static T[] ReadFromFile<T>(string filepath, Encoding? encoding = null, FileMode? fileMode = null)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            encoding ??= Encoding.GetEncoding("GBK");
            fileMode ??= FileMode.OpenOrCreate;
            var ts = new List<T>();
            var dir = Path.GetDirectoryName(filepath) ?? throw new Exception();
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
            using (var file = new FileStream(filepath, fileMode.Value, FileAccess.Read))
            {
                using (var reader = new StreamReader(file, encoding))
                {
                    //using (var csv = new CsvReader(reader, true))
                    using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                    {
                        IEnumerable<T> records = csv.GetRecords<T>();
                        ts.AddRange(records);
                    }
                }
            }
            return ts.ToArray();
        }

        /// <summary>
        /// 以 CSV 格式写入文件
        /// </summary>
        /// <param name="ts"></param>
        public static void WriteToFile<T>(string filepath, T[] ts, Encoding? encoding = null, FileMode? fileMode = null)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            encoding ??= Encoding.GetEncoding("GBK");
            fileMode ??= FileMode.Create;
            var dir = Path.GetDirectoryName(filepath) ?? throw new Exception();
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
            using (var file = new FileStream(filepath, fileMode.Value, FileAccess.Write))
            {
                using (var writer = new StreamWriter(file, encoding))
                {
                    using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                    {
                        csv.WriteRecords(ts);
                    }
                }
            }
        }
    }
}
