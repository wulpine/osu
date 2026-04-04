// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using osu.Framework.Lists;
using osu.Game.Beatmaps;
using osu.Game.Beatmaps.ControlPoints;
using osu.Game.Beatmaps.Timing;
using osu.Game.Rulesets.Difficulty.Preprocessing;
using osu.Game.Rulesets.Difficulty.Skills;
using osu.Game.Rulesets.Mods;
using osu.Game.Rulesets.Objects;

namespace osu.Game.Rulesets.Difficulty
{
    public sealed class GradualDifficultyEnumerator
    {
        private readonly IBeatmap beatmap;
        private readonly ProgressiveCalculationBeatmap progressiveBeatmap;

        private readonly Mod[] mods;

        private readonly DifficultyHitObject[] difficultyHitObjects;
        private int difficultyHitObjectCursor = 0;

        private readonly double clockRate;

        private readonly Func<IBeatmap, Mod[], Skill[], double, DifficultyAttributes> createDifficultyAttributes;

        public Skill[] Skills { get; }

        public GradualDifficultyEnumerator(
            IBeatmap beatmap,
            Mod[] mods,
            DifficultyHitObject[] difficultyHitObjects,
            Skill[] skills,
            double clockRate,
            Func<IBeatmap, Mod[], Skill[], double, DifficultyAttributes> createDifficultyAttributes)
        {
            this.beatmap = beatmap;
            progressiveBeatmap = new ProgressiveCalculationBeatmap(beatmap);
            this.mods = mods;
            this.difficultyHitObjects = difficultyHitObjects;
            this.clockRate = clockRate;
            this.createDifficultyAttributes = createDifficultyAttributes;
            Skills = skills;
        }

        private void updateDifficultyHitObjects(CancellationToken cancellationToken)
        {
            var lastHitObject = beatmap.HitObjects.Count <= progressiveBeatmap.HitObjects.Count
                ? beatmap.HitObjects[beatmap.HitObjects.Count - 1]
                : beatmap.HitObjects[progressiveBeatmap.HitObjects.Count];

            while (difficultyHitObjectCursor < difficultyHitObjects.Length)
            {
                var difficultyHitObject = difficultyHitObjects[difficultyHitObjectCursor];
                if (difficultyHitObject.BaseObject.GetEndTime() > lastHitObject.GetEndTime())
                {
                    break;
                }
                difficultyHitObjectCursor++;

                foreach (var skill in Skills)
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    skill.Process(difficultyHitObject);
                }
            }
        }

        public DifficultyAttributes CreateDifficultyAttributes()
        {
            return createDifficultyAttributes(progressiveBeatmap, mods, Skills, clockRate);
        }

        public bool Advance(CancellationToken cancellationToken = default)
        {
            if (beatmap.HitObjects.Count <= progressiveBeatmap.HitObjects.Count)
                return false;

            var hitObject = beatmap.HitObjects[progressiveBeatmap.HitObjects.Count];
            progressiveBeatmap.HitObjects.Add(hitObject);
            updateDifficultyHitObjects(cancellationToken);

            return true;
        }

        public void Skip(int offset, CancellationToken cancellationToken = default)
        {
            progressiveBeatmap.HitObjects.AddRange(beatmap.HitObjects.Skip(progressiveBeatmap.HitObjects.Count).Take(offset));
            updateDifficultyHitObjects(cancellationToken);
        }

        public void SkipToTime(double time, CancellationToken cancellationToken = default)
        {
            while (progressiveBeatmap.HitObjects.Count < beatmap.HitObjects.Count)
            {
                var hitObject = beatmap.HitObjects[progressiveBeatmap.HitObjects.Count];
                if (hitObject.StartTime >= time)
                    break;
                progressiveBeatmap.HitObjects.Add(hitObject);
            }

            updateDifficultyHitObjects(cancellationToken);
        }

        public void SkipToEnd(CancellationToken cancellationToken = default)
        {
            progressiveBeatmap.HitObjects.AddRange(beatmap.HitObjects.Skip(progressiveBeatmap.HitObjects.Count));
            updateDifficultyHitObjects(cancellationToken);
        }

