using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace EthDiagnosticTool.Global.Helper
{
    public static class PathHelper
    {
        /// <summary>
        /// 获取绝对路径。
        /// 如果给定的路径是绝对路径，则直接返回；如果不是，则构造绝对路径。
        /// </summary>
        /// <param name="path"></param>
        /// <param name="defaultRootPath"></param>
        /// <returns></returns>
        public static string GetRootPath(string path, string defaultRootPath)
        {
            if (Path.IsPathRooted(path))
            {
                return path;
            }
            else
            {
                return Path.Combine(defaultRootPath, path);
            }
        }
    }
}
