﻿using System;
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
        public node currentNode;
        private double heuristic(node nodeToEvaluate)
        {
            double heuristicValue = 0;
            if (nodeToEvaluate.playerCurrent.i == nodeToEvaluate.playerFinal.i &&
                nodeToEvaluate.playerCurrent.j == nodeToEvaluate.playerFinal.j) return 3; 
            if (nodeToEvaluate.playerCurrent.i == nodeToEvaluate.enemyCurrent.i &&
                nodeToEvaluate.playerCurrent.j == nodeToEvaluate.enemyCurrent.j) return -1;
                  
            AStar astarToFinish = new AStar(nodeToEvaluate.Maze, nodeToEvaluate.playerFinal);
            Point foundFinish= astarToFinish.findFinal(nodeToEvaluate.playerCurrent);
            int toFinishValue = foundFinish.h + foundFinish.g;


            AStar astarToEnemy = new AStar(nodeToEvaluate.Maze, nodeToEvaluate.enemyCurrent);
            Point foundEnemy = astarToEnemy.findFinal(nodeToEvaluate.playerCurrent);
            int toEnemyValue = foundEnemy.h + foundEnemy.g;

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
        public List<node> findChildren(node nodeToCheck, int player)
        {
            List<node> children = new List<node>();

            List<int> movesI = new List<int>() { 0, -1, -1, -1, 0, 1, 1, 1 };
            List<int> movesJ = new List<int>() { -1, -1, 0, 1, 1, 1, 0, -1 };

            for (int i = 0; i < movesI.Count; i++)
            {
                Point temp = new Point();
                if (player == 1)
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
        public List<double> negamax(node nodeToCheck, int depth, int maximizingPlayer)
        {
            if (depth == 0 || isTerminal(nodeToCheck))
            {
                List<double> score = new() { maximizingPlayer * heuristic(nodeToCheck) + maximizingPlayer * (depth * 0.000001) };
                return score;
            }
            double value = double.NegativeInfinity;
            List<node> children = findChildren(nodeToCheck, maximizingPlayer);
            int childPos = 0;
            for(int i = 0;i < children.Count;i++)
            { 
                double tempValue = (-1) * (negamax(children[i], depth - 1, -maximizingPlayer)[0]);
                if (tempValue > value)
                {
                    value = tempValue;
                    childPos = i;
                }
            }
            List<double> temp = new() { value, childPos };
            return temp;
        }

        public List<double> negamaxWithPruning(node nodeToCheck, int depth, double alpha, double beta, int maximizingPlayer)
        {
            if (depth == 0 || isTerminal(nodeToCheck))
            {
                List<double> score = new() { maximizingPlayer * heuristic(nodeToCheck) + maximizingPlayer * (depth * 0.000001) };
                return score;
            }
            double value = double.NegativeInfinity;
            List<node> children = findChildren(nodeToCheck, maximizingPlayer);
            int childPos = 0;
            for (int i = 0; i < children.Count; i++)
            {
                double tempValue = (-1) * (negamaxWithPruning(children[i], depth - 1,-beta,-alpha, -maximizingPlayer)[0]);
                if (tempValue > value)
                {
                    value = tempValue;
                    childPos = i;
                }
                if (alpha < tempValue) alpha = tempValue;
                if (alpha >= beta) break;
            }
            List<double> temp = new() { value, childPos };
            return temp;
        }

        public List<double> negaScout(node nodeToCheck, int depth, double alpha, double beta, int maximizingPlayer)
        {
            if (depth == 0 || isTerminal(nodeToCheck))
            {
                List<double> score = new() { maximizingPlayer * heuristic(nodeToCheck) + maximizingPlayer * (depth * 0.000001) };
                return score;
            }
            double value = double.NegativeInfinity;
            List<node> children = findChildren(nodeToCheck, maximizingPlayer);
            int childPos = 0;
            for (int i = 0; i < children.Count; i++)
            {
                double tempValue;
                if (i == 0)
                {
                     tempValue = (-1) * (negamaxWithPruning(children[i], depth - 1, -beta, -alpha, -maximizingPlayer)[0]);
                }
                else
                {
                     tempValue = (-1) * (negamaxWithPruning(children[i], depth - 1, (-alpha -1), -alpha, -maximizingPlayer)[0]);
                    if (tempValue > alpha && tempValue < beta) {
                        tempValue = (-1) * (negamaxWithPruning(children[i], depth - 1, -beta, -tempValue, -maximizingPlayer)[0]);
                    }
                }                
                if (tempValue > value)
                {
                    value = tempValue;
                    childPos = i;
                }
                if (alpha < tempValue) alpha = tempValue;
                if (alpha >= beta) break;
            }
            List<double> temp = new() { value, childPos };
            return temp;
        }

        public void playerMove(int algo)
        {
            double childIndex = 0;
            if (algo == 1)
            {
                childIndex = negamaxWithPruning(currentNode, 6, double.NegativeInfinity, double.PositiveInfinity, 1)[1];
            }
            else if (algo == 0)
            {
                childIndex = negamax(currentNode, 6, 1)[1];
            }else
            {
                childIndex = negaScout(currentNode,6,double.NegativeInfinity,double.PositiveInfinity,1)[1];
            }
            
            currentNode = findChildren(currentNode, 1)[(int)childIndex];
        }
        public void enemyMove()
        {
            AStar astar = new AStar(currentNode.Maze, currentNode.playerCurrent);
            Point foundPlayer = astar.findFinal(currentNode.enemyCurrent);
            Console.WriteLine(foundPlayer.previous.i);
            Console.WriteLine(foundPlayer.previous.j);
            Point pointToGo = astar.findPoingToGo(currentNode.enemyCurrent, foundPlayer);
            currentNode = new node(currentNode.Maze, currentNode.playerCurrent, pointToGo, currentNode.playerFinal);
        }
        public void game()
        {
            Console.WriteLine("Choose the algo:\n0 - negamax without pruning\n1 - negamax with pruning\n2 - negascout");
            int algo = Convert.ToInt32(Console.ReadLine());
            consoleWriter writer = new consoleWriter();
            while (!isTerminal(currentNode))
            {
                playerMove(algo);
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
