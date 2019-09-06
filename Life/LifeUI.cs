using System;
using System.Drawing;
using System.Windows.Forms;


namespace Life
{
    public partial class LifeUI : Form
    {
        private const int GRID_BUTTON_DIMENSION_PX = 25;
        private const int GRID_DIMENSION_PX = 450;
        private const int GRID_DIMENSION_CELLS = GRID_DIMENSION_PX / GRID_BUTTON_DIMENSION_PX;

        private static readonly Color ALIVE_CELL_COLOUR = Color.White;
        private static readonly Color DEAD_CELL_COLOUR = Color.DarkGray;

        private Life engine = null;

        public LifeUI()
        {
            InitializeComponent();
            engine = new Life(GRID_DIMENSION_CELLS, GRID_DIMENSION_CELLS);
        }
      
        private void LifeUI_Load(object sender, EventArgs e)
        {
            for (int j = 0; j + GRID_BUTTON_DIMENSION_PX <= GRID_DIMENSION_PX; j += GRID_BUTTON_DIMENSION_PX)
                for (int i = 0; i + GRID_BUTTON_DIMENSION_PX <= GRID_DIMENSION_PX; i += GRID_BUTTON_DIMENSION_PX)
                {
                    Button newButton = new Button();
                    newButton.Size = new Size(GRID_BUTTON_DIMENSION_PX, GRID_BUTTON_DIMENSION_PX);
                    newButton.Location = new Point(i, j);
                    newButton.Click += new EventHandler(ClickCell);
                    gridUI.Controls.Add(newButton);
                }

            UpdateColours();
        }
       
        private void timer_Tick(object sender, EventArgs e)
        {
            engine.Tick();
            generationUI.Text = engine.Ticks.ToString();
            UpdateColours();
        }
       
        private void startUI_Click(object sender, EventArgs e)
        {
            timer.Enabled = true;
            startUI.Enabled = false;
            stopUI.Enabled = true;
        }
        
        private void stopUI_Click(object sender, EventArgs e)
        {
            timer.Enabled = false;
            stopUI.Enabled = false;
            startUI.Enabled = true;
        }
    
        private void clearUI_Click(object sender, EventArgs e)
        {           
            timer.Enabled = false;
            stopUI.Enabled = false;
            startUI.Enabled = true;

            engine = new Life(engine.Height, engine.Width);
            generationUI.Text = engine.Ticks.ToString();

            UpdateColours();
        }
        
        private void ClickCell(object sender, EventArgs e)
        {          
            if (timer.Enabled)
                return;
         
            int buttonLinearIndex = gridUI.Controls.IndexOf(sender as Control);
            int y = buttonLinearIndex / engine.Width;
            int x = buttonLinearIndex % engine.Width;
                  
            engine[y, x] = !engine[y, x];
            ((Button)sender).BackColor =  engine[y, x] ? ALIVE_CELL_COLOUR : DEAD_CELL_COLOUR;
        }
       
        private void UpdateColours()
        {
            for (int linearIndex = 0; linearIndex < gridUI.Controls.Count; ++linearIndex)
                gridUI.Controls[linearIndex].BackColor =
                    engine[linearIndex / engine.Width, linearIndex % engine.Width] ? ALIVE_CELL_COLOUR : DEAD_CELL_COLOUR;
        }
    }
}