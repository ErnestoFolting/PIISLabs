using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Lab2
{
    struct node
    {
        public List<List<int>> Maze = new List<List<int>>();
        public Point playerCurrent;
        public Point enemyCurrent;
        public Point playerFinal;
        public node(List<List<int>> Maze, Point playerCurrent, Point enemyCurrent, Point playerFinal)
        {
            this.Maze = Maze;
            this.playerCurrent = playerCurrent;
            this.enemyCurrent = enemyCurrent;
            this.playerFinal = playerFinal;
        }
    }
    class miniPacman
    {
        private static System.Timers.Timer aTimer;
        public node currentNode;
        private double heuristic(node nodeToEvaluate)
        {
            if (nodeToEvaluate.playerCurrent.i == nodeToEvaluate.playerFinal.i &&
                nodeToEvaluate.playerCurrent.j == nodeToEvaluate.playerFinal.j) return 3; 
            if (nodeToEvaluate.playerCurrent.i == nodeToEvaluate.enemyCurrent.i &&
                nodeToEvaluate.playerCurrent.j == nodeToEvaluate.enemyCurrent.j) return -1;
            
            double heuristicValue;
            AStar astarToFinish = new AStar(nodeToEvaluate.Maze, nodeToEvaluate.playerFinal);
            int toFinishValue = astarToFinish.findFinal(nodeToEvaluate.playerCurrent);

            AStar astarToEnemy = new AStar(nodeToEvaluate.Maze, nodeToEvaluate.enemyCurrent);
            int toEnemyValue = astarToEnemy.findFinal(nodeToEvaluate.playerCurrent);

            heuristicValue = (10*((double)1 / (toFinishValue)) + (1 - ((double)1 / (toEnemyValue))));

            return heuristicValue;
        }
        public miniPacman(node initialNode)
        {
            currentNode = initialNode;
        }

        private bool isTerminal(node nodeToEvaluate)
        {
            if (nodeToEvaluate.playerCurrent.i == nodeToEvaluate.enemyCurrent.i &&
                nodeToEvaluate.playerCurrent.j == nodeToEvaluate.enemyCurrent.j) return true;
            if (nodeToEvaluate.playerCurrent.i == nodeToEvaluate.playerFinal.i &&
                (nodeToEvaluate.playerCurrent.j == nodeToEvaluate.playerFinal.j)) return true;
            return false;
        }

        private bool isCorrect(Point cellToCheck, node nodeToCheck)
        {
            return (cellToCheck.i >= 0 && cellToCheck.i < nodeToCheck.Maze.Count && cellToCheck.j >= 0 && cellToCheck.j < nodeToCheck.Maze[0].Count);
        }
        public List<node> findChildren(node nodeToCheck, bool player)
        {
            List<node> children = new List<node>();

            List<int> movesI = new List<int>() { 0, -1, -1, -1, 0, 1, 1, 1 };
            List<int> movesJ = new List<int>() { -1, -1, 0, 1, 1, 1, 0, -1 };

            for (int i = 0; i < movesI.Count; i++)
            {
                Point temp = new Point();
                if (player)
                {
                    temp.i = nodeToCheck.playerCurrent.i + movesI[i];
                    temp.j = nodeToCheck.playerCurrent.j + movesJ[i];
                    if (isCorrect(temp, nodeToCheck))
                    {
                        node newNode = new node(nodeToCheck.Maze, temp, nodeToCheck.enemyCurrent, nodeToCheck.playerFinal);
                        children.Add(newNode);
                    }
                }
                else
                {
                    temp.i = nodeToCheck.enemyCurrent.i + movesI[i];
                    temp.j = nodeToCheck.enemyCurrent.j + movesJ[i];
                    if (isCorrect(temp, nodeToCheck))
                    {
                        node newNode = new node(nodeToCheck.Maze, nodeToCheck.playerCurrent, temp, nodeToCheck.playerFinal);
                        children.Add(newNode);
                    }
                }
            }
            return children;
        }
        public List<double> minimax(node nodeToCheck, int depth, bool maximizingPlayer)
        {
            if (depth == 0 || isTerminal(nodeToCheck))
            {
                if (!maximizingPlayer)
                {
                    List<double> temp = new() { (depth * 0.000001) + heuristic(nodeToCheck) };
                    return temp;
                }
                else
                {
                    List<double> temp = new() { heuristic(nodeToCheck) - (depth * 0.000001) };
                    return temp;
                }
            }
            if (maximizingPlayer)
            {
                double value = double.NegativeInfinity;
                List<node> children = findChildren(nodeToCheck, true);
                int childPos = 0;
                for(int i = 0;i < children.Count;i++)
                {
                    
                    double tempValue = minimax(children[i], depth - 1, false)[0];
                    if (tempValue > value)
                    {
                        value = tempValue;
                        childPos = i;
                    }
                }
                List<double> temp = new() { value, childPos };
                return temp;
            }
            else
            {
                double value = double.PositiveInfinity;
                List<node> children = findChildren(nodeToCheck, false);
                int childPos = 0;
                for (int i = 0; i < children.Count; i++)
                {
                    double tempValue = minimax(children[i], depth - 1, true)[0];
                    if (tempValue <= value)
                    {
                        value = tempValue;
                        childPos = i;
                    }
                }
                List<double> temp = new() { value, childPos };
                return temp;
            }
        }
        public void playerMove()
        {
            double childIndex = minimax(currentNode, 4, true)[1];
            currentNode = findChildren(currentNode, true)[(int)childIndex];
        }
        public void enemyMove()
        {
            double childIndex = minimax(currentNode, 4, false)[1];
            currentNode = findChildren(currentNode, false)[(int)childIndex];
        }
        public void game()
        {
            consoleWriter writer = new consoleWriter();
            while (!isTerminal(currentNode))
            {
                playerMove();
                writer.printInGameMaze(currentNode.Maze, currentNode.playerCurrent, currentNode.enemyCurrent, currentNode.playerFinal);
                Thread.Sleep(500);
                if (isTerminal(currentNode)) break;
                enemyMove();
                writer.printInGameMaze(currentNode.Maze, currentNode.playerCurrent, currentNode.enemyCurrent, currentNode.playerFinal);
                Thread.Sleep(500);
            }
            if (currentNode.playerCurrent.i == currentNode.playerFinal.i && currentNode.playerCurrent.j == currentNode.playerFinal.j)
            {
                Console.WriteLine("Player won!");
            }
            else
            {
                Console.WriteLine("The enemy won!");
            }
        }
    }
}
