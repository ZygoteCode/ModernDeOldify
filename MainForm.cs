using ColorfulSoft.DeOldify;
using MetroSuite;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

public partial class MainForm : MetroForm
{
    private Image _original, _stable, _artistic;

    public MainForm()
    {
        InitializeComponent();
        Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.RealTime;

        foreach (ProcessThread thread in Process.GetCurrentProcess().Threads)
        {
            thread.PriorityLevel = ThreadPriorityLevel.Highest;
        }

        DeOldifyArtistic.Initialize();
        DeOldifyStable.Initialize();
    }

    private void guna2Button1_Click(object sender, System.EventArgs e)
    {
        openFileDialog1.FileName = "";

        if (openFileDialog1.ShowDialog().Equals(DialogResult.OK))
        {
            _original = Image.FromFile(openFileDialog1.FileName);
            pictureBox1.Image = Utils.ResizeImageKeepingAspectRatio(_original, pictureBox1.Width, pictureBox2.Height);

            guna2Button2.Enabled = true;
            guna2Button3.Enabled = true;
            guna2Button4.Enabled = true;
        }
    }

    private void guna2Button2_Click(object sender, System.EventArgs e)
    {
        saveFileDialog1.FileName = "";

        if (saveFileDialog1.ShowDialog().Equals(DialogResult.OK))
        {
            _stable.Save(saveFileDialog1.FileName);
        }
    }

    private void guna2Button3_Click(object sender, System.EventArgs e)
    {
        saveFileDialog1.FileName = "";

        if (saveFileDialog1.ShowDialog().Equals(DialogResult.OK))
        {
            _artistic.Save(saveFileDialog1.FileName);
        }
    }

    private void guna2Button4_Click(object sender, System.EventArgs e)
    {
        _stable = DeOldifyStable.Colorize((Bitmap)_original);
        _artistic = DeOldifyArtistic.Colorize((Bitmap)_original);

        pictureBox2.Image = Utils.ResizeImageKeepingAspectRatio(_stable, pictureBox2.Width, pictureBox2.Height);
        pictureBox3.Image = Utils.ResizeImageKeepingAspectRatio(_artistic, pictureBox3.Width, pictureBox3.Height);

        MessageBox.Show("Succesfully processed your image, enjoy the results!", "ModernDeOldify",
            MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}