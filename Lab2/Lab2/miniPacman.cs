using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    class miniPacman
    {
        private List<List<int>> Maze = new List<List<int>>();
        private Point playerStart;
        private Point playerFinal;
        private Point enemyStart;
        private Point playerCurrent;
        private Point enemyCurrent;
        private double heuristic()
        {
            double heuristicValue;
            AStar astarToFinish = new AStar(Maze, playerFinal);
            int toFinishValue = astarToFinish.findFinal(playerCurrent);

            AStar astarToEnemy = new AStar(Maze, enemyCurrent);
            int toEnemyValue = astarToEnemy.findFinal(playerCurrent);

            Console.WriteLine(((double)1 / (toFinishValue)));
            Console.WriteLine((1 - ((double)1 / (toEnemyValue))));

            heuristicValue = (((double)1 / (toFinishValue)) + (1 - ((double) 1 / (toEnemyValue))));
            Console.WriteLine(heuristicValue);
            return heuristicValue;
        }
        public miniPacman(Point start, Point final, Point enemyStart, List<List<int>> maze)
        {
            playerStart = start;
            playerFinal = final;
            this.enemyStart = enemyStart;
            playerCurrent = playerStart;
            enemyCurrent = enemyStart;
            Maze = maze;
        }
        public void game()
        {
            heuristic();
        }
    }
}
