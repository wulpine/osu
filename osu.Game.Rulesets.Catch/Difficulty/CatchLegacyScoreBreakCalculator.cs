// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Collections.Generic;
using System.Linq;
using osu.Game.Rulesets.Catch.Mods;
using osu.Game.Rulesets.Mods;
using osu.Game.Rulesets.Scoring;
using osu.Game.Scoring;

namespace osu.Game.Rulesets.Catch.Difficulty
{
    public class CatchLegacyScoreBreakCalculator
    {
        private readonly ScoreInfo score;
        private readonly CatchDifficultyAttributes attributes;

        public CatchLegacyScoreBreakCalculator(ScoreInfo score, CatchDifficultyAttributes attributes)
        {
            this.score = score;
            this.attributes = attributes;
        }

        public double Calculate()
        {
            int countGreat = score.Statistics.GetValueOrDefault(HitResult.Great);
            int countLargeTickHit = score.Statistics.GetValueOrDefault(HitResult.LargeTickHit);
            int countSmallTickHit = score.Statistics.GetValueOrDefault(HitResult.SmallTickHit);
            int countMiss = score.Statistics.GetValueOrDefault(HitResult.Miss);

            if (attributes.MaxCombo == 0 || countMiss == 0 || score.LegacyTotalScore == null)
                return 0;

            double scoreV1Multiplier = attributes.LegacyScoreBaseMultiplier * getLegacyScoreMultiplier();
            double averageHitValue = calculateAverageHitValue();
            int accuracyScore = 300 * countGreat + 100 * countLargeTickHit + 10 * countSmallTickHit;

            double scoreObtainedDuringMaxCombo = calculateScoreAtCombo(score.MaxCombo, averageHitValue, scoreV1Multiplier);
            double remainingScore = score.LegacyTotalScore.Value - accuracyScore - scoreObtainedDuringMaxCombo;

            if (remainingScore <= 0)
                return 1;

            int remainingCombo = attributes.MaxCombo - score.MaxCombo;
            double expectedRemainingScore = calculateScoreAtCombo(remainingCombo, averageHitValue, scoreV1Multiplier);

            double scoreBasedBreakCount = Math.Max(expectedRemainingScore / remainingScore, 1);

            return Math.Min(scoreBasedBreakCount, countMiss);
        }

        private double calculateScoreAtCombo(int combo, double averageHitValue, double scoreV1Multiplier)
        {
            double comboMultiplier = combo > 0 ? (combo - 1) / 2.0 * (combo - 2) : 0;
            double comboScore = averageHitValue * comboMultiplier * scoreV1Multiplier / 25.0;

            return comboScore;
        }

        private double calculateAverageHitValue()
        {
            double comboScore = attributes.MaximumLegacyComboScore;
            comboScore /= attributes.LegacyScoreBaseMultiplier / 25.0;
            comboScore /= (attributes.MaxCombo - 1) / 2.0 * (attributes.MaxCombo - 2);

            return comboScore;
        }

        private double getLegacyScoreMultiplier()
        {
            bool scoreV2 = score.Mods.Any(m => m is ModScoreV2);

            double multiplier = 1.0;

            foreach (var mod in score.Mods)
            {
                switch (mod)
                {
                    case CatchModEasy:
                        multiplier *= 0.5;
                        break;

                    case CatchModNoFail:
                        multiplier *= scoreV2 ? 1.0 : 0.5;
                        break;

                    case CatchModHalfTime:
                    case CatchModDaycore:
                        multiplier *= 0.3;
                        break;

                    case CatchModHardRock:
                        multiplier *= scoreV2 ? 1.0 : 1.12;
                        break;

                    case CatchModDoubleTime:
                    case CatchModNightcore:
                    case CatchModHidden:
                        multiplier *= scoreV2 ? 1.0 : 1.06;
                        break;

                    case CatchModFlashlight:
                        multiplier *= 1.12;
                        break;

                    case CatchModRelax:
                        return 0;
                }
            }

            return multiplier;
        }
    }
}
