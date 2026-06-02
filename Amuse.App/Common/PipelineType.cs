namespace Amuse.App.Common
{
    public enum PipelineType
    {
        // Image
        StableDiffusionPipeline = 0,
        StableDiffusionXLPipeline = 1,
        StableDiffusion3Pipeline = 2,
        LatentConsistencyPipeline = 3,
        FluxPipeline = 20,
        Flux2Pipeline = 201,
        Flux2KleinPipeline = 22,
        ChromaPipeline = 30,
        ZImagePipeline = 40,
        QwenImagePipeline = 50,
        Kandinsky5Pipeline = 60,

        // Video
        WanPipeline = 70,
        LTXPipeline = 80,
        LTX20Pipeline = 81,
        CogVideoXPipeline = 90,
        SkyReelsV2Pipeline = 100,
        HeliosPipeline = 110,

        // Audio
        AceStepPipeline = 200,
        LongCatAudioPipeline = 210,

        // Other
        UpscalePipeline = 500,
        ExtractPipeline = 501,
        WhisperPipeline = 502,
        SupertonicPipeline = 503,
    }
}
