using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZhiBan
{
    class DataFile
    {
        #region 文件序列化

        public static string get_file_path()
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                //打开的文件选择对话框上的标题
                saveFileDialog.Title = "请选择存储位置";
                //设置文件类型
                saveFileDialog.Filter = "*.k24(K24文件)|*.k24";
                //设置默认文件类型显示顺序
                saveFileDialog.FilterIndex = 1;
                //保存对话框是否记忆上次打开的目录
                saveFileDialog.RestoreDirectory = true;
                //按下确定选择的按钮
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //获得文件路径
                    string localFilePath = saveFileDialog.FileName.ToString();
                    System.IO.FileStream ft = System.IO.File.Create(localFilePath);
                    ft.Close();
                    return localFilePath;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "警告");
            }
            return "D://default.k24";
        }

        public static void save_as_byte(string file_path, DamData dd)
        {
            using (FileStream fileStream = new FileStream(file_path, FileMode.OpenOrCreate))
            {
                //新建二进制序列化对象
                BinaryFormatter binaryFormatter = new BinaryFormatter();

                //进行序列化
                binaryFormatter.Serialize(fileStream, dd);
            }

            //wait
            MessageBox.Show("存储完毕！", "存储");
        }
        #endregion

        #region 反序列化
        public static string read_file_path()
        {
            try
            {
                string path = "";
                string FilePath = "";
                OpenFileDialog ChooseExcel = new OpenFileDialog();
                ChooseExcel.Filter = "*.k24(K24文件)|*.k24";
                if (ChooseExcel.ShowDialog() == DialogResult.OK)
                    FilePath = ChooseExcel.FileName;
                else
                {
                    MessageBox.Show("请选择k24文件.");
                    return path;
                }
                if (FilePath == "")
                {
                    MessageBox.Show("未选择k24文件。");
                    return path;
                }
                return FilePath;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public static DamData read_as_byte(string file_path)
        {
            try
            {
                //新建文件流
                using (FileStream fileStream1 = new FileStream(file_path, FileMode.Open))
                {
                    //新建二进制序列化对象
                    BinaryFormatter binaryFormatter = new BinaryFormatter();

                    //进行反序列化
                    DamData dam_data = (DamData)binaryFormatter.Deserialize(fileStream1);

                    return dam_data;
                }
                MessageBox.Show("读取完毕！", "读取");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "警报");
                return null;
            }
        }
        #endregion
    }
}
