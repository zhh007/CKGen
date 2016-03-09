using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace CKGen
{
    public class ExcelHelper
    {
        private static int InsertCell(IRow row, int cellIndex, string value)
        {
            row.CreateCell(cellIndex).SetCellValue(value);
            cellIndex++;
            return cellIndex;
        }

        private static int InsertCell(IRow row, int cellIndex, string value, ICellStyle cellStyle)
        {
            var cell = row.CreateCell(cellIndex);
            cell.SetCellValue(value);
            cell.CellStyle = cellStyle;
            cellIndex++;
            return cellIndex;
        }

        public static void ExportFile(DataSet ds, string filepath)
        {
            if(string.IsNullOrEmpty(filepath))
            {
                return;
            }
            if(ds == null || ds.Tables.Count == 0)
            {
                return;
            }
            try
            {
                IWorkbook hssfworkbook = new HSSFWorkbook();
                using (FileStream fs = File.Create(filepath))//path=mmm.xls;
                {
                    foreach (DataTable table in ds.Tables)
                    {
                        ISheet sheet = hssfworkbook.CreateSheet(table.TableName);

                        //head
                        IRow firstRow = sheet.CreateRow(0);
                        int cellIndex = 0;
                        foreach (DataColumn col in table.Columns)
                        {
                            cellIndex = InsertCell(firstRow, cellIndex, col.ColumnName);
                        }

                        //rows
                        int rowIndex = 1;
                        foreach (DataRow dr in table.Rows)
                        {
                            cellIndex = 0;
                            IRow row = sheet.CreateRow(rowIndex);

                            for (int i = 0; i < table.Columns.Count; i++)
                            {
                                cellIndex = InsertCell(row, cellIndex, dr[i].ToString());
                            }

                            rowIndex++;
                        }
                    }
                    hssfworkbook.Write(fs);//保存文件
                }
            }
            catch (Exception ex)
            {
                //LogHelper
            }
        }
    }
}
