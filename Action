using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADVANCE
{   /// <summary>
/// represents Abstract action that can be taken by a piece
/// </summary>
    public abstract class Action
    {
        /// <summary>
        /// Gets the piece who will be performing the action
        /// </summary>
        public Piece Actor { get; }

        /// <summary>
        /// Gets the square that is the target of the action
        /// </summary>
        public Square Target { get; }

        /// <summary>
        /// Will generate the score that each action would generate
        /// </summary>
        public Dictionary<Colour, double> Scores { get; }

        protected bool success = false;

        /// <summary>
        /// Initialise a new instance of <see cref="Action"/> class involving the specified actor
        /// </summary>
        /// <param name="actor"> Piece to perform an action </param>
        /// <param name="target"> Target square or occupant that will be affected by the action</param>
        protected Action(Piece actor, Square target)
        {
            Actor = actor;
            Target = target;
            Scores = new Dictionary<Colour, double>();
            Scores[Colour.Black] = double.MinValue;
            Scores[Colour.White] = double.MinValue;
        }

        /// <summary>
        /// Performas the action
        /// </summary>
        /// <returns><c>true</c> if the action was successful otherwise <c>false</c> </returns>
        public abstract bool DoAction();
        /// <summary>
        /// Undoes a performed action.
        /// </summary>
        public abstract void UndoAction();

    }
    /// <summary>
    /// Represents a move action
    /// </summary>
    public class Move : Action
    {
        private Square? previousSquare;
        /// <summary>
        /// Iniitializes a new instance of the <see cref="Move"/> class with a specified actor and target
        /// </summary>
        /// <param name="actor">The piece performing the move</param>
        /// <param name="target">The target square</param>

        public Move(Piece actor, Square target) : base(actor, target)
        {
            previousSquare = actor.Square;
        }

        /// <summary>
        /// Performs the move action
        /// </summary>
        /// <returns><c>true</c> if the move is successful otherwise <c>false</c></returns>

        public override bool DoAction()
        {
            Console.WriteLine("move do action activated");
            success = Actor.MoveTo(Target);
            return success; 
        }

        /// <summary>
        /// undo the move action
        /// </summary>
        public override void UndoAction()
        {
            if (success && Actor != null && previousSquare != null)
            {
                Console.WriteLine("move action undone");
                Actor.LeaveBoard();
                Actor.EnterBoard(previousSquare);
            }
        }
    }
    /// <summary>
    /// Representing an attack action
    /// </summary>
    public class Attack : Action
    {
        Square? previousSquare;
        Piece? opponentPiece;
        Player? opponent;

        /// <summary>
        /// initializes an instance of case <see cref="Attack"/> class with specified actor and target
        /// </summary>
        /// <param name="actor">Piece performing the move</param>
        /// <param name="target">piece or square targeted</param>
        public Attack(Piece actor, Square target) : base(actor, target)
        {
            previousSquare = actor.Square;
            opponentPiece = target.Occupant;
            opponent = opponentPiece?.Player;
        }
        /// <summary>
        /// does the attack action
        /// </summary>
        /// <returns><c> true</c> if attack successful otherwise <c>false</c></returns>
        public override bool DoAction()
        {
            Console.WriteLine("attack do action activated");
            success= Actor.Attack(Target);
            return success;
        }
        /// <summary>
        /// undoes attack action
        /// </summary>
        public override void UndoAction()
        {
            Actor.LeaveBoard();
            Console.WriteLine("attack action undone");

            if (opponentPiece != null)
            {
                if (opponentPiece.OnBoard)
                {
                    opponentPiece.LeaveBoard();
                }

                opponentPiece.EnterBoard(Target);

                if (opponentPiece.Player != opponent)
                {
                    opponentPiece.Defect();
                }
            }

            Actor.EnterBoard(previousSquare);
        }
    }
    /// <summary>
    /// represents a build action
    /// </summary>
    public class Build : Action
    {
        public Square? wallSquare;
        public Piece? wall;
    
        public Player? player;

        public Build(Piece actor, Square target) : base(actor, target)
        {
            wallSquare = target;
            wall = wallSquare.Occupant;
        }
        /// <summary>
        /// Performs the build action.
        /// </summary>
        /// <returns><c>true</c> if the build was successful; otherwise, <c>false</c>.</returns>
        public override bool DoAction()
        {
            return Actor.Build(Target);
        }
        /// <summary>
        /// undoes build action
        /// </summary>
        public override void UndoAction()
        {
            if (wallSquare != null)
            {
                wallSquare.Occupant.LeaveBoard();
            }
        }
    }
    /// <summary>
    /// represents a swap action
    /// </summary>
    public class Swap : Action
    {
       

        public Square? jesterSquare;

        public Piece? Friendly;

        public Player? Friend;
        /// <summary>
        /// initialise an instance of a <see cref="Swap"/> with a swapper and a swap target
        /// </summary>
        /// <param name="actor"> Jester to swap</param>
        /// <param name="target"> Friendly piece</param>
        public Swap(Piece actor, Square target) : base(actor, target)
        {

   
            jesterSquare = actor.Square;
            Friendly = target.Occupant;
            Friend = Friendly?.Player;
        }

        /// <summary>
        /// Performs the swap
        /// </summary>
        /// <returns><c>true</c> if can swap otherwise <c>false</c></returns>
        public override bool DoAction()
        {
            return Actor.Swap(jesterSquare, Target);
        }
        /// <summary>
        /// undoes the action
        /// </summary>
        public override void UndoAction()
        {
            if (Friendly.Square != null)
            {
                
                Actor.LeaveBoard();
                Friendly.LeaveBoard();
                Friendly.EnterBoard(Target);
                Actor.EnterBoard(jesterSquare);
            }
        }
    }
    /// <summary>
    /// represents a convert action
    /// </summary>
    public class Convert : Action
    {



        public Piece? enemy;

        public Player? opponent;
        /// <summary>
        /// represents an initialisation of <see cref="Convert"/> with a jester and enemy target
        /// </summary>
        /// <param name="actor">Jester</param>
        /// <param name="target">Enemy piece</param>
        public Convert(Piece actor, Square target) : base(actor, target)
        {
            enemy = target.Occupant;
            opponent = enemy?.Player;
        }
        /// <summary>
        /// does the convert
        /// </summary>
        /// <returns><c>true</c> if successful otherwise <c>false</c></returns>
        public override bool DoAction()
        {
            return Actor.Convert(Target);
        }
        /// <summary>
        /// undoes convert
        /// </summary>
        public override void UndoAction()
        {
            if (enemy.Square != null)
            {

                enemy.Defect();
            }
        }
    }
}
