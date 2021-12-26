using System.Diagnostics;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace SUSViewer
{
    public partial class Form1 : Form
    {
        string SUSFolderPath;
        List<string> SUSFileList;
        public Form1()
        {
            InitializeComponent();
            //Shift_JIS���g����悤�ɂ��܂�
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        }
        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            //"Button"��Ȃ�΁A�{�^�����N���b�N���ꂽ
            if (dgv.Columns[e.ColumnIndex].Name == "DirectoryPath")
            {
                MessageBox.Show(SUSFolderPath + @"\" );
            }
        }

        private void SetGridView(List<string> susfilelist)
        {
            dataGridView1.Rows.Clear();
            foreach (var susfile in susfilelist)
            {
                var susdata = LoadSus(susfile);
                string folderName = Path.GetFileName(Path.GetDirectoryName(susfile));
                dataGridView1.Rows.Add(susdata.Playlevel, susdata.Difficulty, susdata.Title, susdata.Artist, susdata.Designer, susdata.Songid);
            }
            dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        private List<string> GetSusPathList(string Toppath)
        {
            List<string> susfilelist = new();

            IEnumerable<string> subFolders = Directory.EnumerateDirectories(Toppath, "*", SearchOption.AllDirectories);
            foreach (string subfolder in subFolders)
            {
                IEnumerable<string> susfiles = Directory.EnumerateFiles(subfolder, "*.sus", SearchOption.AllDirectories);
                foreach (var item in susfiles)
                {
                    susfilelist.Add(item);
                }
            }

            return susfilelist;
        }
        private static SUSFileData LoadSus(string path)
        {
            using (var reader = new StreamReader(path))
            {
                var sus = new SUSReader().Perse(reader);

                return new SUSFileData()
                {
                    Title = sus.Title,
                    Artist = sus.Artist,
                    Designer = sus.Designer,
                    Difficulty = sus.Difficulty,
                    Jacket = sus.Jacket,
                    Playlevel = sus.Playlevel,
                    Songid = sus.Songid,
                };
            }
        }
        private static void SusDataDebugLog(SUSFileData data)
        {
            Debug.WriteLine("Title " + data.Title);
            Debug.WriteLine("Artist " + data.Artist);
            Debug.WriteLine("Designer " + data.Designer);
            Debug.WriteLine("Diddiculty " + data.Difficulty);
            Debug.WriteLine("PlayLevel " + data.Playlevel);
            Debug.WriteLine("Jacket " + data.Jacket);
            Debug.WriteLine("SongID " + data.Songid);
        }
        private void MenuItemUpdate_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(SUSFolderPath))
            {
                SUSFileList = GetSusPathList(SUSFolderPath);
                SetGridView(SUSFileList);
            }
            else
            {
                OpenAndSetSUS();
            }
        }
        private void �J��OToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenAndSetSUS();
        }
        private void ���O��t���ĕۑ�AToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string SaveFolderPath = FolderOpenDialog("�ۑ���̑I��");
            if (string.IsNullOrWhiteSpace(SaveFolderPath)) return;
            SaveCsv(SaveFolderPath, "SUSScoreListCSV");
            MessageBox.Show("�ۑ����܂���", "����", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void �I��XToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void OpenAndSetSUS()
        {
            SUSFolderPath = FolderOpenDialog("SUS�f�B���N�g���̑I��");
            if (string.IsNullOrWhiteSpace(SUSFolderPath)) return;
            SUSFileList = GetSusPathList(SUSFolderPath);
            SetGridView(SUSFileList);
        }

        // DataGridView��CSV�o�͂���T���v��(C#.NET/VS2005)
        public void SaveCsv(String filepath, string filename)
        {
            StreamWriter sw = new StreamWriter(filepath + @"\" + filename + ".csv", true, System.Text.Encoding.GetEncoding("Shift_JIS"));
            for (int r = 0; r <= dataGridView1.Rows.Count - 1; r++)
            {
                for (int c = 0; c <= dataGridView1.Columns.Count - 1; c++)
                {
                    // DataGridView�̃Z���̃f�[�^�擾
                    String dt = "";
                    if (dataGridView1.Rows[r].Cells[c].Value != null)
                    {
                        dt = dataGridView1.Rows[r].Cells[c].Value.
                            ToString();
                    }
                    if (c < dataGridView1.Columns.Count - 1)
                    {
                        dt = dt + ",";
                    }
                    // CSV�t�@�C������
                    sw.Write(dt);
                }
                sw.Write("\n");
            }
            // CSV�t�@�C���N���[�Y
            sw.Close();
        }
        public static string FolderOpenDialog(string title)
        {
            // �_�C�A���O�̃C���X�^���X�𐶐�
            var dialog = new CommonOpenFileDialog(title)
            {
                // �I���`�����t�H���_�[�X�^�C����
                IsFolderPicker = true,
            };

            // �_�C�A���O��\��
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                return dialog.FileName;
            }
            return "";
        }
        private void �w���vHToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var info = new InfoForm();
            info.ShowDialog();
        }
    }
}