using System.ComponentModel.DataAnnotations;

namespace Amuse.Common
{
    public enum ServerType
    {
        OnnxRuntime = 0,
        PyTorch = 10
    }

    public enum ProcessType
    {
        // Image
        [Display(Name = "TextToImage", ShortName = "T2I", Description = "Generates a brand new synthetic image completely from scratch based on a text prompt.")]
        TextToImage = 0,

        [Display(Name = "ImageToImage", ShortName = "I2I", Description = "Alters a source image changing its style, textures, or composition based on a guiding text prompt.")]
        ImageToImage = 1,

        [Display(Name = "ImageEdit", ShortName = "IE", Description = "Applies localized or structural modifications to an existing image using instruction-based editing commands.")]
        ImageEdit = 2,

        [Display(Name = "ImageInpaint", ShortName = "INP", Description = "Modifies or restores specific, masked areas within an image while preserving the surrounding context.")]
        ImageInpaint = 3,

        [Display(Name = "ImageControlNet", ShortName = "CN", Description = "Applies rigid spatial conditioning (like edge maps, poses, or depth) onto a text-to-image generation process.")]
        ImageControlNet = 4,

        [Display(Name = "ImageToImageControlNet", ShortName = "I2I+CN", Description = "Combines a source image with an explicit spatial guide map to tightly control composition and style concurrently.")]
        ImageToImageControlNet = 5,


        // Video
        [Display(Name = "TextToVideo", ShortName = "T2V", Description = "Synthesizes fluid, moving video frames from scratch using a conceptual text prompt.")]
        TextToVideo = 300,

        [Display(Name = "ImageToVideo", ShortName = "I2V", Description = "Animates a single, static source image into a moving video clip while maintaining character or object consistency.")]
        ImageToVideo = 301,

        [Display(Name = "VideoToVideo", ShortName = "V2V", Description = "Translates a source video into a different style or texture while tracking the underlying motion structures.")]
        VideoToVideo = 302,


        // Audio
        [Display(Name = "TextToAudio", ShortName = "T2A", Description = "Converts written text into spoken voice synthesis, realistic sound effects, or continuous music tracks.")]
        TextToAudio = 400,

        [Display(Name = "AudioToText", ShortName = "A2T", Description = "Transcribes incoming spoken speech or environmental audio signals into formatted, written text.")]
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