        /// <summary>
        /// Used to calculate timed difficulty attributes, where only a subset of hitobjects should be visible at any point in time.
        /// </summary>
        private class ProgressiveCalculationBeatmap : IBeatmap
        {
            private readonly IBeatmap baseBeatmap;

            public ProgressiveCalculationBeatmap(IBeatmap baseBeatmap)
            {
                this.baseBeatmap = baseBeatmap;
            }

            public readonly List<HitObject> HitObjects = new List<HitObject>();

            IReadOnlyList<HitObject> IBeatmap.HitObjects => HitObjects;

            #region Delegated IBeatmap implementation

            public BeatmapInfo BeatmapInfo
            {
                get => baseBeatmap.BeatmapInfo;
                set => baseBeatmap.BeatmapInfo = value;
            }

            public ControlPointInfo ControlPointInfo
            {
                get => baseBeatmap.ControlPointInfo;
                set => baseBeatmap.ControlPointInfo = value;
            }

            public BeatmapMetadata Metadata => baseBeatmap.Metadata;

            public BeatmapDifficulty Difficulty
            {
                get => baseBeatmap.Difficulty;
                set => baseBeatmap.Difficulty = value;
            }

            public SortedList<BreakPeriod> Breaks
            {
                get => baseBeatmap.Breaks;
                set => baseBeatmap.Breaks = value;
            }

            public List<string> UnhandledEventLines => baseBeatmap.UnhandledEventLines;

            public double TotalBreakTime => baseBeatmap.TotalBreakTime;
            public IEnumerable<BeatmapStatistic> GetStatistics() => baseBeatmap.GetStatistics();
            public double GetMostCommonBeatLength() => baseBeatmap.GetMostCommonBeatLength();
            public int BeatmapVersion => baseBeatmap.BeatmapVersion;
            public IBeatmap Clone() => new ProgressiveCalculationBeatmap(baseBeatmap.Clone());

            public double AudioLeadIn
            {
                get => baseBeatmap.AudioLeadIn;
                set => baseBeatmap.AudioLeadIn = value;
            }

            public float StackLeniency
            {
                get => baseBeatmap.StackLeniency;
                set => baseBeatmap.StackLeniency = value;
            }

            public bool SpecialStyle
            {
                get => baseBeatmap.SpecialStyle;
                set => baseBeatmap.SpecialStyle = value;
            }

            public bool LetterboxInBreaks
            {
                get => baseBeatmap.LetterboxInBreaks;
                set => baseBeatmap.LetterboxInBreaks = value;
            }

            public bool WidescreenStoryboard
            {
                get => baseBeatmap.WidescreenStoryboard;
                set => baseBeatmap.WidescreenStoryboard = value;
            }

            public bool EpilepsyWarning
            {
                get => baseBeatmap.EpilepsyWarning;
                set => baseBeatmap.EpilepsyWarning = value;
            }

            public bool SamplesMatchPlaybackRate
            {
                get => baseBeatmap.SamplesMatchPlaybackRate;
                set => baseBeatmap.SamplesMatchPlaybackRate = value;
            }

            public double DistanceSpacing
            {
                get => baseBeatmap.DistanceSpacing;
                set => baseBeatmap.DistanceSpacing = value;
            }

            public int GridSize
            {
                get => baseBeatmap.GridSize;
                set => baseBeatmap.GridSize = value;
            }

            public double TimelineZoom
            {
                get => baseBeatmap.TimelineZoom;
                set => baseBeatmap.TimelineZoom = value;
            }

            public CountdownType Countdown
            {
                get => baseBeatmap.Countdown;
                set => baseBeatmap.Countdown = value;
            }

            public int CountdownOffset
            {
                get => baseBeatmap.CountdownOffset;
                set => baseBeatmap.CountdownOffset = value;
            }

            public int[] Bookmarks
            {
                get => baseBeatmap.Bookmarks;
                set => baseBeatmap.Bookmarks = value;
            }

            #endregion
        }
    }
}
