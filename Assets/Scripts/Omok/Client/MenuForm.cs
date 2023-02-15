using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class MenuForm : Form
    {
        public MenuForm()
        {
            InitializeComponent();
        }

        private void singlePlayButton_Click(object sender, EventArgs e)
        {
            Hide();//현재 창을 숨겨서 보이지 않도록 한다
            SinglePlayForm singlePlayForm = new SinglePlayForm(); //폼 객체 만들기
            singlePlayForm.FormClosed += new FormClosedEventHandler(childForm_Closed);
            singlePlayForm.Show(); //새로운 창 보이도록
        }
        //single 화면이 닫혔을 때 메뉴화면으로 다시 돌아오도록 할 때
        private void childForm_Closed(object sender, FormClosedEventArgs e)
        {
            Show();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            //프로그램 전체 종료
            System.Windows.Forms.Application.Exit();
        }

        private void multiPlayButton_Click(object sender, EventArgs e)
        {
            Hide();
            MultiPlayForm multiPlayForm = new MultiPlayForm();
            multiPlayForm.FormClosed += new FormClosedEventHandler(childForm_Closed);
            multiPlayForm.Show();
        }
    }
}
