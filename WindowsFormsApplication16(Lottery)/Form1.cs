using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
namespace WindowsFormsApplication16_Lottery_
{
    public partial class Form1 : Form
    {
        bool tutPageOn = false;

        //大福彩變數
        int[] arrDFtemp = new int[7];
        List<TextBox> tbArrDFnums = new List<TextBox>();
        List<CheckBox> cbArrDFlocks = new List<CheckBox>();
        int[] intArrDFsort;

        //今彩539變數
        int[] arr539temp = new int[5];
        List<TextBox> tbArr539nums = new List<TextBox>();
        List<CheckBox> cbArr539locks = new List<CheckBox>();
        int[] sort539 = new int[5];

        //Bingo Bingo變數
        int[] arrBBtemp = new int[10];
        List<TextBox> tbArrBBnums = new List<TextBox>();
        List<CheckBox> cbArrBBlocks = new List<CheckBox>();
        int[] sortBB = new int[10];

        //開獎變數
        List<TextBox> tbRolling; //給隨機數字特效使用
        Random roller = new Random();
        int timer2StartDelay = 0;
        string[] rollMessages = new string[13];
        string rollMessage = "正在隨機產生號碼";

        int[] currentArrTemps;
        List<TextBox> currentArrTbs;
        Label currentSortLabel;
        List<Label> currentArrBalls;

        List<Label> lblDFballs = new List<Label>();
        List<Label> lbl539balls = new List<Label>();
        List<Label> lblBBballs = new List<Label>();

        public Form1()
        {
            InitializeComponent();
            //timer1.Enabled = true;  //for debugger
           
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: 這行程式碼會將資料載入 'lotteryDataSet.df' 資料表。您可以視需要進行移動或移除。
            this.dfTableAdapter1.Fill(this.lotteryDataSet.df);
            dfTableAdapter = new lotteryDataSetTableAdapters.dfTableAdapter();
            // TODO: 這行程式碼會將資料載入 'lotteryDataSet.bb' 資料表。您可以視需要進行移動或移除。
            this.bbTableAdapter.Fill(this.lotteryDataSet.bb);
            // TODO: 這行程式碼會將資料載入 'lotteryDataSet._539' 資料表。您可以視需要進行移動或移除。
            this._539TableAdapter.Fill(this.lotteryDataSet._539);
            #region 塞東西到陣列
            //大福彩
            tbArrDFnums.Add(tbDFnum1);
            tbArrDFnums.Add(tbDFnum2);
            tbArrDFnums.Add(tbDFnum3);
            tbArrDFnums.Add(tbDFnum4);
            tbArrDFnums.Add(tbDFnum5);
            tbArrDFnums.Add(tbDFnum6);
            tbArrDFnums.Add(tbDFnum7);

            cbArrDFlocks.Add(cbDF1);
            cbArrDFlocks.Add(cbDF2);
            cbArrDFlocks.Add(cbDF3);
            cbArrDFlocks.Add(cbDF4);
            cbArrDFlocks.Add(cbDF5);
            cbArrDFlocks.Add(cbDF6);
            cbArrDFlocks.Add(cbDF7);

            //今彩539
            tbArr539nums.Add(tb539num1);
            tbArr539nums.Add(tb539num2);
            tbArr539nums.Add(tb539num3);
            tbArr539nums.Add(tb539num4);
            tbArr539nums.Add(tb539num5);

            cbArr539locks.Add(cb539lock1);
            cbArr539locks.Add(cb539lock2);
            cbArr539locks.Add(cb539lock3);
            cbArr539locks.Add(cb539lock4);
            cbArr539locks.Add(cb539lock5);

            //BINGO BINGO
            tbArrBBnums.Add(tbBBnum1);
            tbArrBBnums.Add(tbBBnum2);
            tbArrBBnums.Add(tbBBnum3);
            tbArrBBnums.Add(tbBBnum4);
            tbArrBBnums.Add(tbBBnum5);
            tbArrBBnums.Add(tbBBnum6);
            tbArrBBnums.Add(tbBBnum7);
            tbArrBBnums.Add(tbBBnum8);
            tbArrBBnums.Add(tbBBnum9);
            tbArrBBnums.Add(tbBBnum10);

            cbArrBBlocks.Add(cbBBlock1);
            cbArrBBlocks.Add(cbBBlock2);
            cbArrBBlocks.Add(cbBBlock3);
            cbArrBBlocks.Add(cbBBlock4);
            cbArrBBlocks.Add(cbBBlock5);
            cbArrBBlocks.Add(cbBBlock6);
            cbArrBBlocks.Add(cbBBlock7);
            cbArrBBlocks.Add(cbBBlock8);
            cbArrBBlocks.Add(cbBBlock9);
            cbArrBBlocks.Add(cbBBlock10);

            lblDFballs.Add(lblDFball1);
            lblDFballs.Add(lblDFball2);
            lblDFballs.Add(lblDFball3);
            lblDFballs.Add(lblDFball4);
            lblDFballs.Add(lblDFball5);
            lblDFballs.Add(lblDFball6);
            lblDFballs.Add(lblDFball7);

            lbl539balls.Add(lbl539ball1);
            lbl539balls.Add(lbl539ball2);
            lbl539balls.Add(lbl539ball3);
            lbl539balls.Add(lbl539ball4);
            lbl539balls.Add(lbl539ball5);
            lbl539balls.Add(lbl539ball6);

            lblBBballs.Add(lblBBball1);
            lblBBballs.Add(lblBBball2);
            lblBBballs.Add(lblBBball3);
            lblBBballs.Add(lblBBball4);
            lblBBballs.Add(lblBBball5);
            lblBBballs.Add(lblBBball6);
            lblBBballs.Add(lblBBball7);
            lblBBballs.Add(lblBBball8);
            lblBBballs.Add(lblBBball9);
            lblBBballs.Add(lblBBball10);
            #endregion
            //大幅彩開獎日 
            #region UI
            CultureInfo cul = new CultureInfo("zh-tw", false);
            DateTime tDay = DateTime.Today;
            DateTime nextDate;
            string todayOfWeek = string.Format("今天是{0:yyyy/MM/dd} {1}，", tDay, cul.DateTimeFormat.DayNames[(int)tDay.DayOfWeek]);

            lblTime.Text = string.Format(todayOfWeek);
            if ((int)tDay.DayOfWeek == 3 || (int)tDay.DayOfWeek == 6)
            {
                lblTime.Text += "下一個開獎日期是今天!";
            }
            else
            {
                if ((int)tDay.DayOfWeek <= 2)
                {
                    nextDate = tDay.AddDays(3 - (int)tDay.DayOfWeek);
                }
                else
                {
                    nextDate = tDay.AddDays(6 - (int)tDay.DayOfWeek);
                }
                lblTime.Text += string.Format("下一個開獎日期為: {0:yyyy/MM/dd} {1}!", nextDate, cul.DateTimeFormat.DayNames[(int)nextDate.DayOfWeek]);
            }
            //其他開獎日
            lbl539Time.Text = string.Format(todayOfWeek);
            lbl539Time.Text += "今彩539星期一至星期六皆有開獎!";
            lblBBtime.Text = string.Format(todayOfWeek);
            lblBBtime.Text += "Bingo Bingo 每日每五分鐘開獎!";

            //隱藏教學
            tabControl1.Width -= 200;
            #endregion

        }

