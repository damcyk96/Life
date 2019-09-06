using System;


namespace Life
{
    class Life
    {
       
        public Life(int h, int w)
        {
            if (h <= 0 || w <= 0)
                throw new ArgumentOutOfRangeException("Dimensions must be positive!");

            Height = h;
            Width = w;
            Ticks = 0;
            Grid = new bool[h, w];
        }

        public int Height { get; private set; }     
        public int Width { get; private set; }      
        public int Ticks { get; set; }             
        private bool[,] Grid { get; set; }          

        
        public bool this[int y, int x]
        {
            get
            {
                if (IsOutOfBounds(y, x))
                    throw new ArgumentOutOfRangeException("Coordinates must be non-negative!");

                return Grid[y, x];
            }

            set
            {
                if (IsOutOfBounds(y, x))
                    throw new ArgumentOutOfRangeException("Coordinates must be non-negative!");

                Grid[y, x] = value;
            }
        }

        public void Tick()
        {
            bool[,] clone = GetClonedBoard();

            for (var j = 0; j < Height; ++j)
                for (var i = 0; i < Width; ++i)
                    clone[j, i] = GetNextState(j, i);

            Grid = clone;
            ++Ticks;
        }

        private bool IsOutOfBounds(int y, int x)
        {
            return y < 0 || y >= Height || x < 0 || x >= Width;
        }

        private int GetLiveNeighbours(int y, int x)
        {
            if (IsOutOfBounds(y, x))
                throw new ArgumentOutOfRangeException("Coordinates must be non-negative!");

            int count = 0;

            for (var j = y - 1; j < y + 2; ++j)
                for (var i = x - 1; i < x + 2; ++i)
                    if (!IsOutOfBounds(j, i))
                        count += Convert.ToInt32(Grid[j, i]);

            return count - Convert.ToInt32(Grid[y, x]);
        }

        private bool GetNextState(int y, int x)
        {
            if (IsOutOfBounds(y, x))
                throw new ArgumentOutOfRangeException("Coordinates must be non-negative!");

            switch (GetLiveNeighbours(y, x))
            {
                case 2:
                    return Grid[y, x];      
                case 3:
                    return true;            
                default:
                    return false;
            }
        }

        private bool[,] GetClonedBoard()
        {
            bool[,] clone = new bool[Height, Width];

            for (var j = 0; j < Height; ++j)
                for (var i = 0; i < Width; ++i)
                    clone[j, i] = Grid[j, i];

            return clone;
        }
    }
}