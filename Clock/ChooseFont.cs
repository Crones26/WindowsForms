using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Text;

namespace Clock
{
	public partial class ChooseFontForm : Form
	{
		//public Font font {  get; set; }
		public Font SelectedFont { get; private set; }

		public ChooseFontForm()
		{
			InitializeComponent();
			LoadFonts();
			cbFonts.SelectedIndex = 0;
		}

		void LoadFonts()
		{
			Directory.SetCurrentDirectory("..\\..\\Fonts");
			Console.WriteLine(Directory.GetCurrentDirectory());

			cbFonts.Items.AddRange(GetFontsFormat("*ttf"));
			cbFonts.Items.AddRange(GetFontsFormat("*otf"));
		}
		static string[] GetFontsFormat(string format)
		{
			string[] files = Directory.GetFiles(Directory.GetCurrentDirectory(), format);
			for (int i = 0; i < files.Length; i++)
				files[i] = files[i].Split('\\').Last();
			return files;
		}
		
		private void cbFonts_SelectedIndexChanged(object sender, EventArgs e)
		{
			PrivateFontCollection pcf = new PrivateFontCollection();
			string full_name = $"{Directory.GetCurrentDirectory()}\\{cbFonts.SelectedItem}";
			pcf.AddFontFile(full_name);
			labelExample.Font = new Font(pcf.Families[0],Convert.ToInt32(nudFontSize.Value));
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			//Font = (labelExample.Font);
			SelectedFont = new Font(labelExample.Font.FontFamily, labelExample.Font.Size, labelExample.Font.Style);
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void btnApply_Click(object sender, EventArgs e)
		{
			// Применяем шрифт к labelExample
			PrivateFontCollection pcf = new PrivateFontCollection();
			string full_name = $"{Directory.GetCurrentDirectory()}\\{cbFonts.SelectedItem}";
			pcf.AddFontFile(full_name);
			labelExample.Font = new Font(pcf.Families[0], Convert.ToInt32(nudFontSize.Value));
		}
	}
}
