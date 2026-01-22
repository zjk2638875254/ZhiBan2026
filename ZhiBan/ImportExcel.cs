//using NPOI.HSSF.UserModel;
//using NPOI.SS.UserModel;
//using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZhiBan
{
    class ImportExcel
    {
        /*
        public static object GetValueType(ICell cell)
        {
            if (cell == null)
                return null;
            switch (cell.CellType)
            {
                case CellType.Blank:
                    return null;
                case CellType.Boolean:
                    return cell.BooleanCellValue;
                case CellType.Numeric:
                    return cell.NumericCellValue;
                case CellType.String:
                    return cell.StringCellValue;
                case CellType.Error:
                    return cell.ErrorCellValue;
                case CellType.Formula:
                default:
                    return "=" + cell.CellFormula;
            }
        }
        //Excel转化DataTable
        public static DataTable GetData(string file)
        {
            DataTable dt = new DataTable();
            IWorkbook workbook;
            string fileExt = Path.GetExtension(file).ToLower();
            using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read))
            {
                fs.Position = 0;
                if (fileExt == ".xlsx")
                    workbook = new XSSFWorkbook(fs);
                else if (fileExt == ".xls")
                    workbook = new HSSFWorkbook(fs);
                else
                    workbook = null;
                if (workbook == null)
                    return null;
                ISheet sheet = workbook.GetSheetAt(0);

                //表头  
                List<int> columns = new List<int>();

                IRow row = sheet.GetRow(0);
                int col_num = row.LastCellNum;
                for (int i = 0; i < col_num; i++)
                {
                    dt.Columns.Add(new DataColumn("Col" + i.ToString()));
                    columns.Add(i);
                }
                //MessageBox.Show(col_num.ToString());

                //数据  
                for (int i = sheet.FirstRowNum; i <= sheet.LastRowNum; i++)
                {
                    DataRow dr = dt.NewRow();
                    bool hasValue = false;
                    for (int j = 0; j < col_num; j++)
                    {
                        dr[j] = GetValueType(sheet.GetRow(i).GetCell(j));
                        if (dr[j] != null && dr[j].ToString() != string.Empty)
                            hasValue = true;
                    }
                    if (hasValue)
                    {
                        dt.Rows.Add(dr);
                    }
                }
            }
            return dt;
        }
        //读取路径
        public static string read_filepath()
        {
            try
            {
                string path = "";
                string FilePath = "";
                OpenFileDialog ChooseExcel = new OpenFileDialog();
                ChooseExcel.Filter = "*.xls(Excel文件)|*.xls|*.xlsx(Excel文件)|*.xlsx";
                if (ChooseExcel.ShowDialog() == DialogResult.OK)
                    FilePath = ChooseExcel.FileName;
                else
                {
                    MessageBox.Show("请选择Excel文件.");
                    return path;
                }
                if (FilePath == "")
                {
                    MessageBox.Show("未选择Excel文件。");
                    return path;
                }
                return FilePath;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
        //返回第i列数据
        public static string get_col(DataTable original_data, int col_id, ref Dictionary<int, string> col_data)
        {
            col_data.Clear();
            try
            {
                int num = original_data.Rows.Count;
                for (int i = 0; i < num; i++)
                {
                    string value = Convert.ToString(original_data.Rows[i][col_id]);
                    col_data[i] = value;
                }
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        */
    }
}