        //大福彩產生號碼按鈕
        private void btnDFgenerate_Click(object sender, EventArgs e)
        {
            generateNums(40, tbArrDFnums, arrDFtemp, lbDFsort, lblDFballs);
        }

        //今彩539產生號碼按鈕
        private void btn539generate_Click(object sender, EventArgs e)
        {
            generateNums(39, tbArr539nums, arr539temp, lbl539sort, lbl539balls);
        }

        //BingoBingo產生號碼按鈕
        private void btnBBgenerate_Click(object sender, EventArgs e)
        {
            int[] temp = new int[cbbBBstar.SelectedIndex + 1];
            Array.Copy(arrBBtemp, temp, cbbBBstar.SelectedIndex + 1);
            arrBBtemp = temp;
            generateNums(80, tbArrBBnums, arrBBtemp, lblBBsort, lblBBballs);
        }

        //隨機產生號碼
        private void generateNums(int max, List<TextBox> tbs, int[] temp, Label lbsort, List<Label> lblBalls)
        {
            //hide balls
            foreach (Label lbl in lblBalls)
            {
                lbl.Visible = false;
            }

            currentArrTbs = tbs;
            currentArrTemps = temp;
            currentSortLabel = lbsort;
            currentArrBalls = lblBalls;

            Random rand = new Random();
            for (int i = 0; i < temp.Length; i++) //分別開出temp長度個數字
            {
                if (tbs[i].Enabled) //if unlocked
                {
                    temp[i] = rand.Next(1, max+1); //隨機1~max

                    for (int o = 0; o < temp.Length; o++) //若與已開出號碼相同則重來
                    {
                        if (o != i && temp[o] != 0)
                        {
                            if (temp[i] == temp[o])
                            {
                                i--;
                                break;
                            }
                        }
                    }
                }
            }
            roll(tbs, lbsort);
        }

        //將temp中號碼丟到sort內再排序並輸出到label上
        private void sort(Label sortOutput, List<TextBox> tb, int[] sort, int[] temp, List<Label> lblBalls)
        {
            sortOutput.Text = "排序後號碼:                                                                     ";
            sort = new int[temp.Length];
            Array.Copy(temp, sort, temp.Length);
            Array.Sort(sort);

            for (int i = 0; i < sort.Length; i++)
            {
                lblBalls[i].Text = sort[i].ToString();
                lblBalls[i].Visible = true;
            }
        }
        
