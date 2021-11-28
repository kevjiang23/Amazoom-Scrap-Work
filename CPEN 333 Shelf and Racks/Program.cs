using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;

namespace CPEN_333_Shelf_and_Racks
{
    class Program
    {
        static void Main(string[] args)
        {
            int numCols = 8;
            int numRows = 5;
            int numShelves = 5;

            int totalGrids = 0; // Indicator for how many grids were generated (to help error checking)

            // Generate 2D warehouse grid of racks; can keep track of whether a rack is there by 
            // checking occupancy attribute in Rack class
            Rack[,] grid = new Rack[numRows, numCols];

            // Generate racks for outer columns (columns of A and H)
            for (int i = 0; i < numRows; i++) { 
            
                grid[i, 0] = new Rack(1, 1, numShelves); // Column A only has right shelves
                grid[i, numCols - 1] = new Rack(1, 0, numShelves); // Column H only has left shelves
                Console.WriteLine("Grid {0} by {1} has been initialized!", i, 0);
                Console.WriteLine("Grid {0} by {1} has been initialized!", i, (numCols - 1));
                totalGrids += 2;
            }

            // Generate inner racks with shelves on both sides (columns B - G)
           for (int j = 1; j < numCols - 1; j++)
            {
                for (int i = 0; i < numRows; i++)
                {
                    grid[i, j] = new Rack(1, 0, numShelves); // Left shelves
                    grid[i, j] = new Rack(1, 1, numShelves); // Right shelves
                    Console.WriteLine("Grid {0} by {1} has been initialized!", i, j);
                    totalGrids++;
                }
            }

            Console.WriteLine("Warehouse grid has been fully generated, with {0} grids in total", totalGrids);
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
            this.shelf_levels = 0;
            this.shelves = Array.Empty<Shelf>();
        }
    }

    public class Shelf
    {
        public int level;
        public double weight_capacity;
        public double volume_capacity;

        // Shelf constructors
        public Shelf(int level, double weight_capacity, double volume_capacity)
        {
            this.level = level;
            this.weight_capacity = weight_capacity;
            this.volume_capacity = volume_capacity;
        }

        public Shelf()
        {
            this.level = 1;
            this.weight_capacity = 5000;
            this.volume_capacity = 10000;
        }

    }

    public class Tile
    {
        public int rack_occupancy;
        public int robot_occupancy;
        // Nested classes
    }
}
