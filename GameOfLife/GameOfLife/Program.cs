using System;

namespace GameOfLife
{
    class Program
    {
        static void Main(string[] args)
        {
            string startingPattern = args.Length > 0 ? args[0].ToLower(): "random";
            
            int iterations = 4;
            int size = 8;
        
            int[,] currentIteration = SetupGrid(size, startingPattern);
            
            PrintIteration(currentIteration);


            for (int i = 1; i < iterations; i++)
            {
                currentIteration = ReturnNextIteration(currentIteration, size);
                PrintIteration(currentIteration);
            }
        }

        static int[,] SetupGrid(int size, string startingPattern)
        {
            int[,] grid = new int[size, size];
            switch (startingPattern)
            {
                case "blinker":
                    grid[3, 4] = 1;
                    grid[4, 4] = 1;
                    grid[5, 4] = 1;
                    break;
                case "toad":
                    grid[3, 3] = 1;
                    grid[3, 4] = 1;
                    grid[3, 5] = 1;
                    
                    grid[4, 2] = 1;
                    grid[4, 3] = 1;
                    grid[4, 4] = 1;
                    break;
                case "beacon":
                    grid[1, 5] = 1;
                    grid[2, 5] = 1;
                    grid[1, 6] = 1;
                    grid[2, 6] = 1;
                    
                    grid[3, 3] = 1;
                    grid[4, 3] = 1;
                    grid[3, 4] = 1;
                    grid[4, 4] = 1;
                    break;
                
                default:
                    Random random = new Random();
                    for (int i = 0; i < grid.GetLength(0); i++)
                    {
                        for (int j = 0; j < grid.GetLength(1); j++)
                        {
                            grid[i, j] = random.Next(2);
                        }
                    }

                    break;
            }

            return grid;
        }

        static int[,] ReturnNextIteration(int[,] currentIteration, int size)
        {
            int[,] nextIteration = new int[size, size];

            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    int liveNeighbors = GetLivingNeighbors(currentIteration, row, col, size);

                    if (currentIteration[row, col] == 1)
                    {
                        if (liveNeighbors == 2 || liveNeighbors == 3)
                        {
                            nextIteration[row, col] = 1;
                        }
                        else
                        {
                            nextIteration[row, col] = 0;
                        }
                    }
                    else
                    {
                        if (liveNeighbors == 3)
                        {
                            nextIteration[row, col] = 1;
                        }
                    }
                }
            }

            return nextIteration;
        }

        static void PrintIteration(int[,] iteration)
        {
            int rows = iteration.GetLength(0);
            int cols = iteration.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (iteration[i, j] == 1)
                    {
                        Console.Write("X"+ "  ");
                    }
                    else
                    {
                        Console.Write("."+ "  ");
                    }
                }
                Console.WriteLine();
            }
        }
        
        static int GetLivingNeighbors(int[,] board, int row, int col, int size)
        {
            int liveNeighbors = 0;
            
            // stores the computed offsets of the neighboring cells
            int[,] neighborOffsets = new int[,]
            {
                { -1, -1 }, { -1, 0 }, { -1, 1 }, { 0, -1 }, { 0, 1 }, { 1, -1 }, { 1, 0 }, { 1, 1 }
            };
            
            for (int i = 0; i < neighborOffsets.GetLength(0); i++)
            {
                int dRow = neighborOffsets[i, 0];
                int dCol = neighborOffsets[i, 1];

                // allows it to be a toridial grid using the modulo operator
                int neighborRow = (row + dRow + size) % size;
                int neighborCol = (col + dCol + size) % size;

                if (board[neighborRow, neighborCol] == 1)
                    liveNeighbors++;
            }
            
            return liveNeighbors;
        }
        
    }
}