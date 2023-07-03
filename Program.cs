using System;
using System.Net;
using System.Numerics;

class MazeGame
{
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to the Maze Game!");
        Console.WriteLine("Use the arrow keys to move. Try to reach the end of the maze.");
        Console.WriteLine("Press any key to start the game.");
        Console.ReadKey(true);

        Maze maze = new Maze(50, 50);
        maze.Generate();

        Player player = new Player(maze.StartRow, maze.StartCol);
        EndPoint endPoint = new EndPoint(maze.EndRow, maze.EndCol);

        Console.Clear();
        maze.Print(player.Row, player.Col);

        while (true)
        {
            ConsoleKeyInfo key = Console.ReadKey(true);
            switch (key.Key)
            {
                case ConsoleKey.UpArrow:
                    if (player.Row > 0 && !maze.IsWall(player.Row - 1, player.Col))
                    {
                        player.Move(-1, 0);
                    }
                    break;
                case ConsoleKey.DownArrow:
                    if (player.Row < maze.Height - 1 && !maze.IsWall(player.Row + 1, player.Col))
                    {
                        player.Move(1, 0);
                    }
                    break;
                case ConsoleKey.LeftArrow:
                    if (player.Col > 0 && !maze.IsWall(player.Row, player.Col - 1))
                    {
                        player.Move(0, -1);
                    }
                    break;
                case ConsoleKey.RightArrow:
                    if (player.Col < maze.Width - 1 && !maze.IsWall(player.Row, player.Col + 1))
                    {
                        player.Move(0, 1);
                    }
                    break;
                default:
                    break;
            }

            Console.Clear();
            maze.Print(player.Row, player.Col);

            if (player.Row == endPoint.Row && player.Col == endPoint.Col)
            {
                Console.WriteLine("Congratulations! You have reached the end of the maze.");
                Console.WriteLine("Press any key to exit.");
                Console.ReadKey(true);
                break;
            }
        }
    }
}

class Maze
{
    private int[,] grid;
    private int height;
    private int width;
    private int startRow;
    private int startCol;
    private int endRow;
    private int endCol;
    private Random random = new Random();

    public int Height { get { return height; } }
    public int Width { get { return width; } }
    public int StartRow { get { return startRow; } }
    public int StartCol { get { return startCol; } }
    public int EndRow { get { return endRow; } }
    public int EndCol { get { return endCol; } }

    public Maze(int height, int width)
    {
        this.height = height;
        this.width = width;
        grid = new int[height, width];
        startRow = random.Next(height);
        startCol = random.Next(width);
        endRow = random.Next(height);
        endCol = random.Next(width);
    }

    public void Generate()
    {
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                grid[i, j] = random.Next(2);
            }
        }

        grid[startRow, startCol] = 0;
        grid[endRow, endCol] = 0;
    }

    public bool IsWall(int row, int col)
    {
        return grid[row, col] == 1;
    }

    public void Print(int playerRow, int playerCol)
    {
        Console.WriteLine();
        for (int i = 0; i < width * 2 + 1; i++)
        {
            Console.Write("#");
        }
        Console.WriteLine();
        for (int i = 0; i < height; i++)
        {
            Console.Write("#");
            for (int j = 0; j < width; j++)
            {
                if (i == playerRow && j == playerCol)
                {
                    Console.Write("X ");
                }
                else if (i == endRow && j == endCol)
                {
                    Console.Write("E ");
                }
                else
                {
                    if (IsWall(i, j))
                    {
                        Console.Write("# ");
                    }
                    else
                    {
                        Console.Write("  ");
                    }
                }
            }
            Console.WriteLine("#");
        }
        for (int i = 0; i < width * 2 + 1; i++)
        {
            Console.Write("#");
        }
        Console.WriteLine();
    }
}

class Player
{
    private int row;
    private int col;

    public int Row { get { return row; } }
    public int Col { get { return col; } }

    public Player(int row, int col)
    {
        this.row = row;
        this.col = col;
    }

    public void Move(int dRow, int dCol)
    {
        row += dRow;
        col += dCol;
    }
}

class EndPoint
{
    private int row;
    private int col;

    public int Row { get { return row; } }
    public int Col { get { return col; } }

    public EndPoint(int row, int col)
    {
        this.row = row;
        this.col = col;
    }
}