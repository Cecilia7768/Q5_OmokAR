using System;
using System.Drawing;
using UnityEngine;

public partial class SinglePlayForm : MonoBehaviour
{
    //15*15 바둑판 형태로 구성
    private const int rectSize = 33; //오목판 셀 크기
    private const int edgeCount = 15; //오목판 선 개수

    private enum Horse { none = 0, BLACK, WHITE };
    private Horse[,] board = new Horse[edgeCount, edgeCount]; //2차원 배열 형태 만들기
    private Horse nowPlayer = Horse.BLACK; //기본적으로 검은돌 먼저 수행

    private bool playing = false; //게임 진행중 체크

    //승리판정함수
    private bool judge()
    {
        for (int i = 0; i < edgeCount - 4; i++) //가로  11,* ~ 15,* 까지 가능
            for (int j = 0; j < edgeCount; j++)
                if (board[i, j] == nowPlayer && board[i + 1, j] == nowPlayer && board[i + 2, j] == nowPlayer &&
                   board[i + 3, j] == nowPlayer && board[i + 4, j] == nowPlayer)
                    return true; //승리
        for (int i = 0; i < edgeCount; i++) //세로  *,11 ~ *,15까지 가능
            for (int j = 0; j < edgeCount - 4; j++)
                if (board[i, j] == nowPlayer && board[i, j + 1] == nowPlayer && board[i, j + 2] == nowPlayer && board[i, j + 3] == nowPlayer &&
                    board[i, j + 4] == nowPlayer)
                    return true;
        for (int i = 0; i < edgeCount - 4; i++) //Y=X 직선
            for (int j = 0; j < edgeCount - 4; j++)
                if (board[i, j] == nowPlayer && board[i + 1, j + 1] == nowPlayer && board[i + 2, j + 2] == nowPlayer &&
                    board[i + 3, j + 3] == nowPlayer && board[i + 4, j + 4] == nowPlayer)
                    return true;
        for (int i = 4; i < edgeCount; i++)
            for (int j = 0; j < edgeCount - 4; j++)
                if (board[i, j] == nowPlayer && board[i - 1, j + 1] == nowPlayer && board[i - 2, j + 2] == nowPlayer &&
                    board[i - 3, j + 3] == nowPlayer && board[i - 4, j + 4] == nowPlayer)
                    return true;
        return false;
    }
    //게임 다시 초기화
    private void refresh()
    {
        this.boardPicture.Refresh();
        for (int i = 0; i < edgeCount; i++)
        {
            for (int j = 0; j < edgeCount; j++)
            {
                board[i, j] = Horse.none;
            }
        }
    }

    private void playButton_Click(object sender, EventArgs e)
    {
        if (!playing) //게임 진행중이 아니라면
        {
            refresh(); //화면 초기화
            playing = true;
            playButton.Text = "재시작";
            status.Text = nowPlayer.ToString() + " 플레이어의 차례입니다.";
        }
        else //게임 실행중
        {
            refresh();
            status.Text = "게임이 재시작 되었습니다.";
        }
    }

    //오목판 클릭시 발생 이벤트
    private void boardPicture_MouseDown(object sender, MouseEventArgs e)
    {
        if (!playing)
        {
            MessageBox.Show("게임을 시작해주세요");
            return;
        }
        Graphics g = this.boardPicture.CreateGraphics(); //그림 그리기 위해 그래픽스 객체 만들기
        int x = e.X / rectSize; //몇 번째 셀 선택했는지 
        int y = e.Y / rectSize;
        //모든 셀의 위치는 0- 14
        if (x < 0 || y < 0 || x >= edgeCount || y >= edgeCount)
        {
            MessageBox.Show("테두리 벗어날 수 없음");
            return;
        }
        //MessageBox.Show(x + ", " + y);
        if (board[x, y] != Horse.none) return; //놓으려는 곳에 아무것도 없어야 한다.
        board[x, y] = nowPlayer;
        if (nowPlayer == Horse.BLACK)
        {
            SolidBrush brush = new SolidBrush(Color.Black);
            g.FillEllipse(brush, x * rectSize, y * rectSize, rectSize, rectSize);
        }
        else
        {
            SolidBrush brush = new SolidBrush(Color.White);
            g.FillEllipse(brush, x * rectSize, y * rectSize, rectSize, rectSize);
        }
        if (judge())
        {
            status.Text = nowPlayer.ToString() + "플레이어 승리";
            playing = false;
            playButton.Text = "게임시작";
        }
        else //오목이 만들어지지 않은 상태라면
        {
            nowPlayer = ((nowPlayer == Horse.BLACK) ? Horse.WHITE : Horse.BLACK);
            status.Text = nowPlayer.ToString() + "플레이어 차례입니다.";
        }
    }

    //오목판이 처음에 어떻게 구성되는지 정의
    private void boardPicture_Paint(object sender, PaintEventArgs e)
    {
        Graphics gp = e.Graphics;
        Color lineColor = Color.Black; //오목판의 선 색
        Pen p = new Pen(lineColor, 2);
        //오목판 테두리 그리기
        gp.DrawLine(p, rectSize / 2, rectSize / 2, rectSize / 2, rectSize * edgeCount - rectSize / 2); // 좌측
        gp.DrawLine(p, rectSize / 2, rectSize / 2, rectSize * edgeCount - rectSize / 2, rectSize / 2); // 상측
        gp.DrawLine(p, rectSize / 2, rectSize * edgeCount - rectSize / 2, rectSize * edgeCount - rectSize / 2, rectSize * edgeCount - rectSize / 2); // 하측
        gp.DrawLine(p, rectSize * edgeCount - rectSize / 2, rectSize / 2, rectSize * edgeCount - rectSize / 2, rectSize * edgeCount - rectSize / 2); // 우측
        p = new Pen(lineColor, 1);
        //대각선 방향으로 이동하면서 십자가 모양의 선 그리기
        for (int i = rectSize + rectSize / 2; i < rectSize * edgeCount - rectSize / 2; i += rectSize)
        {
            gp.DrawLine(p, rectSize / 2, i, rectSize * edgeCount - rectSize / 2, i);
            gp.DrawLine(p, i, rectSize / 2, i, rectSize * edgeCount - rectSize / 2);
        }
    }
}
