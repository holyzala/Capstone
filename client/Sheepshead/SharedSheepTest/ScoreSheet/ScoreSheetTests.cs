using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharedSheep.Player;
using System.Collections.Generic;
using SharedSheep.ScoreSheet;
using System.Diagnostics;
using System.Linq;

namespace SharedSheepTest.ScoreSheetTests
{
    [TestClass]
    public class ScoreSheetTests
    {
        LocalPlayer p1 = new LocalPlayer("p1");
        LocalPlayer p2 = new LocalPlayer("p2");
        LocalPlayer p3 = new LocalPlayer("p3");
        LocalPlayer p4 = new LocalPlayer("p4");
        LocalPlayer p5 = new LocalPlayer("p5");
        List<IPlayer> players;


        Score score;

        [TestMethod]
        public void AddGameScoreTest()
        {
            players = new List<IPlayer>() { p1, p2, p3, p4, p5 };
            score = new Score(players);

            score.AddGameScore(p1, p3, 3, false, 65);
            score.AddGameScore(p2, p1, 4, false, 50);
            score.AddGameScore(p3, null, 5, false, 90);
            score.AddGameScore(p4, p5, 0, false, 0);
            score.AddGameScore(p5, p3, 6, false, 120);

            Debug.Write("\r\n p1: ");
            score.Scores[p1].ForEach(i => Debug.Write(i+", "));
            Debug.Write("\r\n p2: ");
            score.Scores[p2].ForEach(i => Debug.Write(i + ", "));
            Debug.Write("\r\n p3: ");
            score.Scores[p3].ForEach(i => Debug.Write(i +", "));
            Debug.Write("\r\n p4: ");
            score.Scores[p4].ForEach(i => Debug.Write(i +", "));
            Debug.Write("\r\n p5: ");
            score.Scores[p5].ForEach(i => Debug.Write(i +", "));

            Assert.IsTrue(score.Scores[p1].SequenceEqual(new List<int>() { 2, -1, -1, 3, -3 }));
            Assert.IsTrue(score.Scores[p2].SequenceEqual(new List<int>() { -1,-2,-1,3,-3 }));
            Assert.IsTrue(score.Scores[p3].SequenceEqual(new List<int>() { 1,1,4,3,3 }));

        }

        [TestMethod]
        public void TotalTest()
        {
            players = new List<IPlayer>() { p1, p2, p3, p4, p5 };
            score = new Score(players);

            score.AddGameScore(p1, p3, 5, false, 65);
            score.AddGameScore(p2, p1, 4, false, 50);
            score.AddGameScore(p3, null, 4, false, 90);

            Assert.AreEqual(0, score.Total()[p1]);
            Assert.AreEqual(-4, score.Total()[p2]);
            Assert.AreEqual(6, score.Total()[p3]);

        }

        
        [TestMethod]
        public void WinningPlayerTest()
        {
            players = new List<IPlayer>() { p1, p2, p3, p4, p5 };
            score = new Score(players);

            score.AddGameScore(p1, p3, 3, false, 65);
            score.AddGameScore(p2, p1, 5, false, 50);
            score.AddGameScore(p3, null, 5, false, 90);
            score.AddGameScore(p4, p5, 0, false, 0);
            score.AddGameScore(p5, p3, 6, false, 120);

            Assert.AreEqual(p3.Name, score.WinningPlayer.Name);
            Assert.AreNotEqual(p1.Name, score.WinningPlayer.Name);

        }

    }
}
