// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using Newtonsoft.Json;
using osu.Game.Beatmaps;
using osu.Game.Rulesets.Difficulty;

namespace osu.Game.Rulesets.Catch.Difficulty
{
    public class CatchDifficultyAttributes : DifficultyAttributes
    {
        [JsonProperty("legacy_score_base_multiplier")]
        public double LegacyScoreBaseMultiplier { get; set; }

        [JsonProperty("maximum_legacy_combo_score")]
        public double MaximumLegacyComboScore { get; set; }

        public override IEnumerable<(int attributeId, object value)> ToDatabaseAttributes()
        {
            foreach (var v in base.ToDatabaseAttributes())
                yield return v;

            // Todo: osu!catch should not output star rating in the 'aim' attribute.
            yield return (ATTRIB_ID_AIM, StarRating);
            yield return (ATTRIB_ID_LEGACY_SCORE_BASE_MULTIPLIER, LegacyScoreBaseMultiplier);
            yield return (ATTRIB_ID_MAXIMUM_LEGACY_COMBO_SCORE, MaximumLegacyComboScore);
        }

        public override void FromDatabaseAttributes(IReadOnlyDictionary<int, double> values, IBeatmapOnlineInfo onlineInfo)
        {
            base.FromDatabaseAttributes(values, onlineInfo);

            StarRating = values[ATTRIB_ID_AIM];
            LegacyScoreBaseMultiplier = values[ATTRIB_ID_LEGACY_SCORE_BASE_MULTIPLIER];
            MaximumLegacyComboScore = values[ATTRIB_ID_MAXIMUM_LEGACY_COMBO_SCORE];
        }
    }
}
