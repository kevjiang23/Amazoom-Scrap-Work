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
            // checking occupancy attribute in tile class
            Tile[,] grid = new Tile[numRows, numCols];

            // Generate tiles for empty grids
            for (int j = 0; j < numCols; j++)
            {
                grid[0, j] = new Tile();
                grid[numRows - 1, j] = new Tile();
                totalGrids += 2;
            }

            // Generate tiles for outer columns with only ONE rack (index 1-3 of columns A and H)
            for (int i = 1; i < numRows - 1; i++)
            {

                grid[i, 0] = new Tile(1, 0, 1, numShelves);  // Column A only has right shelves
                grid[i, numCols - 1] = new Tile(1, 0, 0, numShelves); // Column H only has left shelves
                Console.WriteLine("Grid {0} by {1} has been initialized!", i, 0);
                Console.WriteLine("Grid {0} by {1} has been initialized!", i, (numCols - 1));
                totalGrids += 2;
            }

            // Generate inner tiles with racks on both sides (columns B - G)
            for (int j = 1; j < numCols - 1; j++)
            {
                for (int i = 1; i < numRows - 1; i++)
                {
                    grid[i, j] = new Tile(1, 0, 2, numShelves); // All inner columns have shelves on both sides
                    Console.WriteLine("Grid {0} by {1} has been initialized!", i, j);
                    totalGrids++;
                }
            }
            Console.WriteLine("Warehouse grid has been fully generated, with {0} grids in total", totalGrids);
        }
    }

    public class Rack // Each rack will contain shelves; a rack can be on the left side, right side or both sides of a grid
    {
        public int shelf_levels;
        public Shelf[] shelves;

        // Rack constructors
        public Rack(int shelf_levels)
        {
            this.shelf_levels = shelf_levels;
            this.shelves = new Shelf[shelf_levels];
        }
        public Rack()
        {
            this.shelf_levels = 0;
            this.shelves = new Shelf[0]; 
        }
    }

    public class Shelf // Constructing the rack class will generate a series of shelves; helps contain items and keep track of storage space
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

    public class Tile // Each tile will be the building block of the warehouse grid; will keep track of rack and robot locations
    {
        public int rack_occupancy; // 1) Check rack_occupancy, 2) check side to identify where the racks are located
        public int robot_occupancy;
        public int side; // 0 = left rack, 1 = right rack, 2 = racks on both sides
        public Rack leftRack;
        public Rack rightRack;
        public int shelvesNumber;

    // Tile constructors 
    public Tile(int rack_occupancy, int robot_occupancy, int side, int shelvesNumber)
        {
            this.rack_occupancy = rack_occupancy;
            this.robot_occupancy = robot_occupancy;

            if (side == 0) // side = 0 means only a left rack
                this.leftRack = new Rack(shelvesNumber);
            else if (side == 1) // side = 1 means only a right rack
                this.rightRack = new Rack(shelvesNumber);
            else // side = 2 means shelves on both sides
            {
                this.leftRack = new Rack(shelvesNumber);
                this.rightRack = new Rack(shelvesNumber);
            }
        }
        public Tile()
        {
            this.rack_occupancy = 0;
            this.robot_occupancy = 0;
            this.side = 0;
            this.leftRack = new Rack();
            this.rightRack = new Rack();
        }
    }
}