        //BB調整星數combo box
        private void cbbBBstar_SelectedIndexChanged(object sender, EventArgs e)
        {
            int[] temp = new int[arrBBtemp.Length+1];
            Array.Copy(arrBBtemp, temp, arrBBtemp.Length);
            arrBBtemp = temp; //調整temp長度
            //逐一隱藏或顯示按鈕
            for (int i = 0; i < 10; i++)
            {
                if (i > cbbBBstar.SelectedIndex)
                {
                    //隱藏的話也要把鎖定去掉
                    tbArrBBnums[i].Visible = false;
                    cbArrBBlocks[i].Visible = false;
                    tbArrBBnums[i].Enabled = true;
                }
                else
                {
                    tbArrBBnums[i].Visible = true;
                    cbArrBBlocks[i].Visible = true;
                    if (cbArrBBlocks[i].Checked == true && tbArrBBnums[i].Enabled == true)
                    {
                        cbArrBBlocks[i].Checked = false;
                    }
                }
            }
            //若少於6個則隱藏下面的隱藏按鈕標籤
            if (cbbBBstar.SelectedIndex < 5)
            {
                label3.Visible = false;
            }
            else
            {
                label3.Visible = true;
            }
        }

        //大福彩儲存號碼按鈕
        private void btnDFsave_Click(object sender, EventArgs e)
        {
            saveNums("df");
        }

        //539儲存號碼按鈕
        private void btn539save_Click(object sender, EventArgs e)
        {
            saveNums("539");
        }

        //BB儲存號碼按鈕
        private void btnBBsave_Click(object sender, EventArgs e)
        {
            saveNums("bb");
        }

        //儲存號碼
        private void saveNums(string type)
        {

            #region//確認變數
            List<TextBox> tbs = null;
            int[] temp = null;
            int max = 0;
            Label lblsort = null;
            int tbsNum = 0;
            ListBox lbox = null;
            List<CheckBox> cbs = null;
            List<Label> lblBalls = null;
            switch (type)
            {
                case "df":

                     tbs = tbArrDFnums;
                     temp = arrDFtemp;
                     max = 40;
                     lblsort = lbDFsort;
                     tbsNum = 7;
                     lbox = lboxDFsave;
                     cbs = cbArrDFlocks;
                     lblBalls = lblDFballs;
                    break;
                case "539":
                    tbs = tbArr539nums;
                    temp = arr539temp;
                    max = 39;
                    lblsort = lbl539sort;
                    tbsNum = 5;
                    lbox = lbox539save;
                    cbs = cbArr539locks;
                    lblBalls = lbl539balls;
                    break;
                case "bb":
                    tbs = tbArrBBnums;
                    temp = arrBBtemp;
                    max = 80;
                    lblsort = lblBBsort;
                    tbsNum = cbbBBstar.SelectedIndex + 1;
                    lbox = lboxBBsave;
                    cbs = cbArrBBlocks;
                    lblBalls = lblBBballs;
                    break;
            }
            #endregion

            for (int i = 0; i < tbsNum; i++)
            {
                if (checkTbVal(tbs[i], max)) //逐一檢查tbDFnums是否合格
                {
                    for (int o = 0; o < tbsNum; o++) //檢查是否與其他號碼相同
                    {
                        if (o != i && temp[o] != 0)
                        {
                            if (tbs[i].Text == tbs[o].Text)
                            {
                                lblsort.Text = "號碼不可重複!";
                                return;
                            }
                        }
                    }
                }
                else
                {
                    //break;  //不合格則取消
                    lblsort.Text = "號碼須為1~" + max.ToString() + "的數字!";
                    return;
                }
            }

            //合格的話再逐一丟到temp裡面
            for (int i = 0; i < tbsNum; i++)
            {
                temp[i] = Int32.Parse(tbs[i].Text);
            }
            //丟完再排序temp 
            intArrDFsort = new int[tbsNum];
            Array.Copy(temp, intArrDFsort, tbsNum); //intArrDFsort拿來共用了
            Array.Sort(intArrDFsort);
            //再把temp存進listbox
            string strSave = "";
            for (int i = 0; i < tbsNum; i++)
            {
                strSave += String.Format("{0,2}", intArrDFsort[i].ToString());
                if (i < tbsNum-1)
                {
                    strSave += ", ";
                }
            }
            //存入DB            
            switch (type)
            {
                case "df":
                    dfTableAdapter.Insert(strSave);
                    this.dfTableAdapter.Fill(this.lotteryDataSet.df);
                    break;
                case "539":
                    _539TableAdapter.Insert(strSave);
                    this._539TableAdapter.Fill(this.lotteryDataSet._539);
                    break;
                case "bb":
                    bbTableAdapter.Insert(strSave);
                    this.bbTableAdapter.Fill(this.lotteryDataSet.bb);
                    break;
            }
            

            #region clearing boxes
            //清空TB的值
            for (int i = 0; i < tbsNum; i++)
            {
                if (tbs[i].Enabled)
                {
                    tbs[i].Text = " ";
                }
            }
            //清空temp陣列
            for (int i = 0; i < tbsNum; i++)
            {
                if (tbs[i].Enabled)
                {
                    temp[i] = 0;
                }
            }
            //解鎖 checkoxes
            for (int i = 0; i < tbsNum; i++)
            {
                if (tbs[i].Enabled)
                {
                    cbs[i].Checked = false;
                }
            }
            lblsort.Text = "號碼已儲存!";
            //list box自動捲到最下面
            lbox.SetSelected(lbox.Items.Count - 1, true);
            lbox.SetSelected(lbox.Items.Count - 1, false);

            //hide balls
            foreach (Label lbl in lblBalls)
            {
                lbl.Visible = false;
            }
            #endregion
        }

