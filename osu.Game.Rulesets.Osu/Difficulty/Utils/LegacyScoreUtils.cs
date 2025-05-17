// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Linq;
using osu.Game.Beatmaps;
using osu.Game.Rulesets.Osu.Objects;

namespace osu.Game.Rulesets.Osu.Difficulty.Utils
{
    public static class LegacyScoreUtils
    {
        /// <summary>
        /// Calculates the average amount of score per object that is caused by slider ticks.
        /// </summary>
        public static double CalculateSliderNestedScorePerObject(IBeatmap beatmap, int objectCount)
        {
            const double big_tick_score = 30;
            const double small_tick_score = 10;

            var sliders = beatmap.HitObjects.OfType<Slider>().ToArray();

            // 1 for head, 1 for tail
            int amountOfBigTicks = sliders.Length * 2;

            // Add slider repeats
            amountOfBigTicks += sliders.Select(s => s.RepeatCount).Sum();

            int amountOfSmallTicks = sliders.Select(s => s.NestedHitObjects.Count(nho => nho is SliderTick)).Sum();

            double totalScore = amountOfBigTicks * big_tick_score + amountOfSmallTicks * small_tick_score;

            return totalScore / objectCount;
        }
    }
}
