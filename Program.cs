using System;
using System.Collections.Generic;
using System.Security.AccessControl;
using System.Xml.Linq;

namespace ADVANCE;

public class Program
{
    private static void Main(string[] args)
    {
        Game game = new Game();

        if (ParseArguments(args, out string infile,
                out string outfile, out Colour colour
        ))
        {
            Load(game, infile);
            PlayOneTurn(game, colour);
            
            Save(game, outfile);
        }
        else
        {
            Console.WriteLine("Error processing command line arguments. Expected: ");
            Console.WriteLine("args[0] == input file path");
            Console.WriteLine("args[1] == output file path");
            Console.WriteLine("args[2] == colour (white or black)");
            Console.WriteLine("Incorrect args provided");
        }
    }

    private static bool ParseArguments(
        string[] args,
        out string infile,
        out string outfile,
        out Colour colour
    )
    {
        infile = string.Empty;
        outfile = string.Empty;
        colour = Colour.White;

        if (args.Length >= 3)
        {
            infile = args[1];
            outfile = args[2];
            return Enum.TryParse<Colour>(args[0], ignoreCase: true, out colour);
        }
        else
        {
            return false;
        }
    }

    private static void Load(Game game, string infile)
    {
        try
        {
            using StreamReader reader = new StreamReader(infile);
            game.Read(reader);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error loading game from '{0}': {1}", infile, ex.Message);
        }
    }

    private static void PlayOneTurn(Game game, Colour colour)
    {
        Player currentPlayer = colour == Colour.White ? game.White : game.Black;
        ADVANCE.Action nextMove = currentPlayer.ChooseMove();
        if (nextMove != null)
        {
            nextMove.DoAction();
        }
    }

    private static void Save(Game game, string outfile)
    {
        try
        {
            using StreamWriter writer = new StreamWriter(outfile);
            game.Write(writer);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error saving game to '{0}': {1}", outfile, ex.Message);
        }
    }
}
