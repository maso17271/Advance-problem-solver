namespace ADVANCE
{
    public class Player
    {
        public Colour Colour { get; }
        public Army Army { get; }
        public Game Game { get; }

        public Player(Colour colour, Game game)
        {
            Colour = colour;
            Game = game;
            Army = new Army(this, Game.Board);
        }
        public Player Opponent
        {
            get
            {
                if (Game.White.Colour == this.Colour)
                {
                    return Game.Black;
                }

                else
                    return Game.White;
            }
        }

        public int Direction
        {
            get
            {
                return Colour == Colour.Black ? 1 : -1;
            }
        }

        public override string ToString()
        {
            return $"Player {Colour}";
        }

        public void FindPossibleActions(List<Action> actions)
        {
            FindPossibleMoves(actions);
            FindPossibleAttacks(actions);

        }

        public void FindPossibleMoves(List<Action> actions)
        {
            foreach (var piece in Army.Pieces)
            {
                foreach (var square in Game.Board.Squares)
                {
                    if (square.IsFree && piece.CanMoveTo(square))
                    {
                        actions.Add(new Move(piece, square));
                    }
                    
                    if (square.IsOccupied && square.Occupant is not NeutralPiece && square.Occupant.Player == this && piece is Jester && piece.CanSwap(square))
                    {
                        actions.Add(new Swap(piece, square));
                    }
                }
            }
        }

        public void FindPossibleAttacks(List<Action> actions)
        {
            foreach (var piece in Army.Pieces)
            {
                foreach (var square in Game.Board.Squares)
                {
                    if(square.IsOccupied && square.Occupant is NeutralPiece && piece.CanAttack(square)) actions.Add(new Attack(piece, square));
                    if (square.IsOccupied && square.Occupant.Player != this
                        && piece.CanAttack(square))

                    {
                        actions.Add(new Attack(piece, square));
                    }
                    if (square.IsFree && piece is Builder && piece.CanBuild(square))
                    {
                        actions.Add(new Build(piece, square));
                    }
                    if(square.IsOccupied && square.Occupant is not NeutralPiece && square.Occupant.Player != this && piece.CanConvert(square))
                    {
                        actions.Add(new Convert(piece, square));
                    }
                    
                }
            }
        }
        private bool IsGeneralInDangerAfterAction(Action action)
        {
            action.DoAction();

            foreach (var square in Game.Board.Squares)
            {


                if (square.isThreatened(Opponent))
                {
                    foreach (Piece piece in square.ThreateningPieces)
                    {
                        
                    }
                }



                if (Colour == Colour.Black)
                {
                    if (square.Occupant is General && square.Occupant.Player == this && square.isThreatened(Opponent))
                    {
                        
                        action.UndoAction();

                        return true;
                    }
                }
                if (Colour == Colour.White)
                {
                    if (square.Occupant is General && square.Occupant.Player == this && square.isThreatened(Opponent))
                    {
                        
                        action.UndoAction();

                        return true;
                    }
                }


            }
            action.UndoAction();
            return false;


        }

        /* public Action ChooseMove(List<Action>? actions = null)
         {
             if (actions == null) actions = new List<Action>();

             FindPossibleActions(actions);

             // Return a random action
             if (actions.Count() == 0)
                 return null;
             else
                 return actions[rand.Next(actions.Count)];
         }*/



        public Action ChooseMove(List<Action>? actions = null)
        {
            if (actions == null) actions = new List<Action>();

            FindPossibleActions(actions);

            if (actions.Count() == 0)
            {
                return null;
            }
            else
            {
                List<Action> validActions = new List<Action>();

                foreach (var action in actions)
                {
                    if (!IsGeneralInDangerAfterAction(action))
                    {
                        validActions.Add(action);
                        
                        Console.WriteLine(validActions.Count);

                   
                       
                    }
                }

                if (validActions.Count() == 0)
                {
                    return null;
                }

                return validActions[rand.Next(validActions.Count())];
            }

        }

        static Random rand = new Random();


        }
    }
