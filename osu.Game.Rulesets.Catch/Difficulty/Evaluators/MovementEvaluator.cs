// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using osu.Game.Rulesets.Catch.Difficulty.Preprocessing;
using osu.Game.Rulesets.Difficulty.Preprocessing;

namespace osu.Game.Rulesets.Catch.Difficulty.Evaluators
{
    public static class MovementEvaluator
    {
        private const double direction_change_bonus = 12.5;

        public static double EvaluateDifficultyOf(DifficultyHitObject current)
        {
            var catchCurrent = (CatchDifficultyHitObject)current;
            var catchLast = (CatchDifficultyHitObject)current.Previous(0);

            double distanceAddition = Math.Pow(Math.Abs(catchCurrent.DistanceMoved), 1.3) / 500;
            double sqrtStrain = Math.Sqrt(catchCurrent.StrainTime);

            double edgeDashBonus = 0;

            // Direction change bonus.
            if (Math.Abs(catchCurrent.DistanceMoved) > 0.1)
            {
                if (current.Index >= 1 && Math.Abs(catchLast.DistanceMoved) > 0.1 && Math.Sign(catchCurrent.DistanceMoved) != Math.Sign(catchLast.DistanceMoved))
                {
                    double bonusFactor = Math.Min(Math.Abs(catchCurrent.DistanceMoved) / CatchDifficultyHitObject.ABSOLUTE_PLAYER_POSITIONING_ERROR, 1.0);
                    distanceAddition += direction_change_bonus / sqrtStrain * bonusFactor;

                    // Bonus for tougher direction switches and edge dashes at this point.
                    if (catchCurrent.LastObject.DistanceToHyperDash <= 10.0f)
                        edgeDashBonus = 0.3 * bonusFactor;
                }

                // Base bonus for every movement, giving some weight to streams.
                distanceAddition += 7.5 * Math.Min(Math.Abs(catchCurrent.DistanceMoved), CatchDifficultyHitObject.NORMALIZED_HALF_CATCHER_WIDTH * 2)
                                    / (CatchDifficultyHitObject.NORMALIZED_HALF_CATCHER_WIDTH * 6) / sqrtStrain;
            }

            // Bonus for edge dashes.
            if (catchCurrent.LastObject.DistanceToHyperDash <= 10.0f)
            {
                if (!catchCurrent.LastObject.HyperDash)
                    edgeDashBonus += 1.0;

                distanceAddition *= 1.0 + edgeDashBonus * ((10 - catchCurrent.LastObject.DistanceToHyperDash) / 10);
            }

            return distanceAddition / catchCurrent.StrainTime;
        }
    }
}