        //大福彩刪除號碼按鈕
        private void btnDFdelete_Click(object sender, EventArgs e)
        {
            foreach (Label lbl in lblDFballs)
            {
                lbl.Visible = false;
            }
            //刪除lbox中選取的項目
            for (int i = lboxDFsave.SelectedItems.Count - 1; i >= 0; i--)
            {
                dfBindingSource.Remove(lboxDFsave.SelectedItem);
            }
            dfBindingSource.EndEdit();
            dfTableAdapter.Update(lotteryDataSet.df);
            this.dfTableAdapter.Fill(this.lotteryDataSet.df);
            lboxDFsave.ClearSelected();
            lbDFsort.Text = "號碼已刪除!";
        }

        //539刪除號碼按鈕
        private void btn539deleteNum_Click(object sender, EventArgs e)
        {
            foreach (Label lbl in lbl539balls)
            {
                lbl.Visible = false;
            }
            for (int i = lbox539save.SelectedItems.Count - 1; i >= 0; i--)
            {
                _539BindingSource.Remove(lbox539save.SelectedItem);
            }
            _539BindingSource.EndEdit();
            _539TableAdapter.Update(lotteryDataSet._539);
            this._539TableAdapter.Fill(this.lotteryDataSet._539);
            lbox539save.ClearSelected();
            lbl539sort.Text = "號碼已刪除!";
        }

        //BB刪除號碼按鈕
        private void btnBBdelete_Click(object sender, EventArgs e)
        {
            foreach (Label lbl in lblBBballs)
            {
                lbl.Visible = false;
            }
            for (int i = lboxBBsave.SelectedItems.Count - 1; i >= 0; i--)
            {
                bbBindingSource.Remove(lboxBBsave.SelectedItem);
            }
            bbBindingSource.EndEdit();
            bbTableAdapter.Update(lotteryDataSet.bb);
            this.bbTableAdapter.Fill(this.lotteryDataSet.bb);
            lboxBBsave.ClearSelected();
            lblBBsort.Text = "號碼已刪除!";
        }

        #region 鎖號按鈕
        //大福彩
        private void cbDF1_CheckedChanged(object sender, EventArgs e)
        {
            TextBox tb = tbDFnum1;
            CheckBox cb = cbDF1;
            int i = 0;

            if (!cb.Checked)
            {
                tb.Enabled = true;
                arrDFtemp[i] = 0;
            }
            else
            {
                if (checkTbVal(tb, 40))
                {
                    tb.Enabled = false;
                    if (arrDFtemp[i] == 0)
                    {
                        arrDFtemp[i] = Convert.ToInt32(tb.Text);
                    }  
                }
                else
                {
                    cb.Checked = false;
                }
            }
        }

        private void cbDF2_CheckedChanged(object sender, EventArgs e)
        {
            TextBox tb = tbDFnum2;
            CheckBox cb = cbDF2;
            int i = 1;

            if (!cb.Checked)
            {
                tb.Enabled = true;
                arrDFtemp[i] = 0;
            }
            else
            {
                if (checkTbVal(tb, 40))
                {
                    tb.Enabled = false;
                    if (arrDFtemp[i] == 0)
                    {
                        arrDFtemp[i] = Convert.ToInt32(tb.Text);
                    }
                }
                else
                {
                    cb.Checked = false;
                }
            }
        }

        private void cbDF3_CheckedChanged(object sender, EventArgs e)
        {
            TextBox tb = tbDFnum3;
            CheckBox cb = cbDF3;
            int i = 2;

            if (!cb.Checked)
            {
                tb.Enabled = true;
                arrDFtemp[i] = 0;
            }
            else
            {
                if (checkTbVal(tb, 40))
                {
                    tb.Enabled = false;
                    if (arrDFtemp[i] == 0)
                    {
                        arrDFtemp[i] = Convert.ToInt32(tb.Text);
                    }
                }
                else
                {
                    cb.Checked = false;
                }
            }
        }

