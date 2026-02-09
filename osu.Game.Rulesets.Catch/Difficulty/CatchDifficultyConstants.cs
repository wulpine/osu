// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

namespace osu.Game.Rulesets.Catch.Difficulty
{
    public record CatchDifficultyConstants
    {
        public static CatchDifficultyConstants Default { get; } = new CatchDifficultyConstants();

        public double SrScalerY0 { get; init; } = 1.0;
        public double SrScalerY1 { get; init; } = 2.0;
        public double SrScalerY2 { get; init; } = 3.0;
        public double SrScalerY3 { get; init; } = 4.0;
        public double SrScalerY4 { get; init; } = 5.0;
        public double SrScalerY5 { get; init; } = 6.0;
        public double SrScalerY6 { get; init; } = 7.0;
        public double SrScalerY7 { get; init; } = 8.0;
        public double SrScalerY8 { get; init; } = 9.0;
        public double SrScalerY9 { get; init; } = 10.0;
        public double SrScalerY10 { get; init; } = 11.0;
        public double ApproachRateSecondConstant { get; init; } = 0.43250312085515485;
        public double BeginningTimePenaltyPower { get; init; } = 0.42483720068639547;
        public double BeginningFullPenalty { get; init; } = 0.6147783618526128;
        public double DefaultDecayWeight { get; init; } = 0.9;
        public double[] DecayWeights { get; init; } = new[] { 0.9, 0.86, 0.81, 0.729, 0.6561 };
        public double LocalStarRatingMaxConstant { get; init; } = 1.9314907095826686;
        public double LocalStarRatingMinConstant { get; init; } = 0.9758189456714981;
        public double LocalStarRatingCorrelationConstant { get; init; } = 0.4397797709011138;
        public double PerformanceLengthLinearPace { get; init; } = 0.30442696917090545;
        public double PerformanceLengthCutoff { get; init; } = 1700.1788586428409;
        public double PerformanceLengthLogarithmicPace { get; init; } = 0.5376645372408155;
        public double PerformanceValueMultiplier { get; init; } = 1.0944609676968209;
        public double PrecisionRawWeightHyperjumps { get; init; } = 0.9441731633277796;
        public double PrecisionRawWeightHyperjumpAfterJump { get; init; } = 0.9516816791224951;
        public double PrecisionRawWeightJumpAfterHyperjump { get; init; } = 0.9984613564016297;
        public double PrecisionRawWeightJumps { get; init; } = 0.913682080259714;
        public double PrecisionDelayedWeight { get; init; } = 0.6410954003324065;
        public double PrecisionStrainAmplitude { get; init; } = 74.59263538660932;
        public double PrecisionStrainShift { get; init; } = 2.7182894245805485;
        public double PrecisionStrainPace { get; init; } = 26.679985010385373;
        public double PrecisionStrainMultiplier { get; init; } = 41.411459850915236;
        public double MaxPrecisionCorrection { get; init; } = 1.6950508806286932;
        public double SpeedSnapAmplitude { get; init; } = 15.638791496156156;
        public double SpeedSnapShift { get; init; } = -43.23567365216375;
        public double SpeedSnapPace { get; init; } = 52.05882397767609;
        public double SpeedSnapMultiplier { get; init; } = 0.9922824003753967;
        public double SpeedBurstAmplitude { get; init; } = 9.040324988798575;
        public double SpeedBurstShift { get; init; } = -28.91121405285478;
        public double SpeedBurstPace { get; init; } = 68.65374996316815;
        public double SpeedBurstMultiplier { get; init; } = 1.5473795583084686;
        public double SpeedConsistencyAmplitude { get; init; } = 25.286020624263372;
        public double SpeedConsistencyShift { get; init; } = 19.50740310238877;
        public double SpeedConsistencyPace { get; init; } = 47.58333687026297;
        public double SpeedConsistencyMultiplier { get; init; } = 1.0386359349408476;
        public double ReadingHighCsPower { get; init; } = 1.5225426734566225;
        public double ReadingHighCsRate { get; init; } = 0.47973363422473014;
        public double ReadingHighCsPenaltyHypers { get; init; } = 0.6349494522506675;
        public double ReadingLocalRhythmPenalty { get; init; } = 0.8707416071932552;
        public double ReadingExplicitRhythmPenalty { get; init; } = 0.9310414856444548;
        public double ReadingImplicitRhythmPenalty { get; init; } = 0.8481770816270627;
        public double ReadingSimilarDistancePenalty { get; init; } = 0.8304374055690238;
        public double ReadingAlternatingDistancePenalty { get; init; } = 0.8518173579708035;
        public double ReadingHyperchainPenalty { get; init; } = 0.9507072783446766;
        public double ReadingNonHyperchainPenalty { get; init; } = 0.956115307398902;
        public double ReadingHighVelocityNerf { get; init; } = 0.0011260557104272872;
        public double ReadingHighDistanceBuff { get; init; } = 0.18149033898534875;
        public double ReadingFakeActionBuff { get; init; } = 1.0459020650700555;
        public double ReadingFuturePrecisionBuff { get; init; } = 0.1289137212417447;
        public double StandingWidthAdditiveConstant { get; init; } = 1.2839078826556656;
        public double PrecisionCorrectionDistanceExponent { get; init; } = 0.7760320589694504;
        public double PrecisionCorrectionTimeExponent { get; init; } = 0.7230909398414097;
        public double PrecisionCorrectionDistanceWeight { get; init; } = 0.10657478984392157;
        public double FinalPPMultiplier { get; init; } = 0.28220449436285;
    }
}
