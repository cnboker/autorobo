using System.Data;
using System.Linq;
using System.Windows.Forms;
using System;

namespace AutoRobo.WebHelper
{
    public partial class DataGridViewWithPaging : UserControl
    {
        private int _CurrentPage = 1;

        private string _FirstButtonText = string.Empty;
        public string FirstButtonText
        {
            get
            {
                if (_FirstButtonText == string.Empty)
                    return btnFirst.Text;
                else
                    return _FirstButtonText;
            }
            set
            {
                _FirstButtonText = value;
                btnFirst.Text = _FirstButtonText;
            }
        }

        private string _LastButtonText = string.Empty;
        public string LastButtonText
        {
            get
            {
                if (_LastButtonText == string.Empty)
                    return btnLast.Text;
                else
                    return _LastButtonText;
            }
            set
            {
                _LastButtonText = value;
                btnLast.Text = _LastButtonText;
            }
        }

        private string _PreviousButtonText = string.Empty;
        public string PreviousButtonText
        {
            get
            {
                if (_PreviousButtonText == string.Empty)
                    return btnPrevious.Text;
                else
                    return _PreviousButtonText;
            }
            set
            {
                _PreviousButtonText = value;
                btnPrevious.Text = _PreviousButtonText;
            }
        }

        private string _NextButtonText = string.Empty;
        public string NextButtonText
        {
            get
            {
                if (_NextButtonText == string.Empty)
                    return btnNext.Text;
                else
                    return _NextButtonText;
            }
            set
            {
                _NextButtonText = value;
                btnNext.Text = _NextButtonText;
            }
        }

        private int _Width;
        public int ControlWidth
        {
            get
            {
                if (_Width == 0)
                    return dataGridView1.Width;
                else
                    return _Width;
            }
            set
            {
                _Width = value;
                dataGridView1.Width = _Width;
            }
        }

        private int _Height;
        public int ControlHeight
        {
            get
            {
                if (_Height == 0)
                    return dataGridView1.Height;
                else
                    return _Height;
            }
            set
            {
                _Height = value;
                dataGridView1.Height = _Height;
            }
        }

        private int _PateSize = 15;
        public int PageSize
        {
            get
            {
                return _PateSize;
            }
            set
            {
                _PateSize = value;
            }
        }

        private DataTable _DataSource;
        public DataTable DataSource
        {
            get
            {
                return _DataSource;
            }
            set
            {
                _DataSource = value;
            }
        }

        public DataGridViewWithPaging()
        {
            InitializeComponent();
        }

        private void btnFirst_Click(object sender, System.EventArgs e)
        {
            if (_CurrentPage == 1)
            {
                //MessageBox.Show("You are already on First Page.");
                return;
            }
            else
            {
                _CurrentPage = 1;
                dataGridView1.DataSource = ShowData(_CurrentPage);
            }
        }

        private void btnPrevious_Click(object sender, System.EventArgs e)
        {
            if (_CurrentPage == 1)
            {
               // MessageBox.Show("You are already on First page, you can not go to previous of First page.");
                return;
            }
            else
            {
                _CurrentPage -= 1;
                dataGridView1.DataSource = ShowData(_CurrentPage);
            }
        }

        private void btnNext_Click(object sender, System.EventArgs e)
        {
            int lastPage = (DataSource.Rows.Count / PageSize) + 1;
            if (_CurrentPage == lastPage)
            {
                //MessageBox.Show("You are already on Last page, you can not go to next page of Last page.");
                return;
            }
            else
            {
                _CurrentPage += 1;
                dataGridView1.DataSource = ShowData(_CurrentPage);
            }            
        }

        private void btnLast_Click(object sender, System.EventArgs e)
        {
            int previousPage = _CurrentPage;
            _CurrentPage = (DataSource.Rows.Count / PageSize) + 1;

            if (previousPage == _CurrentPage)
            {
                MessageBox.Show("已经到最后一行.");
            }
            else
            {
                dataGridView1.DataSource = ShowData(_CurrentPage);
            }
        }

        public void DataBind(DataTable dataTable)
        {
            if (dataTable == null) return;
            DataSource = dataTable;
            dataGridView1.DataSource = ShowData(1);
           // SetRowNumber(dataGridView1);
        }

        /// <summary>
        /// 写行号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetRowNumber(DataGridView dgv)
        {
            foreach (DataGridViewRow row in dgv.Rows)
            {
                row.HeaderCell.Value = String.Format("{0}", row.Index + 1);
            }
        }


        private DataTable ShowData(int pageNumber)
        {
            DataTable dt = new DataTable();
            int startIndex = PageSize * (pageNumber - 1);
            var result = DataSource.AsEnumerable().Where((s, k) => (k >= startIndex && k < (startIndex + PageSize)));

            foreach (DataColumn colunm in DataSource.Columns)
            {
                dt.Columns.Add(colunm.ColumnName);
            }

            foreach (var item in result)
            {
                dt.ImportRow(item);
            }

            txtPaging.Text = string.Format( "{0} / {1}", pageNumber, (DataSource.Rows.Count / PageSize) + 1);
            return dt;
        }
    }
}