        private void cbDF4_CheckedChanged(object sender, EventArgs e)
        {
            TextBox tb = tbDFnum4;
            CheckBox cb = cbDF4;
            int i = 3;

            if (!cb.Checked)
            {
                tb.Enabled = true;
                arrDFtemp[i] = 0;
            }
            else
            {
                if (checkTbVal(tb, 40))
                {
                    tb.Enabled = false;
                    if (arrDFtemp[i] == 0)
                    {
                        arrDFtemp[i] = Convert.ToInt32(tb.Text);
                    }
                }
                else
                {
                    cb.Checked = false;
                }
            }
        }

        private void cbDF5_CheckedChanged(object sender, EventArgs e)
        {
            TextBox tb = tbDFnum5;
            CheckBox cb = cbDF5;
            int i = 4;

            if (!cb.Checked)
            {
                tb.Enabled = true;
                arrDFtemp[i] = 0;
            }
            else
            {
                if (checkTbVal(tb, 40))
                {
                    tb.Enabled = false;
                    if (arrDFtemp[i] == 0)
                    {
                        arrDFtemp[i] = Convert.ToInt32(tb.Text);
                    }
                }
                else
                {
                    cb.Checked = false;
                }
            }
        }

        private void cbDF6_CheckedChanged(object sender, EventArgs e)
        {
            TextBox tb = tbDFnum6;
            CheckBox cb = cbDF6;
            int i = 5;

            if (!cb.Checked)
            {
                tb.Enabled = true;
                arrDFtemp[i] = 0;
            }
            else
            {
                if (checkTbVal(tb, 40))
                {
                    tb.Enabled = false;
                    if (arrDFtemp[i] == 0)
                    {
                        arrDFtemp[i] = Convert.ToInt32(tb.Text);
                    }
                }
                else
                {
                    cb.Checked = false;
                }
            }
        }
        private void cbDF7_CheckedChanged(object sender, EventArgs e)
        {
            TextBox tb = tbDFnum7;
            CheckBox cb = cbDF7;
            int i = 6;

            if (!cb.Checked)
            {
                tb.Enabled = true;
                arrDFtemp[i] = 0;
            }
            else
            {
                if (checkTbVal(tb, 40))
                {
                    tb.Enabled = false;
                    if (arrDFtemp[i] == 0)
                    {
                        arrDFtemp[i] = Convert.ToInt32(tb.Text);
                    }
                }
                else
                {
                    cb.Checked = false;
                }
            }
        }
        //539
        private void cb539lock1_CheckedChanged(object sender, EventArgs e)
        {
            TextBox tb = tb539num1;
            CheckBox cb = cb539lock1;
            int i = 0;

            if (!cb.Checked)
            {
                tb.Enabled = true;
                arr539temp[i] = 0;
            }
            else
            {
                if (checkTbVal(tb, 39))
                {
                    tb.Enabled = false;
                    if (arr539temp[i] == 0)
                    {
                        arr539temp[i] = Convert.ToInt32(tb.Text);
                    }
                }
                else
                {
                    cb.Checked = false;
                }
            }
        }

        private void cb539lock2_CheckedChanged(object sender, EventArgs e)
        {
            TextBox tb = tb539num2;
            CheckBox cb = cb539lock2;
            int i = 1;

            if (!cb.Checked)
            {
                tb.Enabled = true;
                arr539temp[i] = 0;
            }
            else
            {
                if (checkTbVal(tb, 39))
                {
                    tb.Enabled = false;
                    if (arr539temp[i] == 0)
                    {
                        arr539temp[i] = Convert.ToInt32(tb.Text);
                    }
                }
                else
                {
                    cb.Checked = false;
                }
            }
        }

        private void cb539lock3_CheckedChanged(object sender, EventArgs e)
        {
            TextBox tb = tb539num3;
            CheckBox cb = cb539lock3;
            int i = 2;

            if (!cb.Checked)
            {
                tb.Enabled = true;
                arr539temp[i] = 0;
            }
            else
            {
                if (checkTbVal(tb, 39))
                {
                    tb.Enabled = false;
                    if (arr539temp[i] == 0)
                    {
                        arr539temp[i] = Convert.ToInt32(tb.Text);
                    }
                }
                else
                {
                    cb.Checked = false;
                }
            }
        }

        private void cb539lock4_CheckedChanged(object sender, EventArgs e)
        {
            TextBox tb = tb539num4;
            CheckBox cb = cb539lock4;
            int i = 3;

            if (!cb.Checked)
            {
                tb.Enabled = true;
                arr539temp[i] = 0;
            }
            else
            {
                if (checkTbVal(tb, 39))
                {
                    tb.Enabled = false;
                    if (arr539temp[i] == 0)
                    {
                        arr539temp[i] = Convert.ToInt32(tb.Text);
                    }
                }
                else
                {
                    cb.Checked = false;
                }
            }
        }

