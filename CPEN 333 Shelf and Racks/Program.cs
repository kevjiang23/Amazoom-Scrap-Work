using System;
using System.Linq;
using System.Collections.Generic;

namespace CPEN_333_Shelf_and_Racks
{
    class Program
    {
        static void Main(string[] args)
        {
            int racksPerColumn = 3;
            int numCols = 8;
            int numRows = 5;
            int numShelves = 5;

            // Generate 2D warehouse grid of racks; can keep track of whether a rack is there by 
            // checking occupancy attribute in Rack class
            Rack[,] grid = new Rack[numRows, numCols];

            // Generate racks for outer columns
            for (int i = 2; i < numRows; i++)
            {
                grid[i, 1] = new Rack(1, 1, numShelves);
                grid[i, numCols] = new Rack(1, 1, numShelves);
            }

            // Generate inner racks (columns B - G)
            for (int i = 2; i < numCols; i++)
            {
                for (int j = 2; i < numRows; i++)
                {
                    grid[i, j] = new Rack(1, 0, numShelves); // Left shelves
                    grid[i, j] = new Rack(1, 1, numShelves); // Right shelves
                }
            }
  
            Console.WriteLine("Hello World!");
        }
    }

    public class Rack
    {
        public int occupancy;
        public int side;
        public int shelf_levels;
        public Shelf[] shelves;
    
        // Rack constructors
        public Rack(int occupancy, int side, int shelf_levels)
        {
            this.occupancy = occupancy;
            this.side = side;
            this.shelf_levels = shelf_levels;
            this.shelves = new Shelf[shelf_levels];
        }
        public Rack()
        {
            this.occupancy = 0;
            this.side = 0; // 0 = left, 1 = right
            this.shelf_levels = 5;
            this.shelves = new Shelf[5];
        }
    }

    public class Shelf
    {
        public int level;
        public double weight_capacity;

        // Shelf constructors
        public Shelf(int level, double weight_capacity)
        {
            this.level = level;
            this.weight_capacity = weight_capacity;
        }

        public Shelf()
        {
            this.level = 1;
            this.weight_capacity = 5000;
        }
    }
}
