using System;
using System.Collections.Generic;

namespace DungeonOfK
{


    class Character
    {
        public string name { get; set; }
        public Point location { get; set; }
        public string shape { get; set; }

        public Character(string Name, Point Location)
        {
            name = Name;
            location = Location;
            shape = name.Substring(0, 1);
        }



    }

    public struct Point
    {
        public int x;
        public int y;
        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }




    class Program
    {

        static void PrintMap(int width, int height, List<Character> characters, Dictionary<Point, string> grid, List<Point> walls, Point key, Point door)
        {
            Console.Clear();

            string top = " ";
            string line = "|"; // will still need to print this out first

            for (int i = 0; i < width; i++)
            {
                top += "_ ";
                // line += "_|";
            }

            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine();
            }

            int formatSpacing = (Console.WindowWidth / 2 + top.Length / 2);
            top = String.Format("{0," + formatSpacing + "}", top);
            Console.WriteLine(top); // this can stay the same


            for (int i = 0; i < height; i++)
            {
                line = "|";
                for (int n = 0; n < width; n++)
                {
                    Point loopPoint = new Point(n, i);

                    if (walls.Contains(loopPoint))
                    {
                        grid[loopPoint] = "||";
                    }
                    else
                    {

                        foreach (Character character in characters)
                        {
                            if (character.location.x == n && character.location.y == i)
                            {
                                grid[loopPoint] = character.shape + "|";
                            }
                            else if (loopPoint.x == key.x && loopPoint.y == key.y)
                            {
                                grid[loopPoint] = "K|";
                            }
                            else if (loopPoint.x == door.x && loopPoint.y == door.y)
                            {
                                grid[loopPoint] = "D|";
                            }
                            else
                            {
                                grid[loopPoint] = "_|";
                            }
                        }
                    }



                    line += grid[loopPoint];
                }
                line = String.Format("{0, " + formatSpacing + "}", line);
                Console.WriteLine(line);
            }
        }

        static void MoveCharacter()
        {

        }

        static void Main(string[] args)
        {


            //  _ _ 
            // |_|_|   (0, 0) and (1, 0)
            // |_|_|   (0, 1) and (1, 1)

            // width of 2 boxes, 2 x 2 + 1 to get total length
            // height of 2 boxes, 3 total lines
            // first line needs just the width number of underscores with spaces on all sides
            // second line needs: pipe, underscore, pipe, underscore, pipe


            // need to fill the dictionary with the tuples, 


            // should store the boxes somewhere

            //Console.Write("Enter width: ");
            //int width = Int32.Parse(Console.ReadLine());
            //Console.Write("Enter height: ");
            //int height = Int32.Parse(Console.ReadLine());

            int width = 10;
            int height = 10;


            int windowMid = Console.WindowWidth / 2;




            Dictionary<Point, string> grid = new Dictionary<Point, string>();

            for (int i = 0; i < height; i++)
            {
                for (int n = 0; n < width; n++)
                {
                    grid.Add(new Point(n, i), "");
                }
            }

            List<Character> characters = new List<Character>();

            //Console.Write("Enter your name: ");
            //string name = Console.ReadLine();
            string name = "Player";
            //Console.Write("Enter your X coordinate: ");
            //int x = Int32.Parse(Console.ReadLine());
            //Console.Write("Enter your Y coordinate: ");
            //int y = Int32.Parse(Console.ReadLine());

            int x = 1;
            int y = 1;

            List<Point> walls = new List<Point>();

            for (int i = 0; i <= 9; i++)
            {
                walls.Add(new Point(0, i));
                walls.Add(new Point(i, 0));
                walls.Add(new Point(9, i));
                walls.Add(new Point(i, 9));
            }
            for (int i = 1; i <= 7; i++)
            {
                walls.Add(new Point(i, 2));
            }
            for (int i = 2; i <= 8; i++)
            {
                walls.Add(new Point(i, 4));
            }
            walls.Add(new Point(2, 6));
            walls.Add(new Point(3, 6));
            walls.Add(new Point(3, 7));
            walls.Add(new Point(3, 8));
            walls.Add(new Point(5, 6));
            walls.Add(new Point(6, 6));
            walls.Add(new Point(7, 6));
            walls.Add(new Point(5, 7));
            walls.Add(new Point(5, 8));
            walls.Add(new Point(5, 9));

            Point key = new Point(2, 8);
            Point door = new Point(6, 8);

            bool hasKey = false;


            Character me = new Character(name, new Point(x, y));
            characters.Add(me);

            bool done = false;
            while (!done)
            {
                PrintMap(width, height, characters, grid, walls, key, door);

                Console.WriteLine();


                // options to move, UP, DOWN, LEFT, RIGHT
                string whichWay = "Which direction would you like to move?";
                whichWay = String.Format("{0, " + ((Console.WindowWidth / 2) + (whichWay.Length / 2)) + " }", whichWay);
                Console.WriteLine(whichWay);
                string directions = "(UP, DOWN, LEFT, RIGHT): ";
                directions = String.Format("{0, " + ((Console.WindowWidth / 2) + (directions.Length / 2)) + " }", directions);
                Console.Write(directions);
                string move = Console.ReadLine().ToUpper();
                Point newLocation = new Point(0, 0);
                switch (move)
                {
                    case "UP":
                        newLocation = new Point(me.location.x, me.location.y - 1);
                        if (walls.Contains(newLocation)) { }
                        else if (me.location.y - 1 < 0)
                        {
                            me.location = new Point(me.location.x, height - 1);
                        }
                        else
                        {
                            me.location = new Point(me.location.x, me.location.y - 1);
                        }
                        break;
                    case "DOWN":
                        newLocation = new Point(me.location.x, me.location.y + 1);
                        if (walls.Contains(newLocation)) { }
                        else if (me.location.y + 1 > height - 1)
                        {
                            me.location = new Point(me.location.x, 0);
                        }
                        else
                        {
                            me.location = new Point(me.location.x, me.location.y + 1);
                        }
                        break;
                    case "LEFT":
                        newLocation = new Point(me.location.x - 1, me.location.y);
                        if (walls.Contains(newLocation)) { }
                        else if (me.location.x - 1 < 0)
                        {
                            me.location = new Point(width - 1, me.location.y);
                        }
                        else
                        {
                            me.location = new Point(me.location.x - 1, me.location.y);
                        }
                        break;
                    case "RIGHT":
                        newLocation = new Point(me.location.x + 1, me.location.y);
                        if (walls.Contains(newLocation)) { }
                        else if (me.location.x + 1 > width - 1)
                        {
                            me.location = new Point(0, me.location.y);
                        }
                        else
                        {
                            me.location = new Point(me.location.x + 1, me.location.y);
                        }
                        break;
                    default:
                        Console.WriteLine("Sorry, that was not a valid option. Please try again.");
                        break;
                }
                if (!hasKey)
                {
                    if (me.location.x == key.x && me.location.y == key.y)
                    {
                        hasKey = true;
                        key.x = -1;
                        key.y = -1;
                    }
                }
                if (hasKey && me.location.x == door.x && me.location.y == door.y)
                {
                    done = true;
                }



            }

            Console.Clear();
            for (int i = 0; i < 7; i++)
            {
                Console.WriteLine();
            }

            // should probably start making a function to middle align
            string winMessage = "CONGRATULATIONS! YOU WON!";
            winMessage = String.Format("{0, " + ((Console.WindowWidth / 2) + (winMessage.Length / 2)) + "}", winMessage);
            for (int i = 0; i < 7; i++)
            {
                Console.WriteLine();
            }
            Console.WriteLine(winMessage);















        }
    }
}