        private void cb539lock5_CheckedChanged(object sender, EventArgs e)
        {
            TextBox tb = tb539num5;
            CheckBox cb = cb539lock5;
            int i = 4;

            if (!cb.Checked)
            {
                tb.Enabled = true;
                arr539temp[i] = 0;
            }
            else
            {
                if (checkTbVal(tb, 39))
                {
                    tb.Enabled = false;
                    if (arr539temp[i] == 0)
                    {
                        arr539temp[i] = Convert.ToInt32(tb.Text);
                    }
                }
                else
                {
                    cb.Checked = false;
                }
            }
        }
        //Bingo Bingo
        private void cbBBlock1_CheckedChanged(object sender, EventArgs e)
        {
            TextBox tb = tbBBnum1;
            CheckBox cb = cbBBlock1;
            int i = 0;

            if (!cb.Checked)
            {
                tb.Enabled = true;
                arrBBtemp[i] = 0;
            }
            else
            {
                if (checkTbVal(tb, 80))
                {
                    tb.Enabled = false;
                    if (arrBBtemp[i] == 0)
                    {
                        arrBBtemp[i] = Convert.ToInt32(tb.Text);
                    }
                }
                else
                {
                    cb.Checked = false;
                }
            }
        }
        
        private void cbBBlock2_CheckedChanged(object sender, EventArgs e)
        {
            TextBox tb = tbBBnum2;
            CheckBox cb = cbBBlock2;
            int i = 1;

            if (!cb.Checked)
            {
                tb.Enabled = true;
                arrBBtemp[i] = 0;
            }
            else
            {
                if (checkTbVal(tb, 80))
                {
                    tb.Enabled = false;
                    if (arrBBtemp[i] == 0)
                    {
                        arrBBtemp[i] = Convert.ToInt32(tb.Text);
                    }
                }
                else
                {
                    cb.Checked = false;
                }
            }
        }

        private void cbBBlock3_CheckedChanged(object sender, EventArgs e)
        {
            TextBox tb = tbBBnum3;
            CheckBox cb = cbBBlock3;
            int i = 2;

            if (!cb.Checked)
            {
                tb.Enabled = true;
                arrBBtemp[i] = 0;
            }
            else
            {
                if (checkTbVal(tb, 80))
                {
                    tb.Enabled = false;
                    if (arrBBtemp[i] == 0)
                    {
                        arrBBtemp[i] = Convert.ToInt32(tb.Text);
                    }
                }
                else
                {
                    cb.Checked = false;
                }
            }
        }

        private void cbBBlock4_CheckedChanged(object sender, EventArgs e)
        {
            TextBox tb = tbBBnum4;
            CheckBox cb = cbBBlock4;
            int i = 3;

            if (!cb.Checked)
            {
                tb.Enabled = true;
                arrBBtemp[i] = 0;
            }
            else
            {
                if (checkTbVal(tb, 80))
                {
                    tb.Enabled = false;
                    if (arrBBtemp[i] == 0)
                    {
                        arrBBtemp[i] = Convert.ToInt32(tb.Text);
                    }
                }
                else
                {
                    cb.Checked = false;
                }
            }
        }

