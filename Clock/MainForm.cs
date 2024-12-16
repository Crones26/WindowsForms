using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clock
{
	public partial class MainForm : Form
	{
		private PrivateFontCollection privateFonts; // Чтобы шрифт не удалялся как мусор

		public MainForm()
		{
			InitializeComponent();

			// Установка двойной буферизации
			this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
			this.UpdateStyles();

			// Привязка событий для пунктов меню
			cmBackColor.Click += cmBackColor_Click;
			cmForeColor.Click += cmForeColor_Click;

			labelTime.BackColor = Color.Black;
			this.Location = new Point(Screen.PrimaryScreen.Bounds.Width - this.Width, 100);

			// Подключаем шрифт
			LoadCustomFont();
		}

		void LoadCustomFont()
		{
			privateFonts = new PrivateFontCollection();

			// Путь к шрифту
			string fontPath = @"C:\Users\Cympak\source\repos\WindowsForms\Fonts\digital-7.ttf";

			privateFonts.AddFontFile(fontPath);

			// Применяем шрифт
			Font customFont = new Font(privateFonts.Families[0], 42f, FontStyle.Regular);
			labelTime.Font = customFont;
		}

		void SetVisibility(bool visible)
		{
			cbShowDate.Visible = visible;
			cbShowWeekDay.Visible = visible;
			btnHideControls.Visible = visible;
			this.TransparencyKey = visible ? Color.Empty : this.BackColor;
			this.FormBorderStyle = visible ? FormBorderStyle.FixedToolWindow : FormBorderStyle.None;
			this.ShowInTaskbar = visible;
		}

		private void timer_Tick(object sender, EventArgs e)
		{
			labelTime.Text = DateTime.Now.ToString("hh:mm:ss tt", System.Globalization.CultureInfo.InvariantCulture);

			if (cbShowDate.Checked)
			{
				labelTime.Text += "\n" + DateTime.Now.ToString(" dd. MM. yyyy ");
			}
			if (cbShowWeekDay.Checked)
			{
				labelTime.Text += "\n" + DateTime.Now.DayOfWeek;
			}

			notifyIcon.Text = labelTime.Text;

			// Перерисовываем элемент
			labelTime.Invalidate();
		}

		private void btnHideControls_Click(object sender, EventArgs e)
		{
			SetVisibility(false);
		}

		private void labelTime_DoubleClick(object sender, EventArgs e)
		{
			SetVisibility(true);
		}

		private void cmExit_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void cmTopmost_CheckedChanged(object sender, EventArgs e)
		{
			this.TopMost = cmTopmost.Checked;
		}

		private void cmShowDate_CheckedChanged(object sender, EventArgs e)
		{
			cbShowDate.Checked = cmShowDate.Checked;
		}

		private void cbShowDate_CheckedChanged(object sender, EventArgs e)
		{
			cmShowDate.Checked = cbShowDate.Checked;
		}

		private void cmShowWeekDay_CheckedChanged(object sender, EventArgs e)
		{
			cbShowWeekDay.Checked = cmShowWeekDay.Checked;
		}

		private void cbShowWeekDay_CheckedChanged(object sender, EventArgs e)
		{
			cmShowWeekDay.Checked = cbShowWeekDay.Checked;
		}

		private void notifyIcon_DoubleClick(object sender, EventArgs e)
		{
			if (!this.TopMost)
			{
				this.TopMost = true;
				this.TopMost = false;
			}
		}

		private void cmBackColor_Click(object sender, EventArgs e)
		{
			using (ColorDialog colorDialog = new ColorDialog())
			{
				if (colorDialog.ShowDialog() == DialogResult.OK)
				{
					labelTime.BackColor = colorDialog.Color; // Устанавливаем цвет фона
				}
			}
		}

		private void cmForeColor_Click(object sender, EventArgs e)
		{
			using (ColorDialog colorDialog = new ColorDialog())
			{
				if (colorDialog.ShowDialog() == DialogResult.OK)
				{
					labelTime.ForeColor = colorDialog.Color; // Устанавливаем цвет шрифта
				}
			}
		}
	}
}