using System.Security.Principal;
using System.Globalization;
using System.Runtime.Intrinsics.X86;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Linq.Expressions;
using System.IO;
using System.Data;
using System.Collections.Generic;
//using Microsoft.Office.Interop.Excel;
//using APac
using NPOI.XSSF.UserModel; 
using NPOI.HSSF.UserModel; 
using NPOI.SS.UserModel;
using NPOI.SS.Formula.Functions;
using NPOI.OpenXmlFormats.Spreadsheet;
//using NPOI.SS.Util;

namespace LibFromExcelToDT;

public static class TestExcelTableSheet
{

    public static readonly string start = "imported buttons!";



    public static Dictionary<string, DataTable?>? GetXlsSheetName (string xlsFileName, string xlsSheetName, out string msg, out bool status)
    {
        msg = "";
        status = false;

        try
        {
            ///var tmpInputFile = @"C:\Users\Public\Documents\xlsxtt1.csv";
            /// 
            /// ifFirstLineDeleted: true 表示第一行当作表头来处理， 从第二行开始及读取数据返回到 DataTable
            /// 
            DataTable? someDataTable = null;

            someDataTable = transferXlsToDTable(xlsFileName, ifFirstLineDeleted: false, out msg, out status, matchedSheetName: xlsSheetName);

            status = true;
            return new Dictionary<string, DataTable?> { {"nullDefDatatableIdentifier", someDataTable} };
        }
        catch(Exception c)
        {
            //MessageBox.Show(c.ToString());
            msg = c.ToString();
            return null; //new Dictionary<string, DataTable?> { {"MySqlTableSxtt1", null} };
        }
    
    //  return "TestExcelTableSheet.GetXlsSheetName";

    }


    private static DataTable? transferXlsToDTable(string xlsFileNameFullPath, bool ifFirstLineDeleted,
        out string msg, out bool status, string matchedSheetName = "sheet1")
    {
        msg = "in fuction static DataTable? transferXlsToDTable(string xlsFileNameFullPath, bool ifFirstLineDeleted, out string msg, out bool status, string matchedSheetName = \"sheet1\")";
        status = false;
        msg += "\n";
        
        // 默认： Worksheet 第一行 逐列填充表头
        int startDataFillRow = 1;
        // 从第二行开始， 逐行填充数据到 DataTable
        DataTable? retDataTable = null;
        IWorkbook? wb = null;

        // 记录表头列起始值、终值
        int columnNumStarts = 0;
        int columnNumEnds = 0;

        ISheet ws;
        DataColumn? tempColumn = null;
        IRow? sourceRow = null;

        try
        {
            FileStream fStream = File.OpenRead(xlsFileNameFullPath);
            
            if(0 < xlsFileNameFullPath.IndexOf(".xlsx") || xlsFileNameFullPath.IndexOf(".csv") > 0
               || 0 < xlsFileNameFullPath.IndexOf(".xls"))
            {
                // 可以识别 Xlsx Csv Xls
                wb = WorkbookFactory.Create(fStream);
               // wb = WorkbookFactory;
               if(null != wb)
               {
                    for(var sheetIdx = 0; sheetIdx < wb.NumberOfSheets; sheetIdx++)
                    {
                        ws = wb.GetSheetAt(sheetIdx);
                        if(null == ws) continue;

                        //if(matchedSheetName.ToUpper() != ws.SheetName) continue;
                        if (matchedSheetName != ws.SheetName && ws.SheetName.ToUpper() == matchedSheetName.ToUpper())
                        {
                            MessageBox.Show("Warning - 已忽略指定 Sheet 名称中的大小写字符不匹配！");
                        }
                        if(ws.SheetName.ToUpper() != matchedSheetName.ToUpper()) continue;
                        
                        retDataTable = new DataTable();
                        // sheet 匹配成功 （忽略大小写）
                        int dataRowCont = ws.LastRowNum;
                        if(0 < dataRowCont)
                        {
                            IRow firstRow = ws.GetRow(0);
                                columnNumStarts = firstRow.FirstCellNum;
                                columnNumEnds = firstRow.LastCellNum;
                            // 以下， 根据行数和列数对 retDataTable 进行 行列依次写入
                            if(!ifFirstLineDeleted)
                            {
                                /// var totalColumnCounts = firstRow.LastCellNum + 1 - firstRow.FirstCellNum;

                                startDataFillRow = 0;  // TODO:   FIX:   缺少表头时的 弹窗错误提示 “输入的 Excel【名】 Sheet【名】， 不合规范， 缺少首行表头”
                                // 如果第一行内容属于列名称 （默认它就是列名称， 所以都只从第二行开写入）
                                // 第一行的列名称 ； 挨个加到 DataTable.ColumnNameList 之中
                                // TODO:   FIX:
                            }
                            if(true)
                            {
                                /// var totalColumnCounts = firstRow.LastCellNum + 1 - firstRow.FirstCellNum;
                                /// 
                                for(int k = columnNumStarts; k < columnNumEnds; k++)    //  <= ??
                                {
                                    var temp = firstRow.GetCell(k).StringCellValue;
                                    if(null != temp)
                                    {
                                        retDataTable.Columns.Add(tempColumn = new DataColumn(temp));
                                    }
                                    // if(null == temp) continue;
                                }

                            }

                            // 开始逐行添加元素到 DataTable
                            for(int j = startDataFillRow; j < dataRowCont + 1; ++j)  // j <= dataRowCont Error-Index-Okay -- 经过 testXls 验证
                            // 
                            {
                                sourceRow = ws.GetRow(j);
                                if(null == sourceRow) continue;

                                DataRow tempAddedRow = retDataTable.NewRow();
                                for(int k = columnNumStarts; k < columnNumEnds; k++)
                                {
                                    var temp = sourceRow.GetCell(k); //.StringCellValue;
                                    if(null == temp)
                                    {
                                        tempAddedRow[k] = "";
                                    }
                                    else
                                    {
                                        // TODO:  fix 支持更多单元格类型读取
                                        switch(temp.CellType)
                                        {
                                            case CellType.Numeric:
                                                int format = temp.CellStyle.DataFormat;
                                                // 处理日期类型
                                                if(14 == format || format == 31 || 58 == format|| format == 57)
                                                {
                                                    tempAddedRow[k] = temp.DateCellValue;
                                                }
                                                else
                                                {
                                                    tempAddedRow[k] = temp.NumericCellValue;
                                                }
                                                break;
                                            case CellType.String:
                                                tempAddedRow[k] = temp.StringCellValue;
                                                break;
                                            default:
                                                tempAddedRow[k] = "";
                                                break;
                                        }
                                    }
                                }

                                retDataTable.Rows.Add(tempAddedRow);
                                // 加入一行数据到 retDataTable 完成
                            }

                        }

                    }
               }
               //Microsoft.Office.Interop.Excel? n = null;

            }

            msg = "";
            status = true;
            return retDataTable;
        }
        catch(Exception n)
        {
            msg += n.ToString();
            ///MessageBox.Show(n.ToString());
            return null;
        }
    }


    internal struct Temp
    {

    }
}
