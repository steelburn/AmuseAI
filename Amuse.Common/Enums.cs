namespace Amuse.Common
{
    public enum ServerType
    {
        OnnxRuntime = 0,
        PyTorch = 10
    }

    public enum ProcessType
    {
        TextToImage = 0,
        ImageToImage = 1,
        ImageEdit = 2,
        ImageInpaint = 3,
        ImageControlNet = 4,
        ImageToImageControlNet = 5,

        TextToVideo = 300,
        ImageToVideo = 301,
        VideoToVideo = 302,

        TextToAudio = 400,
        AudioToText = 500
    }

    public enum EnvironmentMode
    {
        Create = 0,
        Load = 1,
        Update = 2,
        Rebuild = 3,
        Reinstall = 4
    }

    public enum DataType
    {
        Float32 = 0,
        Bfloat16 = 1,
        Float16 = 2,
        Float8 = 3,
        Int8 = 6,
        Int4 = 7
    }

    public enum QuantizationType
    {
        Q16Bit = 0,
        Q8Bit = 1,
        Q4Bit = 2
    }

    public enum MemoryModeType
    {
        Device = 0,
        OffloadCPU = 1,
        OffloadModel = 2,
        Balanced = 3
    }

    public enum SchedulerType
    {
        LMS = 0,
        Euler = 1,
        EulerAncestral = 2,
        DDPM = 3,
        DDIM = 4,
        KDPM2 = 5,
        KDPM2Ancestral = 6,
        DDPMWuerstchen = 10,
        LCM = 20,
        FlowMatchEuler = 30,
        FlowMatchHeun = 31,
        PNDM = 40,
        Heun = 41,
        UniPCMultistep = 42,
        DPMSolverMultistep = 43,
        DPMSolverSinglestep = 45,
        DPMSolverSDE = 46,
        DEISMultistep = 47,
        EDMEuler = 48,
        EDMDPMSolverMultistep = 49,
        FlowMatchLCM = 50,
        IPNDM = 51,
        CogVideoXDDIM = 52,
        CogVideoXDPM = 53,
        Helios = 54,
        HeliosDMD = 55,
        TCD = 56,
        SCM = 57,
        SASolver = 58,
        LTXEulerAncestral = 59,
    }

    public enum TimestepSpacingType
    {
        Leading = 0,
        Trailing = 1,
        Linspace = 2
    }

    public enum AlgorithmType
    {
        DPMSolver = 0,
        DPMSolverPlus = 1,
        SDE_DPMSolver = 2,
        SDE_DPMSolverPlus = 3,
        DEIS = 4,
        DataPrediction = 5,
        NoisePrediction = 6
    }

    public enum SolverType
    {
        Midpoint = 0,
        Heun = 1,
        BH1 = 2,
        BH2 = 3,
        LogRho = 4
    }

    public enum BetaScheduleType
    {
        Linear = 0,
        ScaledLinear = 1,
        Cosine = 2,
        SquaredCosine = 3,
        Sigmoid = 4,
        Laplace = 5,
        Exponential = 6
    }

    public enum PredictionType
    {
        Epsilon = 0,
        Variable = 1,
        Sample = 2,
        FlowPrediction = 3,
        Trigflow = 4
    }

    public enum VarianceType
    {
        FixedSmall = 0,
        FixedSmallLog = 1,
        FixedLarge = 2,
        FixedLargeLog = 3,
        Learned = 4,
        LearnedRange = 5
    }

    public enum TimeShiftType
    {
        Linear = 0,
        Exponential = 1
    }

    public enum AlphaTransformType
    {
        Cosine = 0,
        Exponential = 1,
        Laplace = 2
    }

    public enum FinalSigmasType
    {
        Zero = 0,
        SigmaMin = 1
    }

    public enum InterpolationType
    {
        Linear = 0,
        LogLinear = 1
    }

    public enum TimestepType
    {
        Discrete = 0,
        Continuous = 1
    }

    public enum SigmaScheduleType
    {
        Karras = 0,
        Exponential = 1
    }

    public enum UpscaleModeType
    {
        Nearest = 0,
        Linear = 1,
        Bilinear = 2,
        Bicubic = 3,
        Trilinear = 4,
        Area = 5,
        NearestExact = 6
    }
}
