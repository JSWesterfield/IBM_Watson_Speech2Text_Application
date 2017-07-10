using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace ATTeamE.web.Services
{
    public class TextToSpeechServices
    {
        public static Stream Authenticate()
        {
            TextToSpeechService _textToSpeech = new TextToSpeechService();
            string username = "be81879e-381c-47de-a44e-60f7650dc9e8";
            string password = "zkrJ6PqpsXdg";
            //string url = "https://stream.watsonplatform.net/text-to-speech/api";



            _textToSpeech.SetCredential(username, password);
            var voices = _textToSpeech.GetVoices();
            var strem = _textToSpeech.Synthesize("Hello Sabio Nation! Go win some money!", Voice.EN_ALLISON, AudioType.WAV);

            return strem;
        }
    }
}