        private void cbBBlock5_CheckedChanged(object sender, EventArgs e)
        {
            TextBox tb = tbBBnum5;
            CheckBox cb = cbBBlock5;
            int i = 4;

            if (!cb.Checked)
            {
                tb.Enabled = true;
                arrBBtemp[i] = 0;
            }
            else
            {
                if (checkTbVal(tb, 80))
                {
                    tb.Enabled = false;
                    if (arrBBtemp[i] == 0)
                    {
                        arrBBtemp[i] = Convert.ToInt32(tb.Text);
                    }
                }
                else
                {
                    cb.Checked = false;
                }
            }
        }
        private void cbBBlock6_CheckedChanged(object sender, EventArgs e)
        {
            TextBox tb = tbBBnum6;
            CheckBox cb = cbBBlock6;
            int i = 5;

            if (!cb.Checked)
            {
                tb.Enabled = true;
                arrBBtemp[i] = 0;
            }
            else
            {
                if (checkTbVal(tb, 80))
                {
                    tb.Enabled = false;
                    if (arrBBtemp[i] == 0)
                    {
                        arrBBtemp[i] = Convert.ToInt32(tb.Text);
                    }
                }
                else
                {
                    cb.Checked = false;
                }
            }
        }
        private void cbBBlock7_CheckedChanged(object sender, EventArgs e)
        {
            TextBox tb = tbBBnum7;
            CheckBox cb = cbBBlock7;
            int i = 6;

            if (!cb.Checked)
            {
                tb.Enabled = true;
                arrBBtemp[i] = 0;
            }
            else
            {
                if (checkTbVal(tb, 80))
                {
                    tb.Enabled = false;
                    if (arrBBtemp[i] == 0)
                    {
                        arrBBtemp[i] = Convert.ToInt32(tb.Text);
                    }
                }
                else
                {
                    cb.Checked = false;
                }
            }
        }
        private void cbBBlock8_CheckedChanged(object sender, EventArgs e)
        {
            TextBox tb = tbBBnum8;
            CheckBox cb = cbBBlock8;
            int i = 7;

            if (!cb.Checked)
            {
                tb.Enabled = true;
                arrBBtemp[i] = 0;
            }
            else
            {
                if (checkTbVal(tb, 80))
                {
                    tb.Enabled = false;
                    if (arrBBtemp[i] == 0)
                    {
                        arrBBtemp[i] = Convert.ToInt32(tb.Text);
                    }
                }
                else
                {
                    cb.Checked = false;
                }
            }
        }
        private void cbBBlock9_CheckedChanged(object sender, EventArgs e)
        {
            TextBox tb = tbBBnum9;
            CheckBox cb = cbBBlock9;
            int i = 8;

            if (!cb.Checked)
            {
                tb.Enabled = true;
                arrBBtemp[i] = 0;
            }
            else
            {
                if (checkTbVal(tb, 80))
                {
                    tb.Enabled = false;
                    if (arrBBtemp[i] == 0)
                    {
                        arrBBtemp[i] = Convert.ToInt32(tb.Text);
                    }
                }
                else
                {
                    cb.Checked = false;
                }
            }

        }
        private void cbBBlock10_CheckedChanged(object sender, EventArgs e)
        {
            TextBox tb = tbBBnum10;
            CheckBox cb = cbBBlock10;
            int i = 9;

            if (!cb.Checked)
            {
                tb.Enabled = true;
                arrBBtemp[i] = 0;
            }
            else
            {
                if (checkTbVal(tb, 80))
                {
                    tb.Enabled = false;
                    if (arrBBtemp[i] == 0)
                    {
                        arrBBtemp[i] = Convert.ToInt32(tb.Text);
                    }
                }
                else
                {
                    cb.Checked = false;
                }
            }
        }

        #endregion
        private bool checkTbVal(TextBox tb, int max)
        {
            if (tb.Text != "0" && tb.Text != " ")
            {
                //有填入資料且為1~max則回傳true
                int result;
                bool ifNum;
                ifNum = Int32.TryParse(tb.Text, out result);

                if (ifNum && result > 0 && result <= max)
                {
                    return true;
                }
                else
                {
                    //不為1~max
                    return false;
                }
            }
            else
            {
                //無填入資料
                return false;
            }
        }
        #region 開獎
        private void timer1_Tick(object sender, EventArgs e)
        {
            //debugger
            string strOutput = "temp: ";
            foreach (int i in currentArrTemps)
            {
                strOutput += i.ToString() + " ";
            }
            strOutput += "\ntbArr: ";
            foreach (TextBox tb in currentArrTbs)
            {
                strOutput += tb.Text + " ";
            }
            lbDebug.Text = strOutput;

            //rolling
            int o = timer2StartDelay - 2;
            if (o < 0) o = 0;
            for (int i = o; i < tbRolling.Count; i++)
            {
                if (tbRolling[i].Enabled)
                {
                    tbRolling[i].Text = roller.Next(1, 80).ToString();
                    //Console.WriteLine("random Num for: tbRolling[" + i + "] " + tbRolling[i]);
                }
            }
        }

        
        private void roll(List<TextBox> tbs, Label lbsort)
        {
            //TIMER1 刷新數字 TIMER2 決定哪個號碼要刷新
            lbsort.Text = rollMessage + "   ";

            timer1.Enabled = true;
            timer1.Interval = 10;
            timer2.Enabled = true;
            timer2.Interval = 200;

            //若為BB則調整tbrolling長度以符合星數選擇
            if (tbs == tbArrBBnums)
            {
                tbRolling = tbs.GetRange(0, cbbBBstar.SelectedIndex + 1);
            }
            else
            {
                tbRolling = tbs;
            }
            

            //關閉按鈕
            btnDFsave.Enabled = false;
            btnDFgenerate.Enabled = false;
            btnUnlock.Enabled = false;
            for (int i = 0; i < 7; i++)
            {
                cbArrDFlocks[i].Enabled = false;
            }
            btn539generate.Enabled = false;
            btn539save.Enabled = false;
            btn539unlock.Enabled = false;
            for (int i = 0; i < 5; i++)
            {
                cbArr539locks[i].Enabled = false;
            }
            btnBBgenerate.Enabled = false;
            btnBBsave.Enabled = false;
            btnBBunlock.Enabled = false;
            for (int i = 0; i < 10; i++)
            {
                cbArrBBlocks[i].Enabled = false;
            }
            cbbBBstar.Enabled = false;
        }

