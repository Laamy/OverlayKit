using OverlayKit.Window;
using System.Drawing;
using System.Windows.Forms;

namespace TransparentWinForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            WinHandler.AddTransparencyLayer(Handle); // transparency key is 0,0,0,(0-255)

            BackColor = Color.Black;

            //TransparencyKey - windows forms also has this (though it excludes a few things like the actual layers)
        }
    }
}
