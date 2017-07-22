using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace paint_App
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            g = pictureBox1.CreateGraphics();
            gr = pictureBox1.CreateGraphics();

            pictureBox1.Refresh();
        }
        //******************
        String asd;
        int max=16;
        Image imageor;
        int x = 1;
        int y = 0;
        public static string writeText = string.Empty;

        Pen pp = new Pen(Color.Black, 3);
        Brush brush = new SolidBrush(Color.Red);

        private Bitmap OriginalImage = null;
        private int X0, Y0, X1, Y1;
        private bool SelectingArea = false;
        private Bitmap SelectedImage = null;
        private Graphics SelectedGraphics = null;
        private Rectangle SelectedRect;
        private bool MadeSelection = false;

        //********************
        Rectangle currentRect;
        OpenFileDialog ofd = new OpenFileDialog();

        bool startPaint = false;
        Graphics g;
        int? initX = null;
        int? initY = null;
        bool drawRectangle = false;
        bool er = false;
        bool br = false;
        bool selectt = false;
        bool write = false;
            

        Font newfont;
        bool drawCircle = false;
        private Graphics gr;
        private Point currentMousePos;
        private Point initialMousePos;
        private bool img=false;

        private void pictureBox1_Click(object sender, EventArgs e)
        {
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (selectt) { 

            // Do nothing if we're not selecting an area.
            if (!SelectingArea) return;

            // Generate the new image with the selection rectangle.
            X1 = e.X;
            Y1 = e.Y;

            // Copy the original image.
            SelectedGraphics.DrawImage(OriginalImage, 0, 0);

            // Draw the selection rectangle.
            using (Pen select_pen = new Pen(Color.WhiteSmoke))
            {

                select_pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                Rectangle rect = MakeRectangle(X0, Y0, X1, Y1);
                SelectedGraphics.DrawRectangle(select_pen, rect);
                   






                }
                pictureBox1.Refresh();
               
            }




            currentMousePos = e.Location;
           pictureBox1.Refresh();


            if (startPaint)
            {
                Bitmap bmp = new Bitmap(pictureBox1.Image);
                Graphics gr = Graphics.FromImage(bmp);
                //Setting the Pen BackColor and line Width
                Pen p = new Pen(Color.Black, max);
                //Drawing the line.
                gr.DrawLine(pp, new Point(initX ?? e.X, initY ?? e.Y), new Point(e.X, e.Y));
                pictureBox1.Image = bmp;
                initX = e.X;
                initY = e.Y;

            }
            if (er)
            {
                Bitmap bmp = new Bitmap(pictureBox1.Image);
                Graphics gr = Graphics.FromImage(bmp);
                Brush brush2 = new SolidBrush(Color.White);
                gr.FillEllipse(brush2, new Rectangle(initX ?? e.X, initY ?? e.Y, max, max));
                pictureBox1.Image = bmp;


            }
            if (br)
            {
                Bitmap bmp = new Bitmap(pictureBox1.Image);
                Graphics gr = Graphics.FromImage(bmp);
                gr.FillEllipse(brush, new Rectangle(initX ?? e.X, initY ?? e.Y, max, max));
                pictureBox1.Image = bmp;


            }

            if (drawRectangle)
             {
               
                using (Pen p2 = new Pen(Color.Blue, 3))
                {
                    
                     currentRect = Rectangle.FromLTRB(this.initialMousePos.X,
                                                   this.initialMousePos.Y,
                                                   currentMousePos.X,
                                                   currentMousePos.Y);
                    Brush brush2 = new SolidBrush(Color.Black);

                    

                    startPaint = false;
                    // gr.FillRectangle(p2, currentRect);

                }
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            pictureBox1.Refresh();
            if (selectt) { 

            // Do nothing if we're not selecting an area.
            if (!SelectingArea) return;
            SelectingArea = false;

            // Stop selecting.
            SelectedGraphics = null;

            // Convert the points into a Rectangle.
            SelectedRect = MakeRectangle(X0, Y0, X1, Y1);
            MadeSelection = (
                (SelectedRect.Width > 0) &&
                (SelectedRect.Height > 0));
                
                // Enable the menu items appropriately.
            }
            Bitmap bmp = new Bitmap(pictureBox1.Image);
            Graphics gr = Graphics.FromImage(bmp);

            Pen p2 = new Pen(Color.Blue, 3);
            Pen p3 = pp;
            if (x > y){
                gr.DrawRectangle(p3, currentRect);
                pictureBox1.Image = bmp;
                y++;
            }

            selectt = false;
            drawRectangle = false;
            startPaint = false;
            er = false;
            br = false;
            initX = null;
            initY = null;
            write = false;

        }
        private void Form1_Load(object sender, EventArgs e)
        {




            


                
            
        }
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {   //********************************
            

               
                
                if (writeText !="") {
if (write)
            {
                FontDialog font = new FontDialog();
                font.ShowColor = true;
                if (font.ShowDialog() == DialogResult.OK)
                {

                        Graphics g = Graphics.FromHwnd(pictureBox1.Handle);
                        Brush brush45 = new SolidBrush(font.Color);

                        Bitmap bmp = new Bitmap(pictureBox1.Image);
                        Graphics gr = Graphics.FromImage(bmp);
                        gr.DrawString(writeText, font.Font, brush45, new PointF(e.X, e.Y));
                        pictureBox1.Image = bmp;

                         
                    
                                }
                }

               

                
               
            }
            write = false;

            this.initialMousePos = e.Location;

            if (checkBox3.Checked)
            {// Save the starting point.
                SelectingArea = true;
                X0 = e.X;
                Y0 = e.Y;
OriginalImage = new Bitmap(pictureBox1.Image);
                this.KeyPreview = true;
                // Make the selected image.
                SelectedImage = new Bitmap(OriginalImage);
                SelectedGraphics = Graphics.FromImage(SelectedImage);
                pictureBox1.Image = SelectedImage;
                startPaint = false;
                selectt = true;
            }

            //********************************
            

            else if (checkBox2.Checked)
            {

                br = true;
                startPaint = false;

            }
            else if (checkBox1.Checked) {

                er = true;
                startPaint = false;

            }else if(checkBox4.Checked)
                startPaint = true;


            else if (drawCircle)
            {
                Brush brush2 = new SolidBrush(Color.Black);
                Bitmap bmp = new Bitmap(pictureBox1.Image);
                Graphics gr = Graphics.FromImage(bmp);
                Pen p = new Pen(Color.Red);
                p.Width = 5.0f;
                gr.DrawEllipse(pp, e.X, e.Y, max*2, max*2);

                pictureBox1.Image = bmp;

                startPaint = false;
                drawCircle = false;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            drawRectangle = true;
            x++;

        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            /*   if (drawRectangle)
               {

                   using (Pen p = new Pen(Color.Blue, 3.0F))
                   {
                       // Create a rectangle with the initial cursor location as the upper-left
                       // point, and the current cursor location as the bottom-right point
                       Rectangle currentRect = Rectangle.FromLTRB(
                                                                  this.initialMousePos.X,
                                                                  this.initialMousePos.Y,
                                                                  currentMousePos.X,
                                                                  currentMousePos.Y);

                       // Draw the rectangle
                       e.Graphics.DrawRectangle(p, currentRect);
                   }
               }*/
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            drawCircle = true;

        }
        
        

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 27)
            {
                if (!SelectingArea) return;
                SelectingArea = false;

                // Stop selecting.
                SelectedImage = null;
                SelectedGraphics = null;
                pictureBox1.Image = OriginalImage;
                pictureBox1.Refresh();

                // There is no selection.
                MadeSelection = false;

                // Enable the menu items appropriately.
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            selectt = true;
            startPaint = false;

        }
      /*  private void EnableMenuItems()
        {
            mnuEditCopy.Enabled = MadeSelection;
            mnuEditCut.Enabled = MadeSelection;
            mnuEditPasteStretched.Enabled = MadeSelection;
            mnuEditPasteCentered.Enabled = MadeSelection;
        }*/

        // Return a Rectangle with these points as corners.
        private Rectangle MakeRectangle(int x0, int y0, int x1, int y1)
        {
            return new Rectangle(
                Math.Min(x0, x1),
                Math.Min(y0, y1),
                Math.Abs(x0 - x1),
                Math.Abs(y0 - y1));
        }

        private void button6_Click(object sender, EventArgs e)
        {
            // Do nothing if the clipboard doesn't hold an image.
            if (!Clipboard.ContainsImage()) return;

            // Get the clipboard's image.
            Image clipboard_image = Clipboard.GetImage();

            // Figure out where to put it.
            int cx = SelectedRect.X + (SelectedRect.Width - clipboard_image.Width) / 2;
            int cy = SelectedRect.Y + (SelectedRect.Height - clipboard_image.Height) / 2;
            Rectangle dest_rect = new Rectangle(
                cx, cy,
                clipboard_image.Width,
                clipboard_image.Height);

            // Copy the new image into position.
            using (Graphics gr = Graphics.FromImage(OriginalImage))
            {
                gr.DrawImage(clipboard_image, dest_rect);
            }

           

            // Display the result.
            pictureBox1.Image = OriginalImage;
            pictureBox1.Refresh();

            SelectedImage = null;
            SelectedGraphics = null;
            MadeSelection = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            CopyToClipboard(SelectedRect);
            System.Media.SystemSounds.Beep.Play();
        }

        private void saveToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SaveFile();
        }

        private void openToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Do You want to save the changes made to " + this.Text, "Save", MessageBoxButtons.YesNoCancel);
            if (dr == DialogResult.Yes)
            {
                SaveFile();
                OpenFile();
            }
            else if (dr == DialogResult.No)
            {
                OpenFile();
            }
        
            else
                OpenFile();
    }

        private void button4_Click(object sender, EventArgs e)
        {
            // Copy the selection to the clipboard.
            CopyToClipboard(SelectedRect);

            // Blank the selected area in the original image.
            using (Graphics gr = Graphics.FromImage(OriginalImage))
            {
                using (SolidBrush br = new SolidBrush(pictureBox1.BackColor))
                {
                    gr.FillRectangle(br, SelectedRect);
                }
            }

            // Display the result.
            SelectedImage = new Bitmap(OriginalImage);
            pictureBox1.Image = SelectedImage;

            // Enable the menu items appropriately.
            SelectedImage = null;
            SelectedGraphics = null;
            MadeSelection = false;

            System.Media.SystemSounds.Beep.Play();
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            startPaint = false;

        }

        private void button7_Click(object sender, EventArgs e)
        {
            pictureBox1.Image.RotateFlip(RotateFlipType.RotateNoneFlipX);
            pictureBox1.Refresh();

        }

        private void button8_Click(object sender, EventArgs e)
        {
            pictureBox1.Image.RotateFlip(RotateFlipType.RotateNoneFlipY);
            pictureBox1.Refresh();

        }

        private void newToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Do you want to save?", "New Doc", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

            if (dr == DialogResult.No)

                pictureBox1.Image=Image.FromFile("C:/Users/oguzhan/Documents/Visual Studio 2015/Projects/paint_App/Adsız.png");
            else if (dr == DialogResult.Yes)
            {

                SaveFile();
                pictureBox1.Image = Image.FromFile("C:/Users/oguzhan/Documents/Visual Studio 2015/Projects/paint_App/Adsız.png");

            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            ColorDialog c = new ColorDialog();
            if (c.ShowDialog() == DialogResult.OK)
            {
                button9.BackColor = c.Color;
                pp = new Pen(c.Color, 3);
                brush = new SolidBrush(c.Color);

            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            max++;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            max--;
        }

        private void CopyToClipboard(Rectangle src_rect)
        {
            // Make a bitmap for the selected area's image.
            Bitmap bm = new Bitmap(src_rect.Width, src_rect.Height);
            
            // Copy the selected area into the bitmap.
            using (Graphics gr = Graphics.FromImage(bm))
            {
                Rectangle dest_rect = new Rectangle(0, 0, src_rect.Width, src_rect.Height);
                gr.DrawImage(OriginalImage, dest_rect, src_rect, GraphicsUnit.Pixel);
            }

            // Copy the selection image to the clipboard.
            Clipboard.SetImage(bm);
        }

       

        private void SaveFile()
        {
            SaveFileDialog saveFile1 = new SaveFileDialog();

            saveFile1.DefaultExt = "*.png";
            saveFile1.Filter = "Png Image|*.png";



            if (saveFile1.ShowDialog() == System.Windows.Forms.DialogResult.OK &&
               saveFile1.FileName.Length > 0)
            {
                pictureBox1.Image.Save(saveFile1.FileName, ImageFormat.Png);
                MessageBox.Show("save successfully", "Address File : " + saveFile1.FileName, MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void button12_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Zoom(pictureBox1.Image, new Size(10, 10));

        }

        private void button13_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Zoomout(pictureBox1.Image, new Size(10,10));

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            write = true;

            Form2 fr = new Form2();
            fr.ShowDialog();
        }

        private void OpenFile()
        {
            


            if (ofd.ShowDialog() == DialogResult.Cancel)
            {
                pictureBox1.Update();
            }
            else
            {
                pictureBox1.Image = Image.FromFile(ofd.FileName);
                this.Text = ofd.FileName;
                asd = ofd.FileName;

            }img = true;

        }
           
        Image Zoom(Image im,Size sz)
        {

            Bitmap bbb = new Bitmap(im,im.Width+(im.Width*sz.Width/100),im.Height+(im.Height*sz.Height/100));
            Graphics grp = Graphics.FromImage(bbb);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            return bbb;



        }
        Image Zoomout(Image im, Size sz)
        {

            Bitmap bbb = new Bitmap(im, im.Width - (im.Width * sz.Width / 100), im.Height - (im.Height * sz.Height / 100));
            Graphics grp = Graphics.FromImage(bbb);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            return bbb;



        }

    }
}