        private void stopRoll()
        {
            timer1.Enabled = false;
            timer1.Stop();
            timer2.Enabled = false;
            timer2.Stop();

            //SORT temp中的號碼並貼到LABEL上
            sort(currentSortLabel, currentArrTbs, intArrDFsort, currentArrTemps, currentArrBalls);
            timer2StartDelay = 0; // 歸零

            //重新開啟按鈕
            btnDFsave.Enabled = true;
            btnDFgenerate.Enabled = true;
            btnUnlock.Enabled = true;
            for (int i = 0; i < 7; i++)
            {
                cbArrDFlocks[i].Enabled = true;
            }
            btn539generate.Enabled = true;
            btn539save.Enabled = true;
            btn539unlock.Enabled = true;
            for (int i = 0; i < 5; i++)
            {
                cbArr539locks[i].Enabled = true;
            }
            btnBBgenerate.Enabled = true;
            btnBBsave.Enabled = true;
            btnBBunlock.Enabled = true;
            for (int i = 0; i < 10; i++)
            {
                cbArrBBlocks[i].Enabled = true;
            }
            cbbBBstar.Enabled = true;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            //ROLL MESSAGE
            currentSortLabel.Text = rollMessage;
            if (timer2StartDelay % 2 == 0)
            {
                currentSortLabel.Text += "...";
            }
            else
            {
                currentSortLabel.Text += ".  ";
            }

            //timer2StartDelay 0~2不做事
            timer2StartDelay += 1;
            
            if (timer2StartDelay >= 3)
            {
                //若號碼鎖定則timer2StartDelay + 1，否則從TEMP中拿出正確號碼
                if (tbRolling[timer2StartDelay - 3].Enabled == false)
                {
                   // timer2StartDelay += 1;
                }
                else
                {
                    tbRolling[timer2StartDelay - 3].Text = currentArrTemps[timer2StartDelay - 3].ToString();
                    //Console.WriteLine("Correct num for: " + "tbRolling " + (timer2StartDelay - 3).ToString() + " " + tbRolling[timer2StartDelay - 3].Text);
                    //Console.WriteLine("Correct num for: " + "currentTbs " + (timer2StartDelay - 3).ToString() + " " + currentArrTbs[timer2StartDelay - 3].Text);
                }
            }

            //號碼都開出來了就停止
            if (timer2StartDelay >= tbRolling.Count + 2)
            {
                stopRoll();
            }
        }
#endregion
        //全部解鎖按鈕
        private void btnUnlock_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 7; i++)
            {
                cbArrDFlocks[i].Checked = false;
            }
        }
        private void btn539unlock_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 5; i++)
            {
                cbArr539locks[i].Checked = false;
            }
        }
        private void btnBBunlock_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 10; i++)
            {
                cbArrBBlocks[i].Checked = false;
            }
        }

        //改變程式標題
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch(tabControl1.SelectedIndex)
            {
                case 0:
                    Form1.ActiveForm.Text = "大福彩號碼產生器";
                    break;
                case 2:
                    Form1.ActiveForm.Text = "Bingo Bingo號碼產生器";
                    break;
                case 1:
                    Form1.ActiveForm.Text = "今彩539號碼產生器";
                    break;
            }
        }
        #region Bottom links
        //logo連結至官網
        private void pbLogo_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.taiwanlottery.com.tw/index_new.aspx");
        }
        private void llbNewestResult_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.taiwanlottery.com.tw/result_all.aspx#11");
        }
        private void llbHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.taiwanlottery.com.tw/Lotto/Lotto740/history.aspx");
        }
        private void llbAbout_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string strOutput;
            strOutput = "作者: 李俊威 (Jay)\n此程式為資策會第89期APP開發班之C#課程練習作品。\n2016/05/05 ";
            MessageBox.Show(strOutput, "作者資訊", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        #endregion
        #region 教學頁面
        //教學連結
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.taiwanlottery.com.tw/Lotto740/index.asp");
        }
        private void llblBB_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.taiwanlottery.com.tw/BINGOBINGO/index.asp");
        }
        private void llbl539_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.taiwanlottery.com.tw/DailyCash/index.asp");
        }

        //教學按鈕
        private void btnTutorial_Click(object sender, EventArgs e)
        {
            turnTutorialPage();
        }
        private void btn539tutorial_Click(object sender, EventArgs e)
        {
            turnTutorialPage();
        }
        private void btnBBtutorial_Click(object sender, EventArgs e)
        {
            turnTutorialPage();
        }
        private void turnTutorialPage()
        {
            if (tutPageOn)
            {
                tabControl1.Width -= 200;
                tutPageOn = false;
            }
            else
            {
                tabControl1.Width += 200;
                tutPageOn = true;
            }
        }










        #endregion
    }
}

            


            
            
            