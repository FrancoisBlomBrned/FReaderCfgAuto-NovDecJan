using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.IO;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.CodeAnalysis.CSharp;

namespace EthDiagnosticTool.Global
{
    internal class CompileHelper
    {
        private readonly static List<PortableExecutableReference> executableReferences = AppDomain.CurrentDomain.GetAssemblies().Where(p => !p.IsDynamic).Select(x => MetadataReference.CreateFromFile(x.Location)).ToList();  // 当前程序集环境，仅在加载此类时执行 1 次。

        #region 属性

        #endregion


        #region 方法

        /// <summary>
        /// 动态编译生成程序集
        /// </summary>
        /// <param name="code">需要动态编译的代码</param>
        /// <returns>动态生成的程序集</returns>
        public static bool GenerateAssemblyFromCode(string code, out Assembly? assembly, out string message)
        {
            bool r = false;
            assembly = null;
            message = "";

            // 从代码中转换表达式树
            SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(code);

            // 随机程序集名称
            string assemblyName = Path.GetRandomFileName();


            // 创建编译对象
            CSharpCompilation compilation = CSharpCompilation.Create(assemblyName, new[] { syntaxTree }, executableReferences, new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

            using (var ms = new MemoryStream())
            {
                // 将编译好的IL代码放入内存流
                EmitResult result = compilation.Emit(ms);

                // 编译失败，提示
                if (!result.Success)
                {
                    IEnumerable<Diagnostic> failures = result.Diagnostics.Where(diagnostic =>
                                diagnostic.IsWarningAsError ||
                                diagnostic.Severity == DiagnosticSeverity.Error);
                    var errorMsg = new List<string>();
                    foreach (Diagnostic diagnostic in failures)
                    {
                        errorMsg.Add(string.Format("{0}: {1}", diagnostic.Id, diagnostic.GetMessage()));
                    }
                    message = string.Join('\n', errorMsg.ToArray());
                    r = false;
                }
                else
                {
                    // 编译成功，从内存中加载编译好的程序集
                    ms.Seek(0, SeekOrigin.Begin);
                    assembly = Assembly.Load(ms.ToArray());
                    r = true;
                }
            }
            return r;
        }
        #endregion
    }
}
