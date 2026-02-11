// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

namespace osu.Game.Rulesets.Catch.Difficulty
{
    public record CatchDifficultyConstants
    {
        public static CatchDifficultyConstants Default { get; } = new CatchDifficultyConstants();

        public double SrPreMultiplier { get; init; } =  0.7832023921381424;
        public double SrScalerY0 { get; init; } =  0.8759777562552482;
        public double SrScalerY1 { get; init; } =  1.8285863061868888;
        public double SrScalerY2 { get; init; } =  3.49813387527634;
        public double SrScalerY3 { get; init; } =  4.343541009238826;
        public double SrScalerY4 { get; init; } =  4.965539802568557;
        public double SrScalerY5 { get; init; } =  5.81471788081099;
        public double SrScalerY6 { get; init; } =  7.1345211790964775;
        public double SrScalerY7 { get; init; } =  8.495090487529616;
        public double SrScalerY8 { get; init; } =  8.978933881787196;
        public double SrScalerY9 { get; init; } =  9.50704162413777;
        public double SrScalerY10 { get; init; } =  10.507694514645852;
        public double SrOffset { get; init; } =  0.212;
        public double ApproachRateSecondConstant { get; init; } =  0.43494946766265297;
        public double BeginningTimePenaltyPower { get; init; } =  0.36221558435389;
        public double BeginningFullPenalty { get; init; } =  0.6720686852002504;
        public double DefaultDecayWeight { get; init; } = 0.9;
        public double[] DecayWeights { get; init; } = new[] { 0.9, 0.86, 0.81, 0.729, 0.6561 };
        public double LocalStarRatingMaxConstant { get; init; } =  1.127117711779149;
        public double LocalStarRatingMinConstant { get; init; } =  0.4136600028894615;
        public double LocalStarRatingCorrelationConstant { get; init; } =  0.5522225433811977;
        public double PerformanceLengthLinearPace { get; init; } =  0.4602894242225053;
        public double PerformanceLengthCutoff { get; init; } =  1700.0;
        public double PerformanceLengthLogarithmicPace { get; init; } =  0.38982535604893975;
        public double PerformanceValueMultiplier { get; init; } =  1.0975231394483769;
        public double PrecisionRawWeightHyperjumps { get; init; } =  0.9413298105269166;
        public double PrecisionRawWeightHyperjumpAfterJump { get; init; } =  0.9354934555692938;
        public double PrecisionRawWeightJumpAfterHyperjump { get; init; } =  1.0;
        public double PrecisionRawWeightJumps { get; init; } =  0.8445099757545701;
        public double PrecisionDelayedWeight { get; init; } =  0.8442428242318754;
        public double PrecisionStrainAmplitude { get; init; } =  47.78309013503319;
        public double PrecisionStrainShift { get; init; } =  -7.41793247662217;
        public double PrecisionStrainPace { get; init; } =  33.60715817857766;
        public double PrecisionStrainMultiplier { get; init; } =  40.588524308697245;
        public double MaxPrecisionCorrection { get; init; } =  1.3992501802202764;
        public double SpeedSnapAmplitude { get; init; } =  22.06645203307213;
        public double SpeedSnapShift { get; init; } =  1.7293855270362277;
        public double SpeedSnapPace { get; init; } =  52.535731744077765;
        public double SpeedSnapMultiplier { get; init; } =  1.1223860508953816;
        public double SpeedBurstAmplitude { get; init; } =  32.85523563130678;
        public double SpeedBurstShift { get; init; } =  -5.669117472527526;
        public double SpeedBurstPace { get; init; } =  53.86595760480941;
        public double SpeedBurstMultiplier { get; init; } =  0.7564829770353529;
        public double SpeedConsistencyAmplitude { get; init; } =  14.826288201276217;
        public double SpeedConsistencyShift { get; init; } =  -2.2541501044886365;
        public double SpeedConsistencyPace { get; init; } =  57.3962973118673;
        public double SpeedConsistencyMultiplier { get; init; } =  1.6499953313375357;
        public double ReadingHighCsPower { get; init; } =  1.5656070686483239;
        public double ReadingHighCsRate { get; init; } =  0.3629793290727813;
        public double ReadingHighCsPenaltyHypers { get; init; } =  0.704663753811184;
        public double ReadingLocalRhythmPenalty { get; init; } =  0.9682335335269503;
        public double ReadingExplicitRhythmPenalty { get; init; } =  0.9181242817044083;
        public double ReadingImplicitRhythmPenalty { get; init; } =  0.9775808085843127;
        public double ReadingSimilarDistancePenalty { get; init; } =  0.8641644672066617;
        public double ReadingAlternatingDistancePenalty { get; init; } =  0.9573571615225105;
        public double ReadingHyperchainPenalty { get; init; } =  0.9573164115617693;
        public double ReadingNonHyperchainPenalty { get; init; } =  0.9667480243651405;
        public double ReadingHighVelocityNerf { get; init; } =  0.02713385774057155;
        public double ReadingHighDistanceBuff { get; init; } =  0.34683004921951277;
        public double ReadingFakeActionBuff { get; init; } =  1.0946041467813792;
        public double ReadingFuturePrecisionBuff { get; init; } =  0.22283210885931914;
        public double StandingWidthAdditiveConstant { get; init; } =  1.209896626020114;
        public double PrecisionCorrectionDistanceExponent { get; init; } =  0.6831795506993774;
        public double PrecisionCorrectionTimeExponent { get; init; } =  0.634707037357876;
        public double PrecisionCorrectionDistanceWeight { get; init; } =  0.10063662622031824;
        public double FinalPPMultiplier { get; init; } =  1.059497223924349;
    }
}
