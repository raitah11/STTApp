// This C# script is designed to handle speech-to-text functionality.

using Microsoft.CognitiveServices.Speech;
using System;
using System.Threading.Tasks;
using DotNetEnv;

class SpeechToText
{
    static async Task Main(string[] args)
    {
        // Load environment variables from .env file to not expose in the code the API key.
        Env.Load();
        // Retrieve the subscription key and region from environment variables.
        var SUBSCRIPTION_KEY = Environment.GetEnvironmentVariable("SUBSCRIPTION_KEY");
        var SUBSCRIPTION_REGION = Environment.GetEnvironmentVariable("SUBSCRIPTION_REGION");
        if (string.IsNullOrEmpty(SUBSCRIPTION_KEY) || string.IsNullOrEmpty(SUBSCRIPTION_REGION))
        {
            Console.WriteLine("Please set the SUBSCRIPTION_KEY and SUBSCRIPTION_REGION environment variables.");
            return;
        }

        // Create a speech configuration using the subscription key and region, for authentication to connect with the Azure service.
        var config = SpeechConfig.FromSubscription(SUBSCRIPTION_KEY, SUBSCRIPTION_REGION);
        using var recognizer = new SpeechRecognizer(config);

        Console.WriteLine("Speak into your microphone.");

        // Start recognition and wait for a result. 
        // Permission to use the microphone is required and asked for on cue (first use). 
        var result = await recognizer.RecognizeOnceAsync();

        if (result.Reason == ResultReason.RecognizedSpeech)
        {
            Console.WriteLine($"Recognized: {result.Text}");
        }
        else if (result.Reason == ResultReason.NoMatch)
        {
            Console.WriteLine("No speech could be recognized.");
        }
    }
}